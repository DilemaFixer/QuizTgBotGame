using Quiz.MessageSendingHandler;
using Quiz.State;
using Telegram.Bot.Types;

namespace Quiz;

public class InfoState : IState
{
    private GameStatistics _statistics;
    
    public InfoState(GameStatistics statistics)
    {
        _statistics = statistics;
    }
    
    public async Task Update(string message, string userId, Message messageObj)
    {
        switch (message)
        {
            case "back":
                await ChatHeandler.SendWireMessage("Главное меню", messageObj.Chat.Id, InlinesKeybord.StartKeybord);
                break;
            case "statistics":
                await _statistics.Print();
                break;
            default:
                ChatHeandler.Send(Constante.NonCommand , InlinesKeybord.BackKeyboardMarkup);
                break;
        }
    }

    public void Start()
    {
    }
}