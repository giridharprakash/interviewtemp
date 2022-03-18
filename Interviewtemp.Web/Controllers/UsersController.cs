using interviewtemp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace interviewtemp.Controllers;

public class UsersController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var userRequestViewModel = new UserRequestViewModel();
        return View(userRequestViewModel);
    }
    
    [HttpPost]
    public IActionResult Index(UserRequestViewModel userRequestViewModel)
    {
        return RedirectToAction("UserRegistered", userRequestViewModel);
    }
    
    [HttpGet]
    public IActionResult UserRegistered(UserRequestViewModel userRequestViewModel)
    {
        return View(userRequestViewModel);
    }
    
}