using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Folkbanken
{
    [Serializable]
    class Card
    {

        private int cardNumber;
        private int CVCCode;
        private int PINCode;
        private DateTime expirationDate;
               

        
        public Card(int tempCardNumber, int tempCVCCode, int tempPINCode) //constructor
        {                                                                     

            cardNumber = tempCardNumber;
            CVCCode = tempCVCCode;
            PINCode = tempPINCode;            
        }

        public int GetCardNumber() //functions for constructor
        { 
            return cardNumber; 
        }

        public int GetCVCNumber()
        { 
            return CVCCode; 
        }

        public int GetPINNumber() 
        { 
            return PINCode; 
        }

    }
    
}
