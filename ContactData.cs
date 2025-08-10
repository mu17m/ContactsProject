using System;
using System.Data;
using System.Data.SqlClient;
using ContactsDataAccessLayer.DataAccessSettings;

namespace ContactsDataAccessLayer.ContactData
{
    public class clsContactDataAccess
    {
        public static int AddNewContact(string FirstName, string LastName, string Email, string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            int ContactID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Insert into Contacts(FirstName, LastName, Email, Phone, Address, DateOfBirth, CountryID, ImagePath)" +
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
                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {
                    ContactID = InsertedID;
                };
            }
            catch 
            {
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return ContactID;
        }
        public static bool UpdateContact(int ContactID, string FirstName, string LastName, string Email, string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            bool IsDone = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Update Contacts set FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone, Address = @Address, DateOfBirth = @DateOfBirth, CountryID = @CountryID, ImagePath = @ImagePath where ContactID = @ContactID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactID", ContactID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@ImagePath", ImagePath);

            try
            {
                connection.Open();
                int rowEffect = command.ExecuteNonQuery();
                if(rowEffect > 0)
                {
                    IsDone = true;
                }
            }
            catch
            {
               // Console.WriteLine(ex.Message);
                IsDone = false;
            }
            finally
            {
                connection.Close();
            }
            return IsDone;

        }
        public static bool GetContactInfoByID(int id, ref string FirstName, ref string LastName, ref string Email, ref string Phone, ref string Address, ref DateTime BirthDate, ref int CountryID, ref string ImagePath)
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

                if (reader.Read())
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
            catch 
            {
                //Console.WriteLine(ex.Message);
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }
        public static DataTable GetAllContacts()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Select * from Contacts";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            finally 
            { 
                connection.Close();
            }
            return dt;
        }
        public static bool DeleteContact(int ContactID) 
        {
            int rowEffect;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Delete from Contacts where ContactID = @ContactID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactID", ContactID);
            try
            {
                connection.Open();
                rowEffect = command.ExecuteNonQuery();
            }
            catch            
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return (rowEffect > 0);
        }
        public static bool IsContactExist(int ContactID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "Select Found = 1 from Contacts where ContactID = @ContactID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactID", ContactID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsFound = reader.HasRows;
            }
            catch
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }
            return IsFound;
        }
    }

}
