using Microsoft.AspNetCore.Mvc;

namespace VehicleManagementSystem.MVC.Areas.Error.Controllers;

[Route(template: "/Error/{statusCode}")]
public class ErrorController : Controller
{
    // GET
    public IActionResult Index(int statusCode)
    {
        Response.Clear();
        Response.StatusCode = statusCode;
        
        switch (statusCode)
        {
            case 401:
                return View("UnauthorizedError");
            case 404:
                return View("PageNotFoundError");
            case 500:
                return View("InternalServerError");
            default:
                return View("GenericError");
        }
    }
}