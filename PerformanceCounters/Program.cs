using System.Diagnostics;

PerformanceCounter cpucounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
PerformanceCounter ramcounter = new PerformanceCounter("Память", "Доступно МБ");
PerformanceCounter hddcounter = new PerformanceCounter("Логический диск", "Свободно мегабайт", "_Total");
PerformanceCounter dotnetcounter = new PerformanceCounter("Приложения ASP.NET", "Общее число ошибок", "__Total__");
PerformanceCounter networkcounter = new PerformanceCounter("Сетевой адаптер", "Всего байт/с", "Realtek PCIe FE Family Controller");

Console.WriteLine(cpucounter.NextValue());
Console.WriteLine(ramcounter.NextValue());
Console.WriteLine(hddcounter.NextValue());
Console.WriteLine(dotnetcounter.NextValue());
Console.WriteLine(networkcounter.NextValue());



var categoryes = PerformanceCounterCategory.GetCategories();

foreach (var category in categoryes)
{
    Console.WriteLine(category.CategoryName);
}
