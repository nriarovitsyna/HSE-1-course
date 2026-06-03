using System.Globalization;
using ClassLibrary.Booking;
using Spectre.Console;
namespace ClassLibrary.ConsoleMenu
{
    public static class PaymentSystem
    {
        /// <summary>
        /// Метод для покупки забронированных билетов
        /// </summary>
        /// <param name="bookedTickets"></param>
        public static void HandleBookedTickets(TicketBooking bookedTickets)
        {
            // Генерация мероприятий
            var nameEvents = bookedTickets.GetActiveReservations().Select(e => e.Name).ToList();
            var nameEvent = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Выберите мероприятие:")
                    .PageSize(10)
                    .AddChoices(nameEvents));

            // Поиск выбранного мероприятия 
            var ticket = bookedTickets.GetActiveReservations().FirstOrDefault(t => t.Name == nameEvent);
            var ticketCount = ticket!.Count;

            // Подтверждение покупки 
            if (ConfirmPurchase(ticketCount, nameEvent))
            {
                // Оплата
                if (!ProcessPayment(ticket, ticketCount)) return;
                ticket.ShowTicket();
                ticket.GenerateQRCode();
                Console.ReadKey();
                bookedTickets.DeleteBookedTicket(ticket);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Покупка отменена.[/]");
            }
        }

        /// <summary>
        /// Метод для покупки доступных (незабронированных) билетов
        /// </summary>
        /// <param name="events"></param>
        public static void HandleAvailableTickets(ListEvents events)
        {
            // Генерация мероприятий 
            var nameEvents = events.Select(e => e.Name).ToList();
            var nameEvent = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Выберите мероприятие:")
                    .PageSize(10)
                    .AddChoices(nameEvents));

            // Поиск выбранного мероприятия и проверка на наличие билетов 
            var choiceEvent = events.FirstOrDefault(e => e.Name == nameEvent);
            if (choiceEvent!.AvailableQuantityTickets == 0)
            {
                Console.WriteLine($"Ошибка: Доступных билетов на \"{choiceEvent.Name}\" нет.");
                return;
            }
            
            // Получение количества билетов
            int ticketCount;
            while (true)
            {
                var input = AnsiConsole.Ask<string>("Введите количество билетов: ");
                if (int.TryParse(input, out ticketCount) && ticketCount > 0 &&
                    ticketCount <= choiceEvent!.AvailableQuantityTickets)
                {
                    break;
                }

                AnsiConsole.MarkupLine("[red]Ошибка: Некорректный ввод данных или недостаточно билетов.[/]");
            }

            // Подтверждение покупки 
            if (ConfirmPurchase(ticketCount, nameEvent))
            {
                // Оплата
                if (!ProcessPayment(choiceEvent, ticketCount)) return;
                choiceEvent.SoldTickets += ticketCount;
                choiceEvent.AvailableQuantityTickets -= ticketCount;

                var ticketInstance = new Ticket(choiceEvent, ticketCount);
                ticketInstance.ShowTicket();
                ticketInstance.GenerateQRCode();
                Console.ReadKey();
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Покупка отменена.[/]");
            }
        }

        /// <summary>
        /// Метод запрашивает подтверждение на покупку 
        /// </summary>
        /// <param name="ticketCount"></param>
        /// <param name="nameEvent"></param>
        /// <returns></returns>
        private static bool ConfirmPurchase(int ticketCount, string nameEvent)
        {
            return AnsiConsole.Confirm($"Подтвердите покупку {ticketCount} билет(ов) на \"{nameEvent}\"?");
        }

        /// <summary>
        /// Метод оповещает пользователя о статусе покупки 
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="ticketCount"></param>
        private static bool ProcessPayment(dynamic ticket, int ticketCount)
        {
            var payment = PayByCard();
            if (payment)
            {
                double totalPrice = ticketCount * ticket.Price;
                AnsiConsole.MarkupLine("[green]Покупка подтверждена![/]");
                AnsiConsole.MarkupLine($"[green]Вы купили {ticketCount} билет(ов) на \"{ticket.Name}\"[/]");
                AnsiConsole.MarkupLine($"Общая стоимость: [yellow]{totalPrice}[/]");

                Console.ReadKey();
                Console.Clear();
                return true;
            }
            
            AnsiConsole.MarkupLine("[red]Ошибка: Оплаты не прошла.[/]");
            return false;
        }

        /// <summary>
        /// Метод имитирует оплату с  помощью банковской системой 
        /// </summary>
        /// <returns></returns>
        private static bool PayByCard()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Введите данные банковской карты:");
                var cardData =
                    AnsiConsole.Ask<string>("Введите данные банковской карты (в формате XXXX-XXXX-XXXX-XXXX): ");
                cardData = cardData.Replace(" ", "").Replace("-", "");
                var cardDate =
                    AnsiConsole.Ask<string>("Введите дата окончанию действования банковской карты (в формате ММ/ГГ): ");
                var cardCode = AnsiConsole.Ask<string>("Введите CVC2/CVV2 код (в формате XXX): ");

                var isValidDate = DateTime.TryParseExact(
                    cardDate,
                    new[] { "MM/yy", "MM/yyyy" },
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out _);

                if (cardData.Length == 16 &&
                    cardData.All(char.IsDigit) &&
                    cardCode.Length == 3 &&
                    int.TryParse(cardCode, out _) &&
                    isValidDate)
                {
                    break;
                }

                AnsiConsole.MarkupLine("[red]Ошибка: Некорректный ввод данных карты.[/]");
                Console.ReadKey();
            }

            // Случайный выбор прошла/не прошла оплата 
            return new Random().Next(0, 2) == 1;
        }
    }
}