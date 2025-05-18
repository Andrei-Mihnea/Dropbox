using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using HttpServer.Controller;

namespace HttpServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");
            listener.Start();

            Console.WriteLine("Serverul ruleaza pe http://localhost:8080/");

            while (true)
            {
                var context = listener.GetContext();
                var request = context.Request;
                var response = context.Response;
                var authHandler = new AuthController();
                var fileHandler = new FileController();
                switch (request.HttpMethod)
                {
                    case "POST":
                        switch (request.Url.AbsolutePath)
                        {
                            case "/login":
                                authHandler.HandleLogin(context);
                                break;

                            case "/register":
                                authHandler.HandleRegister(context);
                                break;
                            case "/upload":
                                fileHandler.HandleUpload(context);
                                break;
                        }
                        break;
                }
            }
        }
    }
}
