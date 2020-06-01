using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BKind.Data;
using BKind.Models;

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

        // Display all msgs in DB upon website load
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUsername = currentUser.UserName;
            }

            var messages = await _context.Messages.ToListAsync();
            return View(messages);
        }

        // Add msg to DB upon form submission
        public async Task<IActionResult> Create(Message message)
        {
            if (ModelState.IsValid)
            {
                var sender = await _userManager.GetUserAsync(User);
                message.UserId = sender.Id;
                message.Username = User.Identity.Name;

                await _context.Messages.AddAsync(message);
                await _context.SaveChangesAsync();

                //return Ok(); // Generates an error in browser because there is no View/Create.cshtml
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