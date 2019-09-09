using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using SamplingPrototype.Data;
using Newtonsoft.Json;

namespace SamplingDesktop
{
    public partial class Form1 : Form
    {
        static readonly HttpClient client = new HttpClient();

        public Form1()
        {
            InitializeComponent();

            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();
            grid.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = nameof(SiteDataGridWrapper.SiteDataId), Name = "Id" });
            grid.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = nameof(SiteDataGridWrapper.UploadedBy), Name = "Uploaded By" });
            grid.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = nameof(SiteDataGridWrapper.BriefDescription), Name = "Description" });
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var response = await client.GetAsync("https://localhost:44374/api/Values/Testy");

            var content = await response.Content.ReadAsStringAsync();

            var siteDataList = JsonConvert.DeserializeObject<List<SiteData>>(content);

            grid.DataSource = siteDataList.Select(sd => new SiteDataGridWrapper(sd)).ToList();

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            // Load all documents 

            var obj = new MongoData()
            {
                someInteger = (int)theInteger.Value,
                someString = theString.Text
            };

            var content = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("https://localhost:44374/api/Values", byteContent);

            MessageBox.Show("Posted");
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            // get the selected item from the main grid
            var cells = grid.SelectedCells;

            if (cells.Count < 1)
                return;

            var siteData = grid.Rows[cells[0].RowIndex].DataBoundItem as SiteDataGridWrapper;

            if (siteData == null)
                return;



            // get its id
            // send this off to the API to get the history

            var response = await client.GetAsync($"https://localhost:44374/api/Values/{siteData.SiteDataId}");

            var content = await response.Content.ReadAsStringAsync();

            var siteDataList = JsonConvert.DeserializeObject<List<SiteData>>(content);

            historyGrid.DataSource = siteDataList.Select(sd => new SiteDataGridWrapper(sd)).ToList();
        }
    }
}
