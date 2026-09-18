namespace InvoiceApp;

public interface IReceiptMethods
{
    void CalculateAmounts(Items item, out decimal AmtExclMoms, out decimal MomsAmt, out decimal AmtInclMoms);
}
