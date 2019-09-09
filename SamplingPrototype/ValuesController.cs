using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SamplingPrototype.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SamplingPrototype
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var client = new MongoClient("mongodb://localhost");
            var db = client.GetDatabase("test");
            var collectionNames = db.ListCollectionNames();
            //CollectionNames = string.Join(", ", collectionNames.ToList());

            var testCollection = db.GetCollection<MongoData>("testdata");
            var list = testCollection.Find(FilterDefinition<MongoData>.Empty).ToList();

            return list.Select(q => string.Join(", ", q._id, q.someInteger, q.someString));
        }

        [HttpGet]
        [Route("Testy")]
        public List<SiteData> GetAllCurrentSiteData()
        {
            var client = new MongoClient("mongodb://localhost");
            var db = client.GetDatabase("test");
            var testCollection = db.GetCollection<SiteData>("SiteData_Current");
            var list = testCollection.Find(FilterDefinition<SiteData>.Empty).ToList();

            return list;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public List<SiteData> GetSiteDataIncludingHistory(int id)
        {
            var client = new MongoClient("mongodb://localhost");
            var db = client.GetDatabase("test");

            var currentCollection = db.GetCollection<SiteData>("SiteData_Current");

            var builder = Builders<SiteData>.Filter;
            var filter = builder.Eq(u => u.SiteDataId, id);

            var list = currentCollection.Find(filter).ToList();

            var returnValue = new List<SiteData>();

            if(list.Count > 0)
            {
                returnValue.Add(list[0]);

                var archiveCollection = db.GetCollection<SiteData>("SiteData_Archive");
                returnValue.AddRange(archiveCollection.Find(filter).ToEnumerable());
            }

            return returnValue;
        }

        // POST api/<controller>
        [HttpPost]
        public void CreateItem([FromBody]MongoData data)
        {
            var client = new MongoClient("mongodb://localhost");
            var db = client.GetDatabase("test");
            var testCollection = db.GetCollection<MongoData>("testdata");
            testCollection.InsertOne(data);
        }


        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
