namespace Solution;

using System.Text;
using System.IO;

/// <summary>
/// Класс, в котором хроняться все методы для решения дополнительной задачи 
/// </summary>
public static class AdditionalTask
{ 
    
    /// <summary>
    /// Метод, в котором читается последний номер фалйла output(номер).txt из файла config.txt
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static int ReadNomber()
    {
        try
        {
            int nomber; //Переменная, в которой будет хронится текущий номер файла output.txt
            string pathConfig = "../../../../config.txt"; //Путь файла config.txt
            //Проверка на наличие файла config.txt
            if (File.Exists(pathConfig))
            {
                string[] dataConfig = File.ReadAllLines(pathConfig); //Массив, в котором хрониться номер последнего файла output.txt
                nomber = int.Parse(dataConfig[0]) + 1;
                File.WriteAllText(pathConfig, $"{nomber}"); //Записывает текущий номер файла output.txt в файл config.txt
            }
            else
            {
                nomber = 1; //Если файла config.txt не сущетсвет, значит тукущий номер output.txt - 1
                File.WriteAllText(pathConfig, $"{nomber}");
            }

            return nomber;
        }
        catch (Exception)
        {
            throw new Exception("Проблемы с чтением данных из файл");
        }
    }
    
    /// <summary>
    /// Метод для записи файла output(номер).txt
    /// </summary>
    /// <param name="D1"></param>
    /// <param name="D2"></param>
    /// <param name="n"></param>
    /// <exception cref="IOException"></exception>
    public static void CreatNewFile(double D1, double D2, int n)
    {
        try
        {
            string pathOutput = $@"output-{n}.txt"; //Путь файла output(номер).txt, хронится в одной папке с input.txt и output.txt
            string outputLine = $"{D1:f3} {D2:f3}";
            File.WriteAllText(pathOutput, outputLine, Encoding.UTF8); //Записываем максимальню и минамальную разницу в output(номер).txt
            Console.WriteLine($"Файл {pathOutput} успешно записан!");
        }
        catch (IOException)
        {
            throw new IOException("Проблемы с записью данных в файл");
        }
    }
}