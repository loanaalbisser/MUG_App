using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MUG_App.RestService
{
    public class RestService : IRestService
    {
        private readonly HttpClient _client;

        private dynamic _item;
        public RestService()
        {
            _client = new HttpClient {MaxResponseContentBufferSize = 256000};
        }
        
        public async Task<dynamic> GetData(string restUrl)
        {
            var uri = new Uri(string.Format(restUrl));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _item = JsonConvert.DeserializeObject<dynamic>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return _item;
        }

        private static Group.Group Creategroup(string name, string description)
        {
            var group = new Group.Group()
            {
                Name = name,
                Description = description
            };
            return group;
        }

    }
    }