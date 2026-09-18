namespace InvoiceApp;

public class Receipts : IReceiptProps, IReceiptMethods
{
    const decimal MOMS = 0.25M;
    public double ReceiptNo => 8792104;

    public DateTime ReceiptDate => DateTime.Now;

    public double Orderno => 32441243;

    public PaymentMethodEnum PaymentMethod => PaymentMethodEnum.Kustom;

    public void CalculateAmounts(Items item, out decimal AmtExclMoms, out decimal MomsAmt, out decimal AmtInclMoms)
    {
       AmtExclMoms = item.Quantity * item.UnitPrice; 
       MomsAmt = AmtExclMoms * MOMS;
       AmtInclMoms = AmtExclMoms + MomsAmt;
    }
}
