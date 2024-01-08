using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace TpChrono.Models
{
    public class User
    {
        private readonly string connectionString;

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public int Tentatives { get; set; }
        public string Role { get; set; }
        public bool Valid { get; set; }
        public bool Lockout { get; set; }

        public User()
        {
        }

        public static User Connecter(string login, string password)
        {
            var user = new User(); // Create a new User instance

            // Implement logic to authenticate the user
            using (var connection = new SqlConnection(user.connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Users WHERE Login = @Login AND Password = @Password";
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // User authenticated successfully, populate the User object
                            user.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                            user.Nom = reader.GetString(reader.GetOrdinal("Nom"));
                            user.Prenom = reader.GetString(reader.GetOrdinal("Prenom"));
                            user.Login = reader.GetString(reader.GetOrdinal("Login"));
                            user.Role = reader.GetString(reader.GetOrdinal("Role"));
                            user.Valid = reader.GetBoolean(reader.GetOrdinal("Valid"));
                            user.Lockout = reader.GetBoolean(reader.GetOrdinal("Lockout"));
                            // Additional properties assignment...
                            return user;
                        }
                        else
                        {
                            // Authentication failed
                            return null;
                        }
                    }
                }
            }
        }

        public void Register()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Users (Nom, Prenom, Login, Password, Role, Valid, Lockout) " +
                                          "VALUES (@Nom, @Prenom, @Login, @Password, @Role, @Valid, @Lockout)";

                    command.Parameters.AddWithValue("@Nom", Nom);
                    command.Parameters.AddWithValue("@Prenom", Prenom);
                    command.Parameters.AddWithValue("@Login", Login);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@Role", Role);
                    command.Parameters.AddWithValue("@Valid", Valid);
                    command.Parameters.AddWithValue("@Lockout", Lockout);

                    command.ExecuteNonQuery();
                }
            }
        }

   
    public static User Search(string login)
        {
            var user = new User();
          
            SqlConnection con = new SqlConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = login;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter(
                "@login",
                login));
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                user.Nom = reader.GetString(1);
                user.Prenom = reader.GetString(2);




                return user;

            }

            throw new NotImplementedException();
        }

        public void IncrementAttempts()
        {
            // Implement logic to increment login attempts
            Tentatives++;
            // Update the database to reflect the new number of attempts
            // Use the connectionString field to connect to the database
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE User SET Tentaives = @Tentaives WHERE Login = @Login";
                    command.Parameters.AddWithValue("@Tentaives", Tentatives);
                    command.Parameters.AddWithValue("@Login", Login);
                    command.ExecuteNonQuery();
                }
            }


            throw new NotImplementedException();
        }

        // Method to lock user account
        public void Lock()
        {
            Valid = false;
            
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE User SET Valid = @Valid WHERE Login = @Login";
                    command.Parameters.AddWithValue("@Valid", Valid);
                    command.Parameters.AddWithValue("@Login", Login);
                    command.ExecuteNonQuery();
                }
            }
            throw new NotImplementedException();
        }

        public void InitializeAttempts()
        {
            Tentatives = 0;
            
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE User SET Tentaives = @Tentaives WHERE Login = @Login";
                    command.Parameters.AddWithValue("@Tentaives", Tentatives);
                    command.Parameters.AddWithValue("@Login", Login);
                    command.ExecuteNonQuery();
                }
            }
            throw new NotImplementedException();
        }
    }
}

