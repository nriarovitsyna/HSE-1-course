using Spectre.Console;
namespace ClassLibrary.Statistics
{
    public class EventsStatistics : IEventsStatistics
    {
        private readonly List<Event> _events;
        
        /// <summary>
        /// Конструктор с параметром типа ListEvents
        /// </summary>
        /// <param name="events"></param>
        public EventsStatistics(ListEvents events)
        {
            _events = new List<Event>(events);
        }

        /// <summary>
        /// Метод выводит в консоль диаграмму продажи билетов по дням недели и месяцам
        /// </summary>
        public void GetStatisticByDayOfWeekAndMonth()
        {
            // Группировка по дням недели 
            var statisticByDay = _events
                .GroupBy(e => e.Date.DayOfWeek)
                .Select(g => new
                {
                    Day = g.Key,
                    SoldTickets = g.Sum(e => e.SoldTickets)
                })
                .OrderBy(g => g.Day)
                .ToList();

            // Группировка по месяцам
            var statisticByMonth = _events
                .GroupBy(e => e.Date.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    SoldTickets = g.Sum(e => e.SoldTickets)
                })
                .OrderBy(g => g.Month)
                .ToList();
            
            // Подсчет пробелов для красивого вывода в консоль 
            var maxLenghtByDay = statisticByDay.Max(data => Convert.ToString(data.Day).Length) + 1;
            var maxLenghtByMonth = statisticByMonth.Max(data => Convert.ToString(data.Month).Length + 1);

            // Максимальное количество билетов для нормализации графика
            var maxTicketsByDay = statisticByDay.Max(data => data.SoldTickets);
            var maxTicketsByMonth = statisticByMonth.Max(data => data.SoldTickets);
            
            // График по дням недели
            Console.WriteLine("Статистика продажи билетов по дням недели");
            foreach (var data in statisticByDay)
            {
                var bar = new string('█', data.SoldTickets * 50 / maxTicketsByDay);
                var alignment = new string(' ', maxLenghtByDay - Convert.ToString(data.Day).Length);
                AnsiConsole.Markup($"{data.Day}:{alignment}[green]{bar}[/] ({data.SoldTickets})\n");
            }

            Console.WriteLine("\n");

            // График по месяцам
            Console.WriteLine("Статистика продажи билетов по месяцам");
            foreach (var data in statisticByMonth)
            {
                var bar = new string('█', data.SoldTickets * 50 / maxTicketsByMonth);
                var alignment = new string(' ', maxLenghtByMonth - Convert.ToString(data.Month).Length);
                AnsiConsole.Markup($"{data.Month}:{alignment}[green]{bar}[/] ({data.SoldTickets})\n");
            }
        }

        /// <summary>
        /// Метод выводит в консоль диаграмму популярности мероприятий по категориям 
        /// </summary>
        public void GetStatisticByCategories()
        {
            // Группировка по категориям 
            var statisticByCategory = _events
                .GroupBy(e => e.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    SoldTickets = g.Sum(e => e.SoldTickets)
                })
                .OrderBy(g => g.Category)
                .ToList();

            // Подсчет пробелов для красивого вывода в консоль 
            var maxIndents = statisticByCategory.Max(data => Convert.ToString(data.Category).Length) + 1;
            
            // Максимальное количество билетов для нормализации графика
            var maxTicketsByCategory = statisticByCategory.Max(data => data.SoldTickets);
            
            Console.WriteLine("Статистика мероприятий по категориям");
            foreach (var data in statisticByCategory)
            {
                var bar = new string('█', data.SoldTickets * 50 / maxTicketsByCategory);
                var alignment = new string(' ', maxIndents - Convert.ToString(data.Category).Length);
                AnsiConsole.Markup($"{data.Category}:{alignment}[green]{bar}[/] ({data.SoldTickets})\n");
            }
        }

        /// <summary>
        /// Метод выводит в консоль рейтинг мероприятий по выручке
        /// </summary>
        public void GetStatisticByRevenue()
        {
            // Группировка по выручке
            var statisticByRevenue = _events
                .Select(e => new
                {
                    EventName = e.Name,
                    Revenue = e.SoldTickets * e.Price
                })
                .OrderByDescending(e => e.Revenue)
                .ToList();
            
            Console.WriteLine("Рейтинг мероприятий по выручке");
            for (var i = 0; i < statisticByRevenue.Count; i++)
            {
                AnsiConsole.Markup($"{i + 1}. {statisticByRevenue[i].EventName} ({statisticByRevenue[i].Revenue})\n");
            }
        }
    }
}