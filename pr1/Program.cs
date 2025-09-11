using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        CultureInfo.CurrentCulture = new CultureInfo("ru-RU");

        int count;
        do
        {
            Console.Write("Введите количество операций (2-40): ");
        } while (!int.TryParse(Console.ReadLine(), out count) || count < 2 || count > 40);

        string[] names = new string[count];
        decimal[] amounts = new decimal[count];

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"\nОперация #{i + 1}:");
            Console.Write("Название товара/услуги: ");
            names[i] = Console.ReadLine();

            Console.Write("Сумма в рублях: ");
            while (!decimal.TryParse(Console.ReadLine(), out amounts[i]))
            {
                Console.Write("Некорректная сумма! Введите снова: ");
            }
        }

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Вывод данных");
            Console.WriteLine("2. Статистика");
            Console.WriteLine("3. Сортировка по цене");
            Console.WriteLine("4. Конвертация валюты");
            Console.WriteLine("5. Поиск по названию");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите пункт: ");

            switch (Console.ReadLine())
            {
                case "1":
                    PrintData(names, amounts);
                    break;
                case "2":
                    ShowStatistics(amounts);
                    break;
                case "3":
                    BubbleSort(names, amounts);
                    break;
                case "4":
                    ConvertCurrency(amounts);
                    break;
                case "5":
                    SearchByName(names, amounts);
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }
        }
    }

    static void PrintData(string[] names, decimal[] amounts)
    {
        Console.WriteLine("\nСписок расходов:");
        for (int i = 0; i < names.Length; i++)
        {
            Console.WriteLine($"{names[i]} - {amounts[i]:C2}");
        }
    }

    static void ShowStatistics(decimal[] amounts)
    {
        if (amounts.Length == 0) return;

        decimal sum = 0, max = amounts[0], min = amounts[0];
        foreach (var amount in amounts)
        {
            sum += amount;
            if (amount > max) max = amount;
            if (amount < min) min = amount;
        }

        Console.WriteLine("\nСтатистика:");
        Console.WriteLine($"Всего потрачено: {sum:C2}");
        Console.WriteLine($"Средняя сумма: {sum / amounts.Length:C2}");
        Console.WriteLine($"Максимальная трата: {max:C2}");
        Console.WriteLine($"Минимальная трата: {min:C2}");
    }

    static void BubbleSort(string[] names, decimal[] amounts)
    {
        for (int i = 0; i < amounts.Length - 1; i++)
        {
            for (int j = 0; j < amounts.Length - i - 1; j++)
            {
                if (amounts[j] > amounts[j + 1])
                {
                    (amounts[j], amounts[j + 1]) = (amounts[j + 1], amounts[j]);

                    (names[j], names[j + 1]) = (names[j + 1], names[j]);
                }
            }
        }
        Console.WriteLine("Сортировка завершена!");
    }

    static void ConvertCurrency(decimal[] amounts)
    {
        Console.WriteLine("\nДоступные валюты:");
        Console.WriteLine("1. USD (0.011)");
        Console.WriteLine("2. EUR (0.010)");
        Console.WriteLine("3. GBP (0.0085)");
        Console.WriteLine("4. JPY (1.64)");
        Console.WriteLine("5. Ввести свой курс");
        Console.Write("Выберите вариант: ");

        decimal rate;
        switch (Console.ReadLine())
        {
            case "1":
                rate = 0.011m;
                break;
            case "2":
                rate = 0.010m;
                break;
            case "3":
                rate = 0.0085m;
                break;
            case "4":
                rate = 1.64m;
                break;
            case "5":
                Console.Write("Введите курс (рубль к валюте): ");
                while (!decimal.TryParse(Console.ReadLine(), out rate))
                {
                    Console.Write("Некорректный курс! Введите снова: ");
                }
                break;
            default:
                Console.WriteLine("Неверный выбор!");
                return;
        }

        Console.WriteLine("\nКонвертированные суммы:");
        foreach (var amount in amounts)
        {
            Console.WriteLine($"{amount * rate:N2}");
        }
    }

    static void SearchByName(string[] names, decimal[] amounts)
    {
        Console.Write("Введите часть названия для поиска: ");
        string search = Console.ReadLine().ToLower();

        bool found = false;
        for (int i = 0; i < names.Length; i++)
        {
            if (names[i].ToLower().Contains(search))
            {
                Console.WriteLine($"{names[i]} - {amounts[i]:C2}");
                found = true;
            }
        }

        if (!found) Console.WriteLine("Совпадений не найдено");
    }
}
