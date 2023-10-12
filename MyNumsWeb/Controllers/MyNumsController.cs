using MyNumsWeb.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyNumsWeb.Controllers
{
    public class MyNumsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post([FromBody] MyNums myNums)
        {
            //return Request.CreateResponse(HttpStatusCode.OK, $"Server got the nums: {myNums.Nums}");

            var nums = myNums.Nums.Split(',')
                .Where(str => !string.IsNullOrEmpty(str))
                .Select(str => int.Parse(str)).ToArray();

            IMyNumsRepo repo = new MyNumsRepo();
            int count = repo.AddNewNumbers(nums);

            return Request.CreateResponse(HttpStatusCode.OK, $"{count} new numbers added");
        }

        // PUT api/<controller>/5
        // client call e.g.: https://localhost:12345/api/MyNums/S?id=123&note=myText
        [HttpPut]
        public HttpResponseMessage Put(int id, string note)
        {
            IMyNumsRepo repo = new MyNumsRepo();
            repo.UpdateNum(id, note);

            return Request.CreateResponse(HttpStatusCode.OK, $"record {id} was updated on Server");
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }

    public class MyNums
    {
        public string Nums { get; set; }
    }
}