using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Net;
using Tervezési_minták.Adapter;
using Tervezési_minták.Bridge;
using Tervezési_minták.Facade;
using Tervezési_minták.Protorype;
using Tervezési_minták.Proxy;

namespace Tervezési_minták
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Prototype
            Contract c1 = new Contract();
            c1.Subject = "Catering service";
            c1.Company = "Beck and partners";
            c1.Address = new Address("Kobanya utca", 12);
            c1.Price = 200000;

            Contract c2 = c1.GetCopy();

            Console.WriteLine($"{c1.Company} {c1.Subject} {c1.Address.ToString()}" +
                $"\n {c2.Company} {c2.Subject} {c2.Address.ToString()}");

            //Adapter
            INameSource source = new NameRepository();
            NameVisualizer visualizer = new NameVisualizer(new NameSourceToNameDataAdapter(source));
            visualizer.Show();

            //Bridge
            IShapeColor red = new ShapeColor(ConsoleColor.Red, "piros");
            IShapeColor blue = new ShapeColor(ConsoleColor.Blue, "kék");
            Shape s1 = new Rectangle(3, 3, 5, 5, red);
            s1.Draw();

            //Facade
            //var movies = SimpleJsonConsumer<Movie>
            //.DownloadAndConvertJson("https://nikprog.hu/samples/movie.json");

            //foreach (var movie in movies)
            //{
            //    movie.ToString();
            //}

            UI ui = new UI (new RepositoryProxy(new Repository("names.txt")));
            ui.Start();
        }
    }
}
