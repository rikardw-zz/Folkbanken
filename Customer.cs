using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Folkbanken
{
    class Customer
    {
        private int id;
        private string foreName;
        private string lastName;
        private string birthInfo;
        private string streetadress;
        private string postadress;
        private string homephone;
        private string mobilephone;
        private PrivateAccount[] privacc = new PrivateAccount[100];
        private ServiceAccount[] servacc = new ServiceAccount[100];
        private FutureAccount[] futureacct = new FutureAccount[100];         

        public Customer(string tempLastName, string tempSurName, string tempBirthInfo, string tempStreetAdress, string tempPostAdress, string tempHomePhone, string tempMobilePhone)
        { //constructor, sends info to the private ints, encapsulation
            Random rnd = new Random();
            id = rnd.Next(10000, 100000);
            foreName = tempLastName;
            lastName = tempSurName;
            birthInfo = tempBirthInfo;
            streetadress = tempStreetAdress;
            postadress = tempPostAdress;
            homephone = tempHomePhone;
            mobilephone = tempMobilePhone;

        }

        public void OpenAccount(string type) 
        {
            if (type == "Privatkonto")
            {
                PrivateAccount NewAccount = new PrivateAccount();                
            }

            else if (type == "Framtidskonto")
            {
                FutureAccount NewAccount = new FutureAccount();                
            }
            else if (type == "Servicekonto")
            {
                ServiceAccount NewAccount = new ServiceAccount();                
            }
        }

        public string GetLastName() 
        { 
            return lastName; 
        }


        public string GetForeName() 
        { 
            return foreName;        
        }


        public string GetBirthInfo()
        { 
            return birthInfo;
        }


        public string GetStreetAdress() 
        {
            return streetadress;        
        }


        public string GetPostAdress()
        {
            return postadress;
        }


        public string GetHomePhone() 
        {
            
            return homephone;        
        }


        public string GetMobilePhone()
        {
            return mobilephone;
        }

        public int GetId()
        {
            return id;
        }

        public void OpenAccount(int accountType) //Creates/Opens an account
        {
            Account newAccount;    
        }
    } //end class Customer 
}
