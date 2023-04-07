# Twileloop.UOW
Simple plug and play Unit Of Work pattern for your projects using LiteDB

# Usage
```csharp
    const string connectionString = "Filename=<DATABASE_NAME>;Mode=Shared;Password=<PASSWORD>;";
    
    using (var unitOfWork = new UnitOfWork(connectionString)) 
    {
        //Create a repo
        var resourceRepo = unitOfWork.GetRepository<ApiResource>();
        //Start transaction
        unitOfWork.BeginTransaction();
        //Do work
        try 
        {
            var newApiResource = new ApiResource {
                DisplayName = "Survey API",
                Name = "SurveyAPI",
                Scopes = new List<string> { "read", "write" }
            };
            resourceRepo.Add(newApiResource);

            //Commit transaction
            unitOfWork.Commit();
        }
        catch (Exception) 
        {
            //Rollback on failure
            unitOfWork.Rollback();
        }
    }
```