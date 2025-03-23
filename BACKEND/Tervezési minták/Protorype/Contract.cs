using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tervezési_minták.Protorype
{
    public class Contract
    {
        public string Subject { get; set; }
        public string Company { get; set; }
        public int Price { get; set; }
        public Address Address { get; set; }


        public Contract GetCopy2()
        {
            return (Contract)MemberwiseClone();
        }

        public Contract GetCopy()
        {
            string data = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Contract>(data);
        }

    }



}
