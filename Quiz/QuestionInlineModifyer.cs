using Quiz.Configs;
using Quiz.State;
using Telegram.Bot.Types.ReplyMarkups;

namespace Quiz;

public class QuestionInlineModifyer
{
    public InlineKeyboardMarkup ModifyAnswers(QuizQuestion question)
    {
        var modifiedButtons = new List<List<InlineKeyboardButton>>();

        IEnumerable<IEnumerable<InlineKeyboardButton>> buttons = question.AnswerOptions.InlineKeyboard;
        string answer = question.Answer;

        foreach ( IEnumerable<InlineKeyboardButton> buttonsArr in buttons)
        {
            var row = new List<InlineKeyboardButton>();

            foreach (var button in buttonsArr)
            {
                if (button.Text != null && button.CallbackData != null)
                {
                    // Определяем, какую иконку использовать в зависимости от индекса правильного ответа
                    string emoji = (answer.ToLower() == button.CallbackData) ? "✅" : "❌";

                    // Меняем текст кнопки и устанавливаем соответствующий emoji
                    var modifiedButton = new InlineKeyboardButton($"{(string)button.CallbackData} {emoji}")
                    {
                        CallbackData = BotConfig.IGNORE_COMMAND
                    };
/*
                    var modifiedButton = new InlineKeyboardButton($"{button.Text} {emoji}")
                    {
                        CallbackData = button.CallbackData
                    };

          */          
                    row.Add(modifiedButton);
                }
            }

            modifiedButtons.Add(row);
        }

        return new InlineKeyboardMarkup(modifiedButtons);
    }
}