namespace ClassLibrary.ConsoleMenu
{
    public static class ConsoleWork
    {
        /// <summary>
        /// Метод читает данные из консоли и возвращает список мероприятий
        /// </summary>
        /// <returns></returns>
        public static ListEvents ReadConsole()
        {
            var events = new ListEvents();
            Console.WriteLine("Введите текст (введите 'exit' для выхода):");

            while (true)
            {
                var inputLine = Console.ReadLine();
            
                if (inputLine!.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                try
                {

                    var data = inputLine.Split("] [");
                    var @event = new Event
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
                catch (Exception)
                {
                    Console.WriteLine("Ошибка: Данные вводимые в консоль неверного формата. Повторите ввод.");
                    Console.ReadKey();
                    events = new ListEvents();
                    Console.Clear();
                    Console.WriteLine("Введите текст (введите 'exit' для выхода):");
                }
            }
            
            Console.WriteLine("Данные успешно записаны!");
            return events;
        }

        public static void WriteConsole(ListEvents events)
        {
            foreach (var e in events)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}