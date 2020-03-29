using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DrivingDAL;


namespace NetflixWSDAL
{
    public class TransactionDAL
    {
        const string TABEL = "TransactionTBL";
        const string filed1 = "ID";
        const string filed2 = "CardNumber";
        const string filed3 = "Amount";
        const string filed4 = "Payee";



        public static string stringbuilder(string s)
        {

            return "\"" + s + "\"";
        }

        public static DataSet GetAll()
        {
            return OleDbHelper.Fill("select * from " + TABEL, TABEL);
        }

        public static DataSet GetTransactionByCardNumber(string card)
        {
            card = stringbuilder(card);
            return OleDbHelper.Fill("select * from " + TABEL + " WHERE " + filed2 + " = " + card, "TransactionsByCardTbl");
        }

            public static void Insert(string card, int amount,string payee)
        {
            payee = stringbuilder(payee);
            card = stringbuilder(card);
             
            
            OleDbHelper.InsertWithAutoNumKey("INSERT INTO " + TABEL + "(" + filed2 + "," + filed3 + ","+ filed4 + ") VALUES (" + card + "," + amount  + ","+payee+")");

            //  OleDbHelper.DoQuery("INSERT INTO" + TABEL+ "(username,pass,MyAdmin,subscriptiontype,email) VALUES (" + username + "," + pass + "," + MyAdmin1 + "," + sub1 + "," + email + ")");
        }

    }
}
