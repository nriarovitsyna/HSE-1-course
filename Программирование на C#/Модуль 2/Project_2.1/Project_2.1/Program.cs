/*
 * Дисциплина: "Программирование на С#"
 * Группа: БПИ244
 * Студент: Яровицына Наталья
 * Дата: 22.11.2024
 * Задача: Программа обрабатывает данные файла в соответствии с запросами пользователя
 * Вариант: 11
 */

namespace Project_2._1;

class Program
{
    static void Main(string[] args)
    {
        ConsoleKeyInfo keyToExit;
        Console.WriteLine($"Добро пожаловать в программу!");
        do
        {
            try
            {
                Console.Write("Введите полный путь файла, обратите внимание, что структура файла должна совпадать со структурой файла Data_Science_Fields_Salary_Categorization.csv: ");
                string path = @"" + Console.ReadLine();
                if (File.Exists(path))
                {
                    FileStructure fs = new FileStructure();
                    string[][] arrayData = fs.CheckFileStructure(path); // Проверяем данные на корректность, данные заносим в массив массивов 
                    Console.Write($"Меню:\n1.Вывести информацию о группах работников по опыту;\n" +
                                    $"2. Вывести сводную статистику;\n" +
                                    $"3. Вывести список работников с зарплатой от 70% до 80% от максимальной;\n" +
                                    $"4. Вывести список работников, сортированных по году;\n" +
                                    $"Выберите пункт меню: ");
                    string choice = Console.ReadLine();
                    string result;

                    switch (choice)
                    {
                        case "1":
                            Console.Write("Введите уровень опыта (например, SE или MI): ");
                            string inputExperience = Console.ReadLine();
                            Information i = new Information(arrayData, inputExperience);
                            result = i.ExperienceGroup;
                            Console.WriteLine($"Результат работы программы:\n{result}");
                            break;
                        case "2":
                            Statistics st = new Statistics(arrayData);
                            result = st.EmployeeStatistics();
                            Console.WriteLine($"Результат работы программы:\n{result}");
                            break;
                        case "3":
                            HighlyPaidEmployee hpe = new HighlyPaidEmployee(arrayData);
                            result = hpe.HighlyPaidWorkers;
                            Console.WriteLine($"Результат работы программы:\n{result}");
                            break;
                        case "4":
                            AdditionalTask at = new AdditionalTask(arrayData);
                            result = at.GroupsForConsole;
                            Console.WriteLine($"Результат работы программы:\n{result}");
                            break;
                        default:
                            Console.WriteLine($"Такого варианта нет.");
                            break;
                    }
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл отсутствует на диске.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Если хотите завершить программу нажмите Escape, иначе нажмите любую другую клавишу.");
                keyToExit = Console.ReadKey();
            }
        } while (keyToExit.Key != ConsoleKey.Escape);
    }
}