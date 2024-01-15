using Telegram.Bot.Types;

namespace Quiz.State;

public interface IState
{
    // абстактный класс для состояний если игра будет расширяться то можно будет добавить новые состояния
    Task Update(string message, string userId, Message messageObj);
    void Start();
}