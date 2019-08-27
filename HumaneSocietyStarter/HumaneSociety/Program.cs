using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class Program
    {
        static void Main(string[] args)
        {
            //PointOfEntry.Run();

            Animal test = new Animal();
            test.AnimalId = 2;
            test.Name = "Bud";
            //Query.AddAnimal(test);
            Query.RemoveAnimal(test);

        }
    }
}
