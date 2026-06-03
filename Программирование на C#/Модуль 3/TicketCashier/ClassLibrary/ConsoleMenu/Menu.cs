using ClassLibrary.Statistics;
using ClassLibrary.Booking;
using ClassLibrary.InteractiveTable;
using Spectre.Console;
namespace ClassLibrary.ConsoleMenu
{
    public static class Menu
    {
        private static ListEvents _events = new(); // Список для хранения мероприятий
        private static TicketBooking _bookedTickets = new(); // Список для хранения бронированных билетов
        
        /// <summary>
        /// Метод вызывает основное меню
        /// </summary>
        public static void ExecuteUserChoice()
        {
            Console.WriteLine("Добро пожаловать в приложение \"Билетный кассир\"");
            
            // Получаем данные
            GetData();
            Console.ReadKey();  
            
            // Основное меню
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Просмотреть список всех мероприятий");
                Console.WriteLine("2. Добавить мероприятие");
                Console.WriteLine("3. Удалить мероприятие");
                Console.WriteLine("4. Изменить мероприятие");
                Console.WriteLine("5. Работать с таблицей");
                Console.WriteLine("6. Просмотреть календарь");
                Console.WriteLine("7. Купить билет");
                Console.WriteLine("8. Забронировать билет");
                Console.WriteLine("9. Управление бронями");
                Console.WriteLine("10. Получить статистику");
                Console.WriteLine("11. Изменить входные данные");
                Console.WriteLine("12. Выход");

                Console.Write("Выберите действие: ");
                string? choice = Console.ReadLine();

                Console.Clear();
                switch (choice)
                {
                    case "1":
                        DisplayAllEvents();
                        break;
                    case "2":
                        Redactor.AddNewEvent(_events);
                        break;
                    case "3":
                        Redactor.DeleteEvent(_events);
                        break;
                    case "4":
                        Redactor.EditEvent(_events);
                        break;
                    case "5":
                        WorkWithTable();
                        break;
                    case "6":
                        ConsoleCalendar.DisplayCalendar(_events);
                        break;
                    case "7":
                        BuyTicket();
                        break;
                    case "8":
                        ReserveTicket();
                        break;
                    case "9":
                        ManageReservations();
                        break;
                    case "10":
                        GetStatistic();
                        break;
                    case "11":
                        GetData();
                        break;
                    case "12":
                        OutputData();
                        return;
                    default:
                        Console.WriteLine("Ошибка: Выбранного варианта не существует. Повторите ввод.");
                        break;
                } 
                Console.ReadKey();  
            }
        }

        /// <summary>
        /// Метод запрашивает у пользователя способ получения данных и вызывает соответствующие методы
        /// </summary>
        private static void GetData()
        {
            Console.WriteLine("Осуществить ввод данных:");
            Console.WriteLine("1. Через консоль");
            Console.WriteLine("2. Через файл");

            while (true)
            {
                Console.Write("Введите способ ввода данных: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _events = ConsoleWork.ReadConsole();
                        _bookedTickets = new TicketBooking();
                        return;
                    case "2":
                        _events = FileWork.GetDataFromFile();
                        _bookedTickets = new TicketBooking();
                        return;
                    default:
                        Console.WriteLine("Ошибка: Введенного способа ввода данных не существует. Повторите ввод.");
                        break;
                }
            }
        }

        /// <summary>
        /// Метод запрашивает у пользователя способ вывода данных и вызывает соответствующие методы
        /// </summary>
        private static void OutputData()
        {
            Console.WriteLine("Осуществить вывод данных:");
            Console.WriteLine("1. В консоль");
            Console.WriteLine("2. В файл");
            while (true)
            {
                Console.Write("Введите способ вывода данных: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ConsoleWork.WriteConsole(_events);
                        return;
                    case "2":
                        FileWork.WriteFile(_events);
                        return;
                    default:
                        Console.WriteLine("Ошибка: Введенного способа вывода данных не существует. Повторите ввод.");
                        break;
                }
            }
        }
        
        /// <summary>
        /// Метод выводит информацию о доступных мероприятиях в консоль
        /// </summary>
        private static void DisplayAllEvents()
        {
            Console.WriteLine("Информация о всех мероприятиях:");
            foreach (var e in _events)
            {
                Console.WriteLine($"ID: {e.Id}");
                Console.WriteLine($"Название: {e.Name}");
                Console.WriteLine($"Категория: {e.Category}");
                Console.WriteLine($"Дата и время: {e.Date}");
                Console.WriteLine($"Место: {e.Location}");
                Console.WriteLine($"Цена билета: {e.Price}");
                Console.WriteLine($"Количество доступных билетов: {e.AvailableQuantityTickets}");
                Console.WriteLine(new string('-', 50));
            }
        }
        
