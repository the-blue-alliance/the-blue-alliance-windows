using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using TBA.Caches;
using TBA.Models;

namespace TBA
{
    class RxEventClient : IEventClient
    {
        private IEventCache Cache { get; set; }
        private IHttpClient<EventResponse> HttpClient { get; set; }
        private IScheduler Scheduler { get; set; }

        public RxEventClient(IEventCache cache, IHttpClient<EventResponse> httpClient, IScheduler scheduler)
        {
            Cache = cache;
            HttpClient = httpClient;
            Scheduler = scheduler;
        }

        public IObservable<EventModel> GetEvent(string eventKey)
        {
            return GetEventInternal(eventKey)
                .WithCache(() => Cache.GetCachedItem(eventKey), model => Cache.Put(model))
                .DistinctUntilChanged(new EventModelComparer());
        }

        private IObservable<EventModel> GetEventInternal(string eventKey)
        {
            return Observable.Create<EventModel>(observer =>
                Scheduler.Schedule(async () =>
                {
                    var eventResponse = await HttpClient.Get(eventKey);
                    if (!eventResponse.IsSuccessful)
                    {
                        observer.OnError(eventResponse.Exception);
                    }
                    else
                    {
                        observer.OnNext(eventResponse.Data);
                        observer.OnCompleted();
                    }
                }));
        }
    }
}
