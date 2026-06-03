namespace ClassLibrary;

/// <summary>
/// Класс, в котором хранится информация о слотах 
/// </summary>
public class Slot : IJSONObject
{
    private string _id;
    private string _label;
    private string _description;
    private List<string> _required = new();
    private string _actionid;
    
    public Slot(){ }
    
    /// <summary>
    /// Метод возвращает коллекцию строк, представляющую имена всех полей
    /// </summary>
    /// <returns></returns>
    
    public IEnumerable<string> GetAllFields() => new List<string> { "id", "label", "description", "required", "actionid"};

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
            case "description": return _description;
            case "required": return String.Join("\n", _required.Select(n => n.ToString()));
            case "actionid": return _actionid;
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
            case "description": _description = value; break;
            case "required": _required.Add(value); break;
            case "actionid": _actionid = value; break;
            default: throw new KeyNotFoundException($"Поле '{fieldName}' не найдено.");
        }
    }
}