namespace Support.Models;

public partial class Usage
{
    public int Id { get; }
    public DateTime Date { get; set; }
    public string Product { get; set; }
    public decimal Quantity { get; set; }
    public string UsageCenter { get; set; }
    
    public string VehicleIdentifier { get; set; }
    
}

public partial class Usage
{
    public Usage()
    {
        Date = DateTime.Now;
        Product = string.Empty;
        Quantity = 0;
        UsageCenter = string.Empty;
        VehicleIdentifier = string.Empty;
    }

    public Usage(DateTime date, string product, decimal quantity, string usageCenter, string vehicleIdentifier)
    {
        Date = date;
        Product = product;
        Quantity = quantity;
        UsageCenter = usageCenter;
        VehicleIdentifier = vehicleIdentifier;
    }
}