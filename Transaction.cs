using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Folkbanken
{
    [Serializable]
    class Transaction
    {         
        private string type; // "withdraw" or "deposit"
        private DateTime date;
        private double amount;

        public Transaction(string tempType, double tempAmount, DateTime tempDate)
        {
            type = tempType;
            amount = tempAmount;
            date = tempDate;        
        }  
    }
}
