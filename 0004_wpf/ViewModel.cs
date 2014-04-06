using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0004_wpf.DataSetTableAdapters;


namespace _0004_wpf
{
    class ViewModel
    {
        public DataView GridData
        {
            get
            {
                var ds = new DataSet.ARTICLESDataTable();
                ARTICLESTableAdapter articlesAdapter = new ARTICLESTableAdapter();
                articlesAdapter.Fill(ds);
                return ds.DefaultView;
             }
        }

        public DataView ShowMainWindow
        {
            get
            {
                var ds = new DataSet.MainWindowDataTable();
                MainWindowTableAdapter mainWindowAdapter = new MainWindowTableAdapter();
                mainWindowAdapter.Fill(ds);
                return ds.DefaultView;
            }
        }
    }
} 