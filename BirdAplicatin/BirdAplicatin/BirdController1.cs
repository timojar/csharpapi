using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BirdAplicatin
{
    public class BirdController : ApiController
    {
        // GET api/<controller>
        [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
        public Bird[] Get()
        {

            Bird[] birds = new Bird[] {
                new BirdAplicatin.Bird() {Name="varis", Id=1, sightins=new string[] { } },
                 new BirdAplicatin.Bird() {Name="harakka", Id=2, sightins=new string[] { } }

            };


            return birds;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}