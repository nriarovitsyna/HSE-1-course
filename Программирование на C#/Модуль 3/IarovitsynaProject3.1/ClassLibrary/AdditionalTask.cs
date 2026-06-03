namespace ClassLibrary;
using OfficeOpenXml;
using System.IO;
using System.Drawing;
using System.Collections.Generic;

/// <summary>
/// Статический класс, реализующий дополнительную задачу (чтение и запись Exel файла)
/// </summary>
public static class AdditionalTask
{
    /// <summary>
    /// Метод для записи данных в Exel файл
    /// </summary>
    /// <param name="storages"></param>
     public static void WriteExcel(List<Storage> storages)
    {
        Console.Write("Введите имя файла с расширением .xlsx: ");
        string pathXlsx = Console.ReadLine();
        while (File.Exists(pathXlsx) || !pathXlsx.EndsWith(".xlsx"))
        {
            Console.WriteLine($"Название {pathXlsx} некорректно или файл уже существует, или ошибка в расширении");
            Console.Write("Введите имя файла с расширением .xlsx: ");
            pathXlsx = Console.ReadLine();
        }
        var fileInfo = new FileInfo(pathXlsx);
        using (var package = new ExcelPackage(fileInfo))
        {
            var worksheet = package.Workbook.Worksheets.Add("1");
            worksheet.Cells[1, 1].Value = "id";
            worksheet.Cells[1, 2].Value = "label";
            worksheet.Cells[1, 3].Value = "description";
            worksheet.Cells[1, 4].Value = "unique";
            
            Color headerColor = Color.LightPink;
            
            for (int col = 1; col <= 4; col++)
            {
                worksheet.Cells[1, col].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells[1, col].Style.Fill.BackgroundColor.SetColor(headerColor);
            }
            
            int row = 2;
            
            foreach (Storage storage in storages)
            {
                worksheet.Cells[row, 1].Value = storage.GetField("id");
                worksheet.Cells[row, 2].Value = storage.GetField("label");
                worksheet.Cells[row, 3].Value = storage.GetField("description");
                worksheet.Cells[row, 4].Value = storage.GetField("unique");
                row++;
            }
            package.Save();
        }
    }

    /// <summary>
    /// Метод для чтения данных из Exel файла
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static List<Storage> ReadExcel(string filePath)
    {
        List<Storage> storages = new List<Storage>();
        FileInfo fileInfo = new FileInfo(filePath);
        try
        {
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    Storage storage = new Storage();
                    storage.SetField("id", worksheet.Cells[row, 1].Text);
                    storage.SetField("label", worksheet.Cells[row, 2].Text);
                    storage.SetField("description", worksheet.Cells[row, 3].Text);
                    storage.SetField("unique", worksheet.Cells[row, 4].Text);
                    storages.Add(storage);
                }
            }
        }
        catch
        {
            throw new Exception("Файл закрыт для чтения");
        }
        return storages;
    }
}