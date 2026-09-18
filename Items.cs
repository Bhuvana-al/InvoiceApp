namespace InvoiceApp;

public class Items : BaseItem
{
    public override double ItemNo { get; set; }
    public override string? Description { get; set; }
    public override int Quantity { get; set; }
    public override decimal UnitPrice { get; set; }
    public override decimal AmtExclMoms { get; set; }
    public override decimal MomsAmt { get; set; }
    public override decimal AmtInclMoms { get; set; }
}
