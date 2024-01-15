using Telegram.Bot.Types.ReplyMarkups;

namespace Quiz;

public class InlinesKeybord
{
    public static readonly InlineKeyboardMarkup StartKeybord = new InlineKeyboardMarkup(  new InlineKeyboardButton[][] 
    {
        new[] { new InlineKeyboardButton("Start the quiz🚀") { CallbackData = "start game" } },
        new[] { new InlineKeyboardButton("Statistic") { CallbackData = "statistics" } },
    });
    
    public static readonly InlineKeyboardMarkup BackKeyboardMarkup = new InlineKeyboardMarkup(  new InlineKeyboardButton[][] 
    {
        new[] { new InlineKeyboardButton("Back") { CallbackData = "back" } },
    });
    
}