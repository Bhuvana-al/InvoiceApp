namespace InvoiceApp;

public abstract class BaseItem
{
    public abstract double ItemNo {get; set;}
    public abstract string? Description {get; set;}
    public abstract int Quantity {get; set;}
    public abstract decimal UnitPrice {get; set;}

    public abstract decimal AmtExclMoms {get; set;}
    public abstract decimal MomsAmt {get; set;}
    public abstract decimal AmtInclMoms {get; set;}

}
