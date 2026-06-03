namespace ClassLibrary.Sort
{
    public interface IEventsSort
    {
        public void SortById(bool ascending);

        public void SortByName(bool ascending);

        public void SortByData(bool ascending);

        public void SortByLocation(bool ascending);

        public void SortByDescription(bool ascending);

        public void SortByPriceTicket(bool ascending);

        public void SortByAvailableQuantityTickets(bool ascending);

        public void SortBySoldTickets(bool ascending);
    }
}