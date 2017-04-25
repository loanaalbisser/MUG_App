using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using MUG_App.Common;
using MUG_App.Event;
using MUG_App.Group;
using MUG_App.Organizer;
using Newtonsoft.Json;

namespace MUG_App.Services
{
    public class RESTLoaderService : IEventLoaderService, IGroupLoaderService, IOrganizerLoaderService
    {
        private readonly HttpClient _client;
        private dynamic _item;

        public RESTLoaderService()
        {
            _client = new HttpClient { MaxResponseContentBufferSize = 256000 };
        }

        #region IEventLoaderService

        public async Task<IEnumerable<Event.Event>> LoadEventsAsync()
        {
            const string restUrl = "https://api.meetup.com/Mobile-User-Group-Zentralschweiz/events";

            var result = new List<Event.Event>();
            var loadedEvents = await GetDataAsync(restUrl);

            foreach (var loadedEvent in loadedEvents)
            {
                var modelEvent = new Event.Event
                {
                    Title = loadedEvent["name"].ToString(),
                    Description = HtmlFormatter.RemoveHtmlTags(loadedEvent["description"].ToString())
                };

                result.Add(modelEvent);
            }

            return result;
        }

        #endregion

        #region IGroupLoaderService

        public async Task<Group.Group> LoadGroupAsync()
        {
            const string restUrl = "https://api.meetup.com/Mobile-User-Group-Zentralschweiz";

            var loadedGroup = await GetDataAsync(restUrl);

            var result = new Group.Group
            {
                Name = loadedGroup["name"].ToString(),
                Description = HtmlFormatter.RemoveHtmlTags(loadedGroup["description"].ToString()),
                City = loadedGroup["city"].ToString(),
                ImageUrl = loadedGroup["group_photo"]["photo_link"].ToString()
            };

            return result;
        }

        #endregion

        #region IOrganizerLoaderService

        public async Task<IEnumerable<Organizer.Organizer>> LoadOrganizersAsync()
        {
            const string restUrlLoana = "https://api.meetup.com/2/member/216711932?key=123e651e3f70711b4b15151d6d671f75&group_urlname=mobile-user-group-zentralschweiz&sign=true";
            const string restUrlThomas = "https://api.meetup.com/2/member/184741056?key=123e651e3f70711b4b15151d6d671f75&group_urlname=mobile-user-group-zentralschweiz&sign=true";

            var result = new List<Organizer.Organizer>();

            var modelOrganizerLoana = await LoadOrganizerAsync(restUrlLoana);
            var modelOrganizerThomas = await LoadOrganizerAsync(restUrlThomas);

            result.Add(modelOrganizerLoana);
            result.Add(modelOrganizerThomas);

            return result;
        }

        #endregion

        #region Private Methods

        private async Task<dynamic> GetDataAsync(string restUrl)
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

        private async Task<Organizer.Organizer> LoadOrganizerAsync(string restUrl)
        {
            var loadedOrganizer = await GetDataAsync(restUrl);

            var modelOrganizer = new Organizer.Organizer
            {
                Name = loadedOrganizer["name"].ToString(),
                City = loadedOrganizer["city"].ToString(),
                ImageUrl = loadedOrganizer["photo"]["photo_link"].ToString()
            };

            return modelOrganizer;
        }

        #endregion
    }
}