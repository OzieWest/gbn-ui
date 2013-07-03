using GetByNameLibrary.Controllers;
using GetByNameLibrary.Domains;
using GetByNameLibrary.Interfaces;
using GetByNameLibrary.Utilities;
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
		ISerializer _serializer;

		public HomeController()
		{
			_serializer = new JsonSerializer();
		}

		[HttpGet]
		public ActionResult Index()
		{
			var list = _serializer.Load<List<TwitterEntry>>(@"query/tweets.json");

			return View(list);
		}

		[HttpGet]
		public ActionResult Search(String name)
		{
			var original = name;
			name = new Replacer().DelWithRegex(name);

			if (!String.IsNullOrEmpty(name) && name != "скидки" && name.Length < 60)
			{
				var list = _serializer.Load<List<GameEntry>>(@"query/games.json")
									  .Where(ent => ent.SearchString.Contains(name))
									  .OrderBy(ent => ent.SearchString)
									  .ToList();

				ViewBag.Count = list.Count;
				ViewBag.Search = original;

				return View(list);
			}
			else
				return RedirectToAction("Sales");
		}

		[HttpGet]
		public ActionResult Sales()
		{
			var list = _serializer.Load<List<GameEntry>>(@"query/sales.json")
								  .OrderBy(ent => ent.SearchString)
								  .ToList();
									
			ViewBag.Count = list.Count;

			return View(list);
		}

		[HttpGet]
		public ActionResult Critic()
		{	
			var list = _serializer.Load<List<MetaEntry>>(@"query/metacritic.json")
								  .OrderBy(ent => ent.Name)
								  .ToList();

			ViewBag.Count = list.Count;

			return View(list);
		}

		[HttpGet]
		public ActionResult Coops()
		{
			var list = _serializer.Load<List<CoopEntry>>(@"query/coops.json")
								  .OrderBy(ent => ent.Name)
								  .ToList();

			ViewBag.Count = list.Count;

			return View(list);
		}
	}
}
