using System;
using System.Data;
using System.Data.SqlClient;

namespace ContactData
{
    public class clsCountryData
    {
        public static bool GetConuntreiesInfoByID(int CountryID, ref string CountryName)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Select * from Countries where CountryID = @CountryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                connection.Open();
                if (reader.Read())
                {
                    IsFound = true;
                    CountryName = (string)reader["CountryName"];
                }
                else IsFound = false;
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }

        public static int AddNewCountry(string CountryName)
        {
            int CountryID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Insert into Country(CountryName) values(@CountryName) select SCOPE_IDENTITY();\r\n;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                if (Result != null && int.TryParse(Result.ToString(), out int ID))
                {
                    CountryID = ID;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            finally
            { 
                connection.Close(); 
            }
            return CountryID;
        }

       public static bool UpdateCountry(int ID,  string CountryName)
        {
            int rowEffect = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Update Countries set CountryName = @CountryName where CountryID = @CountryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            command.Parameters.AddWithValue("@CountryID", ID);

            try
            {
                connection.Open();
                rowEffect = command.ExecuteNonQuery();
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message); 
              
            }
            finally
            {
                connection.Close();
            }
            return (rowEffect > 0);
        }


        

    }
}
