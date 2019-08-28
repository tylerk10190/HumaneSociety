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

            Query.SearchForAnimalsByMultipleTraits(UserInterface.GetAnimalSearchCriteria());

<<<<<<< HEAD
            //test.AnimalId = 3;
            //test.Name = "Bud";
            //Query.RemoveAnimal(test);
            //Query.UpdateAnimal(7, UserInterface.GetAnimalSearchCriteria());
            Animal animal = new Animal();
            Query.UpdateShot("Rabies", animal);
=======
>>>>>>> d37548109a6fa26bd0d99516d2e5edd73d5b4b88

            //Query.SearchForAnimalsByMultipleTraits(UserInterface.GetAnimalSearchCriteria());

        }
    }
}
