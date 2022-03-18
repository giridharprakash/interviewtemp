using interviewtemp.Models;
using interviewtemp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace interviewtemp.Controllers;

public class UsersController : Controller
{
    private readonly ILogger<UsersController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public UsersController(ILogger<UsersController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    }
    /// <summary>
    /// This page results user sign up form.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Index()
    {
        _logger.LogInformation("User signup form");
        var userRequestViewModel = new UserRequestViewModel();
        return View(userRequestViewModel);
    }
    
    /// <summary>
    /// this page lets you create user.
    /// </summary>
    /// <param name="userRequestViewModel"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Index(UserRequestViewModel userRequestViewModel)
    {
        if (userRequestViewModel is null)
        {
            throw new ArgumentNullException(nameof(userRequestViewModel));   
        }
        
        _logger.LogInformation("User created signup request");
        return RedirectToAction("UserRegistered", userRequestViewModel);
    }
    
    /// <summary>
    /// This page shows the user registered information
    /// </summary>
    /// <param name="userRequestViewModel"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpGet]
    public IActionResult UserRegistered(UserRequestViewModel userRequestViewModel)
    {
        if (userRequestViewModel is null)
        {
            throw new ArgumentNullException(nameof(userRequestViewModel));   
        }

        if (string.IsNullOrWhiteSpace(userRequestViewModel.FirstName))
        {
            throw new ArgumentException("First name cannot be null or empty");
        }
        
        _logger.LogInformation("User registered {FirstName}", userRequestViewModel.FirstName);
        
        return View(userRequestViewModel);
    }
    
    /// <summary>
    /// this page return list of users from third party service.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> UsersList()
    {
        _logger.LogInformation("User list called");

        var client = _httpClientFactory.CreateClient();
        var codeResponse = await client.GetAsync("https://jsonplaceholder.typicode.com/users/1");
        codeResponse.EnsureSuccessStatusCode();
        var user = await codeResponse.Content.ReadFromJsonAsync<User>();
        return View(user);
    }
    
}