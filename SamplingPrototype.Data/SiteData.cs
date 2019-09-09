]using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplingPrototype.Data
{
    public class SiteData
    {
        [JsonIgnore]
        public ObjectId _id { get; set; }
        public int SiteDataId { get; set; }
        public int Version { get; set; }
        public string UploadedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public double[] NOxEmissions { get; set; }
        public SiteInformation SiteInformation { get; set; }
    }

    public class SiteInformation
    {
        public string SiteName { get; set; }
        public string AdditionalNotes { get; set; }
    }
}
