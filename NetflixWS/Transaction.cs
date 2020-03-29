using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetflixWS
{
    public class Transaction
    {

        private int id;
        private string cardNumber;
        private int amount;
        private string payee;


        public Transaction() 
        { 
        
        }

        public int Id { get => id; set => id = value; }
        public string CardNumber { get => cardNumber; set => cardNumber = value; }
        public int Amount { get => amount; set => amount = value; }
        public string Payee { get => payee; set => payee = value; }
    }
}