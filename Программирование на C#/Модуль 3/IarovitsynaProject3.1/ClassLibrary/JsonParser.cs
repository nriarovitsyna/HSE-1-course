namespace ClassLibrary;
using System.Text;

/// <summary>
/// Статический класс для записи и чтения данных в формате JSON
/// </summary>
public static class JsonParser
{
    private static int figuredBracketCount = -1;
    private static int squareBracketCount = -1;
    private static Storage storage = new Storage();
    private static Slot slot = new Slot();
    private static string requiredSlot = null;
    
    /// <summary>
    /// Метод читает данные из файла в формате JSON
    /// </summary>
    /// <param name="line"></param>
    /// <param name="storages"></param>
    public static void ReadJson(string line, List<Storage> storages)
    {
        if (line.Contains('{')) figuredBracketCount++;
        if (line.Contains('}')) figuredBracketCount--;

        if (line.Contains('[')) squareBracketCount++;
        if (line.Contains(']')) squareBracketCount--;

        if (figuredBracketCount == 1 && squareBracketCount == 0)
        {
            if (line.Contains("\"id\""))
                storage.SetField("id",
                    line.Remove(0, line.IndexOf('\"') + 7).Replace("\"", "").Replace(",", ""));

            else if (line.Contains("\"label\""))
                storage.SetField("label",
                    line.Remove(0, line.IndexOf('\"') + 10).Replace("\"", "").Replace(",", ""));

            else if (line.Contains("\"description\""))
                storage.SetField("description",
                    line.Remove(0, line.IndexOf('\"') + 16).Replace("\"", "").Replace(",", ""));

            else if (line.Contains("\"unique\""))
                storage.SetField("unique",
                    line.Remove(0, line.IndexOf('\"') + 10).Replace("\"", "").Replace(",", ""));
        }
        else if (figuredBracketCount == 2 && squareBracketCount == 0)
        {
            if (line.Contains("\"vault\""))
                storage.SetField("aspects",
                    line.Remove(0, line.IndexOf('\"')).Replace("\"", "").Replace(",", ""));

            else if (line.Contains("\"location\""))
                storage.SetField("aspects",
                    line.Remove(0, line.IndexOf('\"')).Replace("\"", "").Replace(",", ""));

            else if (line.Contains("\"vault"))
                storage.SetField("aspects",
                    line.Remove(0, line.IndexOf('\"')).Replace("\"", "").Replace(",", ""));
        }

        else if (figuredBracketCount == 2 && squareBracketCount == 1)
        {
            if (line.Contains("\"id\""))
                slot.SetField("id", line.Remove(0, line.IndexOf('\"') + 7).Replace("\"", "").Replace(",", ""));

            else if (line.Contains("\"label\""))
                slot.SetField("label", line.Remove(0, line.IndexOf('\"') + 10).Replace("\"", "").Replace(",", ""));

            else if (line.Contains("\"description\""))
                slot.SetField("description", line.Remove(0, line.IndexOf('\"') + 16).Replace("\"", "").Replace(",", ""));

            else if (line.Contains("\"actionid\""))
                slot.SetField("actionid", line.Remove(0, line.IndexOf('\"') + 13).Replace("\"", "").Replace(",", ""));
        }
        else if (figuredBracketCount == 3 && squareBracketCount == 1)
        {
            if (!line.Contains("\"required\""))
                slot.SetField("required",
                    line.Remove(0, line.IndexOf('\"')).Replace("\"", "").Replace(",", ""));
        }
        else if (figuredBracketCount == 1 && squareBracketCount == 1 && slot.GetField("description") != null)
        {
            storage.SlotAdd(slot);
            slot = new Slot();
        }
        else if (figuredBracketCount == 0 && squareBracketCount == 0 && storage.GetField("description") != null)
        {
            storages.Add(storage);
            storage = new Storage();
        }
    }
    
    /// <summary>
    /// Метод записывает данные в файл в формате JSON
    /// </summary>
    /// <param name="storages"></param>
    /// <returns></returns>
    public static string WriteJson(List<Storage> storages)
    {
        StringBuilder outData = new StringBuilder();
        outData.AppendLine("{");
        outData.AppendLine("    \"elements\": [");
        
        foreach (Storage storage in storages)
        {
            outData.AppendLine("\t{");
            outData.AppendLine($"\t \"id\": \"{storage.GetField("id")}\",");
            outData.AppendLine($"\t \"label\": \"{storage.GetField("label")}\",");
            outData.AppendLine("\t \"aspects\": {");
            outData.AppendLine("\t      \"" + storage.GetField("aspects").Replace(":", "\":").Replace("\n", "\n\t       "));
            outData.AppendLine("\t  },");
            outData.AppendLine("\t  \"slots\": [");

            for (int i = 0; i < storage.SlotCount; i++)
            {
                outData.AppendLine("\t      {");
                outData.AppendLine($"\t         \"id\": \"{storage.SlotGet(i).GetField("id")}\",");
                outData.AppendLine($"\t         \"label\": \"{storage.SlotGet(i).GetField("label")}\",");
                outData.AppendLine($"\t         \"description\": \"{storage.SlotGet(i).GetField("description")}\",");
                outData.AppendLine("\t         \"required\": {");
                outData.AppendLine("\t\t    \"" + storage.SlotGet(i).GetField("required").Replace(":", "\":").Replace("\n", "\n\t\t    "));
                outData.AppendLine("\t         }");
                outData.AppendLine($"\t         \"actionid\": \"{storage.SlotGet(i).GetField("actionid")}\",");
                outData.AppendLine("\t      },");
            }
            
            outData.AppendLine("\t  ],");
            outData.AppendLine($"\t \"description\": \"{storage.GetField("description")}\",");
            outData.AppendLine($"\t \"unique\": \"{storage.GetField("unique")}\",");
            
            if (storages.IndexOf(storage) == storages.Count - 1) outData.AppendLine("\t}");
            else outData.AppendLine("\t},");
        }
        outData.AppendLine("    ]");
        outData.AppendLine("}");
        
       return outData.ToString();
    }
}