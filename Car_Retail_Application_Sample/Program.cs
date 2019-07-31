using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Car Retail Application Sample by Aaser Truusa
 * 
 * We need to build a car retailer application. The core of the application is to show cars and calculate car prices for customer.  
 * Every car belongs to a model and for every model there is a regular price and sometimes also a campaign price. 
 * Car also can have accessories with prices (for simplification only regular price – accessories have name and regular price only). 
 * Make a small programm that can handle this logic and calculate the car price for the car (no need for database or UI, just the business logic code is needed).
 * 
 * NOTE: Program expects flawless user input.
 */

namespace CarRetailApp_Sample
{
    class Program
    {
        private static List<Model> models = new List<Model>();
        private static List<Car> cars = new List<Car>();
        private static List<Accessory> accessories = new List<Accessory>();
        private static string selectedModel, selectedCar;
        private static string[] selectedAccessories;
        private static double modelPrice, accessoryPrice, orderPrice;

        static void Main(string[] args)
        {
            SetUpSampleData();
            Console.WriteLine();

            ShowModels();
            Console.WriteLine();

            SelectModel();
            Console.WriteLine();

            ShowCars();
            Console.WriteLine();

            SelectCar();
            Console.WriteLine();

            ShowAccessories();
            Console.WriteLine();

            SelectAccessory();
            Console.WriteLine();

            ShowFinalPrice();
            Console.WriteLine();

            Console.Write("Press <any> key to exit...");
            Console.ReadKey();
        }

        private static void ShowFinalPrice()
        {
            Console.WriteLine("Selected Products: ");
            Console.WriteLine($"* Model: \n  Name: {selectedModel} \n  Price: {modelPrice} EUR");
            Console.WriteLine($"* Car: \n  Name: {selectedCar}");
            Console.WriteLine($"* Accessroy(ies): ");
            foreach (var accessory in selectedAccessories)
            {
                var currentAccessory = accessories.Where(x => x.Name == accessory).ToList();
                foreach (var accessory2 in currentAccessory)
                {
                    if (selectedAccessories.Count() > 1)
                        Console.WriteLine($"  -> Name: { accessory2.Name } \n     Price: { accessory2.RegularPrice } EUR");
                    else
                        Console.WriteLine($"  Name: { accessory2.Name } \n  Price: { accessory2.RegularPrice } EUR");
                }
            }
            Console.WriteLine();
            Console.WriteLine($"Final Price: { orderPrice } EUR");
        }

        private static void SelectAccessory()
        {
            // Get selected accessories
            Console.Write("Select accessory(ies) (Insert names separated by ', '): ");
            string userInput = Convert.ToString(Console.ReadLine());

            if (userInput.Contains(", "))
            {
                selectedAccessories = userInput.Split(new string[] { ", " }, StringSplitOptions.None);
            }
            else
            {
                selectedAccessories = new string[] { userInput };
            }

            // Print selected accessories
            Console.WriteLine("Selected accessory(ies): ");
            foreach (var accessory in selectedAccessories)
            {
                var currentAccessory = accessories.Where(x => x.Name == accessory).ToList();
                foreach (var accessory2 in currentAccessory)
                {
                    Console.WriteLine($"* Name: { accessory2.Name } \n  Price: { accessory2.RegularPrice } EUR");

                    accessoryPrice += accessory2.RegularPrice;
                    orderPrice += accessory2.RegularPrice;
                }
            }
        }

        private static void ShowAccessories()
        {
            var JoinedDB = from model in models
                           join car in cars on model.ModelID equals car.CarID
                           join accessory in accessories on car.CarID equals accessory.AccessoryID
                           select new
                           {
                               AccessoryName = accessory.Name,
                               AccessoeyRegularPrice = accessory.RegularPrice,
                           };

            Console.WriteLine("Available accessories: ");
            foreach (var accessory in JoinedDB)
            {
                Console.WriteLine($"* Name: { accessory.AccessoryName } \n  Regular Price: { accessory.AccessoeyRegularPrice } EUR");
            }
        }

