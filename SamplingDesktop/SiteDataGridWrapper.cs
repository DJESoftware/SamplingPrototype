using SamplingPrototype.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamplingDesktop
{
    public class SiteDataGridWrapper
    {
        public SiteData SiteData { get; private set; }
        public string BriefDescription { get; private set; }

        public int SiteDataId => SiteData.SiteDataId;
        public string UploadedBy => SiteData.UploadedBy;

        public SiteDataGridWrapper(SiteData siteData)
        {
            if (siteData == null)
                throw new ArgumentNullException("Cannot pass in a null SiteData object");

            SiteData = siteData;
            updateBriefDescription();
        }

        private void updateBriefDescription()
        {
            BriefDescription = $"NOx Emissions: ({string.Join(", ", SiteData.NOxEmissions)})";
        }
    }
}
