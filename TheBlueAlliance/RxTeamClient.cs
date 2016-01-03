using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using TBA.Caches;
using TBA.DataServices;
using TBA.Models;

namespace TBA
{
    class RxTeamClient : ITeamClient
    {
        private ITeamCache Cache { get; set; }
        private TeamHttpClient HttpClient { get; set; }
        private IScheduler Scheduler { get; set; }

        public RxTeamClient(ITeamCache cache, TeamHttpClient httpClient, IScheduler scheduler)
        {
            Cache = cache;
            HttpClient = httpClient;
            Scheduler = scheduler;
        }

        public IObservable<TeamModel> GetTeam(string teamKey)
        {
            return GetTeamInternal(teamKey)
                .WithCache(() => Cache.GetCachedItem(teamKey), model => Cache.Put(model))
                .DistinctUntilChanged(new TeamModelComparer());
        }

        private IObservable<TeamModel> GetTeamInternal(string teamKey)
        {
            return Observable.Create<TeamModel>(observer =>
                Scheduler.Schedule(async () =>
                {
                    var teamResponse = await HttpClient.Get(teamKey);
                    if (!teamResponse.IsSuccessful)
                    {
                        observer.OnError(teamResponse.Exception);
                    }
                    else
                    {
                        observer.OnNext(teamResponse.Data);
                        observer.OnCompleted();
                    }
                }));
        }
    }
}
