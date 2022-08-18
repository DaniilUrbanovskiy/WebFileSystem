using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index(int? folderId)
        {
            var folders = FolderModel.Mapper(await _folderService.GetFolders(folderId));
            ViewData["FolderId"] = folderId;
            return View(folders);
        }
        [HttpPost]
        public async Task<IActionResult> AddFolder(FolderModel folderModel)
        {
            await _folderService.AddFolder(folderModel.Name, folderModel.ParentId);
            return RedirectToAction("Index", "Home", new { @folderId = folderModel.ParentId });
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFolder(FolderModel folderModel)
        {
            await _folderService.RemoveByName(folderModel.Name);
            return RedirectToAction("Index", "Home", new { @folderId = folderModel.ParentId});
        }

    }
}
