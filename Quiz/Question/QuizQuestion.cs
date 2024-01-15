using Newtonsoft.Json;
using Telegram.Bot.Types.ReplyMarkups;

namespace Quiz.State;

[JsonObject(MemberSerialization.Fields)]
public class QuizQuestion
{
    public QuizQuestion( string question, string answer, string[] answerOptions)
    {
        Question = question;
        Answer = answer;
        AnswerOptions = ConvertTo(answerOptions);
    }

    private InlineKeyboardMarkup ConvertTo(string[] answerOptions)
    {
        InlineKeyboardButton[][] buttons = new InlineKeyboardButton[answerOptions.Length][];
        for (int i = 0; i < answerOptions.Length; i++)
        {
            buttons[i] = new InlineKeyboardButton[] { new InlineKeyboardButton(answerOptions[i]) { CallbackData = answerOptions[i].ToLower() } };
        }
        return new InlineKeyboardMarkup(buttons);
    }

    public string Question { get; } 
    public string Answer { get; } 

    public InlineKeyboardMarkup AnswerOptions { get; }
}