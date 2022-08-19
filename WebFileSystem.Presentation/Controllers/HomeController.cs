﻿using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using WebFileSystem.Presentation.Models;
using WebFileSystem.Services;

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

    }
}
