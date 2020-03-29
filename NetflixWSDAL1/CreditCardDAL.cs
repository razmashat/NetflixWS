using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DrivingDAL;

namespace NetflixWSDAL
{
    public class CreditCardDAL
    {

        const string TABEL = "CreditCardTBL";
        const string filed1 = "CardNumber";
        const string filed2 = "Owner";
        const string filed3 = "CVV";
        const string filed4 = "ExpMonth";
        const string filed5 = "ExpYear";
        const string filed6 = "FirstName";
        const string filed7 = "LastName";
        const string filed8 = "Balance";



        public static string stringbuilder(string s)
        {

            return "\"" + s + "\"";
        }


        public static DataSet GetAll()
        {
            return OleDbHelper.Fill("select * from " + TABEL, TABEL);
        }


        public static DataSet GetCreditCardnByCardNumber(string card)
        {
            card = stringbuilder(card);
            return OleDbHelper.Fill("select * from " + TABEL + " WHERE " + filed1 + " = " + card, "CreditCardByCardTbl");
        }


        public static bool IsExist(string card)
        {

            DataSet ds = GetCreditCardnByCardNumber(card);
            return (ds.Tables["adminByIdTbl"].Rows.Count > 0);


        }
    }
}
