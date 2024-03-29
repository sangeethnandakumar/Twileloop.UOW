﻿using Twileloop.UOW.LiteDB.Support;
using Twileloop.UOW.MongoDB.Support;

namespace Twileloop.UOW.Demo
{
    public class Program
    {
        static void Main(string[] args)
        {
            //LiteDB
            LiteDBTests();

            //MongoDB
            MongoDBTests();
        }

        private static void LiteDBTests()
        {
            var context = LiteDB.Support.Extensions.BuildDbContext(option =>
            {
                option.Connections = new List<LiteDBConnection>
                {
                    new LiteDBConnection("DatabaseA", "Filename=DatabaseA.db; Mode=Shared; Password=****;")
                };
            });
            var uow = new LiteDB.Core.UnitOfWork(context);
            var carRepo = uow.GetRepository<Car>();
            uow.BeginTransaction();
            carRepo.AddRange(new List<Car>
            {
                new Car
                {
                    Make = "BMW",
                    Model = "S Series"
                },
                new Car
                {
                     Make = "Bugatty",
                    Model = "Chiron"
                },
            });
            uow.Commit();
            foreach (var car in carRepo.GetAll())
            {
                Console.WriteLine($"Car is {car.Make} {car.Model}");
            }
        }

        private static void MongoDBTests()
        {
            var context = MongoDB.Support.Extensions.BuildDbContext(option =>
            {
                option.Connections = new List<MongoDBConnection>
                {
                    new MongoDBConnection("MongoDB", "mongodb+srv://****:****@cluster0.tzxpkhv.mongodb.net/")
                };
            });
            var uow = new MongoDB.Core.UnitOfWork(context);
            var carRepo = uow.GetRepository<Car>();
            carRepo.AddRange(new List<Car>
            {
                new Car
                {
                    Make = "BMW",
                    Model = "S Series"
                },
                new Car
                {
                     Make = "Bugatty",
                    Model = "Chiron"
                },
            });
            foreach (var car in carRepo.GetAll())
            {
                Console.WriteLine($"Car is {car.Make} {car.Model}");
            }
        }
    }

    public class Car : EntityBase
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
    }

}
