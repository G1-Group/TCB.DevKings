using System.Net;
using System.Text;
using System.Text.Json;
using TCBApp.Models;
using TCBApp.Services;
using TCBApp.WebApi.Attribute;

namespace TCBApp.WebApi.Controllers;

public class AuthController : ControllerBase
{
    private AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [Route(Path = "/user/login", Method = "POST")]
    public async Task Login(HttpContext context)
    {
       HttpListenerRequest request = context.Request;
       string responseString = "";
       using (StreamReader reader = new StreamReader(request.InputStream))
       {
           string requestBody = reader.ReadToEnd();
           
           UserLoginModel? user = JsonSerializer.Deserialize<UserLoginModel>(requestBody);

           
           if (user is not null)
           {
               var client = await _authService.Login(user);
               if (client is not null)
               {
                   responseString = JsonSerializer.Serialize(client);
               }
               else
                   responseString = "Client not found";
           }
           else
               responseString = "User not found";
           
           HttpListenerResponse response = context.Response;
           byte[] responseBodyBytes = Encoding.UTF8.GetBytes(responseString);
                   
           response.ContentType = "application/json";
           response.ContentLength64 = responseBodyBytes.Length;
           using (Stream output = response.OutputStream)
           {
               output.Write(responseBodyBytes, 0, responseBodyBytes.Length);
           }
           
           response.Close();

       }
    }

    
    [Route(Path = "/user/register", Method = "POST")]
    public async Task Registration(HttpContext context)
    {
        HttpListenerRequest request = context.Request;
        string responseString = "";
        using (StreamReader reader = new StreamReader(request.InputStream))
        {
            string requestBody = reader.ReadToEnd();
           
            UserRegistrationModel? user = JsonSerializer.Deserialize<UserRegistrationModel>(requestBody);
            
            if (user is not null)
            {
                 _authService.RegisterUser(user);
                 responseString = "Successfully registered";
            }
            else
                responseString = "Incorrect information was entered";
           
            HttpListenerResponse response = context.Response;
            byte[] responseBodyBytes = Encoding.UTF8.GetBytes(responseString);
                   
            response.ContentType = "application/json";
            response.ContentLength64 = responseBodyBytes.Length;
            using (Stream output = response.OutputStream)
            {
                output.Write(responseBodyBytes, 0, responseBodyBytes.Length);
            }
           
            response.Close();
        }
    }

}
