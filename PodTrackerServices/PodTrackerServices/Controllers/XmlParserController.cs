using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PodTrackerServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XmlParserController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get(string url)
        {
            
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage res = await client.GetAsync(url))
            using (HttpContent content = res.Content)
            {
                string data = await content.ReadAsStringAsync();
                if (data != null)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(data);
                    string jsonText = JsonConvert.SerializeXmlNode(doc);
                    return new JsonResult(jsonText);
                }
                else {

                    return NotFound();
                }
            }

        }
    }
}