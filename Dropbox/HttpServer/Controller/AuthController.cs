using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BusinessLogic;
using CommonModels;

namespace HttpServer.Controller
{
    public class AuthController
    {
        public readonly DropboxFacade _facade = new DropboxFacade();

        public void HandleLogin(HttpListenerContext context)
        {
            using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
            {
                string body = reader.ReadToEnd();

                var request = JsonSerializer.Deserialize<LoginRequest>(body);

                try
                {
                    var user = _facade.Login(request.Username, request.Password);
                    string responseJson = JsonSerializer.Serialize(user);

                    byte[] buffer = Encoding.UTF8.GetBytes(responseJson);
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";
                    context.Response.OutputStream.Write(buffer, 0 ,buffer.Length);
                }
                catch (Exception ex)
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new { error = ex.Message }));
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";
                    context.Response.OutputStream.Write(buffer , 0 ,buffer.Length);
                }

                context.Response.Close();
            }

        }

        public void HandleRegister(HttpListenerContext context)
        {
            using(var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
            {
                string body = reader.ReadToEnd();
                var request = JsonSerializer.Deserialize<LoginRequest>(body);

                try
                {
                    var newUser = new User
                    {
                        Username = request.Username,
                        PasswordHash = request.Password
                    };

                    _facade.Register(request.Username, request.Password);

                    byte[] buffer = Encoding.UTF8.GetBytes("{\"message\":\"User registered successfully\"}");
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";
                    context.Response.OutputStream.Write(buffer,0,buffer.Length);
                }
                catch (Exception ex)
                {
                    byte[] buffer = Encoding.UTF8.GetBytes (JsonSerializer.Serialize(new {error = ex.Message}));
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                }

                context.Response.Close();
            }
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
