## Migrations

dotnet ef migrations add FixForeignKeys --project Core/Core.csproj --startup-project WorkforcePlanner.csproj --context WorkForceDbContext
dotnet ef database update