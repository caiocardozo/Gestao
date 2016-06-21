using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Resources;

namespace PagSeguro
{
    public class Pagamento
    {
        public void Pay()
        {
            
            const bool isSandbox = true;

           // EnvironmentConfiguration.ChangeEnvironment(isSandbox);


            var credentials = PagSeguroConfiguration.Credentials(isSandbox);

            // Instanciar uma nova requisição de pagamento
            var payment = new PaymentRequest { Currency = Currency.Brl };

            // Adicionar produtos
            payment.Items.Add(new Item("0001", "Orçamento", 1, 20.00m));

            // Código que identifica o pagamento
            payment.Reference = "REF1234";
            
            //// Informações de entrega
            //payment.Shipping = new Shipping
            //{
            //    ShippingType = ShippingType.Sedex,
            //    Cost = 10.00m,
            //    Address = new Address(
            //        "BRA",
            //        "SP",
            //        "Sao Paulo",
            //        "Jardim Paulistano",
            //        "01452002",
            //        "Av. Brig. Faria Lima",
            //        "1384",
            //        "5o andar"
            //        )
            //};

            // Informações do remetente
            payment.Sender = new Sender(
                "Joao Comprador",
                "wagnogueira19@gmail.com",
                new Phone("11", "56273440")
            );

            // URL a redirecionar o usuário após pagamento
            payment.RedirectUri = new Uri("http://www.agilizaorcamento.com.br");

            // Informações extras para identificar o pagamento.
            // Essas informações são livres para adicionar o que for necessário.
            //payment.AddMetaData(MetaDataItemKeys.GetItemKeyByDescription("CPF do passageiro"), "123.456.789-09", 1);
            //payment.AddMetaData("PASSENGER_PASSPORT", "23456", 1);

            //// Outra forma de definir os parâmetros de pagamento.
            //payment.AddParameter("senderBirthday", "07/05/1980");
            //payment.AddIndexedParameter("itemColor", "verde", 1);
            //payment.AddIndexedParameter("itemId", "0003", 3);
            //payment.AddIndexedParameter("itemDescription", "Mouse", 3);
            //payment.AddIndexedParameter("itemQuantity", "1", 3);
            //payment.AddIndexedParameter("itemAmount", "200.00", 3);

            //var senderCpf = new SenderDocument(Documents.GetDocumentByType("CPF"), "03078690164");
            //payment.Sender.Documents.Add(senderCpf);
     

            var paymentRedirectUri = payment.Register(credentials);

        }
    }
}
