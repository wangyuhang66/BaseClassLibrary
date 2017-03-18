using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;

namespace Maticsoft.DBUtility
{
    public class DbHelperOracle
    {
        public static string connectString = ConfigurationManager.AppSettings["ConnectionStringOracle"];

        public static DataSet GetDataSet(string sql)
        {
            return GetDataSet(sql, null);
        }

        public static DataSet GetDataSet(string sql, List<OracleParameter> paramList)
        {
            DataSet ds = new DataSet();
            OracleConnection conn = new OracleConnection(connectString);
            try
            {
                conn.Open();
                OracleCommand comm = new OracleCommand(sql, conn);
                comm.CommandTimeout = 9999999;
                if (paramList != null)
                {
                    comm.Parameters.AddRange(paramList.ToArray());
                }
                OracleDataAdapter da = new OracleDataAdapter(comm);
                da.Fill(ds, "table1");
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
                ds = null;
            return ds;
        }

        public static bool Execute(string sql)
        {
            return Execute(sql, null);
        }

        public static bool Execute(string sql, List<OracleParameter> paramList)
        {
            bool success = false;
            OracleConnection conn = new OracleConnection(connectString);
            try
            {
                conn.Open();
                OracleCommand comm = new OracleCommand(sql, conn);
                comm.CommandTimeout = 9999999;
                if (paramList != null)
                {
                    comm.Parameters.AddRange(paramList.ToArray());
                }
                int rows = comm.ExecuteNonQuery();
                if (rows > 0)
                    success = true;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return success;
        }

        public static bool ExecuteTrans(Dictionary<string, List<OracleParameter>> sqlAndParamList)
        {
            bool success = false;
            OracleConnection conn = new OracleConnection(connectString);
            try
            {
                conn.Open();
                OracleTransaction trans = conn.BeginTransaction();
                try
                {
                    //Command
                    OracleCommand comm = conn.CreateCommand(); ;
                    comm.CommandTimeout = 9999999;
                    comm.Transaction = trans;
                    foreach (string sql in sqlAndParamList.Keys)
                    {
                        comm.CommandText = sql;
                        comm.Parameters.Clear();
                        List<OracleParameter> paramList = sqlAndParamList[sql];
                        if (paramList != null)
                        {
                            comm.Parameters.AddRange(paramList.ToArray());
                        }
                        int rows = comm.ExecuteNonQuery();
                    }
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    throw;
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return success;
        }
    }
}
