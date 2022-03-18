using interviewtemp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace interviewtemp.Controllers;

public class UsersController : Controller
{
    private readonly ILogger<UsersController> _logger;

    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
    }
    [HttpGet]
    public IActionResult Index()
    {
        _logger.LogInformation("User signup form");
        var userRequestViewModel = new UserRequestViewModel();
        return View(userRequestViewModel);
    }
    
    [HttpPost]
    public IActionResult Index(UserRequestViewModel userRequestViewModel)
    {
        _logger.LogInformation("User created signup request");
        return RedirectToAction("UserRegistered", userRequestViewModel);
    }
    
    [HttpGet]
    public IActionResult UserRegistered(UserRequestViewModel userRequestViewModel)
    {
        _logger.LogInformation("User registered {FirstName}", userRequestViewModel.FirstName);
        
        return View(userRequestViewModel);
    }
    
}