        private static void SelectCar()
        {
            Console.Write("Select car (Insert car name): ");
            selectedCar = Console.ReadLine();

            Console.WriteLine("Selected car: ");
            cars = cars.Where(x => x.Name == selectedCar).ToList();

            foreach (var car in cars)
            {
                Console.WriteLine($"* Name: { car.Name }");
            }
        }

        private static void ShowCars()
        {
            var JoinedDB = from model in models
                           join car in cars on model.ModelID equals car.CarID
                           where model.Name == selectedModel
                           select car.Name;

            Console.WriteLine("Available cars: ");
            foreach (var car in JoinedDB)
            {
                Console.WriteLine($"* Name: { car }");
            }
        }

        private static void SelectModel()
        {
            Console.Write("Select model (Insert model name): ");
            selectedModel = Console.ReadLine();

            Console.WriteLine("Selected Model: ");
            models = models.Where(x => x.Name == selectedModel).ToList();

            foreach (var model in models)
            {
                if (model.CampaignPrice > 0)
                {
                    Console.WriteLine($"* Name: { model.Name } \n  Price: { model.CampaignPrice } EUR");
                    modelPrice += model.CampaignPrice;
                    orderPrice += model.CampaignPrice;
                }
                else
                {
                    Console.WriteLine($"* Name: { model.Name } \n  Price: { model.RegularPrice } EUR");
                    modelPrice += model.RegularPrice;
                    orderPrice += model.RegularPrice;
                }
            }
        }

        private static void ShowModels()
        {
            Console.WriteLine("All Models: ");

            foreach (var model in models)
            {
                Console.WriteLine($"* Name: { model.Name } \n  Regular Price: { model.RegularPrice } EUR \n  Campaign Price: { model.CampaignPrice } EUR");
            }
        }

        private static void SetUpSampleData()
        {
            models.Add(new Model { ModelID = 1, Name = "Model1", RegularPrice = 55000, CampaignPrice = 49000 });
            models.Add(new Model { ModelID = 2, Name = "Model2", RegularPrice = 45000, CampaignPrice = 43000 });
            models.Add(new Model { ModelID = 3, Name = "Model3", RegularPrice = 35000, CampaignPrice = 31000 });
            cars.Add(new Car { CarID = 1, Name = "Car11" });
            cars.Add(new Car { CarID = 1, Name = "Car12" });
            cars.Add(new Car { CarID = 1, Name = "Car13" });
            cars.Add(new Car { CarID = 2, Name = "Car21" });
            cars.Add(new Car { CarID = 2, Name = "Car22" });
            cars.Add(new Car { CarID = 2, Name = "Car23" });
            cars.Add(new Car { CarID = 3, Name = "Car31" });
            cars.Add(new Car { CarID = 3, Name = "Car32" });
            cars.Add(new Car { CarID = 3, Name = "Car33" });
            accessories.Add(new Accessory { AccessoryID = 1, Name = "Accessory11", RegularPrice = 1100 });
            accessories.Add(new Accessory { AccessoryID = 1, Name = "Accessory12", RegularPrice = 1200 });
            accessories.Add(new Accessory { AccessoryID = 1, Name = "Accessory13", RegularPrice = 1300 });
            accessories.Add(new Accessory { AccessoryID = 2, Name = "Accessory21", RegularPrice = 2100 });
            accessories.Add(new Accessory { AccessoryID = 2, Name = "Accessory22", RegularPrice = 2200 });
            accessories.Add(new Accessory { AccessoryID = 2, Name = "Accessory23", RegularPrice = 2300 });
            accessories.Add(new Accessory { AccessoryID = 3, Name = "Accessory31", RegularPrice = 3100 });
            accessories.Add(new Accessory { AccessoryID = 3, Name = "Accessory32", RegularPrice = 3200 });
            accessories.Add(new Accessory { AccessoryID = 3, Name = "Accessory33", RegularPrice = 3300 });
        }
    }
}
