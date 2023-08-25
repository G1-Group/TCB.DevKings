using System.Data;
using System.Net;

namespace TCBApp.WebApi;

public class Watcher
{
    public HttpContext HttpContext { get; set; }

    public async Task AddMiddlevareToThreadPool(BaseMiddlevare middlevare)
    {
        //ThreadPool.QueueUserWorkItem( middlevare.ExecuteAsync(),"");
    }
    
    
    
}