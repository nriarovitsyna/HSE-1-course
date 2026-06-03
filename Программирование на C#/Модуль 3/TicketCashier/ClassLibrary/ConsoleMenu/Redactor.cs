using System.Globalization;
namespace ClassLibrary.ConsoleMenu
{
    public static class Redactor
    {
        /// <summary>
        /// Метод добавляет новое мероприятие в список
        /// </summary>
        /// <param name="events"></param>
        public static void AddNewEvent(ListEvents events)
        {
            try
            {
                Console.WriteLine("Добавление нового мероприятия");

                // Ввод названия
                string name;
                do
                {
                    Console.Write("Название мероприятия: ");
                    name = Console.ReadLine() ?? "";
                    if (string.IsNullOrWhiteSpace(name))
                        Console.WriteLine("Ошибка: Название не может быть пустым. Повторите ввод.");
                } while (string.IsNullOrWhiteSpace(name));

                // Ввод категории
                string category;
                do
                {
                    Console.Write("Категория мероприятия: ");
                    category = Console.ReadLine() ?? "";
                    if (string.IsNullOrWhiteSpace(category))
                        Console.WriteLine("Ошибка: Категория не может быть пустой. Повторите ввод.");
                } while (string.IsNullOrWhiteSpace(category));

                // Ввод даты
                DateTime date;
                while (true)
                {
                    Console.Write("Дата и время (ДД.ММ.ГГГГ ЧЧ:ММ): ");
                    if (DateTime.TryParse(Console.ReadLine(), out date) && date > DateTime.Now)
                        break;
                    Console.WriteLine("Ошибка: Неверный формат даты или времени. Повторите ввод.");
                }

                // Ввод места
                string location;
                do
                {
                    Console.Write("Место проведения: ");
                    location = Console.ReadLine() ?? "";
                    if (string.IsNullOrWhiteSpace(location))
                        Console.WriteLine("Ошибка: Место не может быть пустым. Повторите ввод.");
                } while (string.IsNullOrWhiteSpace(location));

                // Ввод описания
                string description;
                do
                {
                    Console.Write("Описание мероприятия: ");
                    description = Console.ReadLine() ?? "";
                    if (string.IsNullOrWhiteSpace(description))
                        Console.WriteLine("Ошибка: Описание не может быть пустым. Повторите ввод.");
                } while (string.IsNullOrWhiteSpace(description));

                // Ввод цены
                double priceTicket;
                while (true)
                {
                    Console.Write("Цена билета: ");
                    if (double.TryParse(Console.ReadLine(), out priceTicket) && priceTicket > 0)
                        break;
                    Console.WriteLine("Ошибка: Цена должна быть положительным числом. Повторите ввод.");
                }

                // Ввод количества билетов
                int quantityTickets;
                while (true)
                {
                    Console.Write("Количество билетов: ");
                    if (int.TryParse(Console.ReadLine(), out quantityTickets) && quantityTickets > 0)
                        break;
                    Console.WriteLine("Ошибка: Количество должно быть положительным целым числом. Повторите ввод.");
                }

                // Генерация ID
                var newId = events.DefaultIfEmpty(new Event { Id = 0 }).Max(e => e.Id) + 1;

                var newEvent = new Event
                {
                    Id = newId,
                    Name = name,
                    Category = category,
                    Date = date,
                    Location = location,
                    Description = description,
                    Price = priceTicket,
                    AvailableQuantityTickets = quantityTickets,
                };

                events.AddEvent(newEvent);
                Console.WriteLine("Мероприятие успешно добавлено!");
            }
            catch (Exception)
            {
                Console.WriteLine($"Ошибка: Не удалось добавить мероприятие.");
            }
        }
        
