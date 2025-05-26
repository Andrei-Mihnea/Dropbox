using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BusinessLogic;
using CommonModels;

//Autor Mihnea Andrei
namespace HttpServer.Controller
{
    public class FileController
    {
        private readonly DropboxFacade _facade = new DropboxFacade();

        public void HandleUpload(HttpListenerContext context)
        {
            try
            {
                using (var reader = new StreamReader(context.Request.InputStream))
                {
                    string body = reader.ReadToEnd();

                    var uploadRequest = JsonSerializer.Deserialize<UploadRequest>(body);

                    _facade.UploadFile(new User { Id = uploadRequest.UserId }, uploadRequest.FilePath);

                    context.Response.StatusCode = (int)HttpStatusCode.OK; //equivalent for 200
                    context.Response.ContentType = "application/json";
                    byte[] buffer = Encoding.UTF8.GetBytes("{\"message\":\"Upload Successful\"}");
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);

                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                byte[] buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new { error = ex.Message }));
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            }

            context.Response.Close();
        }

        public void HandleList(HttpListenerContext context)
        {
            try
            {
                using(var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
                {
                    string body = reader.ReadToEnd();

                    var user = JsonSerializer.Deserialize<UserIdRequest>(body);
                    var files = _facade.GetUserFiles(user.UserId);

                    string json = JsonSerializer.Serialize(files);

                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";
                    byte[] buffer = Encoding.UTF8.GetBytes(json);
                    context.Response.OutputStream.Write(buffer,0,buffer.Length);
                }
            }
            catch (Exception ex)
            { 
                context.Response.StatusCode= (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                byte[] buffer = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new { error = ex.Message }));
                context.Response.OutputStream.Write (buffer, 0, buffer.Length);
            }

            context.Response.Close();

        }
    }

    public class UploadRequest
    {
        public int UserId {  get; set; }
        public string FilePath {  get; set; }
    }

    public class UserIdRequest
    {
        public int UserId { get; set; }
    }

    public class DeleteFileRequest
    {
        public int FileId {  get; set; }
    }
}
