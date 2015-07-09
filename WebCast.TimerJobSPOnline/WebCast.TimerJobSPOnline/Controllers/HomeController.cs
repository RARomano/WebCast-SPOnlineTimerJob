using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebCast.TimerJobSPOnline.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{

			ViewBag.Title = "Home Page";

			return View();
		}

		internal static ClientContext CreateClientContext(string url)
		{
			Uri uri = new Uri(url);

			string realm = TokenHelper.GetRealmFromTargetUrl(uri);

			//Get the access token for the URL.  
			//   Requires this app to be registered with the tenant
			string accessToken = TokenHelper.GetAppOnlyAccessToken(
				TokenHelper.SharePointPrincipal,
				uri.Authority, realm).AccessToken;

			var clientContext = TokenHelper.GetClientContextWithAccessToken(uri.ToString(), accessToken);

			return clientContext;
		}

		[HttpGet]
		public string AddItem()
		{
			try
			{
				string url = "";
				using (var ctx = CreateClientContext(url))
				{
					var list = ctx.Web.Lists.GetByTitle("WebCast");

					var item = list.AddItem(new ListItemCreationInformation());
					item["Title"] = "WebCast_" + DateTime.Now.ToString("dd_MM_yyyy");
					item.Update();

					ctx.ExecuteQuery();
				}
				return "OK";
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}
	}
}
