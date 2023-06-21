<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/sangeethnandakumar/Twileloop.UOW">
    <img src="https://iili.io/HPIj6ss.png" alt="Logo" width="80" height="80">
  </a>

  <h2 align="center"> Twileloop UOW</h2>
  <h4 align="center"> Free | Open-Source | Fast </h4>
</div>

## About
A lightweight and ready-made implementation of unit of work pattern + and a NoSQL database like LiteDB or MongoDB. With ability to use multiple LiteDB or MongoDB databases, ready-made CRUD operations repository and thread-safe features.

A plug & play package where you don't need to write lot and lot and lots of code to setp repositories and stuff. It's easy as 2 steps below

## License
> Twileloop.UOW.LiteDB - is licensed under the MIT License. See the LICENSE file for more details.

> Twileloop.UOW.MongoDB - is licensed under the MIT License. See the LICENSE file for more details.

#### This library is absolutely free. If it gives you a smile, A small coffee would be a great way to support my work. Thank you for considering it!
[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/sangeethnanda)

# Usage

## 1. Register all databases to ASP.NET dependency injection
```csharp
//LiteDB
builder.Services.AddUnitOfWork((uow) => {
    uow.Connections = new List<LiteDBConnection>
    {
        new LiteDBConnection("DatabaseA", "Filename=DatabaseA.db; Mode=Shared; Password=****;"),
        new LiteDBConnection("DatabaseB", "Filename=DatabaseB.db; Mode=Shared; Password=****;")
    };
});

//MongoDB
builder.Services.AddUnitOfWork((uow) => {
    uow.Connections = new List<MongoDBConnection>
    {
        new MongoDBConnection("DatabaseA", "mongodb+srv://Uername:****@Cluster"),
        new MongoDBConnection("DatabaseB", "mongodb+srv://Uername:****@Cluster")
    };
});
```

## 2. For No Dependency Injection Setup
```csharp
//LiteDB
var context = LiteDB.Support.Extensions.BuildDbContext(option =>
    {
        option.Connections = new List<LiteDBConnection>
        {
            new LiteDBConnection("DatabaseA", "Filename=DatabaseA.db; Mode=Shared; Password=****;"),
            new LiteDBConnection("DatabaseB", "Filename=DatabaseB.db; Mode=Shared; Password=****;")
        };
    });
var uow = new LiteDB.Core.UnitOfWork(context);

//MongoDB
var context = MongoDB.Support.Extensions.BuildDbContext(option =>
    {
        option.Connections = new List<MongoDBConnection>
        {
            new MongoDBConnection("DatabaseA", "mongodb+srv://Username:****@Cluster"),
            new MongoDBConnection("DatabaseB", "mongodb+srv://Username:****@Cluster")
        };
    });
var uow = new MongoDB.Core.UnitOfWork(context);
```

## 2. Inject and Use as required
```csharp
    [ApiController]
    public class HomeController : ControllerBase 
    {
        private readonly UnitOfWork uow;

        public HomeController(UnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpGet]
        public IActionResult Get() 
        {            
            try
            {
                // Step 1: Point to a database
                uow.UseDatabase("<DB_NAME>");

                //Step 2: Get a repository for your model 'Dogs'
                var dogRepo = uow.GetRepository<Dogs>();

                //Step 3: Do some fetch
                allDogs = dogRepo.GetAll().ToList();

                //Step 4: Or any CRUD operations you like
                dogRepo.Add(new Dog());

                //Step 5: Finally, commit or rollback if transaction need to maintain. That's it
                uow.Commit();

                return Ok(allDogs);
            }
            catch(Exception)
            {
                uow.Rollback();
            }            
        }

    }
```
