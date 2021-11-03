using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using CarDealer.Data;
using CarDealer.DTO;
using CarDealer.DTO.Import;
using CarDealer.Models;

namespace CarDealer
{
    public class StartUp
    {
        private static string salesXml = File.ReadAllText("../../../Datasets/sales.xml");
        private static string suppliersXml = File.ReadAllText("../../../Datasets/suppliers.xml");
        private static string partsXml = File.ReadAllText("../../../Datasets/parts.xml");
        private static string carsXml = File.ReadAllText("../../../Datasets/cars.xml");
        private static string customersXml = File.ReadAllText("../../../Datasets/customers.xml");

        public static void Main(string[] args)
        {
            var database = new CarDealerContext();
      //      Console.WriteLine(GetLocalSuppliers(database,suppliersXml));
          Console.WriteLine(ImportParts(database, partsXml)); 
          Console.WriteLine(ImportCars(database, carsXml));
         Console.WriteLine(ImportCustomers(database, customersXml));
         Console.WriteLine(ImportSales(database, salesXml));

            Console.WriteLine(GetCarsWithTheirListOfParts(database));
        }

        public static string GetLocalSuppliers(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(SuppliersImportModel[]), new XmlRootAttribute("Suppliers"));
            var textRead = new StringReader(inputXml);
            var convert = serializer.Deserialize(textRead) as SuppliersImportModel[] ;
            var suppliers = convert.Select(x => new Supplier
                {
                    Name = x.Name,
                    IsImporter = x.IsImporter
                }
            ).ToArray();

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
            return  $"Successfully imported {suppliers.Length}";
        }

        public static string ImportParts(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(PartsImportModel[]), new XmlRootAttribute("Parts"));
            var textRead = new StringReader(inputXml);
            var convert = serializer.Deserialize(textRead) as PartsImportModel[];
            var suppliers = convert.Where(x=>x.SupplierId !=null).Select(x => new Part
                {
                    Name = x.Name,
                    Price= x.Price,
                    Quantity = x.Quantity,
                    SupplierId = x.SupplierId
                }
            ).ToList();

            context.Parts.AddRange(suppliers);
            context.SaveChanges();

