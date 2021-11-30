using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IB_Banco_XY.Controllers
{
    [Authorize]
    public class DashBoardController : Controller
    {
        [Route("/dashboard")]
        public IActionResult DashBoard()
        {
            return View();
        }
    }
}
