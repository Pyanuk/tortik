using System;
using System.Collections.Generic;
using System.IO;

class Progra
{
    static void Main(string[] args)
    {
        bool exit = false;
        List<CakeOrder> orders = new List<CakeOrder>();

        while (!exit)
        {
            // Главное меню выбора торта
            int cakeShape = ConsoleMenu.DisplayMenu("Выберите форму торта:", new string[] { "Круглый", "Квадратный", "Прямоугольный" });
            int cakeSize = ConsoleMenu.DisplayMenu("Выберите размер торта:", new string[] { "Маленький", "Средний", "Большой" });
            int cakeFlavor = ConsoleMenu.DisplayMenu("Выберите вкус торта:", new string[] { "Шоколадный", "Ванильный", "Фруктовый" });
            int cakeQuantity = ConsoleMenu.DisplayMenu("Выберите количество тортов:", new string[] { "1", "2", "3" });

            // Меню выбора глазури
            int frostingType = ConsoleMenu.DisplayMenu("Выберите глазурь:", new string[] { "Шоколадная", "Сливочная", "Карамельная" });

            // Меню выбора декора
            int decorType = ConsoleMenu.DisplayMenu("Выберите декор:", new string[] { "Цветные конфетки", "Шоколадные стружки", "Фрукты" });

            // Создание объекта заказа торта и добавление в список заказов
            CakeOrder order = new CakeOrder((CakeShape)cakeShape, (CakeSize)cakeSize, (CakeFlavor)cakeFlavor, (CakeQuantity)cakeQuantity, (FrostingType)frostingType, (DecorType)decorType);
            orders.Add(order);

            Console.WriteLine("Заказ принят!");

            // Подсчет суммарной цены и вывод информации о заказе
            decimal totalCost = 0;
            Console.WriteLine("Информация о заказе:");
            foreach (CakeOrder cakeOrder in orders)
            {
                Console.WriteLine(cakeOrder.ToString());
                totalCost += cakeOrder.GetTotalCost();
            }
            Console.WriteLine("Суммарная цена заказа: $" + totalCost);

            // Сохранение заказа в файл
            SaveOrderToFile(orders);

            // Проверка продолжения оформления заказа
            Console.WriteLine("Желаете оформить еще один заказ? (Y/N)");
            string option = Console.ReadLine();
            exit = (option.ToLower() != "y");
        }
    }

    static void SaveOrderToFile(List<CakeOrder> orders)
    {
        decimal totalCost = 0;
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, "Инфо_тортика.txt");

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Информация о заказе:");
            foreach (CakeOrder cakeOrder in orders)
            {
                writer.WriteLine(cakeOrder.ToString());
            }
            writer.WriteLine("Суммарная стоимость заказа: $" + totalCost);
        }
    }
}

class ConsoleMenu
{
    public static int DisplayMenu(string headerText, string[] options)
    {
        Console.Clear();
        Console.WriteLine(headerText);
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }
        Console.WriteLine("Выберите номер пункта меню и нажмите Enter:");

        ConsoleKeyInfo key;
        int selectedIndex = 0;

        do
        {
            key = Console.ReadKey();

            if (key.Key == ConsoleKey.UpArrow)
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = options.Length - 1;
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                selectedIndex++;
                if (selectedIndex >= options.Length)
                    selectedIndex = 0;
            }
        } while (key.Key != ConsoleKey.Enter);

        Console.WriteLine();

        return selectedIndex;
    }
}

enum CakeShape
{
    Круглый,
    Квадратный,
    Прямоугольный
}

enum CakeSize
{
    Маленький,
    Средний,
    Большой
}

enum CakeFlavor
{
    Шоколадный,
    Ванильный,
    Фруктовый
}

enum CakeQuantity
{
    Один = 1,
    Два,
    Три
}

enum FrostingType
{
    Шоколадная,
    Сливочная,
    Карамельная
}

enum DecorType
{
    Цветные_конфетки,
    Шоколадные_стружки,
    Фрукты
}

class CakeOrder
{
    private CakeShape shape;
    private CakeSize size;
    private CakeFlavor flavor;
    private CakeQuantity quantity;
    private FrostingType frostingType;
    private DecorType decorType;

    public CakeOrder(CakeShape shape, CakeSize size, CakeFlavor flavor, CakeQuantity quantity, FrostingType frostingType, DecorType decorType)
    {
        this.shape = shape;
        this.size = size;
        this.flavor = flavor;
        this.quantity = quantity;
        this.frostingType = frostingType;
        this.decorType = decorType;
    }

    public decimal GetTotalCost()
    {
        decimal basePrice = 10;
        decimal pricePerCake = 5;
        decimal pricePerFrosting = 2;
        decimal pricePerDecor = 3;

        decimal totalCost = basePrice + ((int)quantity - 1) * pricePerCake + pricePerFrosting + pricePerDecor;

        return totalCost;
    }

    public override string ToString()
    {
        return $"Торт: {shape}, {size}, {flavor}, Количество: {quantity}, Глазурь: {frostingType}, Декор: {decorType}";
    }


}





