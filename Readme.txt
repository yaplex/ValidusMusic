Add new migration
dotnet-ef migrations add EmptyDatabaseMigration --project ValidusMusic.DataProvider --startup-project ValidusMusic.Api

push changes to DB
 dotnet-ef database update --project ValidusMusic.DataProvider --startup-project ValidusMusic.Api