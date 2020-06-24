using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BKind.Data;
using BKind.Models;
using System.Dynamic;

namespace BKind.Controllers
{
    public class ChatController : Controller
    {
        public readonly BKindContext _context;
        public readonly UserManager<AppUser> _userManager;
        public ChatController(BKindContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Afisarea tuturor msgs din DB cand se incarca site-ul
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User); //identificarea user-ului curent

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUsername = currentUser.UserName;
            }

            var messages = await _context.Messages.ToListAsync(); //preluarea tuturor mesgs din db
            return View(messages);
        }

        // Add msg to DB 
        public async Task<IActionResult> Create(Message message)
        {
            if (ModelState.IsValid)
            {
                var sender = await _userManager.GetUserAsync(User);
                message.UserId = sender.Id;
                message.Username = User.Identity.Name;

                await _context.Messages.AddAsync(message); //adaugare mesaj la bd
                await _context.SaveChangesAsync(); //salvarea schimbarilor

                return RedirectToAction("Index");
            }

            return Error();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}