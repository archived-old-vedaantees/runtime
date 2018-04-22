using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vedaantees.Framework.Providers.Communications.ServiceBus;
using Vedaantees.Framework.Providers.Logging;
using Vedaantees.Framework.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Vedaantees.Hosts.Api
{
    public class SingularityController : Controller
    {
        private readonly ICommandService _commandService;
        private readonly IQueryService _queryService;
        private readonly ILogger _logger;
        private readonly ApiClientRegistrations _apiClientRegistrations;
        
        public SingularityController(ICommandService commandService, IQueryService queryService, ILogger logger, ApiClientRegistrations apiClientRegistrations)
        {
            _commandService = commandService;
            _queryService = queryService;
            _logger = logger;
            _apiClientRegistrations = apiClientRegistrations;
        }

        [Route("ping")]
        public IActionResult Ping()
        {
            return Content("ping");
        }

        [Route("file/{filePath}")]
        public IActionResult DownloadFile(string filePath)
        {
            var m = new MemoryStream();
            //_fileStore.Download(filePath).Result.CopyTo(m);
            return File(ToByteArray(m), ToMimeType(filePath));
        }
        
        private byte[] ToByteArray(Stream stream)
        {
            stream.Position = 0;
            byte[] buffer = new byte[stream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < stream.Length;)
                totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);

            return buffer;
        }

        private string ToMimeType(string filePath)
        {
            return string.Empty;
        }



        [Route("{*url}")]
        [HttpPost]
        public async Task<IActionResult> Action()
        {
            try
            {
                string json = string.Empty;

                if(Request.HasFormContentType)
                {
                    var jObject = new JObject();
                    foreach (var item in Request.Form)
                        jObject.Add(item.Key, new JValue(item.Value));

                    json = JsonConvert.SerializeObject(jObject);
                }
                else
                {
                    var stream = new StreamReader(HttpContext.Request.Body);
                    json = await stream.ReadToEndAsync();
                }
                  
                var url = (string) HttpContext.GetRouteValue("url");
                var apiClientRegistration = _apiClientRegistrations.FirstOrDefault(p=>p.Route==url);
                    
                if (apiClientRegistration == null)
                    return NotFound();

                var request = JsonConvert.DeserializeObject(json, apiClientRegistration.Type);

                if (typeof(Command).IsAssignableFrom(apiClientRegistration.Type))
                {
                    var command = request as Command;
                    command.RequestedBy = HttpContext.User.Claims.FirstOrDefault(p => p.Type == "sub")?.Value;


                    if (Request.HasFormContentType)
                    {
                        if (Request.Form.Files != null)
                        {
                            return Ok(_commandService.ExecuteCommandWithAttachments((Command) request,
                                                                                    Request.Form.Files.Select(p => new Attachment
                                                                                    {
                                                                                        FileStream = p.OpenReadStream(),
                                                                                        Name = p.Name
                                                                                    }).ToList()));
                        }
                    }
                    
                    return Ok(_commandService.ExecuteCommand((Command)request));
                }

                if (apiClientRegistration.Type.IsSubClassOfGeneric(typeof(QueryRequest<>)))
                {
                    var responseType = apiClientRegistration.Type.BaseType?.GetGenericArguments()[0];
                    var method = _queryService.GetType().GetMethod("ExecuteQuery");
                    var generic = method.MakeGenericMethod(apiClientRegistration.Type, responseType);
                    var query =  (dynamic) request;
                    query.RequestedBy = HttpContext.User.Claims.FirstOrDefault(p => p.Type == "sub")?.Value;
                    var result = generic.Invoke(_queryService, new[] { request });
                    return Ok(result);
                }
                    
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.Error(e,"");
                return StatusCode(500);
            }
        }
    }
}