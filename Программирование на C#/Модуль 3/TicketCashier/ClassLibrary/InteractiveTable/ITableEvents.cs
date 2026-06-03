using Spectre.Console;
namespace ClassLibrary.InteractiveTable
{
    public interface ITableEvents
    {
        public Table CreateTable();

        public void ShowTable(Table table);
        
        public void FilterTable();

        public void SortTable();

    }
}