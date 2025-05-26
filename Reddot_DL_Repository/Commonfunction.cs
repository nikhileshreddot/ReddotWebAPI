using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Drawing;
using Microsoft.Extensions.Configuration.Json;
using Reddot_EF;

namespace Reddot_DL_Repository
{
   public class Commonfunction
    {
        SqlConnection SqlConn = null;
        SqlCommand SqlCmd = null;
        SqlDataAdapter da = null;
        SqlTransaction trans = null;
        string errormsg;
        DataSet ds = null;
        string Conn;
    
        public Commonfunction()
        {
            var configuation = GetConfiguration();
            Conn = configuation.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }
        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
        //        public Commonfunction()
        //        {


        //            Conn = configuration.GetConnectionString("DefaultConnection");
        ////Conn = ConfigurationManager.ConnectionStrings["ConnectionStrings:DefaultConnection"].ToString();
        //        }

        public bool ExecuteNonQuery(string SqlCommondText, SqlParameter[] p)
        {
            errormsg = "";
            bool t = false;
            using (SqlConn = new SqlConnection(Conn))
            {
                SqlConn.Open();
                trans = SqlConn.BeginTransaction();
                using (SqlCmd = new SqlCommand(SqlCommondText, SqlConn, trans))
                {
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    int k = p.Length;
                    int j = 0;
                    while (j < k)
                    {
                        SqlCmd.Parameters.AddWithValue(p[j].ParameterName, p[j].Value);
                        j = j + 1;
                    }
                    try
                    {
                        SqlCmd.CommandTimeout = 0;
                        int i = SqlCmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            trans.Commit();
                            t = true;
                        };
                    }
                    catch (Exception ex)
                    {
                        errormsg = ex.Message;
                        trans.Rollback();
                        t = false;
                    }
                    finally
                    {
                        trans.Dispose();
                    }
                }

            }
            return t;
        }
        #region Sp_ForInsDelUpdDataOut
        public List<Outcls1> ExecuteNonQueryListID(string SqlCommondText, SqlParameter[] p)
        {
            errormsg = "";
            bool t = false;
            List<Outcls1> str1 = new List<Outcls1>();
            str1.Clear();
            using (SqlConn = new SqlConnection(Conn))
            {
                SqlConn.Open();
                trans = SqlConn.BeginTransaction();
                using (SqlCmd = new SqlCommand(SqlCommondText, SqlConn, trans))
                {
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    int k = p.Length;
                    int j = 0;
                    while (j < k - 2)
                    {
                        SqlCmd.Parameters.AddWithValue(p[j].ParameterName, p[j].Value);
                        j = j + 1;
                    }
                    SqlCmd.Parameters.Add(p[k - 2].ParameterName, SqlDbType.Int).Direction = ParameterDirection.Output;
                    SqlCmd.Parameters.Add(p[k - 1].ParameterName, SqlDbType.NVarChar, 1000).Direction = ParameterDirection.Output;
                    try
                    {
                        SqlCmd.CommandTimeout = 0;
                        int i = SqlCmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            trans.Commit();
                            t = true;
                            str1.Add(new Outcls1
                            {
                                Outtf = t,
                                Id = Convert.ToInt32(SqlCmd.Parameters[p[k - 2].ParameterName].Value.ToString()),
                                Responsemsg = SqlCmd.Parameters[p[k - 1].ParameterName].Value.ToString()
                            });
                        }
                        else
                        {
                            t = false;
                            str1.Add(new Outcls1
                            {
                                Outtf = t,
                                Id = Convert.ToInt32(SqlCmd.Parameters[p[k - 2].ParameterName].Value.ToString()),
                                Responsemsg = SqlCmd.Parameters[p[k - 1].ParameterName].Value.ToString()
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        errormsg = ex.Message;
                        trans.Rollback();
                        t = false;
                        str1.Add(new Outcls1
                        {
                            Outtf = t,
                            Id = -1,
                            Responsemsg = errormsg
                        });
                    }
                    finally
                    {
                        trans.Dispose();
                    }
                }

            }
            return str1;
        }
        #endregion

        #region Sp_RetriveDataset
        public DataSet ExecuteDataSet(string SqlCommandText, CommandType cmdd, SqlParameter[] p)
        {
            using (SqlConn = new SqlConnection(Conn))
            {
                try
                {
                    SqlConn.Open();
                    da = new SqlDataAdapter(SqlCommandText, SqlConn);
                    da.SelectCommand.CommandTimeout = 0;
                    da.SelectCommand.CommandType = cmdd;
                    da.SelectCommand.Parameters.AddRange(p);
                    ds = new DataSet();
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    errormsg = ex.Message;
                }
            }
            return ds;
        }
        #endregion


        #region RetriveDataset
        public DataSet ExecuteDataSet(string SqlCommandText)
        {
            using (SqlConn = new SqlConnection(Conn))
            {
                try
                {
                    SqlConn.Open();
                    da = new SqlDataAdapter(SqlCommandText, SqlConn);
                    da.SelectCommand.CommandTimeout = 0;
                    ds = new DataSet();
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    errormsg = ex.Message;
                }
            }

            return ds;
        }
        public int ExecuteNonQuery(string SqlCommondText)
        {
            int rowaffected = 0;
            using (SqlConnection cn = new SqlConnection(Conn))
            {
                try
                {
                    cn.Open();
                    SqlCmd = new SqlCommand(SqlCommondText, cn);
                    rowaffected = SqlCmd.ExecuteNonQuery();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    if (trans != null)
                        trans.Rollback();
                    throw;
                }
            }
            return rowaffected;
        }

        #endregion
    }
}
