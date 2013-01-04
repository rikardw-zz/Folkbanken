using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Folkbanken
{
    class Transaction
    { 
        private Account theAccount;

        public void AddorReduceMoney()
        { //Method to put in or withdraw money **********************************************

        }

        public string DateGet() { //Method. Returns current time with DateGet

            DateTime date1 = DateTime.Now;
            string dateInfo = Convert.ToString(date1); 
            return dateInfo;  

        } 

        public void SavedMoney() //shows saved money
        {
            theAccount.GetCurrentMoney();      
        }

        public double MinusMoney(double moneyReduce) //Reduces money - Ta ut pengar
        {
            
            moneyReduce =- theAccount.GetCurrentMoney();
            DateGet();
            return moneyReduce;

        
        }

        public double PlusMoney(double moneyIn)   //Adds money - Lägger till pengar
        {
            moneyIn =+ theAccount.GetCurrentMoney();
            DateGet();
            return moneyIn;
        
        }
    }
}
