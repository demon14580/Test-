using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    static class People
    {
        static public string Name;
        static public string Age;
        static public string Address;

        static public void ShowInfo(string Name,string Age, string Address)
        {
            Console.WriteLine(Name + " " + Age + " " + Address);
        }
    }
}
