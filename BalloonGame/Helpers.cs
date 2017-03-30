using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreciaTOPMigle;

namespace BalloonGame
{
    static class Helpers
    {
        public static Dictionary<int, IEnumerable<Balloon>> GetPilotWithBalloon(List<Pilot> listPilot, List<Balloon> listBalloon)
        {
            return (from pilot in listPilot  //Join ir group
                    join balloon in listBalloon on pilot.PilotId equals balloon.PilotId
                    group new { Pilot = pilot.PilotId, Balloon = balloon } by pilot.PilotId)
                 .ToDictionary(g => g.Key,
                                g => g.Select(x => x.Balloon));
        }

        static public List<Player> GetScores()   // DataAdapter ir DataTable
        {
            using (SqlConnection c = new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=TreciaTOPMigle.FirmDBContext;Trusted_Connection=True;"))
            {
                c.Open();
                using (SqlDataAdapter a = new SqlDataAdapter("SELECT * FROM Players", c))
                {
                    DataTable t = new DataTable();
                    a.Fill(t);
                    return t.AsEnumerable().Select(m => new Player()
                    {
                        FirmName = m.Field<string>("FirmName"),
                        Score = m.Field<long>("Score"),
                    }).ToList();
                }
            }
        }

    }
}
