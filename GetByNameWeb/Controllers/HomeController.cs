using GetByNameLibrary.Domains;
using SerializeLibra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GetByNameWeb.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var list = new JsonSerializer().Load<List<TwitterEntry>>(@"tweets.json");

			ViewBag.Entries = list;

			return View();
		}
	}
}
