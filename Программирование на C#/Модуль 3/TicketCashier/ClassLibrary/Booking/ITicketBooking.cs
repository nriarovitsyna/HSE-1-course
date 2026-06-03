namespace ClassLibrary.Booking
{
    public interface ITicketBooking
    {
        public void BookTicket(Event bookedEvent, int count);

        public List<Ticket> GetActiveReservations();

        public void DeleteBookedTicket(Ticket ticket);
    }
}