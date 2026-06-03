using Spectre.Console;
namespace ClassLibrary.ConsoleMenu
{
    public static class ConsoleCalendar
    {
        /// <summary>
        /// Метод создает интерактивный календарь
        /// </summary>
        /// <param name="events"></param>
        public static void DisplayCalendar(ListEvents events)
        {
            var currentDate = DateTime.Today;
            var isNavigating = true;

            // Создание календаря
            while (isNavigating)
            {
                AnsiConsole.Clear();

                var calendar = new Calendar(currentDate.Year, currentDate.Month)
                    .Culture("ru-RU")
                    .HighlightStyle(Style.Parse("yellow bold"));

                foreach (var e in events.Where(e =>
                             e.Date.Year == currentDate.Year &&
                             e.Date.Month == currentDate.Month))
                {
                    calendar.AddCalendarEvent(e.Date.Year, e.Date.Month, e.Date.Day);
                }

                // Меню
                AnsiConsole.Write(calendar);
                AnsiConsole.WriteLine();
                AnsiConsole.WriteLine("←/→ - месяцы, ↑/↓ - годы");
                AnsiConsole.WriteLine("Enter - перейти к выбору дня, E - назад");

                var key = Console.ReadKey(intercept: true).Key;

                // Обработка действий пользователя 
                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        currentDate = currentDate.AddMonths(-1);
                        break;
                    case ConsoleKey.RightArrow:
                        currentDate = currentDate.AddMonths(1);
                        break;
                    case ConsoleKey.UpArrow:
                        currentDate = currentDate.AddYears(1);
                        break;
                    case ConsoleKey.DownArrow:
                        currentDate = currentDate.AddYears(-1);
                        break;
                    case ConsoleKey.Enter:
                        // Ввод и поиск дня 
                        var selectedDay = AnsiConsole.Prompt(
                            new TextPrompt<int>("Введите число:")
                                .Validate(day =>
                                {
                                    if (day >= 1 && day <= DateTime.DaysInMonth(currentDate.Year, currentDate.Month))
                                        return ValidationResult.Success();
                                    return ValidationResult.Error("Некорректное число для данного месяца");
                                }));

                        var selectedDate = new DateTime(currentDate.Year, currentDate.Month, selectedDay);

                        var eventsOnDate = events.Where(e => e.Date.Date == selectedDate.Date).ToList();

                        // Вывод календаря
                        AnsiConsole.Clear();
                        AnsiConsole.Write(new Calendar(currentDate.Year, currentDate.Month)
                            .Culture("ru-RU")
                            .HighlightStyle(Style.Parse("yellow bold"))
                            .AddCalendarEvent(selectedDate.Year, selectedDate.Month, selectedDate.Day)
                            .HighlightStyle(Style.Parse("cyan bold")));

                        if (eventsOnDate.Any())
                        {
                            AnsiConsole.WriteLine($"\nМероприятия на {selectedDate:dd.MM.yyyy}:");
                            foreach (var e in eventsOnDate)
                            {
                                AnsiConsole.WriteLine($"- {e.Name} ({e.Date:HH:mm}) - {e.Location}");
                            }
                        }
                        else
                        {
                            AnsiConsole.WriteLine($"\nНа {selectedDate:dd.MM.yyyy} мероприятий нет");
                        }

                        AnsiConsole.WriteLine("\nНажмите E для возврата...");
                        while (Console.ReadKey(intercept: true).Key != ConsoleKey.E)
                        {
                        }

                        break;
                    case ConsoleKey.E:
                        isNavigating = false;
                        AnsiConsole.Clear();
                        break;
                }
            }
        }
    }
}