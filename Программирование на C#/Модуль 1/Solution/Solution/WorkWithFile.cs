namespace Solution;

using System.Text;
using System.IO;

/// <summary>
/// Класс, в котором хронятся все методы для решения основной задачи
/// </summary>
public static class WorkWithFile
{ 
    /// <summary>
    /// Метод читатет данные из файла, обрабоатывает их и возвращает вещественный массив масивов
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="FileNotFoundException"></exception>
    /// <exception cref="IOException"></exception>
    public static double[][] ReadFile(string fileName)
    {
        try
        {
            string[] lines = File.ReadAllLines(fileName); //Массив всех строк, содержащихся в файле 
            //Проверка на корректность файла, он должен состоять из 2-х строк
            if (lines.Length == 2)
            {
                string[][] fileData = new string[2][]; //Массив, в котором будут хрониться все значаения из файла
                fileData[0] = lines[0].Split(';');
                fileData[1] = lines[1].Split(';');
                double[][] processedData = new double[2][]; //Обработанный массив, котором будут хрониться все вещественные значения из файла
                //Цикл для определения размера массивов processedData[0] и processedData[1]
                for (int i = 0; i < fileData.GetLength(0); i++)
                {
                    int cntGoodvalues = 0;//Переменная для подсчета вещественных значений из одной строки файла 
                    for (int j = 0; j < fileData[i].Length; j++)
                    {
                        if (double.TryParse(fileData[i][j], out _))
                        {
                            cntGoodvalues++;
                        }
                    }

                    processedData[i] = new double[cntGoodvalues];
                }
                //Цикл заполняет массив вещественными заначениями из файла  
                for (int i = 0; i < fileData.GetLength(0); i++)
                {
                    int indexInProcessedData = 0; //Переменная для запоминания последнего заполненного индекса в массиве processedData
                    for (int j = 0; j < fileData[i].Length; j++)
                    {
                        if (double.TryParse(fileData[i][j], out _))
                        {
                            processedData[i][indexInProcessedData] = double.Parse(fileData[i][j]);
                            indexInProcessedData++;
                        }
                    }
                }

                return processedData;
            }
            else
            {
                throw new Exception("Проблемы с чтением данных из файла.");
            }
        }
        catch (FileNotFoundException)
        {
            throw new FileNotFoundException("Входной файл на диске отсутствует.");
        }
        catch (IOException)
        {
            throw new IOException("Проблемы с открытием файла.");
        }
        catch (Exception)
        {
           throw new Exception("Проблемы с чтением данных из файла.");
        }
    }

    /// <summary>
    /// Метод предназначен для поиска максимальной разницы между массивами
    /// </summary>
    /// <param name="processedData"></param>
    /// <returns></returns>
    public static double FindMaxDifference(double[][] processedData)
    {
        double D1 = 0; //Переменная для хранения максимальной разницы
        //Цикл проходится по значениям из двух массивов, сравнивает их разницу и максимальное значение присваивает D1
        for (int i = 0; i < processedData[0].Length; i++)
        {
            for (int j = 0; j < processedData[1].Length; j++)
            {
                D1 = Math.Max(D1, Math.Abs(processedData[0][i] - processedData[1][j]));
            }
        }

        return D1;
    }

    /// <summary>
    /// Метод предназначен для поиска минимальной разницы между массивами
    /// </summary>
    /// <param name="processedData"></param>
    /// <returns></returns>
    public static double FindMinDifference(double[][] processedData)
    {
        double D2 = Double.MaxValue; //Переменная для хранения минимальной разницы
        //Цикл проходится по значениям из двух массивов, сравнивает их разницу и минимальное занчение присваивает D2
        for (int i = 0; i < processedData[0].Length; i++)
        {
            for (int j = 0; j < processedData[1].Length; j++)
            {
                    D2 = Math.Min(D2, Math.Abs(processedData[0][i] - processedData[1][j]));
            }
        }
        return D2;
    }
    
    /// <summary>
    /// Метод, который записывает значения D1 и D2 в файл output.txt 
    /// </summary>
    /// <param name="D1"></param>
    /// <param name="D2"></param>
    /// <exception cref="Exception"></exception>
    public static void WriteNewFile(double D1, double D2)
    {
        try
        {
            string pathOutput = @"output.txt"; //Путь файла output.txt (хрониться в папке на одном уровне с запускаемым проектом)
            string outputLine = $"{D1:f3} {D2:f3}";
            File.WriteAllText(pathOutput, outputLine, Encoding.UTF8);
        }
        catch (Exception)
        {
            throw new Exception("Проблемы с записью данных в файл");
        }
    }
    
    /// <summary>
    /// Метод, который перемещает файл input.txt в папку, где хронится output.txt
    /// </summary>
    /// <param name="pathFile"></param>
    /// <exception cref="IOException"></exception>
    public static void MoveFile(string pathFile)
    {
        try
        {
            File.Move(pathFile, "input.txt");
        }
        catch (IOException)
        {
            throw new IOException("Не удалаось переместить файл input.txt.");
        }
    }
}