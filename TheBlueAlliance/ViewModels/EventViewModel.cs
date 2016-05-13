using System;
using System.Reactive.Linq;
using TBA.Models;

namespace TBA.ViewModels
{
    public class EventViewModel
    {
        private IEventClient EventClient { get; set; }

        public EventViewModel(IEventClient eventClient, string eventKey)
        {
            EventClient = eventClient;
            Initialize(eventKey);
        }

        private void Initialize(string eventKey)
        {
            IsLoading = true;
            EventClient.GetEvent(eventKey)
                .Subscribe(onNext: HandleData,
                    onError: HandleException,
                    onCompleted: () => IsLoading = false);
        }

        private void HandleException(Exception exception)
        {
            ErrorMessage = exception.Message;
            IsLoading = false;
        }

        private void HandleData(EventModel data)
        {
            Name = data.Name;
            Location = data.Location;
            StartDate = Convert.ToDateTime(data.StartDate);
        }

        public bool IsLoading { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public string ErrorMessage { get; set; }
    }
}
