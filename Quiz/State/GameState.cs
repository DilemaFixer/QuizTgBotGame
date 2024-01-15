using Quiz.Configs;
using Quiz.MessageSendingHandler;
using Quiz.State;
using Telegram.Bot.Types;

namespace Quiz;

public class GameState : IState
{
    private QuizGame _game;
    private GameStatistics _statistics;
    private Action _gameEndAction;
    
    public GameState(GameStatistics statistics , Action gameEndAction)
    {
        _gameEndAction = gameEndAction;
        CreateNewGame();
        _statistics = statistics;
    }

    public void Start() => _game.SendFirsdQuestion();
    public async Task Update(string message, string userId, Message messageObj) => await _game.HandlingAnswer(message);


    public void HandlingEndOfGame()
    {
        _statistics.HandlingGame(_game.GetGameStatistics());
        CreateNewGame();
        PrintAfterGameInfo();
        _gameEndAction();
    }

    private void PrintAfterGameInfo()
    {
        int lenght = GameConfig.GetQuestions().Length;
        switch (_statistics.LastGameStatistics.IsGameWined())
        {
            case (true, true):
                ChatHeandler.Send(message: $"{Constante.PlayerSayNotAllAnswerRight} {_statistics.LastGameStatistics.CountOfRightAnswers}/{lenght}",  InlinesKeybord.BackKeyboardMarkup);
                break;
            case (true, false):
                ChatHeandler.Send($"{Constante.PlayerSayNotAllAnswerRight} {_statistics.LastGameStatistics.CountOfRightAnswers}/{lenght}",  InlinesKeybord.BackKeyboardMarkup);
                break;
            case (false, false):
                ChatHeandler.Send($"{Constante.MessagePlayerSayAllAnswerWrong} {_statistics.LastGameStatistics.CountOfRightAnswers}/{lenght}", InlinesKeybord.BackKeyboardMarkup);
                break;
        }
    }

    public void CreateNewGame() => _game = new QuizGame(GameConfig.GetQuestions() , HandlingEndOfGame);
}