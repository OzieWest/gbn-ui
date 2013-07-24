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
		String uploadTime;
		String uploadCount;

		public HomeController()
		{
			_serializer = new JsonSerializer();

			var list = _serializer.Load<List<String>>(@"query/statistic.json");
			uploadTime = list[0];
			uploadCount = list[1];
		}

		[HttpGet]
		public ActionResult Index()
		{
			ViewBag.TopGames = this.GetTopGames();
			ViewBag.UploadTime = uploadTime;
			ViewBag.UploadCount = uploadCount;

			var list = _serializer.Load<List<TwitterEntry>>(@"query/tweets.json");

			return View(list);
		}

		[HttpGet]
		public ActionResult Search(String name = "скидки")
		{
			ViewBag.TopGames = this.GetTopGames();
			ViewBag.UploadTime = uploadTime;
			ViewBag.UploadCount = uploadCount;
			ViewBag.Search = name;

			name = new Replacer().DelWithRegex(name);

			if (!String.IsNullOrEmpty(name) && name.Length > 2 && name.Length < 60 && name != "скидки")
			{
				var list = _serializer.Load<List<GameEntry>>(@"query/games.json")
									  .Where(ent => ent.SearchString.Contains(name))
									  .OrderBy(ent => ent.SearchString)
									  .ToList();

				ViewBag.Count = list.Count();

				return View(list);
			}
			else
				return RedirectToAction("Sales");
		}

		protected int GetPagination(int count, int step)
		{
			int result = (count % step > 0) ? (count / step) + 1 : (count / step);

			return result;
		}

		protected int GetPaginationCount(int countNow, int countAll, int step)
		{
			int result = 0;

			if (countNow + step < countAll)
			{
				result = countNow + step;
			}
			else if (countNow + step > countAll)
			{
				result = countAll;
			}

			return result;
		}

		[HttpGet]
		public ActionResult Sales()
		{
			ViewBag.TopGames = this.GetTopGames();
			ViewBag.UploadTime = uploadTime;
			ViewBag.UploadCount = uploadCount;

			var list = _serializer.Load<List<GameEntry>>(@"query/sales.json")
								  .OrderBy(ent => ent.SearchString)
								  .ToList();
									

			ViewBag.Count = list.Count();

			return View(list);
		}

		[HttpGet]
		public ActionResult Critic()
		{
			ViewBag.TopGames = this.GetTopGames();
			ViewBag.UploadTime = uploadTime;
			ViewBag.UploadCount = uploadCount;

			var list = _serializer.Load<List<MetaEntry>>(@"query/metas.json")
								  .OrderBy(ent => ent.Name)
								  .ToList();

			ViewBag.Count = list.Count;

			return View(list);
		}

		[HttpGet]
		public ActionResult Coops()
		{
			ViewBag.TopGames = this.GetTopGames();
			ViewBag.UploadTime = uploadTime;
			ViewBag.UploadCount = uploadCount;

			var list = _serializer.Load<List<CoopEntry>>(@"query/coops.json")
								  .OrderBy(ent => ent.Name)
								  .ToList();

			ViewBag.Count = list.Count;

			return View(list);
		}

		private List<MetaEntry> GetTopGames()
		{
			return _serializer.Load<List<MetaEntry>>(@"query/metas.json")
								  .OrderByDescending(ent => ent.UserScore)
								  .Take(10)
								  .ToList();

		}
	}
}
