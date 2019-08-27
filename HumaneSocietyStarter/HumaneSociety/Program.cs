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

<<<<<<< HEAD
            Animal test = new Animal();
            //test.AnimalId = 3;
            //test.Name = "Bud";
            //Query.RemoveAnimal(test);
            Query.UpdateAnimal(7, UserInterface.GetAnimalSearchCriteria());

=======
            //Animal test = new Animal();
            Category test = new Category();
            //test.AnimalId = 2;
            //test.Name = "Bud";
            //Query.AddAnimal(test);
            Query.GetCategoryId("3");
            
>>>>>>> e32f504f8a85e2463361f7cb6634d4189ae1f1cb

        }
    }
}
