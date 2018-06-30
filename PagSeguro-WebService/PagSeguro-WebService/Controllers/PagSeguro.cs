using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace PagSeguro_WebService.Controllers
{
    public class PagSeguro
    {

        [HttpPost]
        public string CheckOut(string nome, string itens, string valor, string quantidade, string telefone, string codigo)
        {
            string url = @"https://ws.sandbox.pagseguro.uol.com.br/v2/checkout";
            System.Collections.Specialized.NameValueCollection postData = new System.Collections.Specialized.NameValueCollection();
            postData.Add("email", "patrickviannapblv@gmail.com");
            postData.Add("token", "E0917524D91D47DBAF72196114D0E543");
            postData.Add("currency", "BRL");
            postData.Add("itemId1", "0001");
            postData.Add("itemDescription1", itens);
            postData.Add("itemAmount1", valor + ".00");
            postData.Add("itemQuantity1", quantidade);
            postData.Add("itemWeight1", "200");
            postData.Add("reference", codigo);
            postData.Add("senderName", "Vendedor 1");
            postData.Add("senderAreaCode", "44");
            postData.Add("senderPhone", "999999999");
            postData.Add("senderEmail", "v04139650041793846132@sandbox.pagseguro.com.br");
            postData.Add("shippingAddressRequired", "false");

            string xmlString = null;

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                var result = wc.UploadValues(url, postData);

                xmlString = Encoding.ASCII.GetString(result);
            }

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            var code = xmlDoc.GetElementsByTagName("code")[0];

            var date = xmlDoc.GetElementsByTagName("date")[0];

            var paymentUrl = string.Concat("https://sandbox.pagseguro.uol.com.br/v2/checkout/payment.html?code=", code.InnerText);
            return paymentUrl;//Json(paymentUrl, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string ConsultarTransacao(int refe)
        {
            string uri = "https://ws.sandbox.pagseguro.uol.com.br/v2/transactions/?email=studyspaceuvv%40gmail.com&token=3FE5271F0FE04039942AC2B9168FB154&reference=" + refe;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = "GET";
            string xmlString = null;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        xmlString = reader.ReadToEnd();
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(xmlString);
                        var codigo = xmlDoc.GetElementsByTagName("code")[0];
                        reader.Close();
                        dataStream.Close();
                        var resposta = codigo.InnerText.ToString();
                        return resposta;
                    }
                }
            }
                return null;
        }

        [HttpPost]
        public bool Estornar(string code)
        {
            string uri = "https://ws.sandbox.pagseguro.uol.com.br/v2/transactions/refunds?email=studyspaceuvv%40gmail.com&token=3FE5271F0FE04039942AC2B9168FB154&transactionCode=" + code;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Method = "POST";
            string xmlString = null;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        xmlString = reader.ReadToEnd();
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(xmlString);
                        reader.Close();
                        dataStream.Close();
                        return true;
                    }
                }
            }
        }
    }
}