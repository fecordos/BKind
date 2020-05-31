using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BKind.Data;
using BKind.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public async Task<IActionResult> AddReqToHistory([Bind("ID","CategoryID","Description",
            "ImagePath","PersonID","Title")]Request model)        
        {
                var currentUser = await _userManager.GetUserAsync(User);
                var userId = await _userManager.GetUserIdAsync(currentUser);

                var newReqHistory = new ReqHistory()
                {
                    UserId = userId,
                    RequestId = model.ID,
                    CategoryID = model.CategoryID,
                    Description = model.Description,
                    ImagePath = model.ImagePath,
                    PersonID = model.PersonID,
                    Title = model.Title,

                };

                _context.Add(newReqHistory);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Requests");
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveRequestFromUser(int requestID, string userID)
        {
            var reqHistory = await _context.ReqHistory.FindAsync(userID, requestID); //FindAsync primeste cheia compusa formata din cele 2 id-uri


            if (reqHistory != null)
            {
                _context.ReqHistory.Remove(reqHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "ReqHistory");

            }
            return RedirectToAction("Index", "Requests");

        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveReqFromHistory(int ID)
        {
            var reqHistory = await _context.ReqHistory.FindAsync(_userManager.GetUserId(User), ID); //FindAsync primeste cheia compusa formata din cele 2 id-uri


            if (reqHistory != null) {
                _context.ReqHistory.Remove(reqHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","ReqHistory");

            }
            return RedirectToAction("Index", "Requests");

        }

    }
}