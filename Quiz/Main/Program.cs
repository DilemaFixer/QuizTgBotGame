namespace Quiz
{
    internal class Program
    {
        // данные в боте не как не сохраняються так что при перезапуске бота все данные сбрасываються
        
        private Bot BotClient;
        
        static void Main(string[] args)
        {
            var bot = new Bot();
            bot.Run();

            Console.ReadLine();
        }
    }
}