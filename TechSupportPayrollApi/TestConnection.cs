using Npgsql;

class TestConnection
{
    static async Task Main()
    {
        var connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=thranduil592";

        try
        {
            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            Console.WriteLine("✅ Connection successful with thranduil592!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Connection failed: {ex.Message}");
        }
    }
}