using Quiz.Configs;

namespace Quiz.State;

public class QuizGameStatistics
{
    private int CountOfQuestions;
    public int CountOfRightAnswers { get; private set; }
    // класс с данными о статистике игры
    public QuizGameStatistics( int countOfQuestions )
    {
        CountOfQuestions = countOfQuestions;
    }
    
    public (bool,bool) IsGameWined()
    {
        
        if ( CountOfRightAnswers < (CountOfQuestions / GameConfig.PERCENTAGE_OF_CORRECT_ANSWERS_FOR_WIN))
        {
            return (false, false);
        } 
        else if (CountOfRightAnswers == CountOfQuestions)
        {
            return (true, true);
        }
        else
        {
            return (true, false);
        }
    }

    public void AddRightAnswer() => CountOfRightAnswers++;
}