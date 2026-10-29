namespace Taxi.Models;

public class Customs
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int CarId { get; set; }
    public int DriverId { get; set; }
    public string From { get; set; } = "";
    public string To { get; set; } = "";
    public decimal Cost { get; set; }
    public string Status { get; set; } = "Новый";
}
