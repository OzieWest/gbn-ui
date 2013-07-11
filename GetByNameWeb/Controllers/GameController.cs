using GetByNameLibrary.Domains;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace GetByNameWeb.Controllers
{

	public class GameController : ApiController
	{
		SerializeLibra.JsonSerializer _serializer;

		public GameController()
		{
			JsonSerializerSettings jSettings = new Newtonsoft.Json.JsonSerializerSettings();
			GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jSettings;

			_serializer = new SerializeLibra.JsonSerializer();
		}

		[Queryable]
		public IQueryable<GameEntry> Get()
		{
			var list = _serializer.Load<List<GameEntry>>(@"query/games.json");

			var result = new EnumerableQuery<GameEntry>(list);

			return result;
		}
	}
}
