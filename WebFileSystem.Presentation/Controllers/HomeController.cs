using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebFileSystem.Presentation.Models;
using WebFileSystem.Services;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using WebFileSystem.DataAccess.Domain.Entities;
using System;

namespace WebFileSystem.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly FolderService _folderService;

        public HomeController(FolderService folderService)
        {
            _folderService = folderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? folderId = null, string responseMessage = null)
        {
            var folders = FolderModel.Mapper(await _folderService.GetFolders(folderId));

            ViewData["FolderId"] = folderId;
            ViewBag.ResponseMessage = responseMessage;

            return View(folders);
        }

        public IActionResult About() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFolder(FolderModel folderModel)
        {
            var responseMessage = await _folderService.AddFolder(folderModel.Name, folderModel.ParentId);

            return RedirectToAction("Index", "Home", new { @folderId = folderModel.ParentId, @responseMessage = responseMessage });
        }

        public async Task<IActionResult> RemoveFolder(FolderModel folderModel)
        {
            var responseMessage = await _folderService.RemoveByName(folderModel.Name, folderModel.ParentId);

            return RedirectToAction("Index", "Home", new { @folderId = folderModel.ParentId, @responseMessage = responseMessage });
        }

        [HttpPost]
        public async Task<IActionResult> ImportFolderFromCatalog(string[] folders, int? parentId = null)
        {
            if (folders == null)
            {
                HttpContext.Session.SetString("FolderId", parentId.ToString());
                HttpContext.Session.SetString("ResponseMessage", Result.BigFile);
                return RedirectToAction("Index", "Home", new { @folderId = parentId, @responseMessage = Result.BigFile });
            }

            if (folders.Length <= 0)
            {
                var foldeId = HttpContext.Session.GetString("FolderId");
                var message = HttpContext.Session.GetString("ResponseMessage");
                return RedirectToAction("Index", "Home", new { @folderId = foldeId, @responseMessage = message });
            }
            var responseMessage = await _folderService.CreateStructure(folders, parentId);

            var result = responseMessage.Contains(Result.Exists) ? Result.Exists : Result.Created;
            HttpContext.Session.SetString("ResponseMessage", result);
            HttpContext.Session.SetString("FolderId", parentId.ToString());

            return RedirectToAction("Index", "Home", new { @folderId = parentId, @responseMessage = responseMessage });
        }

        [HttpPost]
        public async Task<IActionResult> ImportFolderFromFile(IFormFile file, int? parentId = null)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(reader.ReadLine());
            }
            var folders = result.ToString().Split("\r\n");
            return await ImportFolderFromCatalog(folders, parentId);
        }

        public async Task<IActionResult> ExportStructure(FolderModel folderModel)
        {
            var result = await _folderService.GetStructureByName(folderModel.Name, folderModel.ParentId);

            var structuredFolders = new List<string>();
            var lineBuilder = new StringBuilder();

            for (int i = 0; i < result.Count; i++)
            {
                lineBuilder = await AppendPaths(result[result.Count - i - 1], lineBuilder, result);
                structuredFolders.Add(lineBuilder.ToString());
                lineBuilder.Clear();
            }

            int structureCount = structuredFolders.Count;
            for (int i = 0; i < structuredFolders.Count; i++)
            {
                if (result.Where(x => x.ParentId == result.FirstOrDefault(x => x.Name == structuredFolders[i].Split("/").LastOrDefault()).Id).Any())
                {
                    structuredFolders.Remove(structuredFolders[i]);
                    i--;
                }
            }

            var foldersToString = string.Join("\n", structuredFolders);
            var foldersToBytes = Encoding.UTF8.GetBytes(foldersToString);
            return File(foldersToBytes, "application/octet-stream");
        }

        public async Task<IActionResult> DownloadTemplate() 
        {
            var foldersToString = "/template/template3/template3_1/template3_1_1\n" +
                                  "/template/template1/template1_1/template1_1_1\n" +
                                  "/template/template2";
            var foldersToBytes = Encoding.UTF8.GetBytes(foldersToString);
            return File(foldersToBytes, "application/octet-stream");
        }

        private async Task<StringBuilder> AppendPaths(Folder folder, StringBuilder lineBuilder, List<Folder> result)
        {
            try
            {
                lineBuilder.Insert(0, "/" + folder?.Name);
                if (folder?.ParentId == null)
                {
                    return lineBuilder;
                }

                lineBuilder = await AppendPaths(result.Where(x => x.Id == folder?.ParentId).FirstOrDefault(), lineBuilder, result);
                return lineBuilder;
            }
            catch (Exception)
            {
                return lineBuilder;
            }
        }
    }
}
