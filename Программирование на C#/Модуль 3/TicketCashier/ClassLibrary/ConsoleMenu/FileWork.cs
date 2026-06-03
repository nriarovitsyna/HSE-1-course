using Spectre.Console;
namespace ClassLibrary.ConsoleMenu
{
    public static class FileWork
    {
        /// <summary>
        /// Метод получает список мероприятий из файла 
        /// </summary>
        /// <returns></returns>
        public static ListEvents GetDataFromFile()
        {
            Console.Clear();
            while (true)
            {
                Console.Write("Введите путь к файлу: ");
                var filePath = Console.ReadLine();
                
                try
                {
                    var events = ReadFile(filePath!);
                    Console.WriteLine("Данные записаны!");
                    return events;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Ошибка: Файл не найден. Повторите ввод.");
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Ошибка: Нет доступа к файлу. Повторите ввод.");
                }
                catch (IOException)
                {
                    Console.WriteLine("Ошибка: Произошла ошибка чтения файла. Повторите ввод.");

                }
                catch (Exception)
                {
                    Console.WriteLine("Ошибка: Неверный формат файла. Повторите ввод.");
                }
            }
        }
        /// <summary>
        /// Статический метод читает данные из файла и возвращает объект типа ListEvents
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static ListEvents ReadFile(string filePath)
        {
            var events = new ListEvents();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    var data = line.Split("] [");
                    var @event = new Event()
                    {
                        Id = int.Parse(data[0].Replace("[", "")),
                        Name = data[1],
                        Category = data[2],
                        Date = DateTime.Parse(data[3]),
                        Location = data[4],
                        Description = data[5],
                        Price = double.Parse(data[6]),
                        AvailableQuantityTickets = int.Parse(data[7]),
                        SoldTickets = int.Parse(data[8].Replace("]", "")),
                    };
                    events.AddEvent(@event);
                }
            }
            return events;
        }

        /// <summary>
        /// Статический метод записывает данные в файл
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="events"></param>
        public static void WriteFile(ListEvents events)
        {
            while (true)
            {
                try
                {
                    Console.Write("Введите путь файла: ");
                    var filePath = Console.ReadLine();

                    bool confirmation = true;
                    if (File.Exists(filePath))
                    {
                        confirmation = AnsiConsole.Confirm($"Подтвердите перезапись файла");
                    }

                    if (confirmation)
                    {
                        using (StreamWriter sw = new StreamWriter(filePath))
                        {
                            foreach (var e in events)
                            {
                                sw.WriteLine(e.ToString());
                            }

                        }

                        Console.WriteLine("Файл записан.");
                        return;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Ошибка: Файл не удалось перезаписать. Повторите ввод.");
                }
            }
        }
    }
}