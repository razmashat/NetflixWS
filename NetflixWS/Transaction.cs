using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using NetflixWSDAL;


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


        public Transaction(DataRow row)
        {
            this.id = int.Parse(row["ID"].ToString());
            this.cardNumber = row["CardNumber"].ToString();
            this.amount = int.Parse(row["Amount"].ToString());
            this.payee = row["Payee"].ToString();
        }


        public static List<Transaction> TransactionPayee(string Payee)
        {
            List<Transaction> AllTrans = new List<Transaction>();
            DataSet ds = TransactionDAL.GetTransactionByPayee(Payee);
            for (int i = 0; i < ds.Tables["TransactionsByPayeeTbl"].Rows.Count; i++)
                 AllTrans.Add(new Transaction(ds.Tables["TransactionsByPayeeTbl"].Rows[i]));
            return AllTrans;
        }

        public static Transaction SpesifycPay(string payee,string card) 
        {
            int id = 0;
            Transaction t = new Transaction();
            DataSet ds = TransactionDAL.GetTransactionByCardNumber(card);
            for (int i = 0; i < ds.Tables["TransactionsByCardTbl"].Rows.Count; i++)
            {
                if (ds.Tables["TransactionsByCardTbl"].Rows[i]["Payee"].ToString() == payee && int.Parse(ds.Tables["TransactionsByCardTbl"].Rows[i]["ID"].ToString()) > id)
                {
                    t = new Transaction(ds.Tables["TransactionsByCardTbl"].Rows[i]);
                }
            }
            return t;
        }

        public int ID { get => id; set => id = value; }
        public string CardNumber { get => cardNumber; set => cardNumber = value; }
        public int Amount { get => amount; set => amount = value; }
        public string Payee { get => payee; set => payee = value; }
    }
}