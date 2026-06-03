using System.Collections;
namespace ClassLibrary.Sort
{
    public class EventsSort : IEnumerable<Event>, IEventsSort
    {
        private List<Event> _sortedEvents;

        /// <summary>
        /// Конструктор без параметров 
        /// </summary>
        public EventsSort()
        {
            _sortedEvents = new List<Event>();
        }

        /// <summary>
        /// Конструктор с параметром типа ListEvents
        /// </summary>
        /// <param name="events"></param>
        public EventsSort(ListEvents events)
        {
            _sortedEvents = new List<Event>(events);
        }

        /// <summary>
        /// Метод сортирует мероприятия по Id
        /// </summary>
        /// <param name="ascending"></param>
        public void SortById(bool ascending)
        {
            _sortedEvents = ascending ? _sortedEvents.OrderBy(e => e.Id).ToList() : _sortedEvents.OrderByDescending(e => e.Id).ToList();
        }

        /// <summary>
        /// Метод сортирует мероприятия по названию
        /// </summary>
        /// <param name="ascending"></param>
        public void SortByName(bool ascending)
        {
            _sortedEvents = ascending ? _sortedEvents.OrderBy(e => e.Name).ToList() : _sortedEvents.OrderByDescending(e => e.Name).ToList();
        }

        /// <summary>
        /// Метод сортирует мероприятия по дате 
        /// </summary>
        /// <param name="ascending"></param>
        public void SortByData(bool ascending)
        {
            _sortedEvents = ascending ? _sortedEvents.OrderBy(e => e.Date).ToList() : _sortedEvents.OrderByDescending(e => e.Date).ToList();
        }

        /// <summary>
        /// Метод сортирует мероприятия по месту
        /// </summary>
        /// <param name="ascending"></param>
        public void SortByLocation(bool ascending)
        {
            _sortedEvents = ascending ? _sortedEvents.OrderBy(e => e.Location).ToList() : _sortedEvents.OrderByDescending(e => e.Location).ToList();
        }

        /// <summary>
        /// Метод сортирует мероприятия по описанию
        /// </summary>
        /// <param name="ascending"></param>
        public void SortByDescription(bool ascending)
        {
            _sortedEvents = ascending ? _sortedEvents.OrderBy(e => e.Description).ToList() : _sortedEvents.OrderByDescending(e => e.Description).ToList();
        }

        /// <summary>
        /// Метод сортирует мероприятия по цене билетов
        /// </summary>
        /// <param name="ascending"></param>
        public void SortByPriceTicket(bool ascending)
        {
            _sortedEvents = ascending ? _sortedEvents.OrderBy(e => e.Price).ToList() : _sortedEvents.OrderByDescending(e => e.Price).ToList();
        }

        /// <summary>
        /// Метод сортирует мероприятия по количеству доступных билетов
        /// </summary>
        /// <param name="ascending"></param>
        public void SortByAvailableQuantityTickets(bool ascending)
        {
            _sortedEvents = ascending ? _sortedEvents.OrderBy(e => e.AvailableQuantityTickets).ToList() : _sortedEvents.OrderByDescending(e => e.AvailableQuantityTickets).ToList();
        }

        /// <summary>
        /// Метод сортирует мероприятия по количеству проданных билетов
        /// </summary>
        /// <param name="ascending"></param>
        public void SortBySoldTickets(bool ascending)
        {
            _sortedEvents = ascending ? _sortedEvents.OrderBy(e => e.SoldTickets).ToList() : _sortedEvents.OrderByDescending(e => e.SoldTickets).ToList();
        }

        public IEnumerator<Event> GetEnumerator()
        {
            return _sortedEvents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}