using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Quiz.MessageSendingHandler;

public static class ChatHeandler
{
   private static Message _currentMessage;

   //усть ситуации когда пользователь блокирует бота и это не как не обрабатываеться сразу та что
   //каждая отправка сообщения должна быть обернута в try catch и если что то произошло то в консоль отправляеться ошибка 
   
   public static async Task SendWireMessage(string message , long chatid , InlineKeyboardMarkup markup = null)
   {
      if (_currentMessage != null)
      {
         await Send(message, markup);
         return;
      }
      
      try
      {
         if (markup == null)
         {
            _currentMessage = await Bot.BotClient.SendTextMessageAsync(chatid, message);
         }
         else
         {
            _currentMessage = await Bot.BotClient.SendTextMessageAsync(chatid, message , replyMarkup: markup);
         }
      }
      catch (Exception e)
      {
        Logger.Log(e.Message, MessageType.Error);  
      }
   }

   public static async Task Send(string message , InlineKeyboardMarkup markup = null)
   {
      if (_currentMessage == null) throw new ArgumentException("Target message is null");

      try
      {
         if (_currentMessage.ReplyMarkup == markup || markup == null)
         {
            await Bot.BotClient.EditMessageTextAsync(_currentMessage.Chat.Id, _currentMessage.MessageId, message);
         }
         else
         {
            await Bot.BotClient.EditMessageTextAsync(_currentMessage.Chat.Id, _currentMessage.MessageId, message,
               replyMarkup: markup);
         }
      }
      catch (Exception e)
      {
         Logger.Log(e.Message, MessageType.Error);  
      }
   }
}