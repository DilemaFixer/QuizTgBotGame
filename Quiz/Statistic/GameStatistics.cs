using Quiz.MessageSendingHandler;

namespace Quiz.State;

public class GameStatistics
{
    public QuizGameStatistics LastGameStatistics { get; private set; }
    public int CountOfPlaingGames { get; private set; }
    public int CountOfWinGames { get; private set; }

    public void HandlingGame(QuizGameStatistics statistics)
    {
        LastGameStatistics = statistics;
        var stats = statistics.IsGameWined();
        
        if ((stats.Item1 && stats.Item2))
               CountOfWinGames++;
        
        CountOfPlaingGames++;    
    }
    
    public void GameLosed() => CountOfPlaingGames++;
    public async Task Print() => await ChatHeandler.Send($"{Constante.MessageBeforTotalScoreOfPlaingGame} " +
                                                         $"{CountOfPlaingGames}\n {Constante.MessageBeforTotalScoreOfPlaingWinGame} {CountOfWinGames}" , InlinesKeybord.BackKeyboardMarkup);
    
}