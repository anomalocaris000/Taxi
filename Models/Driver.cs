namespace Taxi.Models;

public class Driver
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Phone { get; set; } = "";
    public bool IsAvailable { get; set; } = true;
}