        /// <summary>
        /// Метод запрашивает у пользователя тип билета (бронированный или нет) для покупки и вызывает соответствующие методы
        /// </summary>
        private static void BuyTicket()
        {
            Console.WriteLine("Покупка билетов");
            var booking = AnsiConsole.Confirm($"Вы бронировали билеты?");
        
            if (booking)
            {
                if (_bookedTickets.GetActiveReservations().Count != 0)
                {
                    PaymentSystem.HandleBookedTickets(_bookedTickets);
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Ошибка: Резервированных билетов нет.[/]");
                }
            }
            else
            {
                PaymentSystem.HandleAvailableTickets(_events);
            }
        }

        /// <summary>
        /// Метод работает с меню таблицы
        /// </summary>
        private static void WorkWithTable()
        {
            var table = new TableEvents(_events);
            table.ShowTable(table.CreateTable());

            while (true)
            {
                var inputKey = Console.ReadKey();
                // Фильтрация
                if (inputKey.Key == ConsoleKey.F)
                {
                    Console.WriteLine();
                    table.FilterTable();
                }
                //Сортировка
                else if (inputKey.Key == ConsoleKey.S)
                {
                    Console.WriteLine();
                    table.SortTable();
                }
                // Сброс таблицы
                else if (inputKey.Key == ConsoleKey.R)
                {
                    table.ResetFilters();
                    table.ShowTable(table.CreateTable());
                }
                // Выход
                else if (inputKey.Key == ConsoleKey.E)
                {
                    Console.Clear();
                    return;
                }
            }
        }
        
        /// <summary>
        /// Метод бронирует билет
        /// </summary>
        private static void ReserveTicket()
        {
            // Получаем названия мероприятий
            var nameEvents = _events.Select(e => e.Name).ToList();
            var nameEvent = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Выберите мероприятие:")
                    .PageSize(10)
                    .AddChoices(nameEvents));
            
            // Выбор пользователя и проверка на наличие билетов
            var choiceEvent = _events.FirstOrDefault(e => e.Name == nameEvent);
            if (choiceEvent.AvailableQuantityTickets == 0)
            {
                Console.WriteLine($"Ошибка: Доступных билетов на \"{choiceEvent.Name}\" нет.");
                return;
            }
            
            // Запрос количества билетов
            int ticketCount;
            while (true)
            {
                var input = AnsiConsole.Ask<string>("Введите количество билетов: ");
                if (int.TryParse(input, out ticketCount) && ticketCount > 0 && ticketCount <= choiceEvent!.AvailableQuantityTickets)
                {
                    break;
                }
                AnsiConsole.MarkupLine("[red]Ошибка: Некорректный ввод данных или недостаточно билетов.[/]");
            }

            // Подтверждение бронирования
            var confirmation = AnsiConsole.Confirm($"Подтвердите бронь {ticketCount} билет(ов) на \"{nameEvent}\"?");
            if (confirmation)
            { 
                _bookedTickets.BookTicket(choiceEvent, ticketCount);
                AnsiConsole.MarkupLine("[green]Бронь подтверждена! Бронь будет не действительна через 15 минут![/]");
               
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Бронь отменена.[/]");
            }
        }

        /// <summary>
        /// Работа со списком бронированных билетов 
        /// </summary>
        private static void ManageReservations()
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Просмотр активных броней.");
            Console.WriteLine("2. Отмена брони");

            if (_bookedTickets.GetActiveReservations().Count != 0)
            {
                Console.Write("Выберите действие: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Информация о бронированных билетах:");
                        foreach (var t in _bookedTickets.GetActiveReservations())
                        {
                            t.ShowTicket();
                            Console.WriteLine(new string('-', 50));
                        }
                        break;
                    case "2":
                        Console.Write("Укажите уникальный номер (Id) билета:");
                        var id = Console.ReadLine();
                        
                        var deletedTicket = _bookedTickets.GetActiveReservations().FirstOrDefault(t => t.Id == id);
                        if (deletedTicket != null)
                        {
                            _bookedTickets.DeleteBookedTicket(deletedTicket);
                        }
                        else
                        {
                            Console.WriteLine("Ошибка: Билета с веденным уникальным номером нет.");
                        }
                        break;
                    default:
                        Console.WriteLine("Ошибка: Введенного варианта нет. Повторите ввод.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Ошибка: Список бронированных мероприятий пуст.");
            }

        }

        /// <summary>
        /// Метод запрашивает у пользователя разрешение на вывод статистики
        /// </summary>
        private static void GetStatistic()
        {
            var analysis = new EventsStatistics(_events);
            
            var confirmation = AnsiConsole.Confirm("Получить статистику продажи билетов по дням недели и месяцам");
            if (confirmation)
            {
                analysis.GetStatisticByDayOfWeekAndMonth();
            }

            Console.ReadKey();
            Console.Clear();
            confirmation = AnsiConsole.Confirm("Посмотреть диаграмму популярности мероприятий по категории");
            if (confirmation)
            {
                analysis.GetStatisticByCategories();
            }
            
            Console.ReadKey();
            Console.Clear();
            confirmation =  AnsiConsole.Confirm("Получить рейтинг мероприятий по выручки");
            if (confirmation)
            {
                analysis.GetStatisticByRevenue();
            }
        }
    }
}