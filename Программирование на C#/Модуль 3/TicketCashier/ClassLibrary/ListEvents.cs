using System.Collections;
namespace ClassLibrary
{
    public class ListEvents : IEnumerable<Event>
    {
        private List<Event> _events;

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public ListEvents()
        {
            _events = new List<Event>();
        }

        /// <summary>
        /// Конструктор с параметром 
        /// </summary>
        /// <param name="events"></param>
        public ListEvents(IEnumerable<Event> events)
        {
            _events = events.ToList();
        }

        /// <summary>
        /// Индексатор 
        /// </summary>
        /// <param name="index"></param>
        public Event this[int index] => _events[index];

        /// <summary>
        /// Метод возвращает количсетво жлементов в списке 
        /// </summary>
        public int Count => _events.Count;

        /// <summary>
        /// Метод добавляет событие 
        /// </summary>
        /// <param name="event"></param>
        public void AddEvent(Event @event)
        {
            _events.Add(@event);
        }
        
        /// <summary>
        /// Метод удаляет событие 
        /// </summary>
        /// <param name="event"></param>
        public void RemoveEvent(Event @event)
        {
            _events.Remove(@event);
        }

        public IEnumerator<Event> GetEnumerator()
        {
            return _events.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}