using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Tortik;

namespace Tortik
{
    internal class ElementsofMenu
    {

        public ElementsofMenu(int el, string discription, int price, Action action)
        {
            this.EL = el;
            this.Discription = discription;
            this.Price = price;
            this.Action = action;

        }

        public int EL { get; set; }
        public string Discription { get; set; }
        public int Price { get; set; }
        public Action Action { get; set; }


    }
    public static class Global
    {
        public static int Price = 0;
        public static List<string> Order = new List<string> { };
    }
}
public class Menu
    {
        public Menu PlayMenu { get; set; }
        public string Zagolovok { get; set; }
        public string Arrow { get; set; }
        public ConsoleColor ArrowColour { get; set; }
        public ConsoleColor ForegroundColour { get; set; }
        public ConsoleColor MenuItemColour { get; set; }
        public ConsoleColor ZagolovokColour { get; set; }

        public int AllPrice { get; private set; } = 0;

        private List<ElementsofMenu> ItemList;
        public int[] prices = new int[] { };

        private int position;
        private bool Exit;
        public int Cena;

        public Menu(string arrow = "->")
        {
            ItemList = new List<ElementsofMenu>();
            position = 0;

            this.Arrow = arrow;
            ArrowColour = ConsoleColor.Magenta;
            ForegroundColour = ConsoleColor.White;
            MenuItemColour = ConsoleColor.White;
            ZagolovokColour = ConsoleColor.White;
        }
        public void Draw()
        {
            Console.Clear();
            Console.WriteLine(Zagolovok);

            for (int i = 0; i < ItemList.Count; i++)
            {
                if (i == position)
                {
                    Console.ForegroundColor = ArrowColour;
                    Console.Write(Arrow + " ");
                    Console.WriteLine(ItemList[i].Discription);
                    Console.ForegroundColor = ForegroundColour;
                }
                else
                {
                    Console.Write(new string(' ', (Arrow.Length + 0)));
                    Console.WriteLine(ItemList[i].Discription);
                }
            }
            Console.WriteLine("Цена:" + Global.Price);
            Console.WriteLine("Заказ: ");
            foreach (string item in Global.Order)
            {
                Console.WriteLine(item);
            }
        }
        public void ShowMenu()
        {
            Console.CursorVisible = false;
            Console.Clear();
            Draw();
            Exit = false;
            while (!Exit)
            {
                MenuUpdate();

            }
        }

        public void HideMenu()
        {

            Exit = true;
        }

        public void MenuUpdate()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Enter:
                    {
                        Console.Clear();
                        Console.CursorVisible = true;
                        ItemList[position].Action();
                        Global.Price += ItemList[position].Price;
                        Global.Order.Add(ItemList[position].Discription);
                        Console.CursorVisible = false;
                        Console.Clear();
                        Draw();
                        break;
                    }
                case ConsoleKey.Escape:
                    {
                        if (PlayMenu != null)
                        {
                            HideMenu();
                        }
                        break;
                    }
                case ConsoleKey.UpArrow:
                    {
                        position--;
                        if (position < 0)
                        {
                            position++;
                            Console.Clear();
                            Draw();

                        }
                        break;
                    }
                case ConsoleKey.DownArrow:
                    {

                        if (position < (ItemList.Count - 1))
                        {
                            position++;
                            Console.Clear();
                            Draw();

                        }
                        break;
                    }
            }
        }

        public bool AddItem(int el, string discription, int price, Action action)
        {
            if (!ItemList.Any(item => item.EL == el))
            {

                ItemList.Add(new ElementsofMenu(el, discription, price, action));

                return true;
            }
            return false;
        }
    }
