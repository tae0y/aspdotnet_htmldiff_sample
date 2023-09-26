using HtmlDiffMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HtmlDiff;

namespace HtmlDiffMVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		[HttpPost]
		public ActionResult demodiffDemo()
		{
			string oldText = @"<p>This is some sample text to demonstrate the capability of the <strong>HTML diff tool</strong>.</p>
                    <p>It is based on the Ruby implementation found <a href='http://github.com/myobie/htmldiff'>here</a>. Note how the link has no tooltip</p>
                    <table cellpadding='0' cellspacing='0'>
                    <tr><td>Some sample text</td><td>Some sample value</td></tr>
                    <tr><td>Data 1 (this row will be removed)</td><td>Data 2</td></tr>
                    </table>";
			string newText = @"<p>This is some sample text to demonstrate the awesome capabilities of the <strong>HTML diff tool</strong>.</p><br/><br/>Extra spacing here that was not here before.
                    <p>It is based on the Ruby implementation found <a title='Cool tooltip' href='http://github.com/myobie/htmldiff'>here</a>. Note how the link has a tooltip now and the HTML diff algorithm has preserved formatting.</p>
                    <table cellpadding='0' cellspacing='0'>
                    <tr><td>Some sample <strong>bold text</strong></td><td>Some sample value</td></tr>
                    </table>";

			HtmlDiff.HtmlDiff diffHelper = new HtmlDiff.HtmlDiff(oldText, newText);
			string diffOutput = diffHelper.Build();

			ViewBag.oldText = oldText;
			ViewBag.newText = newText;
			ViewBag.diffOutput = diffOutput;
			return View("DiffView");
		}


		[HttpPost]
		public ActionResult demodiff(string oldtxt, string newtxt)
		{
			string oldText = oldtxt;
			string newText = newtxt;

			HtmlDiff.HtmlDiff diffHelper = new HtmlDiff.HtmlDiff(oldText, newText);
			string diffOutput = diffHelper.Build();

			ViewBag.oldText = oldText;
			ViewBag.newText = newText;
			ViewBag.diffOutput = diffOutput;
			return View("DiffView");
		}
	}
}
