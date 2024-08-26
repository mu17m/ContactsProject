using System;
using System.Data;
using ContactsDataAccessLayer;
using ContactData;

namespace Contact
{
    public class clsCountry
    {
        enum enMode {AddCountry=1, Update=2};
        enMode Mode;
        public int ID { get; set; }
        public string CountryName { get; set; }
            
        public clsCountry()
        {
            this.ID = -1;
            this.CountryName = "";
            Mode = enMode.AddCountry;
        }

        private clsCountry(int ID,  string CountryName)
        {
            this.ID = ID;
            this.CountryName = CountryName;
            Mode = enMode.Update;
        }

        private bool _AddNewCountry(string CountryName)
        {
            this.ID = clsCountryData.AddNewCountry(CountryName);
            return (this.ID != -1);
        }

        private bool _UpdateCountry(int ID, string CountryName)
        {
            return clsCountryData.UpdateCountry(ID, CountryName);
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddCountry:
                if(_AddNewCountry(this.CountryName))
                    {
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateCountry(this.ID, this.CountryName);

                default : return false;
            }
        }









           
        
    }
}
