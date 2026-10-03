using Microsoft.Data.SqlClient;
using SafeVaultDemo.Models;

namespace SafeVault.Data;

public class AuthRepository
{
    private readonly string _connectionString;

    public AuthRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public User? GetUserByUsername(string username)
    {
        const string query =
            @"SELECT Id,
                     Username,
                     Email,
                     PasswordHash,
                     Role
              FROM Users
              WHERE Username = @Username";

        using SqlConnection connection =
            new(_connectionString);

        using SqlCommand command =
            new(query, connection);

        command.Parameters.AddWithValue(
            "@Username",
            username);

        connection.Open();

        using SqlDataReader reader =
            command.ExecuteReader();

        if (!reader.Read())
            return null;

        return new User
        {
            Id = reader.GetInt32(0),
            Username = reader.GetString(1),
            Email = reader.GetString(2),
            PasswordHash = reader.GetString(3),
            Role = reader.GetString(4)
        };
    }
}