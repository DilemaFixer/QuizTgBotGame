using Quiz.Configs;

namespace Quiz;

public static class Logger
{
    // класс занимаеться отправкой ошибкой сообщений в консоль ,
    // это важно что бы отслеживать ошибки,рабочие моменты во время работы бота
    
    public static void Log(string message , MessageType type = MessageType.Info)
    {
        if(string.IsNullOrEmpty(message)) return;
        
        switch (type)
        {
            case MessageType.Info:
                LogInfo(message);
                break;
            case MessageType.Error:
                LogError(message);
                break;
            case MessageType.Warning:
                LogWarning(message);
                break;
        }
    }
    //методы для отправки сообщений в консоль с временем и с пометкой какой это тип
    //сообщения (ошибка,информация,предупреждение) ну и само сообщение + у них разные цвета которые настраиваються в Config.cs 
    private static void LogWarning(string message)
    {
        Console.ForegroundColor = LoggerConfig.WARNING_COLOR;
        Console.WriteLine($"[{DateTime.Now}] [WARNING] {message}");
        Console.ResetColor();
    }

    private static void LogError(string message)
    {
        Console.ForegroundColor = LoggerConfig.ERROR_COLOR;
        Console.WriteLine($"[{DateTime.Now}] [ERROR] {message}");
        Console.ResetColor(); 
    }

    private static void LogInfo(string message)
    {
        Console.ForegroundColor = LoggerConfig.INFO_COLOR;
        Console.WriteLine($"[{DateTime.Now}] [INFO] {message}");
        Console.ResetColor();
    }
}


public enum MessageType
{
    Info,
    Error,
    Warning
}