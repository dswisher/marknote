using System.IO;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Markdig;
using System;

namespace marknote.Controllers
{
    public class NoteController : Controller
    {
        public IActionResult Render(string path)
        {
            string content = Markdown.ToHtml(String.Format("This is a text with some *emphasis*. Path `{0}`.", path));

            ViewData["NoteContent"] = content;

            // string content = String.Format("Hello world!  Path='{0}'.", path);
            // byte[] bytes = Encoding.UTF8.GetBytes(content);

            // var stream = new MemoryStream(bytes);
            // var mimeType = "text/html";
            // // var mimeType = "text/plain";

            // var fileStreamResult = new FileStreamResult(stream, mimeType);

            // // fileStreamResult.FileDownloadName = "FileStreamExample.csv";

            // return fileStreamResult;

            return View();
        }
    }
}
