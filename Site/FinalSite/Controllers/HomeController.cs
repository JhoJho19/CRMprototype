using Microsoft.AspNetCore.Mvc;


public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewBag.HeaderContent = GetRandomHeaderContent();
        return View();
    }

    public IActionResult Projects()
    {
        return View();
    }

    public IActionResult Services()
    {
        return View();
    }

    public IActionResult Blog()
    {
        return View();
    }

    public IActionResult Contacts()
    {
        return View();
    }

    private string GetRandomHeaderContent()
    {
        var options = new[] {
            "Консалтинговые решения для всех!",
            "Доверьтесь экспертам!",
            "Ваш успех - наша цель!"
        };
        return options[new Random().Next(options.Length)];
    }
}
