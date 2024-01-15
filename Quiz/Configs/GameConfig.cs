using keksikq9.Json;
using Quiz.State;

namespace Quiz.Configs;

public class GameConfig
{
    public const float PERCENTAGE_OF_CORRECT_ANSWERS_FOR_WIN = 2;
    public const int WHAIT_TIME_BEHAIND_QUESTIN = 3; // in seconds
    private static QuizQuestion[] _questions;
    
    public static QuizQuestion[] GetQuestions()
    {
        if(_questions == null) 
            _questions = JsonSavingSystem<QuizQuestion>.LoadArray(BotConfig.LINK_TO_JSON_FILE);
        
        return _questions;
    }
}