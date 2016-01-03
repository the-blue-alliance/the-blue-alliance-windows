using System.Collections.Generic;
using System.Linq;
using TBA.DataServices;
using TBA.Models;

namespace TBA.Caches
{
    public interface ITeamCache
    {
        bool HasCached(string key);
        TeamModel GetCachedItem(string teamKey);
        void Put(TeamModel updatedModel);
    }

    public class TeamCache : ITeamCache
    {
        public bool HasCached(string teamKey)
        {
            List<TeamModel> _Team = DataStoreHelper.db.Query<TeamModel>("SELECT * FROM TeamModel WHERE Key = ?", teamKey);

            if (_Team.First() != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public TeamModel GetCachedItem(string teamKey)
        {
            TeamModel _Team = DataStoreHelper.db.Query<TeamModel>("SELECT * FROM TeamModel WHERE Key = ?", teamKey).FirstOrDefault();

            return _Team;
        }

        public void Put(TeamModel updatedModel)
        {
            TeamModel _team = DataStoreHelper.db.Query<TeamModel>("SELECT * FROM TeamModel WHERE Key = ?", updatedModel.Key).FirstOrDefault();
            if (_team == null)
            {
                DataStoreHelper.db.Insert(updatedModel);
                return;
            }
            DataStoreHelper.db.Update(updatedModel);
        }
    }
}
