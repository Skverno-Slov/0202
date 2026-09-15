using System.Diagnostics;

internal class Program
{
    private static readonly SourceSwitch StorageSwitch = new SourceSwitch("StorageSwitch");
    private static readonly TraceSource Ts = new TraceSource("Storage") { Switch = StorageSwitch };

    static int id = 0;

    private static readonly Dictionary<int, string> Database = new Dictionary<int, string>()
        {
            { 1, "Первая запись" },
            { 2, "Вторая запись" }
        };

    static void Main(string[] args)
    {
        try
        {
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "storage.log");

            if (File.Exists(logPath)) File.Delete(logPath);

            using (TextWriterTraceListener textListener = new TextWriterTraceListener(logPath))
            {
                Ts.Listeners.Add(textListener);
                Trace.AutoFlush = true;

                Console.WriteLine("SourceLevels.Warning");
                StorageSwitch.Level = SourceLevels.Warning;
                RunBusinessLogic();
                PrintLogContent(logPath);

                Console.WriteLine("\nSourceLevels.Verbose");
                StorageSwitch.Level = SourceLevels.Verbose;
                RunBusinessLogic();
                PrintLogContent(logPath);

                Console.WriteLine("\nSourceLevels.Off");
                StorageSwitch.Level = SourceLevels.Off;
                RunBusinessLogic();
                PrintLogContent(logPath);
            }
        }
        finally
        {
            Ts.Flush();
            Ts.Close();
        }
    }

    static void RunBusinessLogic()
    {
        Ts.TraceEvent(TraceEventType.Information, id++, "Начало загрузки.");
        Ts.TraceEvent(TraceEventType.Verbose, id++, "Запрос Id = 1.");
        var data = Database[1];
        Ts.TraceEvent(TraceEventType.Information, id++, $"Загрузка данных успешно завершено. Получено: '{data}'.");

        Ts.TraceEvent(TraceEventType.Information, id++, "Удаление данных.");
        int missingId = 99;
        Ts.TraceEvent(TraceEventType.Verbose, id++, $"Попытка удалить id = {missingId}.");
        if (!Database.ContainsKey(missingId))
        {
            Ts.TraceEvent(TraceEventType.Warning, id++, $"Запись с ID = {missingId} не найдена.");
        }
        Ts.TraceEvent(TraceEventType.Information, id++, "Удаление данных завершено с предупреждением.");

        Ts.TraceEvent(TraceEventType.Information, id++, "Сохранение данных.");
        try
        {
            Ts.TraceEvent(TraceEventType.Verbose, id++, "Попытка записи null.");
            string nullData = null;
            if (nullData == null)
                throw new ArgumentNullException(nameof(nullData), "Данные не могут быть null.");
        }
        catch (Exception ex)
        {
            Ts.TraceEvent(TraceEventType.Error, id++, $"Критическая ошибка при сохранение данных. Сообщение: {ex.Message}");
        }
        Ts.TraceEvent(TraceEventType.Information, id++, "Сохранение данных завершено с ошибкой.");
    }

    static void PrintLogContent(string path)
    {
        Ts.Flush();
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                Console.WriteLine($"  > {line}");
            }
            File.WriteAllText(path, string.Empty); 
        }
        else
        {
            Console.WriteLine("Файл не найден.");
        }
    }
}