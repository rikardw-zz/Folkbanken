using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Folkbanken
{
    [Serializable] //enables class to be serializable (for bin files)

    class Customer
    {      
        public string foreName { get; set; }
        public string lastName { get; set; }
        public string birthInfo { get; set; }
        public string streetAdress { get; set; }
        public string postAdress { get; set; }
        public string homePhone { get; set; }
        public string mobilePhone { get; set; }
        public List <PrivateAccount> privateAccounts = new List <PrivateAccount>();
        public List<ServiceAccount> serviceAccounts = new List<ServiceAccount>();
        public List<FutureAccount> futureAccounts = new List<FutureAccount>();

        public override string ToString() //returnerar denna info
        {
            return foreName + " " + lastName;
        }

        /*public Customer(string tempLastName, string tempSurName, string tempBirthInfo, string tempStreetAdress, string tempPostAdress, string tempHomePhone, string tempMobilePhone)
        { //constructor, sends info to the private ints, encapsulation
            Random rnd = new Random();
            foreName = tempLastName;
            lastName = tempSurName;
            birthInfo = tempBirthInfo;
            streetadress = tempStreetAdress;
            postadress = tempPostAdress;
            homephone = tempHomePhone;
            mobilephone = tempMobilePhone;

        }*/
        public void addPrivateAccount(PrivateAccount newAccount) //metod för nytt konto
        {
            //privateAccounts = new List<PrivateAccount>();
            privateAccounts.Add(newAccount);
        }

        public void addServiceAccount(ServiceAccount newAccount) //metod för nytt konto
        {
            serviceAccounts.Add(newAccount);
        }

        public void addFutureAccount(FutureAccount newAccount) //metod för nytt konto
        {
            futureAccounts.Add(newAccount);
        }      
    } //end class Customer 
}
