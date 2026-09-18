using System.ComponentModel.DataAnnotations;

namespace InvoiceApp;

public interface IReceiptProps
{
    double ReceiptNo {get;}
    DateTime ReceiptDate {get;}
    double Orderno {get;}
    PaymentMethodEnum PaymentMethod {get;}
}
