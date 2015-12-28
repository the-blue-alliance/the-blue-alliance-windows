using System;
using TBA.Models;

namespace TBA
{
    public interface IEventClient
    {
        IObservable<EventModel> GetEvent(string eventKey);
    }
}
