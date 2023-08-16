using TCBApp.Services.DataContexts;

namespace TCBApp.Services.DataService;

public class ConversationDataService : DataServiceBase<ChatModel>
{
    public ConversationDataService(DataContext context) : base(context)
    {
    }
}