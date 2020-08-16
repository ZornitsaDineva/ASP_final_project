using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Web;

namespace ContosoUniversity.Controllers
{
    public class DocumentsController : Controller
    {
        private readonly DocumentService _service;

        public DocumentsController(DocumentService service)
        {
            _service = service;
        }

        // GET: Documents
        public IActionResult Index()
        {
            List<Document> documents = _service.Get();
            return View(documents);
        }

        // GET: Documents/Details/5
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = _service.Get(id);
            if (document == null)
            {
                return NotFound();
            }
           
            return View(document);
        }

        // GET: Documents/Download/5
        public IActionResult Download(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = _service.Get(id);
            if (document == null)
            {
                return NotFound();
            }

            return File(document.Content, document.ContentType);

        }

        // GET: Documents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Documents/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<IActionResult> Create([Bind("DocumentID,DocumentTitle,DocumentDescription,DateTime,FormFile")] Document document)
        {

            if (ModelState.IsValid)
            {
                IFormFile formFile = document.FormFile;
  
                using (var ms = new MemoryStream())
                {
                    formFile.CopyTo(ms);
                    document.Content = ms.ToArray();
                    document.ContentType = formFile.ContentType;
                    document.FileName = formFile.FileName;

                }

                SHA1Managed sha1 = new SHA1Managed();
                var hash = sha1.ComputeHash(document.Content);
                string checksum = Convert.ToBase64String(hash);
                document.Checksum = checksum;

                DocumentCheckResponse response = await getDocumentCheckResponseAsync(checksum);
                if (response.checksumExists)
                {
                    ModelState.AddModelError("FormFile", "Document already exists");
                } else
                {
                     _service.Create(document);
                    return RedirectToAction(nameof(Index));
                }          
            }

            return View(document);
        }

        private async System.Threading.Tasks.Task<DocumentCheckResponse> getDocumentCheckResponseAsync(string checksum)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080/DocumentStore/rest/DocumentService/check_documents/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage response = await client.GetAsync("?checksum=" + HttpUtility.UrlEncode(checksum));
                    return await response.Content.ReadAsAsync<DocumentCheckResponse>();
                }
                catch (HttpRequestException e)
                {
                    if (e.Source != null)
                    {
                        Console.WriteLine("HttpRequestException source: {0}", e.Source);
                    }
                }
            }

            return null;
        }

        // GET: Documents/Edit/5
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = _service.Get(id);
            if (document == null)
            {
                return NotFound();
            }
            return View(document);
        }

        // POST: Documents/Edit/5
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("DocumentID,DocumentTitle,DocumentDescription,DateTime")] Document document)
        {
            if (id != document.DocumentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(id, document);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentExists(document.DocumentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(document);
        }

        // GET: Documents/Delete/5
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = _service.Get(id);

            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var document = _service.Get(id);

            if (document == null)
            {
                return NotFound();
            }

            _service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentExists(string id)
        {
            return _service.Get(id) != null;
        }
    }
}
