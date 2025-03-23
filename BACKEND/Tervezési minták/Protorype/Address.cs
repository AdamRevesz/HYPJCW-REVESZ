using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tervezési_minták.Protorype
{
    public class Address
    {
        public string Street { get; set; }
        public int HouseNumber { get; set; }

        public Address(string street, int houseNumber)
        {
            Street = street;
            HouseNumber = houseNumber;
        }

        public override string ToString()
        {
            return $"{Street} {HouseNumber}";
        }

        //public Address GetCopy()
        //{
        //    return (Address)MemberwiseClone();
        //}

        //Deserialize eseteben, itt nincs ennek ertelme, mert nem tudunk ennek a classnak a forraskodjahoz hozzaferni
        public Address GetCopy()
        {
            string data = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Address>(data);
        }
    }

}
