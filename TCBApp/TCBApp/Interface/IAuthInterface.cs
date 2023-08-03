using TCBApp.Models;

namespace TCBApp.Interface;

public interface IAuthInterface
{
    
        public User CreateUser(long user_Id,string phoneNumber,long telegramClientId,string password);
        public bool RemoveUser(long user_Id);
        public User Finduser(long user_Id);
    
}