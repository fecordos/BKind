using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BKind.Data;
using BKind.Models;
using BKind.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BKind.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class AdminController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly BKindContext _context;

        public AdminController(UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager, BKindContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        //User management
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserManagement()
        {
            var users = _userManager.Users; //preluarea tuturor utilizatorilor aplicatiei
            return View(users);
        }

        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserViewModel addUserViewModel)
        {
            if (!ModelState.IsValid) return View(addUserViewModel);

            var appUser = new AppUser()
            {
                UserName = addUserViewModel.Username,
                FirstName = addUserViewModel.FirstName,
                LastName = addUserViewModel.LastName,
                DateOfBirth = addUserViewModel.DateOfBirth,
                City = addUserViewModel.City,
                Country = addUserViewModel.Country,
                Email = addUserViewModel.Email,
            };

            appUser.EmailConfirmed = true; //cand admin-ul adauga un user, adresa de email a user-ului va fi implicit confirmata

            IdentityResult result = await _userManager.CreateAsync(appUser, addUserViewModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("UserManagement", _userManager.Users);
            }

            foreach(IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(addUserViewModel);

        }

        //GET
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id); //identificare user dupa id

            if(user == null)
            {
                return RedirectToAction("UserManagement", _userManager.Users);
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, string FirstName, string LastName, DateTime DateOfBirth,
            string City, string Country, string Email, string PhoneNumber)
        {
            var user = await _userManager.FindByIdAsync(id);

            if(user != null)
            {
                user.FirstName = FirstName;
                user.LastName = LastName;
                user.DateOfBirth = DateOfBirth;
                user.City = City;
                user.Country = Country;
                user.Email = Email;
                user.PhoneNumber = PhoneNumber;

                var result = await _userManager.UpdateAsync(user); //metoda care realizeaza actualizarea datelor

                if (result.Succeeded)
                {
                    return RedirectToAction("UserManagement", _userManager.Users);
                }

                ModelState.AddModelError("","User not updated, something went wrong.");

                return View(user);
            }

            return RedirectToAction("UserManagement", _userManager.Users);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId); //identificare user dupa id

            if(appUser != null)
            {
                //sterg prima data, din baza de date, mesajele utilizatorului
                var allMessages = await _context.Messages.ToListAsync();
                for (int index = 0; index < allMessages.Count; index++)
                {
                    if (allMessages[index].UserId == userId)
                    {
                        allMessages[index].UserId.Remove(2);
                    }
                }


                IdentityResult result = await _userManager.DeleteAsync(appUser); //stergerea user-ului din bd
                if (result.Succeeded)
                {
                    return RedirectToAction("UserManagement");
                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong while deleting this user.");
                }
            }
            else
            {
                ModelState.AddModelError("","This user can't be found");
            }
            return View("UserManagement", _userManager.Users);

        }


        //Role management
        public IActionResult RoleManagement()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleViewModel addRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addRoleViewModel);  //daca validarea esueaza => returnare acelasi view
            }

            var role = new IdentityRole
            {
                Name = addRoleViewModel.RoleName 
            };

            IdentityResult result = await _roleManager.CreateAsync(role); //crearea rolului cu datele primite din front

            if (result.Succeeded)
            {
                return RedirectToAction("RoleManagement", _roleManager.Roles);
            }

            foreach(IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(addRoleViewModel);
        }


        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return RedirectToAction("RoleManagement", _roleManager.Roles);
            }

            var editRoleViewManager = new EditRoleViewModel  //preluare date noi
            {
                ID = role.Id,
                RoleName = role.Name,
                Users = new List<string>()
            };

            foreach(var user in _userManager.Users)
            {
                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    editRoleViewManager.Users.Add(user.UserName);
                }
            }

            return View(editRoleViewManager);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel editRoleViewModel)
        {
            var role = await _roleManager.FindByIdAsync(editRoleViewModel.ID);

            if( role != null)
            {
                role.Name = editRoleViewModel.RoleName;

                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("RoleManagement", _roleManager.Roles);
                }

                ModelState.AddModelError("", "Role not updated, something went wrong.");

                return View(editRoleViewModel);
            }

            return RedirectToAction("RoleManagement", _roleManager.Roles);
        }

        [HttpPost]

        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if(role != null)
            {
                var result = await _roleManager.DeleteAsync(role); //stergerea rolului
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleManagement", _roleManager.Roles);
                }

                ModelState.AddModelError("", "Something went wrong while deleting this role.");
            }
            else
            {
                ModelState.AddModelError("", "This role can't be found.");
            }

            return View("RoleManagement", _roleManager.Roles);
        }


        //Users in roles
        public async Task<IActionResult> AddUserToRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId); //cautare rol dupa id
            if(role == null)
            {
                return RedirectToAction("RoleManagement", _roleManager.Roles);
            }

            var addUserToRoleViewModel = new UserRoleViewModel
            {
                RoleId = role.Id
            };

            foreach(var user in _userManager.Users)
            {
                //daca utilizatorul nu are atasat rolul, ii este atasat 
                if(!await _userManager.IsInRoleAsync(user, role.Name))
                {
                    addUserToRoleViewModel.Users.Add(user);
                }
            }

            return View(addUserToRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(UserRoleViewModel userRoleViewModel)
        {
           
            var user = await _userManager.FindByIdAsync(userRoleViewModel.UserId);
            var role = await _roleManager.FindByIdAsync(userRoleViewModel.RoleId);

            if (user == null)
            {
                return View(userRoleViewModel);
            }

            var result = await _userManager.AddToRoleAsync(user, role.Name); //atasarea rolului utilizatorului

            if (result.Succeeded)
            {
                return RedirectToAction("RoleManagement", _roleManager.Roles);
            }
             
            foreach(IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(userRoleViewModel);
        }

        public async Task<IActionResult> DeleteUserFromRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId); //cautare rol dupa id
            if (role == null)
            {
                return RedirectToAction("RoleManagement", _roleManager.Roles);
            }

            var deleteUserFromRoleViewModel = new UserRoleViewModel
            {
                RoleId = role.Id
            };

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    deleteUserFromRoleViewModel.Users.Add(user); //identificarea tuturor utilizatorilor cu acest rol
                }
            }

            return View(deleteUserFromRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserFromRole(UserRoleViewModel userRoleViewModel)
        {
            var user = await _userManager.FindByIdAsync(userRoleViewModel.UserId); //cautare user dupa id
            var role = await _roleManager.FindByIdAsync(userRoleViewModel.RoleId); //cautare rol dupa id

            if (user == null)
            {
                return View(userRoleViewModel);
            }

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name); //stergere rol

            if (result.Succeeded)
            {
                return RedirectToAction("RoleManagement", _roleManager.Roles);
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(userRoleViewModel);
        }


    }
}