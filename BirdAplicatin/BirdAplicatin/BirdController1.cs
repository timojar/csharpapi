
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace BirdAplicatin
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class BirdController : ApiController
    {


        private static string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName;
        private static string fileName = Path.Combine(path, "birddata.json");

        // GET api/<controller>


        public string Get()
        {
            List<Bird> birds = new List<Bird>();
            birds = readBirdFile();
            System.Diagnostics.Debug.WriteLine(fileName);
            if (birds.Count == 0)
            {
                birds = dummyBirds();
            }


            string json =
                JsonConvert.SerializeObject(
                    birds,
                    Formatting.Indented,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }
                    );

            return json;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]JObject value)
        {
            System.Diagnostics.Debug.WriteLine(value);
            Bird bird = value.ToObject<Bird>();
            List<Bird> birds = readBirdFile();
            bird.Id = birds.Count + 1;
            birds.Add(bird);
            writeBirdFile(birds);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]JObject value)
        {
            Bird bird = value.ToObject<Bird>();
            List<Bird> birds = readBirdFile();
            bird.sightings.Add(DateTime.Now);
            int index = birds.FindIndex(b => b.Id == bird.Id);
            birds.RemoveAt(index);
            birds.Insert(index, bird);
            writeBirdFile(birds);         
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        private void writeBirdFile(List<Bird> birds)
        {
            string json = JsonConvert.SerializeObject(birds);
            System.IO.File.WriteAllText(fileName, json);
        }

        private List<Bird> dummyBirds()
        {
            List<Bird> birds = new List<Bird>();

            birds.Add(new BirdAplicatin.Bird() { Name = "varis", Id = 1, sightings = new List<DateTime>() });
            birds.Add(new BirdAplicatin.Bird() { Name = "harakka", Id = 2, sightings = new List<DateTime>() });

            writeBirdFile(birds);
            return birds;
        }

        private List<Bird> readBirdFile()
        {
            try
            {
                using (StreamReader r = new StreamReader(fileName))
                {
                    string json = r.ReadToEnd();
                    List<Bird> birds = JsonConvert.DeserializeObject<List<Bird>>(json);
                    return birds;
                }
            }
            catch (Exception e)
            {
                return new List<Bird>();
            }
        }

    }
}