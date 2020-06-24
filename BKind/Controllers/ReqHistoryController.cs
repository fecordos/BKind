
using System.Threading.Tasks;
using BKind.Data;
using BKind.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> AddReqToHistory([Bind("ID","CategoryID","Description",
            "ImagePath","PersonID","Title", "DateAdded")]Request model)        
        {
            
                var currentUser = await _userManager.GetUserAsync(User);
                var userId = await _userManager.GetUserIdAsync(currentUser);

            //verific daca nu exista deja in baza de date inregistrarea 
            //Eager loading:
            var firstReqHistoryRecord = await _context.ReqHistory
               .Include(r => r.Category)
               .Include(r => r.Person)
               .FirstOrDefaultAsync(m => (m.RequestId == model.ID && m.UserId == userId));
           
            if(firstReqHistoryRecord == null)
            {
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

                //daca am mai salvat aceasta inregistrare => redirectionare spre pagina Index
                if (newReqHistory == firstReqHistoryRecord)
                {
                    return RedirectToAction("Index");
                }

                //adaugarea se face doar daca nu exista inregistrarea in baza de date
                _context.Add(newReqHistory);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
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