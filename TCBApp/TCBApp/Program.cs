using System.Threading.Channels;
using TCBApp.Models;
using TCBApp.Models.Enums;
using TCBApp.Services;


 string connection = "Host=localhost; Port=5432; Database=postgres; username=postgres; password=3214";

 ConversationDataService service = new ConversationDataService(connection);
// var res= service.Insert(new ChatModel
//  {
//   CreatedDate = DateTime.Now,
//   FromId = 2,
//   ToId = 1,
//   State = ChatState.New,
//   Id = 2
//  }).Result;
//  
 var res = service.GetById(2).Result;
 Console.WriteLine(res.CreatedDate);
 Console.WriteLine(res.State);
// ClientDataService clientDataService = new ClientDataService(connection);
// var res= clientDataService.Insert(new Client
//  {
//
//   UserId = 2,
//   UserName = "Jamolhon",
//   Nickname = "alievvvvg",
//   Status = ClientStatus.Enabled,
//   IsPremium = true
//  }).Result;
// var res= clientDataService.GetById(2).Result;
//  Console.WriteLine(res.UserName);
//  Console.WriteLine(res.Status);
//  Console.WriteLine(res.IsPremium);
// ConversationDataService service = new ConversationDataService(connection);
// ChatModel model = new ChatModel()
// {
//     Id =1,
//     State = ChatState.New,
//     CreatedDate = DateTime.Now,
//     FromId = 4,
//     ToId = 3
// };
// var res=service.Insert(model).Result;
// Console.WriteLine(res);
//
//
// service.GetById(1);


// UserDataService dataService=new UserDataService(connection);
//
// var res = dataService.GetAll().Result;

// var res=dataService.GetById(2).Result;

// var res=dataService.Insert(new User()
// {
//     TelegramClientId = 852096,
//     PhoneNumber = "+998901092778",
//     Password = "222222"
// }).Result;

// foreach (User user in res)
// {
//     Console.WriteLine(user.PhoneNumber);
// }

