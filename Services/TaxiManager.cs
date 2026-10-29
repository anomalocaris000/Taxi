using Taxi.Models;

namespace Taxi.Services;

public class TaxiManager
{
    public List<Car> Cars { get; } = new();
    public List<Driver> Drivers { get; } = new();
    public List<Customer> Customers { get; } = new();
    public List<Customs> Orders { get; } = new();

    private int carId = 1;
    private int driverId = 1;
    private int customerId = 1;
    private int orderId = 1;

    public void AddCar(string brand, string number, int year)
    {
        Cars.Add(new Car { Id = carId++, Brand = brand, Number = number, Year = year });
    }

    public void AddDriver(string name, string phone)
    {
        Drivers.Add(new Driver { Id = driverId++, FullName = name, Phone = phone });
    }

    public void AddCustomer(string name, string phone)
    {
        Customers.Add(new Customer { Id = customerId++, FullName = name, Phone = phone });
    }

    public bool EditCar(int id, string brand, string number, int year)
    {
        var car = Cars.FirstOrDefault(x => x.Id == id);
        if (car == null) return false;
        car.Brand = brand;
        car.Number = number;
        car.Year = year;
        return true;
    }

    public bool EditDriver(int id, string name, string phone)
    {
        var driver = Drivers.FirstOrDefault(x => x.Id == id);
        if (driver == null) return false;
        driver.FullName = name;
        driver.Phone = phone;
        return true;
    }

    public bool EditCustomer(int id, string name, string phone)
    {
        var customer = Customers.FirstOrDefault(x => x.Id == id);
        if (customer == null) return false;
        customer.FullName = name;
        customer.Phone = phone;
        return true;
    }

    public bool AddOrder(int customerId, int carId, int driverId, string from, string to, decimal cost)
    {
        var customer = Customers.FirstOrDefault(x => x.Id == customerId);
        var car = Cars.FirstOrDefault(x => x.Id == carId);
        var driver = Drivers.FirstOrDefault(x => x.Id == driverId);

        if (customer == null || car == null || driver == null || !car.IsAvailable || !driver.IsAvailable || cost < 0)
            return false;

        Orders.Add(new Customs
        {
            Id = orderId++,
            CustomerId = customerId,
            CarId = carId,
            DriverId = driverId,
            From = from,
            To = to,
            Cost = cost
        });

        car.IsAvailable = false;
        driver.IsAvailable = false;
        return true;
    }

    public bool ChangeOrderStatus(int id, string status)
    {
        var order = Orders.FirstOrDefault(x => x.Id == id);
        if (order == null) return false;

        order.Status = status;

        if (status == "Завершён" || status == "Отменён")
        {
            var car = Cars.FirstOrDefault(x => x.Id == order.CarId);
            var driver = Drivers.FirstOrDefault(x => x.Id == order.DriverId);
            if (car != null) car.IsAvailable = true;
            if (driver != null) driver.IsAvailable = true;
        }

        return true;
    }

    public string GetOrderDetails(Customs order)
    {
        var customer = Customers.FirstOrDefault(x => x.Id == order.CustomerId);
        var car = Cars.FirstOrDefault(x => x.Id == order.CarId);
        var driver = Drivers.FirstOrDefault(x => x.Id == order.DriverId);

        return $"Заказ №{order.Id}: {customer?.FullName ?? "Неизвестно"}, " +
               $"{car?.Brand ?? "Неизвестно"} ({car?.Number ?? "нет номера"}), " +
               $"водитель: {driver?.FullName ?? "Неизвестно"}, " +
               $"{order.From} -> {order.To}, стоимость: {order.Cost} руб., статус: {order.Status}";
    }
}