internal class Program
{
        static void Main()
        {
            Menu mainMenu = new Menu();
            Menu FormaTorta = new Menu("~>");
            FormaTorta.Zagolovok = "Форма торта";
            FormaTorta.AddItem(0, "Квадрат - 400", 400, FormaTorta.HideMenu);
            FormaTorta.AddItem(1, "Круг - 500", 500, FormaTorta.HideMenu);
            FormaTorta.AddItem(2, "Сердечко - 700", 700, FormaTorta.HideMenu);
            FormaTorta.AddItem(3, "Котик - 1000", 1000, FormaTorta.HideMenu);
            FormaTorta.PlayMenu = mainMenu;



            Menu SizeTorta = new Menu("~>");
            SizeTorta.Zagolovok = "Размер тортика";
            SizeTorta.AddItem(0, "Обычный(7-8 порций) - 1300", 1300, SizeTorta.HideMenu);
            SizeTorta.AddItem(1, "Бенто-Тортик(1-2 порций) - 800", 800, SizeTorta.HideMenu);
            SizeTorta.AddItem(2, "Средний(10-12 порций) - 1800", 1800, SizeTorta.HideMenu);
            SizeTorta.AddItem(3, "Большой(18-20 порций) - 2500", 2500, SizeTorta.HideMenu);



            SizeTorta.PlayMenu = mainMenu;

            Menu TasteTorta = new Menu("~>");
            TasteTorta.Zagolovok = "Вкус коржей";
            TasteTorta.AddItem(0, "Шоколадные - 300", 300, TasteTorta.HideMenu);
            TasteTorta.AddItem(1, "Ванильные - 350", 350, TasteTorta.HideMenu);
            TasteTorta.AddItem(2, "Шоколадно-ванильеые - 500", 500, TasteTorta.HideMenu);
            TasteTorta.AddItem(3, "Кокосовые - 400", 400, TasteTorta.HideMenu);
            TasteTorta.prices = new int[] { 300, 350, 500, 400 };
            TasteTorta.PlayMenu = mainMenu;

            Menu InsideTorta = new Menu("~>");
            InsideTorta.Zagolovok = "Начинка тортика";
            InsideTorta.AddItem(0, "Шоколадная - 250", 250, InsideTorta.HideMenu);
            InsideTorta.AddItem(1, "Клубничная - 300", 300, InsideTorta.HideMenu);
            InsideTorta.AddItem(2, "Красный бархат - 400", 400, InsideTorta.HideMenu);
            InsideTorta.AddItem(3, "Манго-маракуйя - 500", 500, InsideTorta.HideMenu);
            InsideTorta.prices = new int[] { 250, 300, 400, 500 };


            InsideTorta.PlayMenu = mainMenu;

            Menu CountYarys = new Menu("~>");
            CountYarys.Zagolovok = "Количество ярусов";
            CountYarys.AddItem(0, "Один ярус - 500", 500, CountYarys.HideMenu);
            CountYarys.AddItem(1, "Два яруса - 800", 800, CountYarys.HideMenu);
            CountYarys.AddItem(2, "Три яруса - 1200", 1200, CountYarys.HideMenu);

            CountYarys.PlayMenu = mainMenu;

            Menu Dekor = new Menu("~>");
            Dekor.Zagolovok = "Декор";
            Dekor.AddItem(0, "Ягодный микс - 500", 500, Dekor.HideMenu);
            Dekor.AddItem(1, "Фигурка из мастики - 300", 300, Dekor.HideMenu);
            Dekor.AddItem(2, "Сладости - 400", 400, Dekor.HideMenu);
            Dekor.AddItem(3, "Без декора", 0, Dekor.HideMenu);

            Dekor.PlayMenu = mainMenu;

            mainMenu.Zagolovok = "  Мяу-кафе с вкусными тортиками <3";
            mainMenu.AddItem(0, "Форма Тортика", 0, FormaTorta.ShowMenu);
            mainMenu.AddItem(1, "Размер Тортика", 0, SizeTorta.ShowMenu);
            mainMenu.AddItem(2, "Вкус Коржей", 0, TasteTorta.ShowMenu);
            mainMenu.AddItem(3, "Начинка тортика", 0, InsideTorta.ShowMenu);
            mainMenu.AddItem(4, "Количество ярусов", 0, CountYarys.ShowMenu);
            mainMenu.AddItem(5, "Дополнительный декор", 0, Dekor.ShowMenu);
            mainMenu.AddItem(6, "Конец оформления заказа", 0, Exit);

            mainMenu.ShowMenu();

        }

    private static void Exit()
    {
        Console.WriteLine("Ваш заказ оформлен!");

        string path = "C:\\Users\\rhali\\Desktop\\История заказов.txt";
        string text = "Описание заказа";
        foreach (string item in Global.Order) { Console.WriteLine(item); };
        string price = "Цена: " + Global.Price;
        if (File.Exists(path))
        {

            File.AppendAllText(path, text);
            File.AppendAllText(path, price);
        }
        else
        {
            File.Create(path);
        }
        Environment.Exit(0);
    }

}
