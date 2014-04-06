using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamesDB.Data;
using GamesDB.Data.DataSet1TableAdapters;

namespace GamesDB.View_Model
{
    class MainViewModel
    {
        public DataView GridData
        {
            get
            {
                var ds = new DataSet1.gamesDataTable();
                gamesTableAdapter gmAdapter = new gamesTableAdapter();
                gmAdapter.Fill(ds);
                return ds.DefaultView;
                /*
                using (SqlConnection conn = new SqlConnection("Data Source=ozeron-pc;Initial Catalog=games;Integrated Security=True"))
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM dbo.games";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                }
                */

            }

        }

    }
}