            return $"Successfully imported {suppliers.Count}";
        }
        public static string ImportCars(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(CarsImportModel[]), new XmlRootAttribute("Cars"));
            var textRead = new StringReader(inputXml);
            var convert = serializer.Deserialize(textRead) as CarsImportModel[];
            var cars = new List<Car>();
            foreach (var car in convert)
            {

                var uniqdee = car.Parts.Select(x => x.Id).Distinct().ToArray();
                var all = uniqdee.Where(id => context.Parts.Any(i => i.Id == id));
                var carMake = new Car()
                {
                    Make = car.Make,
                    Model = car.Model,
                    TravelledDistance = car.TraveledDistance,
                    PartCars = all.Select(x => new PartCar
                    {
                        PartId = x
                    }).ToArray()
                };
                cars.Add(carMake);
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();

            return $"Successfully imported {cars.Count}";
        }

        public static string ImportCustomers(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(CustomerImportModel[]), new XmlRootAttribute("Customers"));
            var textRead = new StringReader(inputXml);
            var convert = serializer.Deserialize(textRead) as CustomerImportModel[];
            var suppliers = convert.Select(x => new Customer
                {
                    Name = x.Name,
                    BirthDate = x.BirthDate,
                    IsYoungDriver = x.IsYoungDriver
            }
            ).ToList();

            context.Customers.AddRange(suppliers);
            context.SaveChanges();
            return $"Successfully imported {suppliers.Count}";
        }

        //<Sales>

        public static string ImportSales(CarDealerContext context, string inputXml)
        {
            var serializer = new XmlSerializer(typeof(SaleImportModel[]), new XmlRootAttribute("Sales"));
            var textRead = new StringReader(inputXml);
            var convert = serializer.Deserialize(textRead) as SaleImportModel[];
            var suppliers = convert.Where(x=>context.Cars.Any(y=>y.Id==x.CarId)).Select(x => new Sale
                {
                    CarId = x.CarId,
                    CustomerId = x.CustomerId,
                    Discount = x.Discount
                }
            ).ToList();

            context.Sales.AddRange(suppliers);
            context.SaveChanges();
            return $"Successfully imported {suppliers.Count}";

        }

        public static string GetCarsWithDistance(CarDealerContext context)
        {
            StringBuilder sb = new StringBuilder();
            var namespases = new XmlSerializerNamespaces();
            namespases.Add(string.Empty, string.Empty);

            var suppliers = context.Cars.Where(x => x.TravelledDistance > 2000000)
                .Select(x => new ExportCar
                {
                    Make = x.Make,
                    Model = x.Model,
                    Travelleddistance = x.TravelledDistance
                })
                .OrderBy(x => x.Make)
                .ThenBy(x => x.Model)
                .Take(10)
                .ToArray();

                var xml = new XmlSerializer(typeof(ExportCar[]), new XmlRootAttribute("cars"));
            xml.Serialize(new StringWriter(sb), suppliers, namespases);
            return sb.ToString().TrimEnd();
        }
        public static string GetCarsFromMakeBmw(CarDealerContext context)
        {
            StringBuilder sb = new StringBuilder();
            var namespases = new XmlSerializerNamespaces();
            namespases.Add(string.Empty, string.Empty);

            var suppliers = context.Cars.Where(x => x.Make =="BMW")
                .Select(x => new ExportBMW
                {
                    Id = x.Id,
                    Model = x.Model,
                    Travelleddistance = x.TravelledDistance
                })
                .OrderBy(x => x.Model)
                .ThenByDescending(x => x.Travelleddistance)
                .ToArray();

            var xml = new XmlSerializer(typeof(ExportBMW[]), new XmlRootAttribute("cars"));
            xml.Serialize(new StringWriter(sb), suppliers, namespases);
            return sb.ToString().TrimEnd();
        }

        public static string GetLocalSuppliers(CarDealerContext context)
        {
            StringBuilder sb = new StringBuilder();
            var namespases = new XmlSerializerNamespaces();
            namespases.Add(string.Empty, string.Empty);

            var suppliers = context.Suppliers.Where(s => s.IsImporter == false)
                .Select(x => new ExportSuppliers()
                {
                    Id = x.Id,
                    Name = x.Name,
                    PartsCount = x.Parts.Count
                })
                .ToArray();

            var xml = new XmlSerializer(typeof(ExportSuppliers[]), new XmlRootAttribute("suppliers"));
            xml.Serialize(new StringWriter(sb), suppliers, namespases);
            return sb.ToString().TrimEnd();
        }

        public static string GetCarsWithTheirListOfParts(CarDealerContext context)
        {
            StringBuilder sb = new StringBuilder();
            var namespases = new XmlSerializerNamespaces();
            namespases.Add(string.Empty, string.Empty);

            var suppliers = context.Cars
                .Select(x => new ExporCarWithParts
                {
                 
                    Make = x.Make,
                    Model = x.Model,
                    TravelleDdistance = x.TravelledDistance,
                    Parts = x.PartCars
                        .Select(p => new PartsCarExport()
                        {
                            Name = p.Part.Name,
                            Price = p.Part.Price
                        })
                        .OrderByDescending(p => p.Price)
                        .ToArray()

                })
                .OrderByDescending(x=>x.TravelleDdistance)
                .ThenBy(x=>x.Model)
                .Take(5)
                .ToArray();

            var xml = new XmlSerializer(typeof(ExporCarWithParts[]), new XmlRootAttribute("cars"));
            xml.Serialize(new StringWriter(sb), suppliers, namespases);
            return sb.ToString().TrimEnd();
        }
    }
   
}