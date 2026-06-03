/*
 * Дисциплина: "Программирование на С#"
 * Группа: БПИ244
 * Студент: Яровицына Наталья
 * Дата: 19.10.2024
 * Задача: Обработать данные из файла input.txt, найти максимальную и минимальную разничу между числами из первой и
 * второй строки файла и записать их в файл output.txt и файл output(номер).txt
 */
namespace Solution;

using System;

static class Program
{
    static void Main()
    {
        Console.WriteLine("Добро пожаловать в программу!");
        ConsoleKeyInfo keyToExit; //Переменная, чтобы определить прекращение действия программы 
        do
        {
            try
            {
                Console.Write("Введите путь файла: ");
                string pathInput = @"" + Console.ReadLine(); //Путь файла input.txt
                double[][] processedData = WorkWithFile.ReadFile(pathInput); //Вызываем метод для чтения и обработки данных файла input.txt, которые хронятся в данном массиве массивов
                //Проверка на корректность данных в фале (количество вещественных значений в первой строке должно быть равно количеству вещественных значений во второй строке файла)
                if (processedData[0].Length == processedData[1].Length && processedData[0].Length != 0)
                {
                    double D1 = WorkWithFile.FindMaxDifference(processedData); //Переменная, в которой хронится максимальная разница 
                    double D2 = WorkWithFile.FindMinDifference(processedData); //Переменная, в которой хронится минимальная разница 
                    WorkWithFile.WriteNewFile(D1, D2); //Записываем D1 и D2 в файл output.txt
                    WorkWithFile.MoveFile(pathInput); //Перемещаем файл input.txt в папку, где лежит output.txt
                    Console.WriteLine("Файл input.txt успешно перемещен в папке с запускаемым файлом проекта!");
                    //Дополнительная задача:
                    int nomberOutput = AdditionalTask.ReadNomber(); //Номер последнего output(номер).txt
                    AdditionalTask.CreatNewFile(D1, D2, nomberOutput); //Создаем новый файл output(текущий номер).txt
                    Console.WriteLine("Файл output.txt успешно записан!");
                }
                else
                {
                    Console.WriteLine("Данные, содержащиеся в файле не подлежат обработке!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Если хотите завершить программу нажмите Escape.");
                keyToExit = Console.ReadKey();
            }
        } while (keyToExit.Key != ConsoleKey.Escape); //Программа завершает работу при нажатии кдавиши Escape
    }
}