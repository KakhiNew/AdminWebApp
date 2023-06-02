using AdminWebApp.Entities;
using AdminWebApp.Repositories;
using AdminWebApp.ViewModels;
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
            var vm = new UserActionVm
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
        public IActionResult BlockUsers([FromForm] UserActionVm action)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult UnblockUsers(UserActionVm action)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult DeleteUsers(UserActionVm action)
        {
            throw new NotImplementedException();
        }
    }
}
