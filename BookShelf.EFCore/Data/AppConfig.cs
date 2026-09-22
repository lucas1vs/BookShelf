namespace BookShelf.EFCore.Data;

public static class AppConfig
{
    public static string GetConnectionString()
    {
        return "Server=localhost\\SQLEXPRESS;Database=BookShelfDb;Trusted_Connection=True;TrustServerCertificate=True";
    }
}
