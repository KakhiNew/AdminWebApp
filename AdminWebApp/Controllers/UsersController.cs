using AdminWebApp.Entities;
using AdminWebApp.Repositories;
using AdminWebApp.ViewModels;
using AdminWebApp.ViewModels.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminWebApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _userRepository.GetAllUsers()
                .Select(u => new UserVm
                {
                    Id = u.Id,
                    Email = u.Email,
                    Name = u.Name,
                    Blocked = u.Blocked,
                    LastLoginTime = u.LastLoginTime,
                    RegistrationTime = u.RegistrationTime
                })
                .ToList();
            var vm = new UserListVm
            {
                Users = users
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            _userRepository.UpdateUser(user);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _userRepository.DeleteUser(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult BlockUsers(IEnumerable<UserActionRequestVm> users)
        {
            foreach (var user in users.Where(u => u.Selected))
            {
                _userRepository.BlockUser(user.Id);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UnblockUsers(IEnumerable<UserActionRequestVm> users)
        {
            foreach (var user in users.Where(u => u.Selected))
            {
                _userRepository.UnblockUser(user.Id);
            }
           
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteUsers(IEnumerable<UserActionRequestVm> users)
        {
            foreach (var user in users.Where(u => u.Selected))
            {

                _userRepository.DeleteUser(user.Id);
            }

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
