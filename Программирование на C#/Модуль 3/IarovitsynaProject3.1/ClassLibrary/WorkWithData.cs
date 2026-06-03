namespace ClassLibrary;

/// <summary>
/// Статический класс для работы с данным
/// </summary>
public static class WorkWithData
{
    /// <summary>
    /// Основная задача: метод выводит данные о хранилище
    /// </summary>
    /// <param name="storages"></param>
    public static void ViewStorageInformation(List<Storage> storages)
    {
        Console.Write("Введите id хранилища, информацию о котором вы бы хотели посмотреть: ");
        string? inputId = Console.ReadLine();
        bool check = false;
        foreach (Storage storage in storages)
        {
            if (storage.GetField("id") == inputId)
            {
                check = true;
                Console.WriteLine("Xранилище: " + storage.GetField("label") + " (" + storage.GetField("id") + ")");
                Console.WriteLine();
                Console.WriteLine("Аспекты:\n" + storage.GetField("aspects"));
                Console.WriteLine();
                Console.WriteLine("Слоты для экспедиции:");
                for (int i = 0; i < storage.SlotCount; i++)
                {
                    Console.WriteLine("   " + storage.SlotGet(i).GetField("label") + ": " + storage.SlotGet(i).GetField("description"));
                    Console.WriteLine("      Требуемые аспекты:");
                    Console.WriteLine("         " + storage.SlotGet(i).GetField("required"));
                }
            }
        }
        if (check == false)
        {
            Console.WriteLine("Нет хранилища с таким id");
        }
    }
    
    /// <summary>
    /// Метод для фильтрации данных
    /// </summary>
    /// <param name="storages"></param>
    /// <returns></returns>
    public static List<Storage> DataFiltering(List<Storage> storages)
    {
        string? inputField;

        do
        {
            Console.Write("Напишите поле, по которому будет осуществляться фильтрация: ");
            inputField = Console.ReadLine();
        } while (!storages[0].GetAllFields().Contains(inputField));
        
        Console.Write("Напишите значение поля, по которому будет осуществляться фильтрация: ");
        string? inputValue = Console.ReadLine();
        
        List<Storage> filteredStorages = new List<Storage>();
        
        foreach (Storage storage in storages)
        {
            if (storage.GetField(inputField) == inputValue)
            {
                filteredStorages.Add(storage);
            }
        }

        if (filteredStorages.Count == 0)
        {
                Console.WriteLine($"Не существует хранилищ с со значением {inputValue} в поле {inputField}");
                return storages;
        }
        return filteredStorages;
    }
    
    /// <summary>
    /// Метод для сортировки данных
    /// </summary>
    /// <param name="storages"></param>
    /// <returns></returns>
    public static List<Storage> SortingData(List<Storage> storages)
    {
        string? inputField;
        string? inputCriterion;
        do
        {
            Console.Write("Напишите поле, по которому будет осуществляться сортировка: ");
            inputField = Console.ReadLine();
        } while (!storages[0].GetAllFields().Contains(inputField));
        
        do
        {
            Console.WriteLine("Выберите критерий, по которому будет осуществляться фильтрация:\n1. По возрастанию\n2. По убыванию");
            Console.Write("Ваш выбор: ");
            inputCriterion = Console.ReadLine();
        } while (inputCriterion != "1" && inputCriterion != "2");
        
        switch (inputCriterion)
        {
            case "1": return storages.OrderBy(storage => storage.GetField(inputField)).ToList();
            case "2": return storages.OrderByDescending(storage => storage.GetField(inputField)).ToList(); 
            default: return null;
        }
    }
}