using Microsoft.Data.SqlClient;
using SafeVaultDemo.Models;

namespace SafeVaultDemo.Data;

public class UserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public User? GetUserByUsername(string username)
    {
        const string query =
            @"SELECT Id, Username, Email
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

        if (reader.Read())
        {
            return new User
            {
                Id = reader.GetInt32(0),
                Username = reader.GetString(1),
                Email = reader.GetString(2)
            };
        }

        return null;
    }
}
