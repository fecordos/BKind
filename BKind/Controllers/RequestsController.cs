using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BKind.Data;
using BKind.Models;
using BKind.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using System.Dynamic;
namespace BKind.Controllers
{
    public class RequestsController : Controller
    {
        private readonly BKindContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RequestsController(BKindContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Requests
        public ActionResult Index(string searchString, [Bind("ID")] Category category)
        {

            //dynamic model
            dynamic reqModel = new ExpandoObject();
            reqModel.Requests = _context.Request;
            reqModel.Categories = _context.Category;
            reqModel.Users = _context.Users;

            //asignez unei variabile id-ul categoriei selectate, pt filtrare
            var categoryID = category.ID;


            //searching 
            var requests = from model in _context.Request.Include(r => r.Category)
                           .Include(r => r.Person).OrderByDescending(r => r.ID)  //afisarea celor mai recente cereri adaugate
                           select model;

            //cautare potrivire intre cuvantul furnizat de utilizator si titlul/descriere cerere
            if (!String.IsNullOrEmpty(searchString))
            {
                requests = requests.Where(r => r.Title.Contains(searchString)
                                         || r.Description.Contains(searchString));
            }
            else
            { 
                //filtrare dupa categorie
                if (categoryID != 0)
                {
                    requests = requests.Where(r => r.CategoryID == categoryID);
                }

            }

            if (requests == null)
                {
                    return View();
                }

                reqModel.Requests = requests;
                return View(reqModel);
        }


        // GET: Requests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var request = await _context.Request
                .Include(r => r.Category)
                .Include(r => r.Person)
                .FirstOrDefaultAsync(m => m.ID == id);

            var person = await _context.Person
              .Include(p => p.Address)
              .Include(p => p.Requests)
              .FirstOrDefaultAsync(m => m.ID == request.PersonID);

            var address = await _context.Address
                    .Include(a => a.Persons)
                    .FirstOrDefaultAsync(m => m.ID == person.AddressID);

            var category = await _context.Category
                .Include(c => c.Requests)
                .FirstOrDefaultAsync(m => m.ID == request.CategoryID);

            RequestDetailsViewModel requestDetailsViewModel = new RequestDetailsViewModel()
            {
                Person = person,
                Address = address,
                Category = category,
                Request = request

            };


            if (requestDetailsViewModel == null)
            {
                return NotFound();
            }

            return View(requestDetailsViewModel);
        }

        // GET: Requests/Create
        [Authorize(Roles = "Administrators")]

        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "Name");
            ViewData["PersonID"] = new SelectList(_context.Person, "ID", "FirstName");
            return View();
        }

        // POST: Requests/Create
        // pentru protejarea impotriva atacurilor de tip overposting folosim Bind
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]

        public async Task<IActionResult> Create([Bind("ID,Title,Description,CategoryID,PersonID, Image")] RequestCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

                string uniqueFileName = ProcessUploadedFile(model);

                Request newRequest = new Request()
                {
                    Title = model.Title,
                    Description = model.Description,
                    CategoryID = model.CategoryID,
                    PersonID = model.PersonID,
                    ImagePath = uniqueFileName
                };
                _context.Add(newRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "Name", model.CategoryID);
            ViewData["PersonID"] = new SelectList(_context.Person, "ID", "FirstName", model.PersonID);
            return View(model);
        }

        // GET: Requests/Edit/5
        [Authorize(Roles = "Administrators")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Request.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            RequestEditViewModel model = new RequestEditViewModel()
            {
                ID = request.ID,
                Title = request.Title,
                Description = request.Description,
                CategoryID = request.CategoryID,
                PersonID = request.PersonID,
                ExistingImagePath = request.ImagePath
            };


            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "Name", model.CategoryID);
            ViewData["PersonID"] = new SelectList(_context.Person, "ID", "FirstName", model.PersonID);
            return View(model);
        }


        // POST: Requests/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]

        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,CategoryID,PersonID,Image")] RequestEditViewModel model)
        {
            if (id != model.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var request = await _context.Request
                                         .Include(r => r.Category)
                                         .Include(r => r.Person)
                                         .FirstOrDefaultAsync(m => m.ID == id);
                    request.Title = model.Title;
                    request.Description = model.Description;
                    request.CategoryID = model.CategoryID;
                    request.PersonID = model.PersonID;
                    if (model.Image != null)
                    {
                        if (model.ExistingImagePath != null)
                        {
                            string filePath = Path.Combine(_webHostEnvironment.WebRootPath,
                                          "images", model.ExistingImagePath);
                            System.IO.File.Delete(filePath);
                        }
                        request.ImagePath = ProcessUploadedFile(model);

                    }

                    _context.Update(request);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestExists(model.ID))
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
            ViewData["CategoryID"] = new SelectList(_context.Category, "ID", "Name", model.CategoryID);
            ViewData["PersonID"] = new SelectList(_context.Person, "ID", "FirstName", model.PersonID);
            return View(model);
        }


        private string ProcessUploadedFile(RequestCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName; // Guid = global unique id
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                model.Image.CopyTo(fileStream);
            }

            return uniqueFileName;
        }

        // GET: Requests/Delete/5
        [Authorize(Roles = "Administrators")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _context.Request
                .Include(r => r.Category)
                .Include(r => r.Person)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var request = await _context.Request.FindAsync(id);
            _context.Request.Remove(request);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestExists(int id)
        {
            return _context.Request.Any(e => e.ID == id);
        }

    }
}
