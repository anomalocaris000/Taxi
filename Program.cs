using Taxi.Models;
using Taxi.Services;

var manager = new TaxiManager();

while (true)
{
    Console.WriteLine();
    Console.WriteLine("Служба такси");
    Console.WriteLine("1. Добавить автомобиль");
    Console.WriteLine("2. Добавить водителя");
    Console.WriteLine("3. Добавить клиента");
    Console.WriteLine("4. Создать заказ");
    Console.WriteLine("5. Просмотреть автомобили");
    Console.WriteLine("6. Просмотреть водителей");
    Console.WriteLine("7. Просмотреть клиентов");
    Console.WriteLine("8. Просмотреть заказы");
    Console.WriteLine("9. Изменить автомобиль");
    Console.WriteLine("10. Изменить водителя");
    Console.WriteLine("11. Изменить клиента");
    Console.WriteLine("12. Изменить статус заказа");
    Console.WriteLine("13. Статистика");
    Console.WriteLine("0. Выход");
    Console.Write("Выберите действие: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Write("Марка автомобиля: ");
            var brand = Console.ReadLine() ?? "";
            Console.Write("Государственный номер: ");
            var number = Console.ReadLine() ?? "";
            Console.Write("Год выпуска: ");
            if (int.TryParse(Console.ReadLine(), out var year))
            {
                manager.AddCar(brand, number, year);
                Console.WriteLine("Автомобиль добавлен.");
            }
            else Console.WriteLine("Некорректный год.");
            break;

        case "2":
            Console.Write("ФИО водителя: ");
            var driverName = Console.ReadLine() ?? "";
            Console.Write("Телефон: ");
            var driverPhone = Console.ReadLine() ?? "";
            manager.AddDriver(driverName, driverPhone);
            Console.WriteLine("Водитель добавлен.");
            break;

        case "3":
            Console.Write("ФИО клиента: ");
            var customerName = Console.ReadLine() ?? "";
            Console.Write("Телефон: ");
            var customerPhone = Console.ReadLine() ?? "";
            manager.AddCustomer(customerName, customerPhone);
            Console.WriteLine("Клиент добавлен.");
            break;

        case "4":
            if (!manager.Customers.Any() || !manager.Cars.Any() || !manager.Drivers.Any())
            {
                Console.WriteLine("Сначала добавьте хотя бы одного клиента, автомобиль и водителя.");
                break;
            }

            Console.WriteLine("Клиенты:");
            foreach (var customer in manager.Customers)
                Console.WriteLine($"{customer.Id}. {customer.FullName}");

            Console.Write("ID клиента: ");
            if (!int.TryParse(Console.ReadLine(), out var cId)) { Console.WriteLine("Некорректный ID."); break; }

            Console.WriteLine("Доступные автомобили:");
            foreach (var car in manager.Cars.Where(x => x.IsAvailable))
                Console.WriteLine($"{car.Id}. {car.Brand}, {car.Number}");

            Console.Write("ID автомобиля: ");
            if (!int.TryParse(Console.ReadLine(), out var carId)) { Console.WriteLine("Некорректный ID."); break; }

            Console.WriteLine("Доступные водители:");
            foreach (var driver in manager.Drivers.Where(x => x.IsAvailable))
                Console.WriteLine($"{driver.Id}. {driver.FullName}");

            Console.Write("ID водителя: ");
            if (!int.TryParse(Console.ReadLine(), out var dId)) { Console.WriteLine("Некорректный ID."); break; }

            Console.Write("Откуда забрать клиента: ");
            var from = Console.ReadLine() ?? "";
            Console.Write("Куда доставить: ");
            var to = Console.ReadLine() ?? "";
            Console.Write("Стоимость поездки: ");
            if (!decimal.TryParse(Console.ReadLine(), out var cost) || cost < 0)
            {
                Console.WriteLine("Некорректная стоимость.");
                break;
            }

            Console.WriteLine(manager.AddOrder(cId, carId, dId, from, to, cost)
                ? "Заказ создан."
                : "Не удалось создать заказ. Проверьте ID и доступность автомобиля и водителя.");
            break;

        case "5":
            foreach (var car in manager.Cars)
                Console.WriteLine($"{car.Id}. {car.Brand}, номер: {car.Number}, год: {car.Year}, доступен: {(car.IsAvailable ? "да" : "нет")}");
            break;

        case "6":
            foreach (var driver in manager.Drivers)
                Console.WriteLine($"{driver.Id}. {driver.FullName}, телефон: {driver.Phone}, доступен: {(driver.IsAvailable ? "да" : "нет")}");
            break;

        case "7":
            foreach (var customer in manager.Customers)
                Console.WriteLine($"{customer.Id}. {customer.FullName}, телефон: {customer.Phone}");
            break;

        case "8":
            foreach (var order in manager.Orders)
                Console.WriteLine(manager.GetOrderDetails(order));
            break;

        case "9":
            Console.Write("ID автомобиля: ");
            if (!int.TryParse(Console.ReadLine(), out var editCarId)) { Console.WriteLine("Некорректный ID."); break; }
            Console.Write("Новая марка: ");
            var newBrand = Console.ReadLine() ?? "";
            Console.Write("Новый номер: ");
            var newNumber = Console.ReadLine() ?? "";
            Console.Write("Новый год выпуска: ");
            if (!int.TryParse(Console.ReadLine(), out var newYear)) { Console.WriteLine("Некорректный год."); break; }
            Console.WriteLine(manager.EditCar(editCarId, newBrand, newNumber, newYear) ? "Автомобиль изменён." : "Автомобиль не найден.");
            break;

        case "10":
            Console.Write("ID водителя: ");
            if (!int.TryParse(Console.ReadLine(), out var editDriverId)) { Console.WriteLine("Некорректный ID."); break; }
            Console.Write("Новое ФИО: ");
            var newDriverName = Console.ReadLine() ?? "";
            Console.Write("Новый телефон: ");
            var newDriverPhone = Console.ReadLine() ?? "";
            Console.WriteLine(manager.EditDriver(editDriverId, newDriverName, newDriverPhone) ? "Водитель изменён." : "Водитель не найден.");
            break;

        case "11":
            Console.Write("ID клиента: ");
            if (!int.TryParse(Console.ReadLine(), out var editCustomerId)) { Console.WriteLine("Некорректный ID."); break; }
            Console.Write("Новое ФИО: ");
            var newCustomerName = Console.ReadLine() ?? "";
            Console.Write("Новый телефон: ");
            var newCustomerPhone = Console.ReadLine() ?? "";
            Console.WriteLine(manager.EditCustomer(editCustomerId, newCustomerName, newCustomerPhone) ? "Клиент изменён." : "Клиент не найден.");
            break;

        case "12":
            foreach (var order in manager.Orders)
                Console.WriteLine(manager.GetOrderDetails(order));
            Console.Write("ID заказа: ");
            if (!int.TryParse(Console.ReadLine(), out var editOrderId)) { Console.WriteLine("Некорректный ID."); break; }
            Console.WriteLine("1. В пути");
            Console.WriteLine("2. Завершён");
            Console.WriteLine("3. Отменён");
            Console.Write("Выберите статус: ");
            var statusChoice = Console.ReadLine();
            var status = statusChoice switch
            {
                "1" => "В пути",
                "2" => "Завершён",
                "3" => "Отменён",
                _ => ""
            };
            Console.WriteLine(status != "" && manager.ChangeOrderStatus(editOrderId, status)
                ? "Статус заказа изменён."
                : "Не удалось изменить статус.");
            break;

        case "13":
            Console.WriteLine($"Всего автомобилей: {manager.Cars.Count}");
            Console.WriteLine($"Доступных автомобилей: {manager.Cars.Count(x => x.IsAvailable)}");
            Console.WriteLine($"Всего водителей: {manager.Drivers.Count}");
            Console.WriteLine($"Свободных водителей: {manager.Drivers.Count(x => x.IsAvailable)}");
            Console.WriteLine($"Всего клиентов: {manager.Customers.Count}");
            Console.WriteLine($"Всего заказов: {manager.Orders.Count}");
            Console.WriteLine($"Активных заказов: {manager.Orders.Count(x => x.Status == "Новый" || x.Status == "В пути")}");
            Console.WriteLine($"Завершённых заказов: {manager.Orders.Count(x => x.Status == "Завершён")}");
            Console.WriteLine($"Общая выручка: {manager.Orders.Where(x => x.Status == "Завершён").Sum(x => x.Cost)} руб.");
            break;

        case "0":
            return;

        default:
            Console.WriteLine("Нет такого пункта меню.");
            break;
    }
}
