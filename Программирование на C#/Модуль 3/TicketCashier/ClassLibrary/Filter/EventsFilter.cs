using System.Collections;
namespace ClassLibrary.Filter
{
    public class EventsFilter : IEnumerable<Event>, IEventsFilter
    {
        private List<Event> _filteredEvents;

        /// <summary>
        /// Конструктор без параметров 
        /// </summary>
        public EventsFilter()
        {
            _filteredEvents = new List<Event>();
        }

        /// <summary>
        /// Конструктор с параметром типа ListEvents
        /// </summary>
        /// <param name="notes"></param>
        public EventsFilter(ListEvents notes)
        {
            _filteredEvents = new List<Event>(notes);
        }

        /// <summary>
        /// Метод осуществляет фильтрацию данных по Id
        /// </summary>
        /// <param name="minId"></param>
        /// <param name="maxId"></param>
        public void FilterById(int minId, int maxId)
        {
            _filteredEvents = _filteredEvents.Where(e => e.Id >= minId && e.Id <= maxId).ToList();
        }

        /// <summary>
        /// Метод осуществляет фильтрацию данных по названию
        /// </summary>
        /// <param name="name"></param>
        public void FilterByName(string? name)
        {
            _filteredEvents = _filteredEvents.Where(e => e.Name.Contains(name)).ToList();
        }

        /// <summary>
        /// Метод осуществляет фильтрацию данных по дате
        /// </summary>
        /// <param name="minDate"></param>
        /// <param name="maxDate"></param>
        public void FilterByDate(DateTime minDate, DateTime maxDate)
        {
            _filteredEvents = _filteredEvents
                .Where(e => e.Date >= minDate && e.Date < maxDate)
                .ToList();
        }

        /// <summary>
        /// Метод осуществляет фильтрацию данных по месту
        /// </summary>
        /// <param name="location"></param>
        public void FilterByLocation(string location)
        {
            _filteredEvents = _filteredEvents.Where(e => e.Location.Contains(location)).ToList();
        }

        /// <summary>
        /// Метод осуществляет фильтрацию данных по описанию
        /// </summary>
        /// <param name="description"></param>
        public void FilterByDescription(string description)
        {
            _filteredEvents = _filteredEvents.Where(e => e.Description.Contains(description)).ToList();
        }

        /// <summary>
        /// Метод осуществляет фильтрацию данных по цене билета
        /// </summary>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        public void FilterByPriceTicket(double minPrice, double maxPrice)
        {
            _filteredEvents = _filteredEvents.Where(e => e.Price >= minPrice && e.Price <= maxPrice).ToList();
        }

        /// <summary>
        /// Метод осуществляет фильтрацию данных по количеству доступных билетов
        /// </summary>
        /// <param name="minAvailableQuantity"></param>
        /// <param name="maxAvailableQuantity"></param>
        public void FilterByAvailableQuantityTickets(int minAvailableQuantity, int maxAvailableQuantity)
        {
            _filteredEvents = _filteredEvents.Where(e => e.AvailableQuantityTickets >= minAvailableQuantity && e.AvailableQuantityTickets <= maxAvailableQuantity).ToList();
        }
        
        /// <summary>
        /// Метод осуществляет фильтрацию данных по количеству проданных билетов
        /// </summary>
        /// <param name="minSoldTickets"></param>
        /// <param name="maxSoldTickets"></param>
        public void FilterBySoldTickets(int minSoldTickets, int maxSoldTickets)
        {
            _filteredEvents = _filteredEvents.Where(e => e.SoldTickets >= minSoldTickets && e.SoldTickets <= maxSoldTickets).ToList();
        }

        public IEnumerator<Event> GetEnumerator()
        {
            return _filteredEvents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}