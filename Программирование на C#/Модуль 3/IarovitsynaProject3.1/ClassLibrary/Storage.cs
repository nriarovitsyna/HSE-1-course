namespace ClassLibrary;

/// <summary>
/// Структура, в которой хранится информация о хранилище
/// </summary>
public struct Storage :  IJSONObject
{
    private string _id;
    private string _label;
    private List<string> _aspects = new();
    private List<Slot> _slots = new();
    private string _description;
    private string _unique;
    public Storage() { }
    
    /// <summary>
    /// Метод возвращает число слотов в хранилище
    /// </summary>
    public int SlotCount => _slots.Count;
    
    /// <summary>
    /// Метод добавляет слот в список слотов хранилища
    /// </summary>
    /// <param name="slot"></param>
    public void SlotAdd(Slot slot) => _slots.Add(slot);
    
    /// <summary>
    /// Метод возвращает информацию о слоте
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Slot SlotGet(int index) => _slots[index];
    
    /// <summary>
    /// Метод возвращает коллекцию строк, представляющую имена всех полей
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetAllFields() => new List<string> { "id", "label", "description", "unique" };
    
    /// <summary>
    /// Метод возвращает значение поля
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public string GetField(string fieldName)
    {
        switch (fieldName.ToLower())
        {
            case "id": return _id; 
            case "label": return _label;
            case "aspects": return String.Join("\n", _aspects.Select(n => n.ToString()));
            case "description": return _description;
            case "unique": return _unique;
            default: return null;
        }
    }
    
    /// <summary>
    /// Метод присваивает значение полю
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <exception cref="KeyNotFoundException"></exception>
    public void SetField(string fieldName, string value)
    {
        switch (fieldName.ToLower())
        {
            case "id": _id = value; break;
            case "label": _label = value; break;
            case "aspects": _aspects.Add(value); break;
            case "description": _description = value; break;
            case "unique": _unique = value; break;
            default: throw new KeyNotFoundException($"Поле '{fieldName}' не найдено.");
        }
    }
}