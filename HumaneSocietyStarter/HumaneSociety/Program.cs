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


            //test.AnimalId = 3;
            //test.Name = "Bud";
            //Query.RemoveAnimal(test);
            //Query.UpdateAnimal(7, UserInterface.GetAnimalSearchCriteria());
            Animal animal = new Animal();
            Query.UpdateShot("Rabies", animal);

            //Query.SearchForAnimalsByMultipleTraits(UserInterface.GetAnimalSearchCriteria());

        }
    }
}
