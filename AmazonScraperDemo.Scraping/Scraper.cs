using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;

namespace AmazonScraperDemo.Scraping
{
    public static class Scraper
    {
        public static List<AmazonResult> ScrapeAmazon(string searchQuery)
        {
            var results = new List<AmazonResult>();
            var html = GetAmazonHtml(searchQuery);
            var parser = new HtmlParser();
            var document = parser.ParseDocument(html);
            IHtmlCollection<IElement> searchResultElements = document.QuerySelectorAll(".sg-col-inner");
            foreach (IElement result in searchResultElements)
            {
                var titleSpan = result.QuerySelector("span.a-size-medium.a-color-base.a-text-normal");
                if (titleSpan == null)
                {
                    continue;
                }

                var amazonResult = new AmazonResult();
                amazonResult.Title = titleSpan.TextContent;

                var priceSpan = result.QuerySelector("span.a-offscreen");
                if (priceSpan != null)
                {
                    amazonResult.Price = double.Parse(priceSpan.TextContent.Replace("$", String.Empty));
                }

                var imageElement = result.QuerySelector("img.s-image");
                if (imageElement != null)
                {
                    var imageSrcValue = imageElement.Attributes["src"].Value;
                    amazonResult.ImageUrl = imageSrcValue;
                }

                var anchorTag = result.QuerySelector("a.a-link-normal.a-text-normal");
                if (anchorTag != null)
                {
                    amazonResult.LinkUrl = anchorTag.Attributes["href"].Value;
                }

                results.Add(amazonResult);


            }

            return results;
        }

        private static string GetAmazonHtml(string searchQuery)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            string url = $"https://www.amazon.com/s?k={searchQuery}";
            var client = new HttpClient(handler);
            var html = client.GetStringAsync(url).Result;
            return html;
        }
    }
}
