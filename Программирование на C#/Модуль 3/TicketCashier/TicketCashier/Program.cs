// Яровицына Наталья БПИ 244 Вариант 6 В-side
using ClassLibrary.ConsoleMenu;
namespace TicketCashier
{
    internal static class Program
    {
        private static void Main()
        {
            try
            {
                Menu.ExecuteUserChoice();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}