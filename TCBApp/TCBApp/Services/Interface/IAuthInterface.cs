
using TCBApp.Models;

namespace TCBApp.Interface;

public interface IAuthInterface
{

     // public Task Registration(UserRegstration user);
     public Task<Client> Login(UserLoginModel user);




}