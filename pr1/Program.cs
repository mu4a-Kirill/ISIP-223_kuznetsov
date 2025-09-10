using System;

namespace DailyExpenses
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Учёт дневных расходов");

            int count;
            do
            {
                Console.Write("Введите количество операций (2-40): ");
            } while (!int.TryParse(Console.ReadLine(), out count) || count < 2 || count > 40);

            string[] names = new string[count];
            decimal[] amounts = new decimal[count];

            for (int i = 0; i < count; i++)
            {
                while (true)
                {
                    Console.Write($"Введите операцию {i + 1} (Название; Сумма): ");
                    string[] input = Console.ReadLine().Split(';');

                    if (input.Length == 2 &&
                        decimal.TryParse(input[1].Trim(), out decimal amount))
                    {
                        names[i] = input[0].Trim();
                        amounts[i] = amount;
                        break;
                    }
                    Console.WriteLine("Ошибка формата! Повторите ввод.");
                }
            }

            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Вывод данных");
                Console.WriteLine("2. Статистика");
                Console.WriteLine("3. Сортировка по цене");
                Console.WriteLine("4. Конвертация валюты");
                Console.WriteLine("5. Поиск по названию");
                Console.WriteLine("0. Выход");

                Console.Write("Выберите пункт: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PrintData(names, amounts);
                        break;
                    case "2":
                        ShowStatistics(amounts);
                        break;
                    case "3":
                        BubbleSort(names, amounts);
                        Console.WriteLine("Данные отсортированы!");
                        break;
                    case "4":
                        ConvertCurrency(amounts);
                        break;
                    case "5":
                        SearchByName(names, amounts);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }

        static void PrintData(string[] names, decimal[] amounts)
        {
            Console.WriteLine("\n{0,-40} {1}", "Название", "Сумма");
            Console.WriteLine(new string('-', 50));
            for (int i = 0; i < names.Length; i++)
            {
                Console.WriteLine("{0,-40} {1:C2}", names[i], amounts[i]);
            }
        }

        static void ShowStatistics(decimal[] amounts)
        {
            decimal sum = 0, max = amounts[0], min = amounts[0];
            foreach (decimal amount in amounts)
            {
                sum += amount;
                if (amount > max) max = amount;
                if (amount < min) min = amount;
            }

            Console.WriteLine("\nСтатистика:");
            Console.WriteLine($"Сумма: {sum:C2}");
            Console.WriteLine($"Среднее: {sum / amounts.Length:C2}");
            Console.WriteLine($"Максимум: {max:C2}");
            Console.WriteLine($"Минимум: {min:C2}");
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
        }

        static void ConvertCurrency(decimal[] amounts)
        {
            Console.Write("Введите курс конвертации (рублей за 1 единицу валюты): ");
            if (decimal.TryParse(Console.ReadLine(), out decimal rate) && rate > 0)
            {
                Console.WriteLine("\nКонвертированные суммы:");
                foreach (decimal amount in amounts)
                {
                    Console.WriteLine($"{amount / rate:N2}");
                }
            }
            else
            {
                Console.WriteLine("Некорректный курс!");
            }
        }

        static void SearchByName(string[] names, decimal[] amounts)
        {
            Console.Write("Введите часть названия для поиска: ");
            string search = Console.ReadLine().ToLower();

            Console.WriteLine("\nРезультаты поиска:");
            bool found = false;

            for (int i = 0; i < names.Length; i++)
            {
                if (names[i].ToLower().Contains(search))
                {
                    Console.WriteLine($"{names[i],-40} {amounts[i]:C2}");
                    found = true;
                }
            }

            if (!found) Console.WriteLine("Ничего не найдено!");
        }
    }
}
