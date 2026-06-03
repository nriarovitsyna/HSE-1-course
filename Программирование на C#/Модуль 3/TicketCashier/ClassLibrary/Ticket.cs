using QRCoder;
namespace ClassLibrary
{
    public class Ticket
    {
        public string Id { get; }

        public string Name { get; }

        private DateTime Date { get; }

        private string Location { get; }

        public double Price { get; }

        public int Count { get; }

        private string PathQrCode { get; }

        public DateTime ReservationDate { get; set; }
        
        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="event"></param>
        /// <param name="count"></param>
        public Ticket(Event @event, int count)
        {
            Id = Guid.NewGuid().ToString();
            Name = @event.Name;
            Date = @event.Date;
            Location = @event.Location;
            Price = @event.Price;
            Count = count;
            PathQrCode = $"../../../../{Id}.png";
        }

        /// <summary>
        /// Метод вывод в консоль электронный билет 
        /// </summary>
        public void ShowTicket()
        {
            Console.WriteLine("Электронный билет:");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Название мероприятия: {Name}");
            Console.WriteLine($"Дата и время: {Date:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Место проведения: {Location}");
            Console.WriteLine($"Количество билетов: {Count}");
            Console.WriteLine($"Уникальный номер билета: {Id}");
        }

        /// <summary>
        /// Метод генерирует QRкод
        /// </summary>
        public void GenerateQRCode()
        {
            string text = $"Номер билета: {Id}";

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q))
            using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
            {
                File.WriteAllBytes(PathQrCode, qrCode.GetGraphic(10));
                Console.WriteLine($"QRкод находится в папке проекта");
            }
        }
    }
}