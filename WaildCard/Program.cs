using Microsoft.Data.SqlClient;
using System;

class Program
{
    static void Main()
    {
        string ConnString = "Server=HAYDAR-AL-GHAZA\\MSSQLSERVER19;Database=IMDB;"
            + "User Id=MySQLH;Password=JaaYal1!1;TrustServerCertificate=True";

        using (SqlConnection connection = new SqlConnection(ConnString))
        {
            connection.Open();

            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Search for Title in alphabetical order");
                Console.WriteLine("2. Search for Person in alphabetical order");
                Console.WriteLine("3. Add a movie to the database");
                Console.WriteLine("4. Add a person to the database");
                Console.WriteLine("5. Delete movie information");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice (1-6): ");
                int choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    Console.Write("Enter a title to search (use % for wildcard): ");
                    string searchQuery = Console.ReadLine();

                    string sqlQuery = "SELECT tconst, primaryType, originalTitle, startYear, endYear FROM Titles WHERE primaryType LIKE @SearchQuery ORDER BY primaryType";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("Search Results for Titles:");
                            while (reader.Read())
                            {
                                string tconst = reader["tconst"].ToString();
                                string primaryTitle = reader["primaryType"].ToString();
                                string originalTitle = reader["originalTitle"].ToString();
                                string startYear = reader["startYear"].ToString();
                                string endYear = reader["endYear"].ToString();

                                Console.WriteLine($"tconst: {tconst}, Primary Title: {primaryTitle}, Original Title: {originalTitle}, Start Year: {startYear}, End Year: {endYear}");
                            }
                        }
                    }
                }
                else if (choice == 2)
                {
                    Console.Write("Enter a name to search (use % for wildcard): ");
                    string searchQuery = Console.ReadLine();

                    string sqlQuery = "SELECT nconst, primaryName, birthYear, deathYear FROM Name WHERE primaryName LIKE @SearchQuery ORDER BY primaryName";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("Search Results for People:");
                            while (reader.Read())
                            {
                                string nconst = reader["nconst"].ToString();
                                string primaryName = reader["primaryName"].ToString();
                                string birthYear = reader["birthYear"].ToString();
                                string deathYear = reader["deathYear"].ToString();

                                Console.WriteLine($"nconst: {nconst}, Name: {primaryName}, Birth Year: {birthYear}, Death Year: {deathYear}");
                            }
                        }
                    }
                }
                else if (choice == 3)
                {
                    Console.Write("Enter the movie title: ");
                    string movieTitle = Console.ReadLine();
                    Console.Write("Enter the start year: ");
                    string startYear = Console.ReadLine();
                    Console.Write("Enter the end year: ");
                    string endYear = Console.ReadLine();

                    string tconst = Guid.NewGuid().ToString("N").Substring(0, 12); 

                    string titleType = "movie"; 
                    string originalTitle = "";  
                    bool isAdult = false;  

                    string sqlQuery = "INSERT INTO Titles (tconst, primaryType, startYear, endYear, titleType, originalTitle, isAdult) VALUES (@Tconst, @MovieTitle, @StartYear, @EndYear, @TitleType, @OriginalTitle, @IsAdult)";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Tconst", tconst);
                        command.Parameters.AddWithValue("@MovieTitle", movieTitle);
                        command.Parameters.AddWithValue("@StartYear", startYear);
                        command.Parameters.AddWithValue("@EndYear", endYear);
                        command.Parameters.AddWithValue("@TitleType", titleType);
                        command.Parameters.AddWithValue("@OriginalTitle", originalTitle);
                        command.Parameters.AddWithValue("@IsAdult", isAdult);  
                        command.ExecuteNonQuery();
                        Console.WriteLine("Movie added to the database.");
                    }
                }



         else if (choice == 4)
{
    Console.Write("Enter the person's name: ");
    string personName = Console.ReadLine();
    Console.Write("Enter the birth year: ");
    string birthYear = Console.ReadLine();
    Console.Write("Enter the death year: ");
    string deathYear = Console.ReadLine();

    
    string nconst = Guid.NewGuid().ToString("N").Substring(0, 12); 

    
    string sqlQuery = "INSERT INTO Name (nconst, primaryName, birthYear, deathYear) VALUES (@Nconst, @PersonName, @BirthYear, @DeathYear)";
    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
    {
        command.Parameters.AddWithValue("@Nconst", nconst);
        command.Parameters.AddWithValue("@PersonName", personName);
        command.Parameters.AddWithValue("@BirthYear", birthYear);
        command.Parameters.AddWithValue("@DeathYear", deathYear);
        command.ExecuteNonQuery();
        Console.WriteLine("Person added to the database.");
    }
}

                else if (choice == 5)
                {
                    Console.Write("Enter the tconst of the movie to delete: ");
                    string tconstToDelete = Console.ReadLine();

                    
                    string sqlQuery = "DELETE FROM Titles WHERE tconst = @TconstToDelete";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@TconstToDelete", tconstToDelete);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Movie information deleted from the database.");
                        }
                        else
                        {
                            Console.WriteLine("No movie found with the specified tconst.");
                        }
                    }
                }
                else if (choice == 6)
                {
                   
                    break;
                }
            }
        }
    }
}