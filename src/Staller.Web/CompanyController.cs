using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Staller.Core;
using Staller.Core.Managers;
using Staller.Core.Entities;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Staller.Web
{
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {

        //static readonly List<CompanyEntity> _items = new List<CompanyEntity>()
        //{
        //    new Company { Id = 1, Name = "First Item" }
        //};


        // GET: api/values
        [HttpGet]
        public IEnumerable<CompanyEntity> Get()
        {
            CompanyManager cm = new CompanyManager();
            
            return cm.GetAll().Result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            CompanyManager cm = new CompanyManager();
            var item = cm.GetAll(rowKey: id).Result.SingleOrDefault(); // _items.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return new ObjectResult(item);
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody]CompanyEntity item)
        {
            
            if (!ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = 400;
            }
            else
            {
                item.Id = Guid.NewGuid();
                item.Label = Configuration.Label.Id;
                
                CompanyManager cm = new CompanyManager();
                var res = cm.Save(item);

                res.Wait();
                
                string url = Url.RouteUrl("Get", new { id = item.Id },
                    Request.Scheme, Request.Host.ToUriComponent());

                HttpContext.Response.StatusCode = 201;
                HttpContext.Response.Headers["Location"] = url;
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]CompanyEntity item)
        {
            if (!ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = 400;
            }
            else
            {
                CompanyManager cm = new CompanyManager();
                var res = cm.Save(item);

                res.Wait();

                string url = Url.RouteUrl("Get", new { id = item.Id },
                    Request.Scheme, Request.Host.ToUriComponent());

                HttpContext.Response.StatusCode = 201;
                HttpContext.Response.Headers["Location"] = url;
            }
        }

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var item = _items.FirstOrDefault(x => x.Id == id);
        //    if (item == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    _items.Remove(item);
        //    return new HttpStatusCodeResult(204); // 201 No Content
        //}
    }
}
