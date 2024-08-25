using System;
using System.Data;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public class clsContactDataAccess
    {
        public static bool GetContactInfoByID(int  id, ref string FirstName, ref string LastName, ref string Email, ref string Phone, ref string Address, ref DateTime BirthDate, ref int CountryID, ref string ImagePath)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Contacts WHERE ContactID = @ContactID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactID", id);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if(reader.Read())
                {
                    IsFound = true;
                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    BirthDate = (DateTime)reader["DateOfBirth"];
                    CountryID = (int)reader["CountryID"];
                    ImagePath = (string)reader["ImagePath"].ToString();
                }
                else
                {
                    IsFound = false;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
               IsFound=false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }

        public static int AddNewContact(string FirstName, string LastName, string Email, string Phone,string Address, DateTime DateOfBirth,int CountryID,string ImagePath)
            {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Insert into Contacts(FirstName, LastName, Email, Phone, Address, DateOfBirth, ImagePath)" +
                           "Values (@FirstName, @LastName, @Email, @Phone, @Address, @DateOfBirth, @CountryID, @ImagePath)" +
                           "Select SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            if (ImagePath == "")
            {
            command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }

            try
            {
                connection.Open();
                object Result = command.ExecuteScalar();
                connection.Close();
                if (Result != null && int.TryParse(Result.ToString(), out int ID) 
                    {

                    }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);   
            }
            finally 
            { 
                connection.Close(); 
            }
            
            }
    }
}
