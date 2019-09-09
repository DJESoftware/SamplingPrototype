using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplingPrototype.Data
{
    public class MongoData
    {
        [JsonIgnore]
        public ObjectId _id { get; set; }
        public int someInteger { get; set; }
        public string someString { get; set; }
    }
}
