using System.Linq;
using interviewtemp.Controllers;
using interviewtemp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewTemp.Web.Tests.Controllers;

[TestClass]
public class UsersControllerTests
{
    [TestInitialize]
    public void Initialize()
    {
        // Call mocking functions with Moq library
    }

    [TestMethod]
    public void GetUsersIndex_ShouldReturn_View_Successfully()
    {
        var usersController = new UsersController();
        var result = usersController.Index();

        Assert.AreEqual(typeof(ViewResult), result.GetType());
        Assert.IsInstanceOfType(result, typeof(ViewResult));

        var usersViewResult = (ViewResult)result;
        Assert.IsNotNull(usersViewResult.Model);
        Assert.IsInstanceOfType(usersViewResult.Model, typeof(UserRequestViewModel));
    }

    [TestMethod]
    public void CreateUser_ShouldCreateUser_And_Return_RegisteredView_Successfully()
    {
        const string firstName = "Giridhar";
        
        var usersController = new UsersController();
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
        var usersController = new UsersController();
        var result = usersController.UserRegistered(new UserRequestViewModel(){FirstName = firstName});

        Assert.AreEqual(typeof(ViewResult), result.GetType());
        Assert.IsInstanceOfType(result, typeof(ViewResult));

        var usersViewResult = (ViewResult)result;
        Assert.IsNotNull(usersViewResult.Model);
        Assert.IsInstanceOfType(usersViewResult.Model, typeof(UserRequestViewModel));
        Assert.AreEqual(firstName, ((UserRequestViewModel)usersViewResult.Model).FirstName);
    }
}