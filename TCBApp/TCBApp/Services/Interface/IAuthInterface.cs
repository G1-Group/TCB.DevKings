
using TCBApp.Models;

namespace TCBApp.Interface;

public interface IAuthInterface
{

        public User Registration(long user_Id, long telgramClientId, string nickName, string password);
        public Client LogIn(User user);
       
     

    
}