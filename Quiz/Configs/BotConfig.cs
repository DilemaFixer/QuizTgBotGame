using System.Net.Mime;
using System.Reflection;

namespace Quiz.Configs;

public class BotConfig
{
    public const string BOT_TOKEN = "6543937705:AAEJsgpo22BGDW6DccAKl5wkZ4ObZEoboek";
    public const string IGNORE_COMMAND = "ignor"; // dont change this please =) 
    public static string LINK_TO_JSON_FILE {
        get { return GetLinkToJsonFile();}
    }
    
    private static string GetLinkToJsonFile()
    {
        
        // Получаем путь к папке, где находится текущий исполняемый файл
        string directory = Directory.GetCurrentDirectory();

        // Составляем путь к вашему файлу JSON внутри папки проекта (в данном случае файл называется "data.json")
       return Path.Combine(directory, "Questions.json");
    }
}