namespace Project_2._1;

/// <summary>
/// Класс для проверки структуры файла 
/// </summary>
public class FileStructure
{
    /// <summary>
    /// Метод для проверки структуры данных 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public string[][] CheckFileStructure(string path)
    {
        try
        {
            ReadFile data = new ReadFile(path);
            string[][] arrayData = data.DataArray; // Читаем файл 
            Employee em;
            
            for (int i = 0; i < arrayData.GetLength(0); i++)
            {
                em = new Employee(arrayData[i]);
            }
            return arrayData;
        }
        catch (Exception)
        {
            throw new Exception($"Структура файла {path} не совпадает со структурой файла Data_Science_Fields_Salary_Categorization.csv.\n" +
                                "Можете попробовать снова.");
            
        } 
    }
}