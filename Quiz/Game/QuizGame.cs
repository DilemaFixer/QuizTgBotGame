using Quiz.Configs;
using Quiz.MessageSendingHandler;

namespace Quiz.State;

public class QuizGame 
{
    private QuizQuestion[] _questions;
    private QuizQuestion _currentQuestion;
    private QuizGameStatistics _gameStatistics;
    private QuestionInlineModifyer _modifyer;
    private Action _callBack;
    private int _currentQuestionIndex = 0;
    
    public QuizGame(QuizQuestion[] questions , Action callBack)
    {
        _callBack = callBack;
        _questions = questions;
        _gameStatistics = new QuizGameStatistics(_questions.Length);
        _modifyer = new QuestionInlineModifyer();
    }
    
    public async Task HandlingAnswer(string answer)
    {
        if (answer == _currentQuestion.Answer.ToLower())
        {
            ChatHeandler.Send(Constante.RightAnswer, _modifyer.ModifyAnswers(_currentQuestion));
            _gameStatistics.AddRightAnswer();
        }
        else
        {
            ChatHeandler.Send(Constante.WrongAnswer, _modifyer.ModifyAnswers(_currentQuestion));
        }
        
        await Task.Delay(GameConfig.WHAIT_TIME_BEHAIND_QUESTIN * 1000);
        
        _currentQuestionIndex++;
        if(_currentQuestionIndex >= _questions.Length)
        {
            _callBack();
            return;
        }
        
        _currentQuestion = _questions[_currentQuestionIndex];
        ChatHeandler.Send($"Question {_currentQuestionIndex+1}/{_questions.Length}\n \n {_currentQuestion.Question}", _currentQuestion.AnswerOptions);
    }
    
    public QuizGameStatistics GetGameStatistics() => _gameStatistics;

    public void SendFirsdQuestion()
    {
        _currentQuestion = _questions[_currentQuestionIndex];
        ChatHeandler.Send(_currentQuestion.Question, _currentQuestion.AnswerOptions);
    }
}