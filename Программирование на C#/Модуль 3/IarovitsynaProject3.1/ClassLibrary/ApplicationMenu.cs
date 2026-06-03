namespace ClassLibrary;

/// <summary>
/// Статический класс, реализующий меню приложения 
/// </summary>
public static class ApplicationMenu
{
    /// <summary>
    /// Метод реализует меню приложения 
    /// </summary>
    /// <exception cref="Exception"></exception>
    public static void Menu()
    {
        List<Storage> storages = new List<Storage>();
        string? choose = null;
        do
        {
            try
            {
                Console.WriteLine("1. Ввести данные (консоль/файл);\n2. Отфильтровать данные;\n3. Отсортировать данные;\n4. Вывести данные о хранилищах;\n5. Работа с Exel файлом;\n6. Вывести данные (консоль/файл);\n7. Выход;");
                Console.Write("Введите выбранный пункт: ");
                choose = Console.ReadLine();
                switch (choose)
                {
                    case "1":
                        storages = new List<Storage>();
                        Console.WriteLine("1. Читать данные из файла;\n2. Ввести данные в консоль;");
                        Console.Write("Введите выбранный пункт: ");
                        choose = Console.ReadLine();
                        switch (choose)
                        {
                            case "1":
                                Console.Write("Введите полный путь к файлу в формате Json: ");
                                string pathJson = Console.ReadLine();
                                while (!File.Exists(pathJson) || !pathJson.Contains("vaults.json"))
                                {
                                    Console.WriteLine("Путь файла введен некорректно");
                                    Console.Write("Введите полный путь к файлу в формате Json: ");
                                    pathJson = Console.ReadLine();
                                }

                                try
                                {
                                    using (StreamReader sr = new StreamReader(pathJson))
                                    {
                                        string? line;
                                        while ((line = sr.ReadLine()) != null)
                                        {
                                            JsonParser.ReadJson(line, storages);
                                        }
                                    }
                                }
                                catch
                                {
                                    throw new Exception("Файл закрыт для чтения");
                                }
                                break;
                            case "2":
                                using (Stream inputStream = Console.OpenStandardInput())
                                using (StreamReader sr = new StreamReader(inputStream))
                                {
                                    Console.WriteLine("Введите данные в формате Json (в конце кода напишите \"end\"):");
                                    string? input = sr.ReadLine();
                                    while (input != "end")
                                    {
                                        JsonParser.ReadJson(input, storages);
                                        input = sr.ReadLine();
                                    }
                                }
                                break;
                        }
                        break;
                    case "2":
                        if (storages.Count != 0) storages = WorkWithData.DataFiltering(storages); 
                        else Console.WriteLine("Фильтрация невозможна, введите сначала данные");
                        break;
                    case "3":
                        if (storages.Count != 0) storages = WorkWithData.SortingData(storages);
                        else Console.WriteLine("Сортировка невозможна, введите сначала данные");
                        break;
                    case "4":
                        if (storages.Count != 0) WorkWithData.ViewStorageInformation(storages);
                        else Console.WriteLine("Невозможно посмотреть информацию о хранилищах, введите сначала данные");
                        break;
                    case "5":
                        Console.WriteLine("1. Читать данные из файла;\n2. Ввести данные в файл;");
                        Console.Write("Введите выбранный пункт: ");
                        choose = Console.ReadLine();
                        switch (choose)
                        {
                            case "1":
                                Console.Write("Введите путь к Exel файлу: ");
                                string? pathExel = Console.ReadLine();
                                while (!File.Exists(pathExel) || !pathExel.Contains(".xlsx"))
                                {
                                    Console.WriteLine("Путь файла не корректен");
                                    Console.Write("Введите путь к Exel файлу: ");
                                    pathExel = Console.ReadLine();
                                }
                                storages = AdditionalTask.ReadExcel(pathExel);
                                break;
                            case "2":
                                if (storages.Count != 0) AdditionalTask.WriteExcel(storages);
                                else Console.WriteLine("Невозможно записать данные в Exel файл, введите сначала данные");
                                break;
                        }
                        break;
                    case "6":
                        if (storages.Count != 0)
                        {
                            Console.WriteLine("1. Вывести данные в файл;\n2. Вывести данные в консоль;");
                            Console.Write("Введите выбранный пункт: ");
                            choose = Console.ReadLine();
                            switch (choose)
                            {
                                case "1":
                                    Console.Write("Введите полный путь к файлу в формате Json: ");
                                    string? pathJson = Console.ReadLine();
                                    while (!pathJson.EndsWith(".json"))
                                    {
                                        Console.WriteLine($"Название {pathJson} некорректно (ошибка в расширении)");
                                        Console.Write("Введите имя файла с расширением .json: ");
                                        pathJson = Console.ReadLine();
                                    }
                                    string outputData = JsonParser.WriteJson(storages);
                                    try
                                    {
                                        using (StreamWriter writer = new StreamWriter(pathJson))
                                        {
                                            writer.Write(outputData);
                                        }
                                    }
                                    catch
                                    {
                                        throw new Exception("Файл закрыт для записи");
                                    }

                                    break;
                                case "2":
                                    outputData = JsonParser.WriteJson(storages);
                                    using (Stream outputStream = Console.OpenStandardOutput())
                                    using (StreamWriter sw = new StreamWriter(outputStream))
                                    {
                                        sw.AutoFlush = true; 
                                        sw.WriteLine(outputData);
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Невозможно вывести данные, сначала введите данные");
                        }
                        break;
                    case "7": break;
                    default:
                        Console.WriteLine("Неверный ввод данных");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }while (choose != "7");
    }
}