using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace SampleMobileAzure1
{
    public class Saving
    {
        private string _id;
        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _description;
        [JsonProperty(PropertyName = "Description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private int _cashTotal;
        [JsonProperty(PropertyName = "CashTotal")]
        public int CashTotal
        {
            get { return _cashTotal; }
            set { _cashTotal = value; }
        }

        [Version]
        public string Version { get; set; }
    }
}

