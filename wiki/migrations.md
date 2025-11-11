# Adding migrations
1. Add DbSet to ApplicationDbContext
2. run in terminal `% dotnet ef migrations add <MigatrionTitle> --project Data --startup-project PortalBlazor` (this prepares data for EF migration)
3. run in terminal `dotnet ef database update --project Data --startup-project PortalBlazor`