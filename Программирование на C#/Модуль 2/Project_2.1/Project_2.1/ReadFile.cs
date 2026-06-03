namespace Project_2._1;

/// <summary>
/// Класс предназначенный для чтения данных из файла
/// </summary>
public class ReadFile
{
    private string[][] _arrayData; // Приватное поле для хранения данных из файла 

    /// <summary>
    /// Конструктор для инициализации поля 
    /// </summary>
    /// <param name="namePath"></param>
    public ReadFile(string namePath)
    {
        _arrayData = ReadData(namePath);
    }
    
    /// <summary>
    /// Свойство для получения значения _arrayData
    /// </summary>
    public string[][] DataArray { get => _arrayData; }
    
    /// <summary>
    /// Метод читает данные из файла и создает массив массивов, в котором они хранятся 
    /// </summary>
    /// <param name="namePath"></param>
    private string[][] ReadData(string namePath) 
    {
        try
        {
            string[] readData = File.ReadAllLines(namePath);
            string[][] formattedArray = new string[readData.Length - 1][];

            int firstBadSimvol; // Первое вхождение кавычки 
            int lastBadSimvol; // Последнее вхождение кавычки 
            
            // Метод убирает лишние запятые и кавычки
            for (int i = 1; i < readData.Length; i++)
            {
                firstBadSimvol = readData[i].IndexOf('"') + 1;
                lastBadSimvol = readData[i].LastIndexOf('"') + 1;
                readData[i] = readData[i][..readData[i].IndexOf('"')] +
                              readData[i][firstBadSimvol..readData[i].LastIndexOf('"')].Replace(',', ' ') +
                              readData[i][lastBadSimvol..];
            }

            for (int i = 1; i < readData.Length; i++)
            {
                formattedArray[i - 1] = readData[i].Split(',');
            }

            // Замена точки на запятую в ячейки Salary_In_Rupees, чтобы потом перевести в double
            for (int i = 0; i < formattedArray.GetLength(0); i++)
            {
                formattedArray[i][5] = formattedArray[i][5].Replace('.', ',');
            }

            return formattedArray;
        }
        catch (Exception)
        {
            throw new Exception($"Проблемы с чтением файла {namePath}.");
        }
    }
}