using cw4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace cw4.Services
{
    public class DbService : IDbService
    {
        private IConfiguration _configuration;
        public DbService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool CheckAnimalById(int idAnimal)
        {
            int userCount = 0;
            using (SqlConnection con = new SqlConnection("Data Source=db-mssql16.pjwstk.edu.pl; Initial Catalog=s20296; Integrated Security=True")) {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) from Animal where IdAnimal like @idAnimal", con))
                {
                con.Open();
                sqlCommand.Parameters.AddWithValue("@idAnimal", idAnimal);
                userCount = (int)sqlCommand.ExecuteScalar();
                    if (userCount > 0)
                        return true;
                }
            }
            return false;
            
        }
  
        public List<Animal> GetAnimal(string orderBy)
        {
            var animals = new List<Animal>();
            string conString = "Data Source=db-mssql16.pjwstk.edu.pl; Initial Catalog=s20296; Integrated Security=True";
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                if (orderBy != null)
                    com.CommandText = "SELECT * FROM Animal ORDER BY " + orderBy;
                else com.CommandText = "SELECT * FROM Animal ORDER BY name";
                con.Open();

                

                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    animals.Add(new Animal
                    {
                        IdAnimal = int.Parse(dr["IdAnimal"].ToString()),
                        Name = dr["Name"].ToString(),
                        Description = dr["Description"].ToString(),
                        Category = dr["Category"].ToString(),
                        Area = dr["Area"].ToString()
                    });

                }
                con.Close();
            }
            

            return animals;
        }

        public string PostAnimal(Animal animal)
        {
            string conString = "Data Source=db-mssql16.pjwstk.edu.pl; Initial Catalog=s20296; Integrated Security=True";
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "INSERT INTO Animal (Name, Description, Category, Area) VALUES( '"+ animal.Name + "','" + animal.Description + "','" + animal.Area + "','" + animal.Category + "')";
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
            return "Added new Animal";
        }

        public string PutAnimal(int idAnimal, Animal animal)
        {
            string conString = "Data Source=db-mssql16.pjwstk.edu.pl; Initial Catalog=s20296; Integrated Security=True";
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "UPDATE Animal SET Name = '" +  animal.Name + "', Description = '" + animal.Description + "', Area = '" + animal.Area + "', Category = '" + animal.Category +  "' WHERE IdAnimal = '" + idAnimal + "'";
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
            return "Updated animal";
        }

        public string DeleteAnimal(int idAnimal)
        {

            string conString = "Data Source=db-mssql16.pjwstk.edu.pl; Initial Catalog=s20296; Integrated Security=True";
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand com = new SqlCommand();
                com.Connection = con;
                com.CommandText = "DELETE FROM Animal WHERE IdAnimal = '" + idAnimal + "'";
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
            return "Deleted animal";
        }
    }
}
