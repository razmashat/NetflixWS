using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace NetflixWS
{
    /// <summary>
    /// Summary description for NetflixWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class NetflixWS : System.Web.Services.WebService
    {

        [WebMethod]
        public bool Pay(Card c, Transaction t)
        {
            return Card.DoTransaction(c, t);
        }

        [WebMethod]
        public bool CanPay(Card c, Transaction t)
        {
            return Card.CanDoTransation(c,t);
        }

        [WebMethod]
        public List<Transaction> Income (string payee)
        {
            return Transaction.TransactionPayee(payee);
        }

        [WebMethod]
        public Transaction UserLatesPay(string payee,string card)
        {
            return Transaction.SpesifycPay(payee,card);
        }

        [WebMethod]
        public Card UserPayment(string card)
        {
            return new Card(card);
        }



        [WebMethod]
        public bool IsPaid(string payee,string card) 
        {
            return Transaction.SpesifycPayExist(payee,card);
        }


    }
}
