using ClassLibrary.Filter;
using ClassLibrary.Sort;
using Spectre.Console;
using Color = Spectre.Console.Color;
namespace ClassLibrary.InteractiveTable
{
    public class TableEvents : ITableEvents
    {
        private ListEvents _originalEvents;
        private ListEvents _filteredEvents;

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public TableEvents()
        {
            _originalEvents = new ListEvents();
            _filteredEvents = new ListEvents();
        }

        /// <summary>
        /// Конструктор с параметром
        /// </summary>
        /// <param name="events"></param>
        public TableEvents(ListEvents events)
        {
            _originalEvents = new ListEvents(events.Select(e => new Event
            {
                Id = e.Id,
                Name = e.Name,
                Category = e.Category,
                Date = e.Date,
                Location = e.Location,
                Description = e.Description,
                Price = e.Price,
                AvailableQuantityTickets = e.AvailableQuantityTickets,
                SoldTickets = e.SoldTickets
            }));
            _filteredEvents = new ListEvents(_originalEvents);
        }


        /// <summary>
        /// Метод создает таблицу
        /// </summary>
        /// <returns></returns>
        public Table CreateTable()
        {
            var table = new Table();

            table.AddColumn(new TableColumn("ID").Centered());
            table.AddColumn(new TableColumn("Название").Centered());
            table.AddColumn(new TableColumn("Дата и время").Centered());
            table.AddColumn(new TableColumn("Место").Centered());
            table.AddColumn(new TableColumn("Описание").Centered());
            table.AddColumn(new TableColumn("Цена билета").Centered());
            table.AddColumn(new TableColumn("Количество билетов").Centered());
            table.AddColumn(new TableColumn("Количество проданных билетов").Centered());

            var minAbleTickets = _filteredEvents.Min(e => e.AvailableQuantityTickets);

            foreach (var e in _filteredEvents)
            {
                var rowStyle = e.AvailableQuantityTickets <= minAbleTickets ? new Style(Color.Red) : table.BorderStyle;
                table.AddRow(new Text(e.Id.ToString(), rowStyle),
                    new Text(e.Name, rowStyle),
                    new Text($"{e.Date:dd.MM.yy hh:mm}", rowStyle),
                    new Text(e.Location, rowStyle),
                    new Text(e.Description, rowStyle),
                    new Text($"{e.Price}", rowStyle),
                    new Text($"{e.AvailableQuantityTickets}", rowStyle),
                    new Text($"{e.SoldTickets}", rowStyle));
            }

            table.Title = new TableTitle("Таблица мероприятий");
            table.Border(TableBorder.Rounded);
            table.ShowRowSeparators = true;
            return table;
        }

        /// <summary>
        /// Статический метод выводит таблицу в консоль 
        /// </summary>
        /// <param name="table"></param>
        public void ShowTable(Table table)
        {
            Console.Clear();
            AnsiConsole.Write(table);
            Console.WriteLine("\nМеню:");
            Console.WriteLine("[F] - Фильтрация | [S] - Сортировка | [R] - Сброс | [E] - Выход");
        }

        /// <summary>
        /// Метод сбрасывает все фильтры 
        /// </summary>
        public void ResetFilters()
        {
            _filteredEvents = new ListEvents(_originalEvents);
        }

        /// <summary>
        /// Метод запрашивает у пользователя способ и параметры фильтрации
        /// </summary>
        public void FilterTable()
        {
            var filterEvents = new EventsFilter(_filteredEvents);

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Выберите столбец для фильтрации")
                    .AddChoices(
                    [
                "ID", "Название", "Дата и время",
                "Место", "Описание", "Цена билета",
                "Количество билетов", "Количество проданных билетов"
                    ]));

