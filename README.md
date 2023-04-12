<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/sangeethnandakumar/Twileloop.UOW">
    <img src="https://iili.io/HeD5SzG.png" alt="Logo" width="80" height="80">
  </a>

  <h2 align="center"> Twileloop UOW For LiteDB </h2>
  <h4 align="center"> Free | Open-Source | Fast </h4>

  <p align="center">
    <b> 
Twileloop.UOW is a .NET library that provides a simple and efficient implementation of the Unit Of Work (UOW) pattern using LiteDB. It is a plug-and-play solution that allows you to manage database operations with ease.		 </b>
    <br />
    <a href="https://twileloop.epub.readthedocs.io"><strong>Explore the docs »</strong></a>
    <br />
    <br />
  </p>
  
</div>

## License
> Twileloop.UOW is licensed under the MIT License. See the LICENSE file for more details.

#### A small coffee would be a great way to support my work. Thank you for considering it!
[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/sangeethnanda)


<div align="center">

  <h2 align="center"> DOCUMENTATION </h2>
  <h4 align="center"> 
  This .NET library is a pre-built implementation of the unit of work pattern using LiteDB, providing a simple and efficient way to manage database operations.
  </h4>
  
</div>

# Usage

## 1. Register all databases
```csharp
builder.Services.AddUnitOfWork((uow) => {
    uow.Connections = new List<LiteDBConnection>
    {
        new LiteDBConnection(<DB_NAME_1>, "Filename=DatabaseA.db; Mode=Shared; Password=****;"),
        new LiteDBConnection(<DB_NAME_2>, "Filename=DatabaseB.db; Mode=Shared; Password=****;")
    };
});
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

                //Step 2: Make a repository
                var dogRepo = uow.GetRepository<Dogs>();

                //Step 3: Do a query
                allDogs = dogRepo.GetAll().ToList();

                //Step 4: Or any CRUD operations
                dogRepo.Add(new Dog());

                //Step 5: Commit or rollback to maintain transactions
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
