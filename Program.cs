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
            clsContact contact = new clsContact();

        }
        static void Main(string[] args)
        {
            TestFindContact(5);
        }
    }
}
