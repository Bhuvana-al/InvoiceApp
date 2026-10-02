using System.Numerics;
using System.Reflection;

namespace InvoiceApp;

class Program
{
    
    static Receipts receipt = new ();
    static Customer customer = new ()
    {
        FirstName = "Bhuvaneswari",
        LastName = "Adhiseshan"
    };
    static Address DeliveryAddress = new () 
    {
        AddressLine = "Hisings Backa 6",
        PostalCode = "422 50",
        City = "Gothenburg"
    };
    static Address BillingAddress = new ()
    {
        AddressLine = "Lillhagsparken 66",
        PostalCode = "422 60",
        City = "Gothenburg"        
    };

    static List<Items> items = [];
    static List<Items> orderedItems = [];
    static void Main()
    {
        Console.WriteLine("--------- Invoice App ----------");
        Console.WriteLine("För att se föremål tryck på tangenten 'f'");
        Console.WriteLine("För att beställa föremål tryck på tangenten 'b'");
        Console.WriteLine("För att skriva ut kvitto tryck på tangenten 'k'");
        Console.WriteLine("För att avsluta tryck på tangenten 'x'");

        App();

    }

    static void App()
    {
        try
        {
            ReadItems();
            while(true)
            {
                Console.WriteLine("Vad vill du göra? Tryck på tangenten. ");
                var key = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(key) || key=="x")
                {
                    Environment.Exit(0);
                }
                
                switch (key)
                {
                    case "f":
                        DisplayItems();
                        break;
                    case "b":
                        OrderItems();
                        break;
                    case "k":
                        PrintReceipt();
                        break;
                    case "x":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Ditt val finns inte i menyn");
                        break;
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            App();
        }
    }

    static void ReadItems()
    {
        items.Add (new Items 
        { 
         ItemNo = 3089298,
         Description = "Dewalt Book",
         Quantity = 10,
         UnitPrice = 1336
        });

        items.Add (new Items 
        { 
         ItemNo = 3093357,
         Description = "Dewalt Excenterslip",
         Quantity = 5,
         UnitPrice = 1293.60M
        });
    }
    static void DisplayItems()
    {
        Console.WriteLine("Art.Nr.  Beskrivning     Tillgänglig kvantitet       Å Pris");
        Console.WriteLine("-----------------------------------------------------------------");
        foreach (var item in items)
        {
            Console.WriteLine($"{item.ItemNo}   {item.Description}          {item.Quantity}     {item.UnitPrice}   ");
        }
    }

    static void OrderItems()
    {
        bool ItemExists = false;

        Console.WriteLine("Ange artikelnummer: ");
        var OrderedItemNo = Console.ReadLine();
        
        if (!double.TryParse(OrderedItemNo, out double ItemOrdered))
        {
            throw new Exception("Ogiltig artikel nummer");
        }
        
        Console.WriteLine("Ange kvantitet");
        var OrderQuantity = Console.ReadLine();
        if (!int.TryParse(OrderQuantity, out int QuantityOrdered))
        {
            throw new Exception("Ogiltig kvantitet");
        }

        foreach (var item in items)
        {
            if (item.ItemNo == ItemOrdered)
            {
                ItemExists=true;
                if (ItemExists)
                {
                    if (item.Quantity < QuantityOrdered)
                    {
                        throw new Exception("Tillgänglig kvantitet är otillräcklig.");
                    }
                    orderedItems.Add (new Items 
                        { 
                        ItemNo = ItemOrdered,
                        Quantity = QuantityOrdered,
                        Description = item.Description,
                        UnitPrice = item.UnitPrice});
                    break;
                }
            }
        }
        if (ItemExists == false)
        {
            throw new Exception("Ogiltig artikel nummer");
        }
    }

    static void PrintReceipt()
    {
        
        decimal TotAmtExclMoms = 0;
        decimal TotMoms = 0;
        decimal TotAmtInclMoms = 0;

        Console.WriteLine("-------------- Kvitto -------------");
        Console.WriteLine($"Kvitto Nummer: {receipt.ReceiptNo}");
        Console.WriteLine($"Datum: {receipt.ReceiptDate}");
        Console.WriteLine($"Order Nummer: {receipt.Orderno}");
        Console.WriteLine("LeveransAdress: ");
        Console.WriteLine($"{customer.FirstName} {customer.LastName}");
        Console.WriteLine($"{DeliveryAddress.AddressLine}");
        Console.WriteLine($"{DeliveryAddress.PostalCode}");
        Console.WriteLine($"{DeliveryAddress.City}");
        Console.WriteLine($"FakturaAdress:");
        Console.WriteLine($"{customer.FirstName} {customer.LastName}");
        Console.WriteLine($"{BillingAddress.AddressLine}");
        Console.WriteLine($"{BillingAddress.PostalCode}");
        Console.WriteLine($"{BillingAddress.City}");

        Console.WriteLine("-----------------------------------------------------------------");
        Console.WriteLine("Art.Nr.  Beskrivning  Antal  ÅPris  SummaExclMoms   SummaInclMoms");
        Console.WriteLine("-----------------------------------------------------------------");
        for (var i=0; i<orderedItems.Count; i++)
        {
            receipt.CalculateAmounts(orderedItems[i], out decimal Amt, out decimal MomsValue, out decimal TotAmt);
            orderedItems[i].AmtExclMoms = Amt;
            orderedItems[i].MomsAmt = MomsValue;
            orderedItems[i].AmtInclMoms = TotAmt;

            Console.WriteLine($"{orderedItems[i].ItemNo}, {orderedItems[i].Description}, {orderedItems[i].Quantity}, {orderedItems[i].UnitPrice}, {orderedItems[i].AmtExclMoms}, {orderedItems[i].AmtInclMoms}");
            
            TotAmtExclMoms += orderedItems[i].AmtExclMoms;
            TotMoms += orderedItems[i].MomsAmt;
            TotAmtInclMoms += orderedItems[i].AmtInclMoms;
        }   
        Console.WriteLine("-----------------------------------------------------------------");
        Console.WriteLine($"Belopp exkl. moms: {TotAmtExclMoms}");
        Console.WriteLine($"Moms totalt: {TotMoms}");
        Console.WriteLine($"Summa totalt: {TotAmtInclMoms}");

    }
}
