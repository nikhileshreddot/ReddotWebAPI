using Reddot_DL_Interface;
using Reddot_EF;
using Reddot_View_Model;
using SAPbobsCOM;
using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Reddot_DL_Repository
{
    public class Item_Master_Repository : IItem_Master
    {
        Commonfunction com = new Commonfunction();
        public static string sCode = "", sIPAdress = "", sServerName = "", sDBName1 = "", sDBPassword = "", sDBUserName = "", sB1Password = "", sB1UserName = "";
        public static string SAPServer = "", LICServer = "";
        public static SAPbobsCOM.Company mCompany, oCompanyAE, oCompanyUG, oCompanyTZ, oCompanyKE, oCompanyZM, oCompanyML, oCompanyTRI, oCompanyBT, oCompanyMAU;

        public static bool ConnectToSAP(ref SAPbobsCOM.Company _oCompany, string sDBName)
        {
            try
            {
                Commonfunction Com = new Commonfunction();
                var configuation = Com.GetConfiguration();
                //Conn = configuation.GetSection("APP_Name").GetSection("DefaultConnection").Value;

                string SAPconstring = string.Empty;

                if (sDBName == "SAPAE")
                {
                    SAPconstring = configuation.GetSection("AppSettings").GetSection("SAPCompanyConnectCredsSAPAE").Value;
                }
                else if (sDBName == "SAPKE")
                {
                    SAPconstring = configuation.GetSection("AppSettings").GetSection("SAPCompanyConnectCredsKE").Value;
                }
                else if (sDBName == "SAPKE-Test")
                {
                    SAPconstring = configuation.GetSection("AppSettings").GetSection("SAPCompanyConnectCredsKE").Value;
                }
                else if (sDBName == "SAPTZ")
                {
                    SAPconstring = configuation.GetSection("AppSettings").GetSection("SAPCompanyConnectCredsTZ").Value;
                }
                else if (sDBName == "SAPUG")
                {
                    SAPconstring = configuation.GetSection("AppSettings").GetSection("SAPCompanyConnectCredsUG").Value;
                }
                else if (sDBName == "SAPZM")
                {
                    SAPconstring = configuation.GetSection("AppSettings").GetSection("SAPCompanyConnectCredsZM").Value;
                }
                else if (sDBName == "SAPML")
                {
                    SAPconstring = configuation.GetSection("AppSettings").GetSection("SAPCompanyConnectCredsML").Value;
                }
                else if (sDBName == "SAPTRI")
                {
                    SAPconstring = configuation.GetSection("AppSettings").GetSection("SAPCompanyConnectCredsTRI").Value;
                }
                else if (sDBName == "SAPTRI-TEST")
                {
                    SAPconstring = configuation.GetSection("AppSettings").GetSection("SAPCompanyConnectCredsTRI").Value;
                }
                else if (sDBName == "SAPBT")
                {
                    SAPconstring = configuation.GetSection("AppSettings").GetSection("SAPCompanyConnectCredsBT").Value;
                }
                else if (sDBName == "SAPMAU")
                {
                    SAPconstring = configuation.GetSection("AppSettings").GetSection("SAPCompanyConnectCredsMAU").Value;
                }
                else if (sDBName == "SAPMAU_TEST")
                {
                    SAPconstring = configuation.GetSection("AppSettings").GetSection("SAPCompanyConnectCredsMAU").Value;
                }
                if (string.IsNullOrEmpty(SAPconstring) == false)
                {
                    string[] connElements = SAPconstring.Split(';');

                    SAPServer = connElements[0];
                    sDBUserName = connElements[1];
                    sDBPassword = connElements[2];
                    sDBName1 = connElements[3];
                    LICServer = connElements[6]; //"192.168.56.131:30000";// connElements[6]; ; //
                    sB1UserName = connElements[4];
                    sB1Password = connElements[5];

                    _oCompany = new SAPbobsCOM.Company();//.Company();

                    _oCompany.UseTrusted = false;
                    _oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2019;
                    _oCompany.Server = SAPServer;
                    _oCompany.LicenseServer = LICServer;
                    _oCompany.CompanyDB = sDBName1;
                    _oCompany.UserName = sB1UserName;
                    _oCompany.Password = sB1Password;
                    _oCompany.language = SAPbobsCOM.BoSuppLangs.ln_English;
                    _oCompany.DbUserName = sDBUserName;
                    _oCompany.DbPassword = sDBPassword;


                    int iErrCode = 0;
                    int iCounter = 0;

                    do
                    {

                        iErrCode = _oCompany.Connect();
                        if (iErrCode != 0)
                        {
                            iCounter = iCounter + 1;
                            if (iCounter > 10)
                            {
                                break;
                            }
                            System.Threading.Thread.Sleep(iCounter * 50);
                            GC.Collect();
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(iCounter * 50);
                            GC.Collect();

                            break;
                        }
                    }
                    while (_oCompany.Connected == false);

                    if (iErrCode != 0)
                    {
                        string strErr;
                        _oCompany.GetLastError(out iErrCode, out strErr);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public DataSet Connet_To_SAPDB(string dbname)
        {
            DataSet result_ds = new DataSet();
            DataTable t1 = new DataTable("table");
            t1.Columns.Add("Result");
            t1.Columns.Add("Message");

            try
            {
                int ErrorCode;
                string ErrMessage;

                string[] DB_NameList = dbname.Split(';');

                for (int i = 0; i < DB_NameList.Length; i++)
                {
                    if (DB_NameList[i] == "SAPAE")
                    {
                        if (ConnectToSAP(ref oCompanyAE, DB_NameList[i]) == true)
                            t1.Rows.Add("True", DB_NameList[i]);
                        else
                        {
                            oCompanyAE.GetLastError(out ErrorCode, out ErrMessage);
                            t1.Rows.Add("False", DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                        }
                    }
                    else if (DB_NameList[i] == "SAPKE")
                    {
                        if (ConnectToSAP(ref oCompanyKE, DB_NameList[i]) == true)
                            t1.Rows.Add("True", DB_NameList[i]);
                        else
                        {
                            oCompanyKE.GetLastError(out ErrorCode, out ErrMessage);
                            t1.Rows.Add("False", DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                        }
                    }
                    else if (DB_NameList[i] == "SAPTZ")
                    {
                        if (ConnectToSAP(ref oCompanyTZ, DB_NameList[i]) == true)
                            t1.Rows.Add("True", DB_NameList[i]);
                        else
                        {
                            oCompanyTZ.GetLastError(out ErrorCode, out ErrMessage);
                            t1.Rows.Add("False", DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                        }
                    }
                    else if (DB_NameList[i] == "SAPUG")
                    {
                        if (ConnectToSAP(ref oCompanyUG, DB_NameList[i]) == true)
                            t1.Rows.Add("True", DB_NameList[i]);
                        else
                        {
                            oCompanyUG.GetLastError(out ErrorCode, out ErrMessage);
                            t1.Rows.Add("False", DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                        }
                    }
                    else if (DB_NameList[i] == "SAPZM")
                    {
                        if (ConnectToSAP(ref oCompanyZM, DB_NameList[i]) == true)
                            t1.Rows.Add("True", DB_NameList[i]);
                        else
                        {
                            oCompanyZM.GetLastError(out ErrorCode, out ErrMessage);
                            t1.Rows.Add("False", DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                        }
                    }
                    else if (DB_NameList[i] == "SAPML")
                    {
                        if (ConnectToSAP(ref oCompanyML, DB_NameList[i]) == true)
                            t1.Rows.Add("True", DB_NameList[i]);
                        else
                        {
                            if (oCompanyML == null)
                                t1.Rows.Add("False", DB_NameList[i] + " - ErrCode- DB not connected");
                            else
                            {
                                oCompanyML.GetLastError(out ErrorCode, out ErrMessage);
                                t1.Rows.Add("False", DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                            }
                        }
                    }
                    else if (DB_NameList[i] == "SAPTRI")
                    {
                        if (ConnectToSAP(ref oCompanyTRI, DB_NameList[i]) == true)
                            t1.Rows.Add("True", DB_NameList[i]);
                        else
                        {
                            if (oCompanyTRI == null)
                                t1.Rows.Add("False", DB_NameList[i] + " - ErrCode- DB not connected");
                            else
                            {
                                oCompanyTRI.GetLastError(out ErrorCode, out ErrMessage);
                                t1.Rows.Add("False", DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                            }
                        }
                    }
                    else if (DB_NameList[i] == "SAPBT")
                    {
                        if (ConnectToSAP(ref oCompanyBT, DB_NameList[i]) == true)
                            t1.Rows.Add("True", DB_NameList[i]);
                        else
                        {
                            if (oCompanyBT == null)
                                t1.Rows.Add("False", DB_NameList[i] + " - ErrCode- DB not connected");
                            else
                            {
                                oCompanyBT.GetLastError(out ErrorCode, out ErrMessage);
                                t1.Rows.Add("False", DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                            }
                        }
                    }
                    else if (DB_NameList[i] == "SAPMAU")
                    {
                        if (ConnectToSAP(ref oCompanyMAU, DB_NameList[i]) == true)
                            t1.Rows.Add("True", DB_NameList[i]);
                        else
                        {
                            if (oCompanyMAU == null)
                                t1.Rows.Add("False", DB_NameList[i] + " - ErrCode- DB not connected");
                            else
                            {
                                oCompanyMAU.GetLastError(out ErrorCode, out ErrMessage);
                                t1.Rows.Add("False", DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                            }
                        }
                    }

                }
                result_ds.Tables.Add(t1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result_ds;
        }
        public async Task<List<Outcls1>> SaveItem_SAP(Item_Master_VM IM)
        {
            List<Outcls1> str = new List<Outcls1>();
            string Itemcode = string.Empty;
            try
            {
                int ErrorCode;
                string ErrMessage;
                SAPbobsCOM.Items oItem;
                Connet_To_SAPDB(IM.DBList);
                string[] DB_NameList = IM.DBList.Split(';');

                for (int i = 0; i < DB_NameList.Length; i++)
                {
                    if (DB_NameList[i] == "SAPAE")
                        mCompany = oCompanyAE;
                    else if (DB_NameList[i] == "SAPKE")
                        mCompany = oCompanyKE;
                    else if (DB_NameList[i] == "SAPTZ")
                        mCompany = oCompanyTZ;
                    else if (DB_NameList[i] == "SAPUG")
                        mCompany = oCompanyUG;
                    else if (DB_NameList[i] == "SAPZM")
                        mCompany = oCompanyZM;
                    else if (DB_NameList[i] == "SAPML")
                        mCompany = oCompanyML;
                    else if (DB_NameList[i] == "SAPTRI")
                        mCompany = oCompanyTRI;
                    else if (DB_NameList[i] == "SAPBT")
                        mCompany = oCompanyBT;
                    else if (DB_NameList[i] == "SAPMAU")
                        mCompany = oCompanyMAU;

                    if (mCompany.Connected == true)
                    {
                        SAPCls.initGlobals(DB_NameList[i]);
                        oItem = (SAPbobsCOM.Items)mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);

                        oItem.ItemCode = IM.itmCode;
                        Itemcode = IM.itmCode;
                        oItem.ItemName = IM.itmDesc;
                        oItem.ItemsGroupCode = IM.itmGrpId; //sends group id

                        oItem.UserFields.Fields.Item("FirmCode").Value = IM.mfrId.ToString(); // this is group code
                        oItem.UserFields.Fields.Item("U_BU").Value = IM.itmGrpCode; // this is group code
                        oItem.UserFields.Fields.Item("U_Model").Value = "NA";

                        oItem.UserFields.Fields.Item("U_Category").Value = IM.itmProductCategory; //this is product category
                        oItem.UserFields.Fields.Item("U_ProdLine").Value = IM.itmPL; //this is product Line
                        oItem.UserFields.Fields.Item("U_DashboardCategory").Value = IM.itmProductGrp; //this is product Group
                        oItem.UserFields.Fields.Item("U_HSCode").Value = IM.HSCode; //For HS Code

                        double _Lenght, _Width, _Height, _Weight;
                        _Lenght = IM.Length / 100;
                        _Width = IM.Width / 100;
                        _Height = IM.Height / 100;
                        //Volume = Lenght * Width * Height

                        oItem.PurchaseUnitLength = IM.Length / 100;// length
                        oItem.PurchaseUnitWidth = IM.Width / 100; //Width
                        oItem.PurchaseUnitHeight = IM.Height / 100; //Height

                        // oItem.PurchaseUnitVolume = Volume


                        oItem.PurchaseUnitWeight = (IM.Weight * 1000); //Weight

                        //oItem.SalesVATGroup = SAPCls.VATOUT;
                        oItem.GLMethod = SAPbobsCOM.BoGLMethods.glm_ItemClass;
                        oItem.DefaultWarehouse = SAPCls.MASTER_WAREHOUSE;

                        int Result = oItem.Add();
                        if (Result != 0)
                        {
                            mCompany.GetLastError(out ErrorCode, out ErrMessage);
                            str.Clear();
                            str.Add(new Outcls1
                            {
                                Cardcode = "",
                                Outtf = false,
                                Id = -1,
                                Responsemsg = "Error : DB-[" + DB_NameList[i] + "] Failed to Connect : " + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage
                            });
                        }
                        else
                        {
                            str.Add(new Outcls1
                            {
                                Cardcode = Itemcode,
                                Outtf = true,
                                Id = 1,
                                Responsemsg = Itemcode + " DB -[" + DB_NameList[i] + "] Successfuly"
                            });
                            // t1.Rows.Add("True", "DB -[" + DB_NameList[i] + "] Successfuly Add Item :[" + itmCode + "]");
                        }
                    }
                    else
                    {
                        mCompany.GetLastError(out ErrorCode, out ErrMessage);

                        str.Clear();
                        str.Add(new Outcls1
                        {
                            Cardcode = "",
                            Outtf = false,
                            Id = -1,
                            Responsemsg = "Error : DB-[" + DB_NameList[i] + "] Failed to Connect : " + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage
                        });
                        // t1.Rows.Add("False", "Error : DB-[" + DB_NameList[i] + "] Failed to Connect : " + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                    }
                }

            }
            catch (Exception ex)
            {
                str.Clear();
                str.Add(new Outcls1
                {
                    Cardcode = "",
                    Outtf = false,
                    Id = -1,
                    Responsemsg = ex.Message
                });
            }
            return str;
        }

        public async Task<List<Outcls1>> ConnectSAP(string DbName)
        {
            List<Outcls1> str = new List<Outcls1>();
            try
            {
                int ErrorCode;
                string ErrMessage;

                string[] DB_NameList = DbName.Split(';');
                str.Clear();
                for (int i = 0; i < DB_NameList.Length; i++)
                {
                    if (DB_NameList[i] == "SAPAE")
                    {
                        if (ConnectToSAP(ref oCompanyAE, DB_NameList[i]) == true)
                        {
                            str.Add(new Outcls1
                            {
                                Cardcode = DB_NameList[i],
                                Outtf = true,
                                Id = 1,
                                Responsemsg = DB_NameList[i]
                            });
                        }
                        else
                        {
                            oCompanyAE.GetLastError(out ErrorCode, out ErrMessage);

                            str.Add(new Outcls1
                            {
                                Cardcode = DB_NameList[i],
                                Outtf = false,
                                Id = 1,
                                Responsemsg = DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage
                            });
                        }
                    }
                    else if (DB_NameList[i] == "SAPKE")
                    {
                        if (ConnectToSAP(ref oCompanyKE, DB_NameList[i]) == true)
                        {
                            str.Add(new Outcls1
                            {
                                Cardcode = DB_NameList[i],
                                Outtf = true,
                                Id = 1,
                                Responsemsg = DB_NameList[i]
                            });
                        }
                        else
                        {
                            oCompanyAE.GetLastError(out ErrorCode, out ErrMessage);

                            str.Add(new Outcls1
                            {
                                Cardcode = DB_NameList[i],
                                Outtf = false,
                                Id = 1,
                                Responsemsg = DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage
                            });
                        }
                    }
                    else if (DB_NameList[i] == "SAPTZ")
                    {
                        if (ConnectToSAP(ref oCompanyTZ, DB_NameList[i]) == true)
                        {
                            str.Add(new Outcls1
                            {
                                Cardcode = DB_NameList[i],
                                Outtf = true,
                                Id = 1,
                                Responsemsg = DB_NameList[i]
                            });
                        }
                        else
                        {
                            oCompanyAE.GetLastError(out ErrorCode, out ErrMessage);

                            str.Add(new Outcls1
                            {
                                Cardcode = DB_NameList[i],
                                Outtf = false,
                                Id = 1,
                                Responsemsg = DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage
                            });
                        }
                    }
                    else if (DB_NameList[i] == "SAPUG")
                    {
                        if (ConnectToSAP(ref oCompanyUG, DB_NameList[i]) == true)
                        {
                            str.Add(new Outcls1
                            {
                                Cardcode = DB_NameList[i],
                                Outtf = true,
                                Id = 1,
                                Responsemsg = DB_NameList[i]
                            });
                        }
                        else
                        {
                            oCompanyAE.GetLastError(out ErrorCode, out ErrMessage);

                            str.Add(new Outcls1
                            {
                                Cardcode = DB_NameList[i],
                                Outtf = false,
                                Id = 1,
                                Responsemsg = DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage
                            });
                        }
                    }
                    else if (DB_NameList[i] == "SAPZM")
                    {
                        if (ConnectToSAP(ref oCompanyZM, DB_NameList[i]) == true)
                        {
                            str.Add(new Outcls1
                            {
                                Cardcode = DB_NameList[i],
                                Outtf = true,
                                Id = 1,
                                Responsemsg = DB_NameList[i]
                            });
                        }
                        else
                        {
                            oCompanyAE.GetLastError(out ErrorCode, out ErrMessage);

                            str.Add(new Outcls1
                            {
                                Cardcode = DB_NameList[i],
                                Outtf = false,
                                Id = 1,
                                Responsemsg = DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage
                            });
                        }
                    }
                    else if (DB_NameList[i] == "SAPML")
                    {
                        if (ConnectToSAP(ref oCompanyML, DB_NameList[i]) == true)
                        {
                            str.Add(new Outcls1
                            {
                                Cardcode = DB_NameList[i],
                                Outtf = true,
                                Id = 1,
                                Responsemsg = DB_NameList[i]
                            });
                        }
                        else
                        {
                            oCompanyAE.GetLastError(out ErrorCode, out ErrMessage);

                            str.Add(new Outcls1
                            {
                                Cardcode = DB_NameList[i],
                                Outtf = false,
                                Id = 1,
                                Responsemsg = DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage
                            });
                        }
                    }

                    else if (DB_NameList[i] == "SAPTRI")
                    {
                        if (ConnectToSAP(ref oCompanyTRI, DB_NameList[i]) == true)
                        {
                            str.Add(new Outcls1
                            {
                                Cardcode = DB_NameList[i],
                                Outtf = true,
                                Id = 1,
                                Responsemsg = DB_NameList[i]
                            });
                        }
                        else
                        {
                            oCompanyAE.GetLastError(out ErrorCode, out ErrMessage);

                            str.Add(new Outcls1
                            {
                                Cardcode = DB_NameList[i],
                                Outtf = false,
                                Id = 1,
                                Responsemsg = DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage
                            });
                        }
                    }
                    else if (DB_NameList[i] == "SAPBT")
                    {
                        if (ConnectToSAP(ref oCompanyBT, DB_NameList[i]) == true)
                        {
                            str.Add(new Outcls1
                            {
                                Cardcode = DB_NameList[i],
                                Outtf = true,
                                Id = 1,
                                Responsemsg = DB_NameList[i]
                            });
                        }
                        else
                        {
                            oCompanyAE.GetLastError(out ErrorCode, out ErrMessage);

                            str.Add(new Outcls1
                            {
                                Cardcode = DB_NameList[i],
                                Outtf = false,
                                Id = 1,
                                Responsemsg = DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage
                            });
                        }
                    }
                    else if (DB_NameList[i] == "SAPMAU")
                    {
                        if (ConnectToSAP(ref oCompanyMAU, DB_NameList[i]) == true)
                        {
                            str.Add(new Outcls1
                            {
                                Cardcode = DB_NameList[i],
                                Outtf = true,
                                Id = 1,
                                Responsemsg = DB_NameList[i]
                            });
                        }
                        else
                        {
                            oCompanyAE.GetLastError(out ErrorCode, out ErrMessage);

                            str.Add(new Outcls1
                            {
                                Cardcode = DB_NameList[i],
                                Outtf = false,
                                Id = 1,
                                Responsemsg = DB_NameList[i] + " - ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage
                            });
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                str.Add(new Outcls1
                {
                    Cardcode = "",
                    Outtf = false,
                    Id = 1,
                    Responsemsg = ex.Message
                });
            }
            return str;
        }

        public async Task<List<ItemListSO>> itemListSOs(string prefix, string dbname, string? DispatchLocation, string? ProductType, string? CustomerType)
        {

            List<ItemListSO> item = new List<ItemListSO>();
            try
            {
                DataSet ds = com.ExecuteDataSet("Exec RDD_SOR_GetList_ItemCode_V1 '" + prefix + "','" + dbname + "','" + DispatchLocation + "','" + ProductType + "','" + CustomerType + "'");


                if (ds.Tables.Count > 0)
                {
                    DataTable dtModule;
                    DataRowCollection drc;

                    dtModule = ds.Tables[0];
                    drc = dtModule.Rows;

                    foreach (DataRow dr in drc)
                    {
                        item.Add(new ItemListSO
                        {
                            ActalQty = !string.IsNullOrWhiteSpace(dr["ActalQty"].ToString()) ? Convert.ToDouble(dr["ActalQty"].ToString()) : 0,
                            DfltWH = !string.IsNullOrWhiteSpace(dr["DfltWH"].ToString()) ? dr["DfltWH"].ToString() : "",
                            ItemCode = !string.IsNullOrWhiteSpace(dr["ItemCode"].ToString()) ? dr["ItemCode"].ToString() : "",
                            ItemName = !string.IsNullOrWhiteSpace(dr["ItemName"].ToString()) ? dr["ItemName"].ToString() : "",
                            OnHand = !string.IsNullOrWhiteSpace(dr["OnHand"].ToString()) ? Convert.ToDouble(dr["OnHand"].ToString()) : 0,
                            ProductType = !string.IsNullOrWhiteSpace(dr["ProductType"].ToString()) ? dr["ProductType"].ToString() : "",
                            Rate = !string.IsNullOrWhiteSpace(dr["Rate"].ToString()) ? Convert.ToDouble(dr["Rate"].ToString()) : 0,
                            VatGourpSa = !string.IsNullOrWhiteSpace(dr["VatGourpSa"].ToString()) ? dr["VatGourpSa"].ToString() : "",


                        }); ;
                    }


                }
            }
            catch (Exception ex)
            {
                item.Clear();
                item.Add(new ItemListSO
                {
                    ActalQty = 0,
                    DfltWH = "",
                    ItemCode = "",
                    ItemName = "",
                    OnHand = 0,
                    ProductType = "",
                    Rate = 0,
                    VatGourpSa = ""


                });
            }
            return item;
        }


        public async Task<DataSet> GetItem_SAP(string DbName, Int64? pagesize, Int32? pageno, string type, string? Itemcode, string username)
        {

            SqlParameter[] Para = {
new SqlParameter("@p_dbname",DbName),
new SqlParameter("@p_pagesize",pagesize),
new SqlParameter("@p_pageno",pageno),
new SqlParameter("@p_type",type),
new SqlParameter("@ItemCode",Itemcode),
new SqlParameter("@p_UserName",username),
};
            return com.ExecuteDataSet("GetItemMasterOdoo", CommandType.StoredProcedure, Para);
        }

        public async Task<DataSet> GetFINALGP (string UserName,DateTime startdate ,DateTime enddate)
        {
            SqlParameter[] Para = {
new SqlParameter("@USER_NAME",UserName),
new SqlParameter("@StateDate",startdate),
new SqlParameter("@EndDate",enddate)
};
            return com.ExecuteDataSet("RDDPBI_CRNT_YEAR_ANALYTICS_LOGIN_API", CommandType.StoredProcedure, Para);
        }
        public async Task<DataSet> GetItem_GP_SAP(string dbname, string itemcode, string warehouse, string qtysell, string pricesell, string curr, string opgrebateid)
        {

            SqlParameter[] Para = {
new SqlParameter("@DB",dbname),
new SqlParameter("@ItemCode",itemcode),
new SqlParameter("@Warehouse",warehouse),
new SqlParameter("@QtySell",qtysell),
new SqlParameter("@PriceSell",pricesell),
new SqlParameter("@Curr",curr),
new SqlParameter("@opgRebateId",opgrebateid),
};
            return com.ExecuteDataSet("RDD_SOR_Get_GPAndGPPer", CommandType.StoredProcedure, Para);
        }

        public async Task<DataSet> GetPaydrop(string Dbname)
        {

            SqlParameter[] para = { new SqlParameter("@DBName", Dbname) };
            return com.ExecuteDataSet("Odoo_Pay_drop", CommandType.StoredProcedure, para);
        }
        public async Task<DataSet> Get_ActiveOPGSelloutList(string basedb, string rebatedb, string itemcode)
        {
            SqlParameter[] Para = {
new SqlParameter("@BaseDB",basedb),
new SqlParameter("@itemcode",itemcode),
new SqlParameter("@RebateForDB",rebatedb),

};
            return com.ExecuteDataSet("RDD_SOR_Get_ActiveOPGSelloutList", CommandType.StoredProcedure, Para);
        }

        public async Task<DataSet> GetItem_Warehouse(string DbName, string itemcode)
        {
            SqlParameter[] sqlParameters = { new SqlParameter("@p_DBName",DbName),
new SqlParameter("@p_prefix",itemcode)};
            return com.ExecuteDataSet("RDD_SOR_GetList_ItemCode_Warehouse_V1", CommandType.StoredProcedure, sqlParameters);
        }

        public async Task<List<InvoiceHeader>> GetItem_SOR_GP_SAP_LIST(string DbName, long? pagesize, int? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username,string types)
        {
            string str = string.Empty;
            DateTime? myTime = null;
            InvoiceHeader Itm1 = new InvoiceHeader();
            List<InvoiceHeader> item = new List<InvoiceHeader>();
            SqlParameter[] sqlParameters = { new SqlParameter("@DBName",DbName),
                                    new SqlParameter("@p_pageno",pageno),
                                    new SqlParameter("@p_pagesize",pagesize),
                                    new SqlParameter("@p_SortColumn",sortcoloumn),
                                    new SqlParameter("@p_SortOrder",sortorder),
                                    new SqlParameter("@s_date",s_date),
                                    new SqlParameter("@e_date",e_date),
                                    new SqlParameter("@p_UserName",username),
                                    new SqlParameter("@p_typ",types)

                                    };
            DataSet ds = com.ExecuteDataSet("RDD_SOR_LIST_GP_INFO_Itemwise", CommandType.StoredProcedure, sqlParameters);
            try
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dtModule;
                    DataRowCollection drc;

                    dtModule = ds.Tables[0];
                    drc = dtModule.Rows;

                    DataTable dtModule1;
                    DataRowCollection drc1;

                    foreach (DataRow dr in drc)
                    {
                        List<Invoicedetails> itemdetails = new List<Invoicedetails>();
                        dtModule1 = ds.Tables[1].Select("Reference=" + dr["Reference"].ToString()).CopyToDataTable();
                        drc1 = dtModule1.Rows;
                        foreach (DataRow dr1 in drc1)
                        {
                            itemdetails.Add(new Invoicedetails
                            {
                               
                                cSimpleCode = !string.IsNullOrWhiteSpace(dr1["cSimpleCode"].ToString()) ? dr1["cSimpleCode"].ToString() : "",
                                Description_1 = !string.IsNullOrWhiteSpace(dr1["Description_1"].ToString()) ? dr1["Description_1"].ToString() : "",
                                GPUSD = !string.IsNullOrWhiteSpace(dr1["GPUSD"].ToString()) ? Convert.ToDouble(dr1["GPUSD"].ToString()) : 0,
                              
                                opgRebate1USD = !string.IsNullOrWhiteSpace(dr1["opgRebate1USD"].ToString()) ? Convert.ToDouble(dr1["opgRebate1USD"].ToString()) : 0,
                                opgSelloutID = !string.IsNullOrWhiteSpace(dr1["opgSelloutID"].ToString()) ? dr1["opgSelloutID"].ToString() : "",
                                Rebate = !string.IsNullOrWhiteSpace(dr1["Rebate"].ToString()) ? Convert.ToDouble(dr1["Rebate"].ToString()) : 0,
                                TamtUSD = !string.IsNullOrWhiteSpace(dr1["TamtUSD"].ToString()) ? Convert.ToDouble(dr1["TamtUSD"].ToString()) : 0,
                                VATGroup= !string.IsNullOrWhiteSpace(dr1["Vatgroup"].ToString()) ? dr1["Vatgroup"].ToString() : "",
                                Vatpercent= !string.IsNullOrWhiteSpace(dr1["vatprcnt"].ToString()) ? Convert.ToDouble(dr1["vatprcnt"].ToString()) : 0,

                                TQuantity = !string.IsNullOrWhiteSpace(dr1["TQuantity"].ToString()) ? Convert.ToDouble(dr1["TQuantity"].ToString()) : 0,
                                
                                WarehouseID = !string.IsNullOrWhiteSpace(dr1["WarehouseID"].ToString()) ? dr1["WarehouseID"].ToString() : ""
                            });
                        }
                        item.Add(new InvoiceHeader
                        {
                            //	PymntGroup


                            PayTerm = !string.IsNullOrWhiteSpace(dr["PayTerm"].ToString()) ? dr["PayTerm"].ToString() : "",
                            GroupNum = !string.IsNullOrWhiteSpace(dr["groupnum"].ToString()) ? Convert.ToInt32(dr["groupnum"].ToString()) : 0,
                            paymnetgroup = !string.IsNullOrWhiteSpace(dr["PymntGroup"].ToString()) ? dr["PymntGroup"].ToString() : "",
                            Renew = !string.IsNullOrWhiteSpace(dr["Renew"].ToString()) ? dr["Renew"].ToString() : "",
                            RenewFrq = !string.IsNullOrWhiteSpace(dr["Renwfrq"].ToString()) ? dr["Renwfrq"].ToString() : "",
                            Renewdt = !string.IsNullOrWhiteSpace(dr["Renewdate"].ToString()) ?  Convert.ToDateTime(dr["Renewdate"].ToString()) : Convert.ToDateTime(myTime),
                            Reference = !string.IsNullOrWhiteSpace(dr["Reference"].ToString()) ? dr["Reference"].ToString() : "",
                            InvoiceNo = !string.IsNullOrWhiteSpace(dr["InvoiceNo"].ToString()) ? dr["InvoiceNo"].ToString() : "",
                            Source = !string.IsNullOrWhiteSpace(dr["Source"].ToString()) ? dr["Source"].ToString() : "",
                            RowNum = !string.IsNullOrWhiteSpace(dr["RowNum"].ToString()) ? Convert.ToInt64(dr["RowNum"].ToString()) : 0,
                            TotalCount = !string.IsNullOrWhiteSpace(dr["TotalCount"].ToString()) ? Convert.ToInt64(dr["TotalCount"].ToString()) : 0,
                            slpcode = !string.IsNullOrWhiteSpace(dr["slpcode"].ToString()) ? Convert.ToInt32(dr["slpcode"].ToString()) : 0,
                            Country= !string.IsNullOrWhiteSpace(dr["country"].ToString()) ? dr["country"].ToString() : "",
                            Transfer= !string.IsNullOrWhiteSpace(dr["Transfer"].ToString()) ?Convert.ToBoolean(dr["Transfer"].ToString()) : false,
                            Account = !string.IsNullOrWhiteSpace(dr["Account"].ToString()) ? dr["Account"].ToString() : "",
                            BizType = !string.IsNullOrWhiteSpace(dr["BizType"].ToString()) ? dr["BizType"].ToString() : "",
                            region = !string.IsNullOrWhiteSpace(dr["region"].ToString()) ? dr["region"].ToString() : "",
                            BIZ_SEG = !string.IsNullOrWhiteSpace(dr["BIZ_SEG"].ToString()) ? dr["BIZ_SEG"].ToString() : "",
                            DOCcur = !string.IsNullOrWhiteSpace(dr["currency"].ToString()) ? dr["currency"].ToString() : "",
                            LPONumber = !string.IsNullOrWhiteSpace(dr["LPONumber"].ToString()) ? dr["LPONumber"].ToString() : "",
                            Month = !string.IsNullOrWhiteSpace(dr["Month"].ToString()) ? dr["Month"].ToString() : "",
                            Name = !string.IsNullOrWhiteSpace(dr["Name"].ToString()) ? dr["Name"].ToString() : "",
                            ProjectCode = !string.IsNullOrWhiteSpace(dr["ProjectCode"].ToString()) ? dr["ProjectCode"].ToString() : "",
                            Totalvalue = !string.IsNullOrWhiteSpace(dr["Totalvalue"].ToString()) ? Convert.ToDouble(dr["Totalvalue"].ToString()) : 0,
                            TxDate = !string.IsNullOrWhiteSpace(dr["TxDate"].ToString()) ? Convert.ToDateTime(dr["TxDate"].ToString()) : DateTime.Now,
                            Year = !string.IsNullOrWhiteSpace(dr["Year"].ToString()) ? dr["Year"].ToString() : "",
                            InvoicedetailsList = itemdetails
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                str = ex.Message.ToString();
                throw;
            }
            return item;
        }

        public async Task<DataSet> Get_Rebate_SAP_LIST(string DbName, long? pagesize, int? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username)
        {
            string str = string.Empty;
          
            SqlParameter[] sqlParameters = { new SqlParameter("@DBName",DbName),
                                    new SqlParameter("@p_pageno",pageno),
                                    new SqlParameter("@p_pagesize",pagesize),
                                    new SqlParameter("@p_SortColumn",sortcoloumn),
                                    new SqlParameter("@p_SortOrder",sortorder),
                                    new SqlParameter("@s_date",s_date),
                                    new SqlParameter("@e_date",e_date),
                                    new SqlParameter("@p_UserName",username),
                                   

                                    };
            
            return com.ExecuteDataSet("RDD_REBATE_LIST", CommandType.StoredProcedure, sqlParameters);
        }

        public async Task<List<JournalHeader>> GetJournal_Rebate_SAP_LIST(string DbName, long? pagesize, int? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username,string types)
        {
            string str = string.Empty;
            JournalHeader Itm1 = new JournalHeader();
            List<JournalHeader> item = new List<JournalHeader>();
            SqlParameter[] sqlParameters = { new SqlParameter("@DBName",DbName),
                                    new SqlParameter("@p_pageno",pageno),
                                    new SqlParameter("@p_pagesize",pagesize),
                                    new SqlParameter("@p_SortColumn",sortcoloumn),
                                    new SqlParameter("@p_SortOrder",sortorder),
                                    new SqlParameter("@s_date",s_date),
                                    new SqlParameter("@e_date",e_date),
                                    new SqlParameter("@p_UserName",username),
                                    new SqlParameter("@p_type",types)

                                    };
            DataSet ds = com.ExecuteDataSet("RDD_Rebate_List_Info", CommandType.StoredProcedure, sqlParameters);
            try
            {
                if (ds.Tables.Count > 0)
                {
                    DataTable dtModule;
                    DataRowCollection drc;

                    dtModule = ds.Tables[0];
                    drc = dtModule.Rows;

                    DataTable dtModule1;
                    DataRowCollection drc1;

                    foreach (DataRow dr in drc)
                    {
                        List<Journaldetails> itemdetails = new List<Journaldetails>();
                        dtModule1 = ds.Tables[1].Select("TransId=" + dr["TransId"].ToString()).CopyToDataTable();
                        drc1 = dtModule1.Rows;
                        foreach (DataRow dr1 in drc1)
                        {
                            itemdetails.Add(new Journaldetails
                            {
                                

                                Line_ID = !string.IsNullOrWhiteSpace(dr1["Line_ID"].ToString()) ? dr1["Line_ID"].ToString() : "",
                                Account = !string.IsNullOrWhiteSpace(dr1["Account"].ToString()) ? dr1["Account"].ToString() : "",
                                Debit = !string.IsNullOrWhiteSpace(dr1["Debit"].ToString()) ? Convert.ToDouble(dr1["Debit"].ToString()) : 0,

                                Credit = !string.IsNullOrWhiteSpace(dr1["Credit"].ToString()) ? Convert.ToDouble(dr1["Credit"].ToString()) : 0,
                                SYSCred = !string.IsNullOrWhiteSpace(dr1["SYSCred"].ToString()) ? Convert.ToDouble(dr1["SYSCred"].ToString()) : 0,
                                SYSDeb = !string.IsNullOrWhiteSpace(dr1["SYSDeb"].ToString()) ? Convert.ToDouble(dr1["SYSDeb"].ToString()) : 0,

                                AcctName = !string.IsNullOrWhiteSpace(dr1["AcctName"].ToString()) ? dr1["AcctName"].ToString() : "",
                                ContraAct = !string.IsNullOrWhiteSpace(dr1["ContraAct"].ToString()) ? dr1["ContraAct"].ToString(): "0",

                                LineMemo = !string.IsNullOrWhiteSpace(dr1["LineMemo"].ToString()) ? dr1["LineMemo"].ToString() : "0",
                               
                                Year = !string.IsNullOrWhiteSpace(dr1["Year"].ToString()) ? dr1["Year"].ToString() : "",
                                Month = !string.IsNullOrWhiteSpace(dr1["Month"].ToString()) ? dr1["Month"].ToString() : "",
                                BU = !string.IsNullOrWhiteSpace(dr1["BU"].ToString()) ? dr1["BU"].ToString() : "",
                                BUGroup = !string.IsNullOrWhiteSpace(dr1["BUGroup"].ToString()) ? dr1["BUGroup"].ToString() : "",
                                CountryMapped = !string.IsNullOrWhiteSpace(dr1["CountryMapped"].ToString()) ? dr1["CountryMapped"].ToString() : ""
                            });
                        }

                       

                        item.Add(new JournalHeader
                        {
                            TransactionName= !string.IsNullOrWhiteSpace(dr["Transaction Name"].ToString()) ? dr["Transaction Name"].ToString() : "",
                            TransactionType = !string.IsNullOrWhiteSpace(dr["Transaction Type"].ToString()) ? dr["Transaction Type"].ToString() : "",
                            BaseRef = !string.IsNullOrWhiteSpace(dr["BaseRef"].ToString()) ? dr["BaseRef"].ToString() : "",
                            Memo = !string.IsNullOrWhiteSpace(dr["Memo"].ToString()) ? dr["Memo"].ToString() : "",
                            ref1 = !string.IsNullOrWhiteSpace(dr["ref1"].ToString()) ? dr["ref1"].ToString() : "",
                            RowNum = !string.IsNullOrWhiteSpace(dr["RowNum"].ToString()) ? Convert.ToInt64(dr["RowNum"].ToString()) : 0,
                            TotalCount = !string.IsNullOrWhiteSpace(dr["TotalCount"].ToString()) ? Convert.ToInt64(dr["TotalCount"].ToString()) : 0,
                            TransId= !string.IsNullOrWhiteSpace(dr["TransId"].ToString()) ?dr["TransId"].ToString() : "0",
                            LocTotal = !string.IsNullOrWhiteSpace(dr["LocTotal"].ToString()) ? Convert.ToDouble(dr["LocTotal"].ToString()) : 0,
                            SysTotal = !string.IsNullOrWhiteSpace(dr["SysTotal"].ToString()) ? Convert.ToDouble(dr["SysTotal"].ToString()) : 0,
                            TaxDate = !string.IsNullOrWhiteSpace(dr["TaxDate"].ToString()) ? Convert.ToDateTime(dr["TaxDate"].ToString()) : DateTime.Now,
                            Year = !string.IsNullOrWhiteSpace(dr["Year"].ToString()) ? dr["Year"].ToString() : "",
                            Month = !string.IsNullOrWhiteSpace(dr["Year"].ToString()) ? dr["Year"].ToString() : "",
                            Project = !string.IsNullOrWhiteSpace(dr["Project"].ToString()) ? dr["Project"].ToString() : "",
                            JournaldetailsList = itemdetails
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                str = ex.Message.ToString();
                throw;
            }
            return item;
        }

        public async Task<DataSet> GetItem_SAP_CurrentDate(string DbName, long? pagesize, int? pageno, string type, string? Itemcode, string username)
        {
            SqlParameter[] Para = {
new SqlParameter("@p_dbname",DbName),
new SqlParameter("@p_pagesize",pagesize),
new SqlParameter("@p_pageno",pageno),
new SqlParameter("@p_type",type),
new SqlParameter("@ItemCode",Itemcode),
new SqlParameter("@p_UserName",username),
};
            return com.ExecuteDataSet("GetItemMasterOdoo_CurrentDate", CommandType.StoredProcedure, Para);
        }
    }
}

