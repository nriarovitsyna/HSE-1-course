namespace ClassLibrary
{ 
    public class Event
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int AvailableQuantityTickets { get; set; }
        public int SoldTickets { get; set; }

        public override string ToString()
        {
            return $"[{Id}] [{Name}] [{Category}] [{Date}] [{Location}] [{Description}] [{Price}] [{AvailableQuantityTickets}] [{SoldTickets}]";
        }
    }
}