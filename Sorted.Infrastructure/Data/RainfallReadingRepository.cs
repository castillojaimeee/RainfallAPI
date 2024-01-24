using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sorted.Application.Interface;
using Sorted.Core.Entities;

namespace Sorted.Infrastructure.Data
{
    public class RainfallReadingRepository : IRainfallReadingRepository
    {
        public async Task<List<RainfallReading>> GetRainfallReading(string stationId, int count)
        {
            string rainfallApiUrl = string.Format("http://environment.data.gov.uk/flood-monitoring/id/stations/{0}/readings?_limit={1}", stationId,count);
            var myClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
            var response = await myClient.GetAsync(rainfallApiUrl);
            var streamResponse = await response.Content.ReadAsStreamAsync();
            string httpWebResult;
            using (var streamReader = new StreamReader(streamResponse))
            {
                httpWebResult = streamReader.ReadToEnd();
            }
            var jsonObject = (JObject)JsonConvert.DeserializeObject(httpWebResult);
            var rainfallReadingArray = (JArray)(jsonObject.Property("items").Value);
            return rainfallReadingArray.ToObject<List<RainfallReading>>();
        }
    }
}
