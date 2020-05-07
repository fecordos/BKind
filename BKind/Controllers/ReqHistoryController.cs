using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BKind.Data;
using BKind.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BKind.Controllers
{
    public class ReqHistoryController : Controller
    {

        private readonly BKindContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ReqHistoryController(BKindContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.ReqHistory.ToListAsync());
        }


        //GET
        public IActionResult AddReqToHistory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReqToHistory(int RequestId, int CategoryId, int PersonId, string Title, string Description, string ImagePath )
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var userId = await _userManager.GetUserIdAsync(currentUser);

            var newReqHistory = new ReqHistory()
            {
                UserId = userId,
                RequestId = RequestId,
                CategoryID = CategoryId,
                PersonID = PersonId,
                Title = Title,
                Description = Description,
                ImagePath = ImagePath
            };
            _context.Add(newReqHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Requests");

        }


        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminateReqFromHistory(int requestId)
        {
            var reqHistory = await _context.ReqHistory.FindAsync(_userManager.GetUserId(User), requestId); //FindAsync primeste cheia compusa formata din cele 2 id-uri
            _context.ReqHistory.Remove(reqHistory);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Requests");
        }

    }
}