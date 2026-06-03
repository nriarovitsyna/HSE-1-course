using System.Text;

namespace Project_2._1;

/// <summary>
/// Класс для получения общей статистики 
/// </summary>
public class Statistics
{
    private int _cntEmployee; // Приватное поле, в котором хранится кол-во строк в файле 
    private int _employeeWithMaxSalaries; // Приватное поле, в котором хранится кол-во работников с наибольшей зарплатой
    private int _employeeWithMinSalaries; // Приватное поле, в котором хранится кол-во работников с наименьшей зарплатой
    private int _dataEngineerLocationGB; // Приватное поле, в котором хранится кол-во работников, работающих из GB
    private string _employeeWithCompanyLocationGB; // Приватное поле, в котором хранится кол-во работников, работающих в компаниях GB, но не находящихся там
    private string _employeeStatistics; // Приватное поле, в котором хранится вся статистика 
    private Employee _em;

    /// <summary>
    /// Конструктор для инициализации полей 
    /// </summary>
    /// <param name="arrayData"></param>
    public Statistics(string[][] arrayData)
    {
        _cntEmployee = arrayData.GetLength(0);
        _employeeWithMaxSalaries = GetEmployeeWithMaxSalaries(arrayData);
        _employeeWithMinSalaries = GetEmployeeWithMinSalaries(arrayData);
        _dataEngineerLocationGB = GetdDataEngineerLocationGB(arrayData);
        _employeeWithCompanyLocationGB = GetEmployeeWithCompanyLocationGB(arrayData);
        _employeeStatistics = $"Общее количество строк с данными: {_cntEmployee}\n" +
                              $"Работники с наибольшей зарплатой: {_employeeWithMaxSalaries}\n" +
                              $"Работники с наименьшей зарплатой: {_employeeWithMinSalaries}\n" +
                              $"Количество Data Engineer работающих из Великобритании: {_dataEngineerLocationGB}\n" +
                              $"Количество работников, работающих в компаниях из Великобритании, но работающих из иной страны:\n" +
                              _employeeWithCompanyLocationGB;
    }
    
    /// <summary>
    /// Свойство для получения данных об общей статистики 
    /// </summary>
    /// <returns></returns>
    public string EmployeeStatistics() => _employeeStatistics;
    
    /// <summary>
    /// Метод для получения _employeeWithMaxSalaries
    /// </summary>
    /// <param name="arrayData"></param>
    /// <returns></returns>
    private int GetEmployeeWithMaxSalaries(string[][] arrayData)
    {
        double maxSalary = 0;
        
        for (int i = 0; i < arrayData.GetLength(0); i++)
        {
            _em = new Employee(arrayData[i]);
            maxSalary = Math.Max(maxSalary, _em.SalaryInRupees);
        }
        
        int employeeWithMaxSalaries = 0;

        for (int i = 0; i < arrayData.GetLength(0); i++)
        {
            _em = new Employee(arrayData[i]);
            if (maxSalary == _em.SalaryInRupees)
            {
                employeeWithMaxSalaries++;
            }
        }
        return employeeWithMaxSalaries;
    }
    
    /// <summary>
    /// Метод для получения _employeeWithMinSalaries
    /// </summary>
    /// <param name="arrayData"></param>
    /// <returns></returns>
    private int GetEmployeeWithMinSalaries(string[][] arrayData)
    {
        double minSalary = 100000000;
        
        for (int i = 0; i < arrayData.GetLength(0); i++)
        {
            _em = new Employee(arrayData[i]);
            minSalary = Math.Min(minSalary, _em.SalaryInRupees);
        }
        
        int employeeWithMinSalaries = 0;

        for (int i = 0; i < arrayData.GetLength(0); i++)
        {
            _em = new Employee(arrayData[i]);
            if (minSalary == _em.SalaryInRupees)
            {
                employeeWithMinSalaries++;
            }
        }
        return employeeWithMinSalaries;
    }

    /// <summary>
    /// Метод для получения _dataEngineerLocationGB
    /// </summary>
    /// <param name="arrayData"></param>
    /// <returns></returns>
    private int GetdDataEngineerLocationGB(string[][] arrayData)
    {
        int dataEngineer = 0;

        for (int i = 0; i < arrayData.GetLength(0); i++)
        {
            _em = new Employee(arrayData[i]);
            if (_em.Designation == "Data Engineer" && _em.EmployeeLocation == "GB")
            {
                dataEngineer++;
            }
        }
        return dataEngineer;
    }

    /// <summary>
    /// Метод для получения _employeeWithCompanyLocationGB
    /// </summary>
    /// <param name="arrayData"></param>
    /// <returns></returns>
    private string GetEmployeeWithCompanyLocationGB(string[][] arrayData)
    {
        StringBuilder employeeWithCompanyLocationGB = new StringBuilder();
        int cntEmployee = 0;
        for (int i = 0; i < arrayData.GetLength(0); i++)
        {
            _em = new Employee(arrayData[i]);
            if (_em.CompanyLocation == "GB" && _em.EmployeeLocation != "GB")
            {
               cntEmployee++;
               employeeWithCompanyLocationGB.Append($"{cntEmployee}: {_em.EmployeeLocation}\n");
            }
        }
        return employeeWithCompanyLocationGB.ToString();
    }
}