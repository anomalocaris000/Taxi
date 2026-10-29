namespace Taxi.Models;

public class Car
{
    public int Id { get; set; }
    public string Brand { get; set; } = "";
    public string Number { get; set; } = "";
    public int Year { get; set; }
    public bool IsAvailable { get; set; } = true;
}
