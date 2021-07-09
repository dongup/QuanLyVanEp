using BaseApiWithIdentity.DataAccess.DAL.Entities;
using Newtonsoft.Json;
namespace BaseApiWithIdentity.DataAccess.Models
{
    public class LogResponse : LogEntity
    {
        public LogResponse() : base()
        {

        }

        public string formatedBody => FormatBody();

        private string FormatBody()
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(RequestBody);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }
    }
}
