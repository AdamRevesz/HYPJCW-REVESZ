using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tervezési_minták.Facade
{
    public static class SimpleJsonConsumer<T>
    {
        static bool isBase64 = false;

        public static void SetBase64()
        {
            isBase64 = true;
        }

        public static T[] DownloadAndConvertJson(string url)
        {
            WebClient wc = new WebClient();
            string result = wc.DownloadString(url);

            if (isBase64)
            {
                result = Convert.FromBase64String(result).ToString();
            }

            T[] items = JsonConvert.DeserializeObject<T[]>(result);
            return items;
        }
    }
}