        /// <summary>
        /// Метод удаляет мероприятие по выбранному ID
        /// </summary>
        /// <param name="events"></param>
        public static void DeleteEvent(ListEvents events)
        {
            // Проверка на не пустоту списка
            if (!events.Any())
            {
                Console.WriteLine("Ошибка: Список мероприятий пуст.");
                return;
            }

            // Список краткой информации о мероприятиях 
            Console.WriteLine("Список мероприятий:");
            foreach (var e in events)
            {
                Console.WriteLine($"ID: {e.Id,-5} | {e.Date:dd.MM.yyyy HH:mm} | {e.Name}");
            }
            Console.WriteLine("\nУдаление мероприятия");
            
            // Поиск мероприятия по ID и удаление
            int id;
            while (true)
            {
                Console.Write("Введите ID мероприятия для удаления (или 0 для отмены): ");
                var input = Console.ReadLine();

                if (input == "0")
                {
                    return;
                }

                if (int.TryParse(input, out id) && id > 0)
                {
                    var eventToRemove = events.FirstOrDefault(e => e.Id == id);

                    if (eventToRemove == null)
                    {
                        Console.WriteLine($"Ошибка: Мероприятие с ID {id} не найдено. Повторите ввод.");
                    }
                    else
                    {
                        events.RemoveEvent(eventToRemove);
                        Console.WriteLine($"Мероприятие \"{eventToRemove.Name}\" (ID {id}) успешно удалено!");
                    }
                }
                else
                    Console.WriteLine("Ошибка: Неверный формат ID. Повторите ввод.");
            }
        }
        
        /// <summary>
        /// Метод редактирует мероприятие по выбранному ID
        /// </summary>
        /// <param name="events"></param>
        public static void EditEvent(ListEvents events)
        {
            if (!events.Any())
            {
                Console.WriteLine("Ошибка: Список мероприятий пуст.");
                return;
            }

            // Вывод краткой информации о мероприятиях
            Console.WriteLine("Список мероприятий:");
            foreach (var e in events)
            {
                Console.WriteLine($"ID: {e.Id,-5} | {e.Date:dd.MM.yyyy HH:mm} | {e.Name}");
            }

            // Выбор ID
            int id;
            while (true)
            {
                Console.Write("\nВведите ID мероприятия для редактирования: ");
                if (int.TryParse(Console.ReadLine(), out id) && events.Any(e => e.Id == id))
                    break;
                Console.WriteLine("Ошибка: Неверный ID. Повторите ввод.");
            }

            var eventToEdit = events.First(e => e.Id == id);

            Console.WriteLine("\nОставьте поле пустым, чтобы не изменять");

            // Изменение названия
            Console.WriteLine($"\nТекущее название: {eventToEdit.Name}");
            Console.Write("Новое название: ");
            var newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
            {
                eventToEdit.Name = newName;
            }

            // Изменение категории
            Console.WriteLine($"\nТекущая категория: {eventToEdit.Category}");
            Console.Write("Новая категория: ");
            var newCategory = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newCategory))
            {
                eventToEdit.Category = newCategory;
            }

            // Изменение даты 
            Console.WriteLine($"\nТекущая дата: {eventToEdit.Date:dd.MM.yyyy HH:mm}");
            Console.Write("Новая дата (дд.мм.гггг чч:мм): ");
            var newDateStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDateStr))
            {
                if (DateTime.TryParseExact(newDateStr, "dd.MM.yyyy HH:mm",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out var newDate))
                {
                    eventToEdit.Date = newDate;
                }
                else
                {
                    Console.WriteLine("Ошибка: Неверный формат даты.");
                    return;
                }
            }

            // Изменение места
            Console.WriteLine($"\nТекущее место: {eventToEdit.Location}");
            Console.Write("Новое место: ");
            var newLocation = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newLocation))
            {
                eventToEdit.Location = newLocation;
            }

            // Изменение описания
            Console.WriteLine($"\nТекущее описание: {eventToEdit.Description}");
            Console.Write("Новое описание: ");
            var newDescription = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newDescription))
            {
                eventToEdit.Description = newDescription;
            }

            // Изменение цены
            Console.WriteLine($"\nТекущая цена: {eventToEdit.Price}");
            Console.Write("Новая цена: ");
            var newPriceStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newPriceStr))
            {
                if (double.TryParse(newPriceStr, out var newPrice) && newPrice > 0)
                {
                    eventToEdit.Price = newPrice;
                }
                else
                {
                    Console.WriteLine("Ошибка: Неверный формат цены.");
                    return;
                }
            }

            // Изменение количества доступных билетов
            Console.WriteLine($"\nТекущее количество доступных билетов: {eventToEdit.AvailableQuantityTickets}");
            Console.Write("Новое количество доступных билетов: ");
            var newQuantityStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newQuantityStr))
            {
                if (int.TryParse(newQuantityStr, out var newQuantity) && newQuantity >= 0)
                {
                    eventToEdit.AvailableQuantityTickets = newQuantity;
                }
                else
                {
                    Console.WriteLine("Ошибка: Неверный формат количества.");
                    return;
                }
            }

            Console.WriteLine("\nМероприятие успешно обновлено!");
        }
    }
}