using System.Text;

namespace Project_2._1;

/// <summary>
/// Класс для записи файлов
/// </summary>
public class WriteNewFile
{
    /// <summary>
    /// Метод для записи файла, в котором хранятся, отсортированные по опыту данные
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public void WriteEmployees(string text)
    {
        try
        {
            string pathOutput = @"employees.csv";
            File.WriteAllText(pathOutput, text, Encoding.UTF8);
            Console.WriteLine($"Файл {pathOutput} успешно записан!");
        }
        catch (Exception)
        {
            throw new Exception("Проблемы с записью файла employees.csv.");
        }
    }

    /// <summary>
    /// Метод для записи файла, в котором хранятся данные о работниках с зарплатой от 70% до 80% от максимальной
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public void WriteSalary7080Employees(string text)
    {
        try
        {
            string pathOutput = "Salary7080-employees.csv";
            File.WriteAllText(pathOutput, text, Encoding.UTF8);
            Console.WriteLine($"Файл {pathOutput} успешно записан!");
        }
        catch (Exception)
        {
            throw new Exception("Проблемы с записью файла Salary7080-employees.csv.");
        }
    }

    /// <summary>
    /// Метод для записи файла, в котором хранятся отсортированные данный по году 
    /// </summary>
    /// <param name="text"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public void WriteEmployeesN(string text, int year)
    {
        try
        {
            string pathOutput = $"Employees-{year}.csv";
            File.WriteAllText(pathOutput, text, Encoding.UTF8);
            Console.WriteLine($"Файл {pathOutput} успешно записан!");
        }
        catch (Exception)
        {
            throw new Exception($"Проблемы с записью файла Employees-{year}.csv.");
        }
    }
    
}