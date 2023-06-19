using Microsoft.AspNetCore.Mvc;

namespace approje.ViewComponents
{
    [ViewComponent(Name = "UserView")]
    public class UserViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var userName = HttpContext.User.Identity.Name;
            return View("Default", userName);
        }
    }
}
