using System.Text;

namespace Project_2._1;

/// <summary>
/// Класс для решения дополнительной задачи (сортировка по году)
/// </summary>
public class AdditionalTask
{
    private string _groupsForConsole; // Приватное поле для хранения данных, выводимых в консоль 
    private string _groupsForFile; // Приватное поле для хранения данных, записанных в файл 
    private Employee _em;

    /// <summary>
    /// Конструктор для инициализации полей 
    /// </summary>
    /// <param name="arrayData"></param>
    public AdditionalTask(string[][] arrayData)
    {
        _groupsForConsole = GetGroupsForConsole(arrayData);
        _groupsForFile = GetGroupsForFile(arrayData);
    }
    
    /// <summary>
    /// Свойство для вывода данных, предназначенных для консоли 
    /// </summary>
    public string GroupsForConsole
    {
        get => _groupsForConsole;
    }
    
    /// <summary>
    /// Метод для получения данных, выводимых на консоль 
    /// </summary>
    /// <param name="arrayData"></param>
    /// <returns></returns>
    private string GetGroupsForConsole(string[][] arrayData)
    {
        int[] years = new int[arrayData.Length];
        for (int i = 0; i < arrayData.GetLength(0); i++)
        {
            _em = new Employee(arrayData[i]);
            years[i] = _em.WorkingYear;
        }
        
        int[] uniqueYears = years.Distinct().ToArray();
        Array.Sort(uniqueYears);
        
        string[] salaryRange = new string[uniqueYears.Length];
        double maxSalary = 0;
        double minSalary = 1000000000;

        for (int i = 0; i < uniqueYears.Length; i++)
        {
            for (int j = 0; j < arrayData.Length; j++)
            {
                _em = new Employee(arrayData[j]);
                if (_em.WorkingYear == uniqueYears[i])
                {
                    maxSalary = Math.Max(maxSalary, _em.SalaryInRupees);
                    minSalary = Math.Min(minSalary, _em.SalaryInRupees);
                }
            }

            salaryRange[i] = $"{minSalary} - {maxSalary}";
            maxSalary = 0;
            minSalary = 1000000000;
        }
        StringBuilder result = new StringBuilder();
        
        for (int i = 0; i < uniqueYears.Length; i++)
        {
            result.Append($"{uniqueYears[i]}: Диапазон зарплат: {salaryRange[i]}\n");
            for (int j = 0; j < arrayData.GetLength(0); j++)
            {
                _em = new Employee(arrayData[j]);
                if (_em.WorkingYear == uniqueYears[i])
                {
                    result.Append(_em.EmployeeData + $"\n");
                }
            }
        }
        return result.ToString();
    }

    /// <summary>
    /// Метод для получения данных, записываемых в файл 
    /// </summary>
    /// <param name="arrayData"></param>
    /// <returns></returns>
    private string GetGroupsForFile(string[][] arrayData)
    {
        int[] listRemoteWorkingRatio = new int[arrayData.Length];

        for (int i = 0; i < arrayData.GetLength(0); i++)
        {
            _em = new Employee(arrayData[i]);
            listRemoteWorkingRatio[i] = _em.RemoteWorkingRatio;
        }
        
        int[] uniqueRemoteWorkingRatio = listRemoteWorkingRatio.Distinct().ToArray();
        Array.Sort(uniqueRemoteWorkingRatio);
        string[][] sortedArray = new string[arrayData.Length][];
        int index = 0;

        for (int i = 0; i < uniqueRemoteWorkingRatio.Length; i++)
        {
            for (int j = 0; j < arrayData.GetLength(0); j++)
            {
                _em = new Employee(arrayData[j]);
                if (_em.RemoteWorkingRatio == uniqueRemoteWorkingRatio[i])
                {
                    sortedArray[index] = arrayData[j];
                    index++;
                }
            }
        }
        
        int[] years = new int[sortedArray.Length];
        
        for (int i = 0; i < sortedArray.GetLength(0); i++)
        {
            _em = new Employee(sortedArray[i]);
            years[i] = _em.WorkingYear;
        }
        
        int[] uniqueYears = years.Distinct().ToArray();
        Array.Sort(uniqueYears);
        
        string[] salaryRange = new string[uniqueYears.Length];
        double maxSalary = 0;
        double minSalary = 1000000000;

        for (int i = 0; i < uniqueYears.Length; i++)
        {
            for (int j = 0; j < sortedArray.Length; j++)
            {
                _em = new Employee(sortedArray[j]);
                if (_em.WorkingYear == uniqueYears[i])
                {
                    maxSalary = Math.Max(maxSalary, _em.SalaryInRupees);
                    minSalary = Math.Min(minSalary, _em.SalaryInRupees);
                }
            }

            salaryRange[i] = $"{minSalary} - {maxSalary}";
            maxSalary = 0;
            minSalary = 1000000000;
        }
        
        StringBuilder result = new StringBuilder();
        WriteNewFile wr = new WriteNewFile();
        
        for (int i = 0; i < uniqueYears.Length; i++)
        {
            result.Append($"{uniqueYears[i]}: Диапазон зарплат: {salaryRange[i]}\n");
            for (int j = 0; j < arrayData.GetLength(0); j++)
            {
                _em = new Employee(sortedArray[j]);
                if (_em.WorkingYear == uniqueYears[i])
                {
                    result.Append(_em.EmployeeData + $"\n");
                }
            }
            wr.WriteEmployeesN(result.ToString(), uniqueYears[i]); // Записываем данные в файл 
        }
        return result.ToString();
    }
}