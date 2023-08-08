using System.Threading.Channels;
using System.Xml.Serialization;
using TCBApp.Models;
using TCBApp.Models.Enums;
using TCBApp.Services;
using TCBApp.TelegramBot;
using TCBApp.TelegramBot.Controllers;
using Telegram.Bot;
using MessageType = Telegram.Bot.Types.Enums.MessageType;

public class Program
{
 public static void Main(string[] args)
 {
  
 }

 //static string com = "Host=localhost; Port=5432; Database=SqlBot; username=postgres; password=Ogabek1407";
 


}



 //string connection = "Host=localhost; Port=5432; Database=postgres; username=postgres; password=3214";

//
//
//  MessageDataService service = new MessageDataService(connection);
//
// var res=service.Insert(new Message
// {
//  FromId = 1,
//  _Message = "HI",
//  ChatId = 2,
//  BoardId = 1,
//  MessageType = TCBApp.Models.Enums.MessageType.BoardMessage,
//  MessageStatus = MessageStatus.NotRe
// }).Result;
// Console.WriteLine(res);

 // BoardDataService service = new BoardDataService(connection);
//
 // var res=service.Insert(new BoardModel
 // {
 //  
 //  NickName = "myboard2",
 //  OwnerId = 2,
 //  BoardStatus = BoardStatus.Processing
 // }).Result;
 //   Console.WriteLine(res);

 
 // ConversationDataService service = new ConversationDataService(connection);
 // var res= service.Insert(new ChatModel
 //  {
 //   CreatedDate = DateTime.Now,
 //   FromId = 1,
 //   ToId = 2,
 //   State = ChatState.New,
 //   Id = 2
 //  }).Result;
 //  
 //  Console.WriteLine(res);


//  ClientDataService clientDataService = new ClientDataService(connection);
//  var res= clientDataService.Insert(new Client
//   {
//  
//    UserId = 2,
//    UserName = "G'olibjon",
//    Nickname = "G_Abdurasulov",
//    Status = ClientStatus.Enabled,
//    IsPremium = false
//   }).Result;
//
// Console.WriteLine(res);
//
//


// UserDataService dataService=new UserDataService(connection);
//
//
// var res=dataService.Insert(new User()
// {
//     TelegramClientId = 822096,
//     PhoneNumber = "+998901092779",
//     Password = "111111"
// }).Result;
//
// Console.WriteLine(res);


