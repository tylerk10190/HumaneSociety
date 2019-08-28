using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class Query
    {        
        //Figure out HumaneSocietyDataContext, how to link it up to our data base?
        static HumaneSocietyDataContext db;
        private static object searchConditions;

        static Query()
        {
            db = new HumaneSocietyDataContext();
        }

        internal static List<USState> GetStates()
        {
            List<USState> allStates = db.USStates.ToList();       

            return allStates;
        }
            
        internal static Client GetClient(string userName, string password)
        {
            Client client = db.Clients.Where(c => c.UserName == userName && c.Password == password).Single();

            return client;
        }

        internal static List<Client> GetClients()
        {
            List<Client> allClients = db.Clients.ToList();

            return allClients;
        }

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int stateId)
        {
            Client newClient = new Client();

            newClient.FirstName = firstName;
            newClient.LastName = lastName;
            newClient.UserName = username;
            newClient.Password = password;
            newClient.Email = email;

            Address addressFromDb = db.Addresses.Where(a => a.AddressLine1 == streetAddress && a.Zipcode == zipCode && a.USStateId == stateId).FirstOrDefault();

            if (addressFromDb == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = streetAddress;
                newAddress.City = null;
                newAddress.USStateId = stateId;
                newAddress.Zipcode = zipCode;                

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                addressFromDb = newAddress;
            }

            newClient.AddressId = addressFromDb.AddressId;

            db.Clients.InsertOnSubmit(newClient);

            db.SubmitChanges();
        }

        internal static void UpdateClient(Client clientWithUpdates)
        {
            Client clientFromDb = null;

            try
            {
                clientFromDb = db.Clients.Where(c => c.ClientId == clientWithUpdates.ClientId).Single();
            }
            catch(InvalidOperationException e)
            {
                Console.WriteLine("No clients have a ClientId that matches the Client passed in.");
                Console.WriteLine("No update have been made.");
                return;
            }
            
            clientFromDb.FirstName = clientWithUpdates.FirstName;
            clientFromDb.LastName = clientWithUpdates.LastName;
            clientFromDb.UserName = clientWithUpdates.UserName;
            clientFromDb.Password = clientWithUpdates.Password;
            clientFromDb.Email = clientWithUpdates.Email;


            Address clientAddress = clientWithUpdates.Address;

 
            Address updatedAddress = db.Addresses.Where(a => a.AddressLine1 == clientAddress.AddressLine1 && a.USStateId == clientAddress.USStateId && a.Zipcode == clientAddress.Zipcode).FirstOrDefault();

            if(updatedAddress == null)
            {
                Address newAddress = new Address();
                newAddress.AddressLine1 = clientAddress.AddressLine1;
                newAddress.City = null;
                newAddress.USStateId = clientAddress.USStateId;
                newAddress.Zipcode = clientAddress.Zipcode;                

                db.Addresses.InsertOnSubmit(newAddress);
                db.SubmitChanges();

                updatedAddress = newAddress;
            }

            clientFromDb.AddressId = updatedAddress.AddressId;
            
            db.SubmitChanges();
        }
        
        internal static void AddUsernameAndPassword(Employee employee)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();

            employeeFromDb.UserName = employee.UserName;
            employeeFromDb.Password = employee.Password;

            db.SubmitChanges();
        }

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.Email == email && e.EmployeeNumber == employeeNumber).FirstOrDefault();

            if (employeeFromDb == null)
            {
                throw new NullReferenceException();
            }
            else
            {
                return employeeFromDb;
            }
        }

        internal static Employee EmployeeLogin(string userName, string password)
        {
            Employee employeeFromDb = db.Employees.Where(e => e.UserName == userName && e.Password == password).FirstOrDefault();

            return employeeFromDb;
        }

        internal static bool CheckEmployeeUserNameExist(string userName)
        {
            Employee employeeWithUserName = db.Employees.Where(e => e.UserName == userName).FirstOrDefault();

            return employeeWithUserName == null;
        }



        internal static void RunEmployeeQueries(Employee employee, string crudOperation)
        {
            switch (crudOperation)
            {
                case "create":
                    db.Employees.InsertOnSubmit(employee);
                    db.SubmitChanges();
                    break;
                case "delete":
                    var foundEmployee = db.Employees.Where(e => e.EmployeeNumber == employee.EmployeeNumber).FirstOrDefault();
                    db.Employees.DeleteOnSubmit(foundEmployee);
                    db.SubmitChanges();
                    break;
                case "read":
                    List<string> EmployeeInfo = new List<string>();
                    var employeeToDisplay = db.Employees.FirstOrDefault(e => e.EmployeeNumber == employee.EmployeeNumber);
                    EmployeeInfo.Add(employeeToDisplay.EmployeeId.ToString());
                    EmployeeInfo.Add(employeeToDisplay.FirstName);
                    EmployeeInfo.Add(employeeToDisplay.LastName);
                    EmployeeInfo.Add(employeeToDisplay.UserName);
                    EmployeeInfo.Add(employeeToDisplay.Password);
                    EmployeeInfo.Add(employeeToDisplay.EmployeeNumber.ToString());
                    EmployeeInfo.Add(employeeToDisplay.Email);
                    UserInterface.DisplayUserOptions(EmployeeInfo);
                    break;
                case "update":
                    var employeeToUpdate = db.Employees.Where(e => e.EmployeeNumber == employee.EmployeeNumber).FirstOrDefault();
                    employeeToUpdate.FirstName = employee.FirstName;
                    employeeToUpdate.LastName = employee.LastName;
                    employeeToUpdate.Email = employee.Email;
                    db.SubmitChanges();
                    break;
            }

        }


        internal static void AddAnimal(Animal animal)
        {
            db.Animals.InsertOnSubmit(animal);
            db.SubmitChanges();
        }

        internal static Animal GetAnimalByID(int id)
        {
            Animal animal = new Animal();
            animal = db.Animals.Where(i => i.AnimalId == id).FirstOrDefault();
            return animal;
        }

        internal static void UpdateAnimal(int animalId, Dictionary<int, string> updates)
        {

            Animal animal = db.Animals.Where(a => a.AnimalId == animalId).FirstOrDefault();
            GetAnimalUpdates(updates, animal);
            db.SubmitChanges();
        }

        internal static void GetAnimalUpdates(Dictionary<int, string> updates, Animal animal)
        {
            foreach (KeyValuePair<int, string> el in updates)
            {
                switch (el.Key)
                {
                    case 1:
                        animal.CategoryId = GetCategoryId((el.Value));
                        break;
                    case 2:
                        animal.Name = el.Value;
                        break;
                    case 3:
                        animal.Age = int.Parse(el.Value);
                        break;
                    case 4:
                        animal.Demeanor = el.Value;
                        break;
                    case 5:
                        if(el.Value == "yes")
                        {
                            animal.KidFriendly = true;
                        }
                        else
                        {
                            animal.KidFriendly = false;
                        }
                        break;
                    case 6:
                        if (el.Value == "yes")
                        {
                            animal.PetFriendly = true;
                        }
                        else
                        {
                            animal.PetFriendly = false;
                        }
                        break;
                    case 7:
                        animal.Weight = int.Parse(el.Value);
                        break;
                    case 8:
                        animal.AnimalId = int.Parse(el.Value);
                        break;

                }
            }

        }

        internal static void RemoveAnimal(Animal animal)
        {
            Animal existingAnimal = db.Animals.Where(a => a.AnimalId == animal.AnimalId).FirstOrDefault();
            db.Animals.DeleteOnSubmit(existingAnimal);
            db.SubmitChanges();
        }
        
        // TODO: Animal Multi-Trait Search
        internal static IQueryable<Animal> SearchForAnimalsByMultipleTraits(Dictionary<int, string> updates)
        {

            
            IQueryable<Animal> animals = db.Animals;
            foreach (KeyValuePair<int, string> update in updates)
            {
                switch (update.Key)
                {
                    case 1:
                        animals = animals.Where(a => a.CategoryId == GetCategoryId(update.Value));
                        break;
                    case 2:
                        animals = animals.Where(a=> a.Name == update.Value);
                        break;
                    case 3:
                        animals = animals.Where(a => a.Age == int.Parse(update.Value));
                        break;
                    case 4:
                        animals = animals.Where(a => a.Demeanor == update.Value);
                        break;
                    case 5:
                        animals = animals.Where(a => a.KidFriendly == bool.Parse(update.Value));
                        break;
                    case 6:
                        animals = animals.Where(a => a.PetFriendly == bool.Parse(update.Value));
                        break;
                    case 7:
                        animals = animals.Where(a => a.Weight == int.Parse(update.Value));
                        break;
                }
            }
            Console.ReadLine();
            return animals;
        }

        //TODO: Misc Animal Things
        internal static int GetCategoryId(string categoryName)
        {

            Category category = db.Categories.Where(c => c.Name == categoryName).FirstOrDefault();
            int categoryID = category.CategoryId;
            return categoryID;

        }

        internal static Room GetRoom(int animalId)
        {
            Room room = new Room();
            room = db.Rooms.Where(r => r.AnimalId == animalId).FirstOrDefault();
            return room;
        }
        
        internal static int GetDietPlanId(string dietPlanName)
        {
            DietPlan dp = db.DietPlans.Where(d => d.Name == dietPlanName).FirstOrDefault();
            int DpID = dp.DietPlanId;
            return DpID;

        }

        // TODO: Adoption CRUD Operations
        internal static void Adopt(Animal animal, Client client)
        {
            var adoptingClient = db.Clients.Where(c => c.ClientId == client.ClientId).FirstOrDefault();
            var adoptedAnimal = db.Animals.Where(a => a.AnimalId == animal.AnimalId).FirstOrDefault();
            Adoption newAdoption = new Adoption();
            newAdoption.AnimalId = adoptedAnimal.AnimalId;
            newAdoption.ClientId = adoptingClient.ClientId;
            newAdoption.ApprovalStatus = "Pending";
            newAdoption.PaymentCollected = false;
            db.Adoptions.InsertOnSubmit(newAdoption);
            db.SubmitChanges();

        }

        internal static IQueryable<Adoption> GetPendingAdoptions()
        {
           
            var pendingAdoptions = db.Adoptions.Where(a => a.ApprovalStatus == "Pending");
            var pendingAdoptionsDictionary = pendingAdoptions.ToDictionary(a => a.AnimalId, a => a.ApprovalStatus);
            foreach (var el in pendingAdoptionsDictionary)
            {
                var keyCatch = db.Animals.Where(a => a.AnimalId == el.Key).FirstOrDefault();
                Console.WriteLine(keyCatch.Name);
            }
            return pendingAdoptions;
        }

        internal static void UpdateAdoption(bool isAdopted, Adoption adoption)
        {

            if(isAdopted == true)
            {
              adoption.ApprovalStatus = "Approved";
            }
            else
            {
<<<<<<< HEAD
                adoption.ApprovalStatus = "Denied";
=======
                var updatedAdoption = db.Adoptions.Where(a => a.AnimalId == adoption.AnimalId).FirstOrDefault();
                updatedAdoption.ApprovalStatus = "Denied";
>>>>>>> af5d44e2a475bcf10b1e8931c5b04697b401340a
                RemoveAdoption(adoption.AnimalId, adoption.ClientId);
            }
            db.SubmitChanges();
        }

        internal static void RemoveAdoption(int animalId, int clientId)
        {
            var adoptedAnimal = db.Adoptions.Where(a => a.AnimalId == animalId && a.ClientId == clientId).FirstOrDefault();

            db.Adoptions.DeleteOnSubmit(adoptedAnimal);
            db.SubmitChanges();

<<<<<<< HEAD
=======

            var adoptedAnimal = db.Adoptions.Where(a => a.AnimalId == animalId && a.ClientId == clientId).FirstOrDefault();                      
            
                db.Adoptions.DeleteOnSubmit(adoptedAnimal);
                db.SubmitChanges();
            
>>>>>>> af5d44e2a475bcf10b1e8931c5b04697b401340a
        }

        
        internal static IQueryable<AnimalShot> GetShots(Animal animal)
        {
            var shotsReceived = db.AnimalShots.Where(s => s.AnimalId == animal.AnimalId);
            return shotsReceived;
        }

        internal static void UpdateShot(string shotName, Animal animal)
        {
            var shotId = db.Shots.Where(a => a.Name == shotName).Select(s => s.ShotId).Single();
            AnimalShot animalShot = new AnimalShot()
            {
                AnimalId = animal.AnimalId,
                ShotId = shotId
            };
            db.AnimalShots.InsertOnSubmit(animalShot);
            db.SubmitChanges();
        }
    }
}