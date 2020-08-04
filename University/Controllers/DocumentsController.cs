﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Services;
using System.Collections.Generic;

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

        // GET: Documents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Documents/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("DocumentID,DocumentTitle,DocumentDescription,DateTime")] Document document)
        {
            if (ModelState.IsValid)
            {
                _service.Create(document);
                return RedirectToAction(nameof(Index));
            }
            return View(document);
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