            try
            {
                switch (choice)
                {
                    case "ID":
                    {
                        Console.Write("Введите минимальное ID (Ввод чтобы пропустить): ");
                        var minInput = Console.ReadLine();
                        Console.Write("Введите максимальное ID (Ввод чтобы пропустить): ");
                        var maxInput = Console.ReadLine();

                        int minId = int.MinValue;
                        int maxId = int.MaxValue;

                        bool minValid = string.IsNullOrEmpty(minInput) || int.TryParse(minInput, out minId);
                        bool maxValid = string.IsNullOrEmpty(maxInput) || int.TryParse(maxInput, out maxId);

                        if (!minValid || !maxValid || minId > maxId)
                        {
                            Console.WriteLine("Ошибка: Некорректный ввод ID.");
                            return;
                        }

                        filterEvents.FilterById(minId, maxId);
                        break;
                    }
                    case "Название":
                    {
                        Console.Write("Введите фильтр (ключевые слова или словосочетание): ");
                        filterEvents.FilterByName(Console.ReadLine()!);
                        break;
                    }
                    case "Дата и время":
                    {
                        Console.Write(
                            "Введите минимальную дату и время в виде ДД.MM.ГГГГ ЧЧ:MM (Ввод чтобы пропустить): ");
                        var minInput = Console.ReadLine();
                        Console.Write(
                            "Введите максимальную дату и время в виде ДД.MM.ГГГГ ЧЧ:MM (Ввод чтобы пропустить): ");
                        var maxInput = Console.ReadLine();
                        DateTime minDate = DateTime.MinValue;
                        DateTime maxDate = DateTime.MaxValue;

                        bool minValid = true;
                        bool maxValid = true;

                        if (!string.IsNullOrEmpty(minInput) && !DateTime.TryParse(minInput, out minDate))
                        {
                            minValid = false;
                        }

                        if (!string.IsNullOrEmpty(maxInput) && !DateTime.TryParse(maxInput, out maxDate))
                        {
                            maxValid = false;
                        }

                        if (!minValid || !maxValid)
                        {
                            Console.WriteLine("Ошибка: Некорректный формат даты.");
                            return;
                        }

                        if (minDate > maxDate)
                        {
                            Console.WriteLine("Ошибка: Минимальная дата не может быть больше максимальной.");
                            return;
                        }

                        filterEvents.FilterByDate(minDate, maxDate);
                        break;
                    }
                    case "Место":
                    {
                        Console.Write("Введите фильтр (ключевые слова или словосочетание): ");
                        filterEvents.FilterByLocation(Console.ReadLine()!);
                        break;
                    }
                    case "Описание":
                    {
                        Console.Write("Введите фильтр (ключевые слова или словосочетание): ");
                        filterEvents.FilterByDescription(Console.ReadLine()!);
                        break;
                    }
                    case "Цена билета":
                    {
                        Console.Write("Введите минимальную цену (Ввод чтобы пропустить): ");
                        var minInput = Console.ReadLine();
                        Console.Write("Введите максимальную цену (Ввод чтобы пропустить): ");
                        var maxInput = Console.ReadLine();

                        double minPrice = double.MinValue;
                        double maxPrice = double.MaxValue;

                        bool minValid = string.IsNullOrEmpty(minInput) || double.TryParse(minInput, out minPrice);
                        bool maxValid = string.IsNullOrEmpty(maxInput) || double.TryParse(maxInput, out maxPrice);

                        if (!minValid || !maxValid || minPrice > maxPrice)
                        {
                            Console.WriteLine("Ошибка: Некорректный ввод цены.");
                            return;
                        }

                        filterEvents.FilterByPriceTicket(minPrice, maxPrice);
                        break;
                    }
                    case "Количество билетов":
                    {
                        Console.Write("Введите минимальное количество (Ввод чтобы пропустить): ");
                        var minInput = Console.ReadLine();
                        Console.Write("Введите максимальное количество (Ввод чтобы пропустить): ");
                        var maxInput = Console.ReadLine();

                        int minQty = int.MinValue;
                        int maxQty = int.MaxValue;

                        bool minValid = string.IsNullOrEmpty(minInput) || int.TryParse(minInput, out minQty);
                        bool maxValid = string.IsNullOrEmpty(maxInput) || int.TryParse(maxInput, out maxQty);

                        if (!minValid || !maxValid || minQty > maxQty)
                        {
                            Console.WriteLine("Ошибка: Некорректный ввод количества.");
                            return;
                        }

                        filterEvents.FilterByAvailableQuantityTickets(minQty, maxQty);
                        break;
                    }
                    case "Количество проданных билетов":
                    {
                        Console.Write("Введите минимальное количество (Ввод чтобы пропустить): ");
                        var minInput = Console.ReadLine();
                        Console.Write("Введите максимальное количество (Ввод чтобы пропустить): ");
                        var maxInput = Console.ReadLine();

                        int minSold = int.MinValue;
                        int maxSold = int.MaxValue;

                        bool minValid = string.IsNullOrEmpty(minInput) || int.TryParse(minInput, out minSold);
                        bool maxValid = string.IsNullOrEmpty(maxInput) || int.TryParse(maxInput, out maxSold);

                        if (!minValid || !maxValid || minSold > maxSold)
                        {
                            Console.WriteLine("Ошибка: Некорректный ввод количества.");
                            return;
                        }

                        filterEvents.FilterBySoldTickets(minSold, maxSold);
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Ошибка: Фильтрация по выбранному столбцу невозможна.");
                        return;
                    }
                }

                if (filterEvents.Count() != 0)
                {
                    _filteredEvents = new ListEvents(filterEvents); 
                    ShowTable(CreateTable());
                }
                else
                {
                    Console.WriteLine("Ошибка: Нет мероприятий удовлетворяющие фильтрам.");
                    Console.ReadKey();
                }
            }
            catch(Exception)
            {
                Console.WriteLine("Ошибка: Нет мероприятий удовлетворяющие фильтрам.");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Метод запрашивает у пользователя параметр и способ сортировки 
        /// </summary>
        public void SortTable()
        {
            var sort = new EventsSort(_filteredEvents);

            var column = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Выберите столбец для сортировки")
                    .AddChoices(new[]
                    {
                "ID", "Название", "Дата и время",
                "Место", "Описание", "Цена билета",
                "Количество билетов", "Количество проданных билетов"
                    }));

            var direction = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Выберите направление")
                    .AddChoices(new[] { "По возрастанию", "По убыванию" }));

            bool ascending = direction.Contains("возрастанию");

            switch (column)
            {
                case "ID": sort.SortById(ascending); break;
                case "Название": sort.SortByName(ascending); break;
                case "Дата и время": sort.SortByData(ascending); break;
                case "Место": sort.SortByLocation(ascending); break;
                case "Описание": sort.SortByDescription(ascending); break;
                case "Цена билета": sort.SortByPriceTicket(ascending); break;
                case "Количество билетов": sort.SortByAvailableQuantityTickets(ascending); break;
                case "Количество проданных билетов": sort.SortBySoldTickets(ascending); break;
            }

            _filteredEvents = new ListEvents(sort);
            ShowTable(CreateTable());
        }
    }
}