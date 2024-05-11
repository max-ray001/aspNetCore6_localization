using GlobalizationDemo.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using System.Diagnostics;

namespace GlobalizationDemo.Controllers;

public class HomeController : Controller
{
    private readonly IHtmlLocalizer<HomeController> _localizer;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IHtmlLocalizer<HomeController> localizer)
    {
        _logger = logger;
        _localizer = localizer;
    }

    public IActionResult Index()
    {
        var result = _localizer["HelloWorld"];
        ViewData["HelloWorld"] = result;
        return View();
    }

    [HttpPost]
    public IActionResult CultureManagement(string cultureName, string returnUrl)
    {
        Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(cultureName)),
            new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });

        return LocalRedirect(returnUrl);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}