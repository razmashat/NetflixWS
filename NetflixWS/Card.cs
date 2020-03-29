using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using NetflixWSDAL;

namespace NetflixWS
{
    public class Card
    {

        private string cardNumber;
        private int owner;
        private int cvv;
        private int expMonth;
        private int expYear;
        private string firstName;
        private string lastName;
        private int balance;


        public Card() 
        { 

        
        
        }

        //21736281231 453 2 22
        public Card(string card) 
        {

            if (CreditCardDAL.IsExist(card))
            {
                DataSet ds = CreditCardDAL.GetCreditCardnByCardNumber(card);
                this.cardNumber = card;
                this.owner = int.Parse(ds.Tables["CreditCardByCardTbl"].Rows[0]["Owner"].ToString());
                this.cvv = int.Parse(ds.Tables["CreditCardByCardTbl"].Rows[0]["CVV"].ToString());
                this.expMonth = int.Parse(ds.Tables["CreditCardByCardTbl"].Rows[0]["ExpMonth"].ToString());
                this.expYear = int.Parse(ds.Tables["CreditCardByCardTbl"].Rows[0]["ExpYear"].ToString());
                this.FirstName = ds.Tables["CreditCardByCardTbl"].Rows[0]["FirstName"].ToString();
                this.lastName = ds.Tables["CreditCardByCardTbl"].Rows[0]["LastName"].ToString();
                this.balance = int.Parse(ds.Tables["CreditCardByCardTbl"].Rows[0]["Balance"].ToString());

            }
            else 
            {
                this.cardNumber = "0";
                this.owner = 0;
                this.cvv = 0;
                this.expMonth = 0;
                this.expYear = 0;
                this.firstName = "0";
                this.lastName = "0";
                this.balance = 0;
            }
        }


        public static bool ValidateCard(Card c)
        {
            if (!CreditCardDAL.IsExist(c.CardNumber))
                return false;
            Card CompareCard = new Card(c.CardNumber);
            if (c.CardNumber != CompareCard.CardNumber || c.owner != CompareCard.owner || c.CVV != CompareCard.CVV || c.ExpMonth != CompareCard.ExpMonth || c.ExpYear != CompareCard.ExpYear || c.FirstName != CompareCard.FirstName || c.LastName != CompareCard.LastName)
                return false;
            c = CompareCard;
            return true;
            
        }
        public static bool CanDoTransation(Card c, Transaction t ) 
        {
            if (!ValidateCard(c) || t.Amount > new Card(c.cardNumber).Balance)
                return false;
            return true;    
        }

        public static bool DoTransaction(Card c, Transaction t) 
        {
            if (!CanDoTransation(c, t))
                return false;
            TransactionDAL.Insert(c.CardNumber, t.Amount, t.Payee);
            CreditCardDAL.UpdateAmount(c.CardNumber,c.Balance - t.Amount);
            return true;
        }

        public static void UpdateName(string fname, string lname,string card) 
        {
            CreditCardDAL.UpdateName(fname,lname,card);
        }

        public string CardNumber { get => cardNumber; set => cardNumber = value; }
        public int Owner { get => owner; set => owner = value; }
        public int CVV { get => cvv; set => cvv = value; }
        public int ExpMonth { get => expMonth; set => expMonth = value; }
        public int ExpYear { get => expYear; set => expYear = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int Balance { get => balance; set => balance = value; }
    }
}