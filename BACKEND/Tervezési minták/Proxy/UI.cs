using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tervezési_minták.Proxy
{
    public class UI
    {
        IRepository repo;
        public UI(IRepository repo)
        {
            this.repo = repo;
        }
        public void Start()
        {
            Console.Clear();
            Console.WriteLine("1: LIST");
            Console.WriteLine("2: CREATE");
            Console.WriteLine("3: REMOVE");
            Console.Write("Enter menu number: ");
            int choosen = int.Parse(Console.ReadLine());
            Console.Clear();
            switch (choosen)
            {
                case 1:
                    foreach (var item in repo.ListNames())
                    {
                        Console.WriteLine(item);
                    }
                    Console.ReadLine();
                    Start();
                    break;
                case 2:
                    Console.WriteLine("New name: ");
                    string data = Console.ReadLine();
                    repo.AddName(data);
                    Start();
                    break;
                case 3:
                    Console.WriteLine("Name to delete: ");
                    string dataToDelete = Console.ReadLine();
                    repo.RemoveName(dataToDelete);
                    Start();
                    break;
                default:
                    break;
            }
        }
    }
}
