using System.Text;

namespace Project_2._1;

/// <summary>
/// Класс для получения данных о работниках с зарплатой от 70% до 80% от максимальной
/// </summary>
public class HighlyPaidEmployee
{
    private string _highlyPaidWorkers; // Приватное поле для хранения данных о работниках с зарплатой от 70% до 80% от максимальной
    private Employee _em;

    /// <summary>
    /// Конструктор для инициализации поля 
    /// </summary>
    /// <param name="arrayData"></param>
    public HighlyPaidEmployee(string[][] arrayData)
    {
        _highlyPaidWorkers = GetHighlyPaidWorkers(arrayData);
    }

    /// <summary>
    /// Свойство для вывода данных
    /// </summary>
    public string HighlyPaidWorkers
    {
        get => _highlyPaidWorkers;
    }
    
    /// <summary>
    /// Получение данных о работниках с зарплатой от 70% до 80% от максимальной
    /// </summary>
    /// <param name="arrayData"></param>
    /// <returns></returns>
    private string GetHighlyPaidWorkers(string[][] arrayData)
    {
        double maxSalary = 0;
        
        for (int i = 0; i < arrayData.GetLength(0); i++)
        {
            _em = new Employee(arrayData[i]);
            maxSalary = Math.Max(maxSalary, _em.SalaryInRupees);
        }
        
        StringBuilder highlyPaidWorkers = new StringBuilder();
        
        for (int i = 0; i < arrayData.GetLength(0); i++)
        {
            _em = new Employee(arrayData[i]);
            if (_em.SalaryInRupees >= maxSalary * 70 / 100 && _em.SalaryInRupees <= maxSalary * 80 / 100)
            {
                highlyPaidWorkers.Append(_em.EmployeeData + $"\n");
            }
        }
        
        WriteNewFile wr = new WriteNewFile();
        wr.WriteSalary7080Employees(highlyPaidWorkers.ToString()); // Запись данных в файл 
        return highlyPaidWorkers.ToString();
    }
}