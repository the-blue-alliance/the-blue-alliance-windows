using System;
using TBA.Models;
namespace TBA.DataServices
{
    public interface ITeamClient
    {
        IObservable<TeamModel> GetTeam(string teamKey);
    }
}
