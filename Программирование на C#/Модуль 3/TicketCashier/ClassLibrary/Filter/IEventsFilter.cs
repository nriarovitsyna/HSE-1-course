namespace ClassLibrary.Filter
{
    public interface IEventsFilter
    {
        public void FilterById(int minId, int maxId);

        public void FilterByName(string? name);

        public void FilterByDate(DateTime minDate, DateTime maxDate);

        public void FilterByLocation(string location);

        public void FilterByDescription(string description);

        public void FilterByPriceTicket(double minPrice, double maxPrice);

        public void FilterByAvailableQuantityTickets(int minQuantity, int maxQuantity);

        public void FilterBySoldTickets(int minSoldTickets, int maxSoldTickets);
    }
}