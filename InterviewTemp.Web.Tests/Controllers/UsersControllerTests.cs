using System.Linq;
using System.Net.Http;
using interviewtemp.Controllers;
using interviewtemp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InterviewTemp.Web.Tests.Controllers;

[TestClass]
public class UsersControllerTests
{
    private Mock<ILogger<UsersController>> _logger = new Mock<ILogger<UsersController>>();
    private Mock<IHttpClientFactory> _httpClientFactory = new Mock<IHttpClientFactory>();
    
    [TestInitialize]
    public void Initialize()
    {
        
    }

    [TestMethod]
    public void GetUsersIndex_ShouldReturn_View_Successfully()
    {
        var usersController = CreateController();
        var result = usersController.Index();

        Assert.AreEqual(typeof(ViewResult), result.GetType());
        Assert.IsInstanceOfType(result, typeof(ViewResult));

        var usersViewResult = (ViewResult)result;
        Assert.IsNotNull(usersViewResult.Model);
        Assert.IsInstanceOfType(usersViewResult.Model, typeof(UserRequestViewModel));
    }

    private UsersController CreateController()
    {
        return new UsersController(_logger.Object, _httpClientFactory.Object);
    }

    [TestMethod]
    public void CreateUser_ShouldCreateUser_And_Return_RegisteredView_Successfully()
    {
        const string firstName = "Giridhar";
        
        var usersController = CreateController();
        var result = usersController.Index(new UserRequestViewModel() { FirstName = firstName });

        Assert.AreEqual(typeof(RedirectToActionResult), result.GetType());
        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

        var usersViewResult = (RedirectToActionResult)result;
        Assert.IsNotNull(usersViewResult);
        Assert.IsNotNull(usersViewResult.RouteValues);
        Assert.IsTrue(usersViewResult.RouteValues.Any());
        Assert.AreEqual("UserRegistered", usersViewResult.ActionName);
        Assert.AreEqual(firstName, usersViewResult.RouteValues["FirstName"]);
    }
    
    [TestMethod]
    public void GetUsersRegistered_ShouldReturn_View_Successfully()
    {
        const string firstName = "Giridhar";
        var usersController = CreateController();
        var result = usersController.UserRegistered(new UserRequestViewModel(){FirstName = firstName});

        Assert.AreEqual(typeof(ViewResult), result.GetType());
        Assert.IsInstanceOfType(result, typeof(ViewResult));

        var usersViewResult = (ViewResult)result;
        Assert.IsNotNull(usersViewResult.Model);
        Assert.IsInstanceOfType(usersViewResult.Model, typeof(UserRequestViewModel));
        Assert.AreEqual(firstName, ((UserRequestViewModel)usersViewResult.Model).FirstName);
    }
}