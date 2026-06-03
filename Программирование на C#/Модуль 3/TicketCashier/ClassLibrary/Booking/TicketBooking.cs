namespace ClassLibrary.Booking
{
    public class TicketBooking : ITicketBooking
    {
        private readonly List<Ticket> _bookedTickets;
        
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public TicketBooking()
        {
            _bookedTickets = new List<Ticket>();
        }

        /// <summary>
        /// Метод бронирует билет
        /// </summary>
        /// <param name="bookedEvent"></param>
        /// <param name="count"></param>
        public void BookTicket(Event bookedEvent, int count)
        {
            var ticket = new Ticket(bookedEvent, count);
            bookedEvent.AvailableQuantityTickets -= count;
            bookedEvent.SoldTickets += count;
            _bookedTickets.Add(ticket);
            ticket.ReservationDate = DateTime.Now.AddMinutes(15);

            // Отмена бронирования после 15 минут
            Task.Delay(TimeSpan.FromMinutes(15)).ContinueWith(_ => { CancelReservation(bookedEvent, ticket); });

        }

        /// <summary>
        /// Метод отменяет бронь билетов и делает их доступными для покупки
        /// </summary>
        /// <param name="eventReserved"></param>
        /// <param name="ticket"></param>
        private void CancelReservation(Event eventReserved, Ticket ticket)
        {
            var booking = _bookedTickets.FirstOrDefault(t => t == ticket);
            if (booking != null)
            {
                _bookedTickets.Remove(booking);
                eventReserved.AvailableQuantityTickets += booking.Count;
                eventReserved.SoldTickets -= booking.Count;
            }
        }

        /// <summary>
        /// Метод возвращает список забронированных билетов
        /// </summary>
        /// <returns></returns>
        public List<Ticket> GetActiveReservations()
        {
            return _bookedTickets.Where(t => t.ReservationDate > DateTime.Now).ToList();
        }

        /// <summary>
        /// Метод удаляет билет 
        /// </summary>
        /// <param name="ticket"></param>
        public void DeleteBookedTicket(Ticket ticket)
        {
            _bookedTickets.Remove(ticket);
        }
    }
}