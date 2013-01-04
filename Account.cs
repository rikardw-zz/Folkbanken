﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Folkbanken
{
    class Account
    {
        private int accountNumber;
        private double money;

        public Account() //constructor (?)
        {
            Random rnd = new Random();
            accountNumber = rnd.Next(10000, 100000);
            money = 0;
        }

        public double GetCurrentMoney()
        {
            return money;
        }

        public void SetCurrentMoney(double tempMoney) 
        {
            money = tempMoney;                   
        }

        public double GetAccountNumber()
        {
            return accountNumber;
        }

    }


    class PrivateAccount : Account
    {

        private Card[] cards = new Card[100]; // allows PrivateAccount to have a large number of cards
        private double creditLimit; //sets a max credit to PrivateUser
        private double interest = 1.0340;
       
        public double GetMaxCredit()
        {
            return creditLimit;
        }

        public bool SurpassingCredit()
        { //creates method to find out if Max credit is passed or not
            //returns false or true
            return GetCurrentMoney() < creditLimit; // if money is more than creditlimit = return True

        }

        public void SetCredit(double tempCreditLimit) //sets the credit limit
        {
            creditLimit = tempCreditLimit;                
        }        
    }   

    class FutureAccount : Account
        {
            
            private double interest = 1.0440;                
            DateTime latestTransaction;


            public bool CheckTransactionFee() //function to see if the yearly transaction surpasses 
            {
                bool chargeFee;
                                
                if (latestTransaction > DateTime.Now.AddYears(-1))
                {
                    chargeFee = true;//only one transaction per year      
                }
                else
                {
                    chargeFee = false;
                }
                return chargeFee;//sends data back if transaction is OK or not.
            }



            public void TransactionFee() { //creates a method to draw the fee 50 kr
                
                double  tempMoney;

                if (CheckTransactionFee() == true)
                {
                    tempMoney = GetCurrentMoney() - 50;
                    SetCurrentMoney(tempMoney);
                }            
            }
                                   
            
        public bool SurpassingCredit()            
                {
                bool awnser;
                if (GetCurrentMoney() <= 0)
                {
                    awnser = true;
                }
                else
                {
                    awnser = false;
                }
                return awnser; // disables having credit
            }
        }

        class ServiceAccount : Account
        {
            private Card[] cards = new Card[100]; //allows 0 to several cards
            double interest = 1.0325;

            public bool SurpassingCredit()
            {
                bool awnser;
                if (GetCurrentMoney() <= 0)
                {
                    awnser = true;
                }
                else
                {
                    awnser = false;
                }
                return awnser; // method fo disabling credit
            
            }                  
        }
}


    


    
    
        


