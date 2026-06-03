using System.Text;

namespace Project_2._1;

/// <summary>
/// Класс для получения данных, отсортированных по опыту 
/// </summary>
public class Information
{
    private string _experienceGroup; // Приватное поле, в котором хранятся отсортированные данные 

    /// <summary>
    /// Конструктор для инициализации приватного поля 
    /// </summary>
    /// <param name="arrayData"></param>
    /// <param name="experienceGroup"></param>
    public Information(string[][] arrayData, string experienceGroup)
    {
        _experienceGroup = SortGroupsByExperience(arrayData, experienceGroup);
    }

    /// <summary>
    /// Свойство, для получения данных из приватного поля 
    /// </summary>
    public string ExperienceGroup
    {
        get => _experienceGroup;
    }

    /// <summary>
    /// Метод сортирует данные из массива массивов по году 
    /// </summary>
    /// <param name="arrayData"></param>
    /// <param name="experienceGroup"></param>
    /// <returns></returns>
    private string SortGroupsByExperience(string[][] arrayData, string experienceGroup)
    {
        // Проверка на корректность входных данных
        if ("MI SE EN EX".Contains(experienceGroup))
        {
            Employee em;
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < arrayData.Length; i++)
            {
                em = new Employee(arrayData[i]);
                if (experienceGroup == Convert.ToString(em.Experience))
                {
                    result.Append(Convert.ToString(em.EmployeeData) + $"\n");
                }
            }
            WriteNewFile wr = new WriteNewFile();
            wr.WriteEmployees(result.ToString());
            return result.ToString();
        }
        return "Такого уровня опыта не существует!";
    }
}