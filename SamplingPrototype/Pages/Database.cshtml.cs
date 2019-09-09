using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SamplingPrototype.Data;

namespace SamplingPrototype.Pages
{
    public class DatabaseModel : PageModel
    {
        public string CollectionNames { get; set; }
        public List<MongoData> Collection { get; set; }

        public void OnGet()
        {
            var client = new MongoClient("mongodb://localhost");
            var db = client.GetDatabase("test");
            var collectionNames = db.ListCollectionNames();
            CollectionNames = string.Join(", ", collectionNames.ToList());

            var testCollection = db.GetCollection<MongoData>("testdata");
            Collection = testCollection.Find(FilterDefinition<MongoData>.Empty).ToList();
        }
    }

    
}