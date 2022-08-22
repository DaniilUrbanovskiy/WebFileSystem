using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebFileSystem.Presentation.Models;
using WebFileSystem.Services;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

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
        [HttpPost]
        public async Task<IActionResult> AddFolder(FolderModel folderModel)
        {
            var responseMessage = await _folderService.AddFolder(folderModel.Name, folderModel.ParentId);

            return RedirectToAction("Index", "Home", new { @folderId = folderModel.ParentId, @responseMessage = responseMessage });
        }
        [HttpPost]
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

    }
}
