using System;
using System.Data;
using ContactsBusinessLayer;

namespace ContactsProject
{
    internal class Program
    {
        static void TestFindContact(int ID)
        {
            clsContact Contact1 = clsContact.Find(ID);

            if (Contact1 != null)
            {
                Console.WriteLine(Contact1.FirstName + " " + Contact1.LastName);
                Console.WriteLine(Contact1.Email);
                Console.WriteLine(Contact1.Phone);
                Console.WriteLine(Contact1.Address);
                Console.WriteLine(Contact1.DateOfBirth);
                Console.WriteLine(Contact1.CountryID);
                Console.WriteLine(Contact1.ImagePath);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Contact [" + ID + "] is NOT Found!");
                Console.ReadLine();
            }
        }

        static void TestAddNew()
        {
            clsContact contact1 = new clsContact();
            contact1.FirstName = "Mostafa";
            contact1.LastName = "Mohmead";
            contact1.Email = "Mo@gmail.com";
            contact1.Phone = "07888";
            contact1.Address = "mo-aa";
            contact1.DateOfBirth = DateTime.Now;
            contact1.CountryID = 2;
            contact1.ImagePath = "";

            if (contact1.Save())
            {
                Console.WriteLine("Success");
                Console.ReadLine();
            }
            else Console.WriteLine("Fail");
            Console.ReadLine();
        }

        static void TestUpdateContact(int ContactID)
        {
            clsContact contact1 = clsContact.Find(ContactID);
            if (contact1 != null) 
            { 
            contact1.FirstName = "Mmm";
            contact1.LastName = "bbb";
            contact1.Email = "ss@gmail.com";
            contact1.Phone = "07888";
            contact1.Address = "Mo-alfff";
            contact1.DateOfBirth = DateTime.Now;
            contact1.CountryID= 2;
            contact1.ImagePath = "";
            }
            if (contact1.Save())
            {
                Console.WriteLine("Success!");
                Console.ReadLine();
            }
            else Console.WriteLine("Fail");
            Console.ReadLine();
        }

        static void TestDeleteContact(int ContactID)
        {
            if(clsContact.IsContactExist(ContactID))
            {

                if (clsContact.DeleteContact(ContactID))
                {
                    Console.WriteLine("Success!");
                    Console.ReadLine();
                }
                else Console.WriteLine("Fail to delete");
            }
            else Console.WriteLine("Contact With ID" + ContactID + "Not Found");
            Console.ReadLine();
        }

        static void ListContacts()
        {
            DataTable dt = clsContact.GetAllContacts();
            Console.WriteLine("Coontacts Data: ");
            foreach(DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["ContactID"]}, {row["FirstName"]}, {row["lastName"]}");
            }
            Console.ReadLine();
        }

        static void IsContactExist(int ContactID)
        {

            if (clsContact.IsContactExist(ContactID))
            {
                Console.WriteLine("Exist!");
            }
            else Console.WriteLine("NOT Found");
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            IsContactExist(2);
        }
    }
}
