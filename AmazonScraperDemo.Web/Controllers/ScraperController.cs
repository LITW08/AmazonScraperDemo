using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmazonScraperDemo.Scraping;

namespace AmazonScraperDemo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScraperController : ControllerBase
    {
        [Route("scrape")]
        public List<AmazonResult> Scrape(string query)
        {
            return Scraper.ScrapeAmazon(query);
        }
    }
}
