using Microsoft.VisualBasic;
using Reddot_DL_Interface;
using Reddot_EF;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Reddot_DL_Repository
{
    public class Bespoke_Repository : IBespoke_Repository
    {

        Commonfunction Com = new Commonfunction();
        

        //public static SAPbobsCOM.CompanyService sCompany = null;
        //public static SAPbobsCOM.GeneralDataCollection oSons;
        //public static SAPbobsCOM.GeneralData oSon;
        //public static SAPbobsCOM.GeneralData oGeneralData;

        ////Dim sCmp As SAPbobsCOM.CompanyService
        //public static SAPbobsCOM.GeneralService oGeneralService;
        //public static SAPbobsCOM.CompanyService sCmp;
        



       
        public async Task<List<Outcls1>> SaveBESPOKE_SAP(MD_BPWBSP BP)
        {
            List<Outcls1> str = new List<Outcls1>();

            string cardcode = string.Empty;
            int Result = 0;
            int ResultR = 0;
            try
            { 
                    if (SAP_ConnectToCompany.ConnectToSAP(BP.DBName) == true)
                    { 
                        string errItemCodes = "", errMachineNos = "";
                    bool errRowFlag = false;
                    int ErrorCode;
                    string ErrMessage;
                    int RefDocSeries, iOrd;
                    string RefDocNum = "", RefDocDate = "", DocNum = "", Usrid = "";
                    
                    SAPbobsCOM.GeneralDataCollection oSons;
                    SAPbobsCOM.GeneralData oSon;
                   SAPbobsCOM.GeneralData oGeneralData;                    
                    SAPbobsCOM.GeneralService oGeneralService;
                    SAPbobsCOM.GeneralDataParams oGeneralParams;
                    SAPbobsCOM.CompanyService sCmp;                    
                    sCmp = SAP_ConnectToCompany.mCompany.GetCompanyService();
                    oGeneralService = sCmp.GetGeneralService("RDD_MD_BPWBSP");
                    oGeneralData = oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralData);

                    Int32  k = 0;
                    DataSet ds = Com.ExecuteDataSet("select isnull(max(x.[row_number]),0) as LinedId from( select -1+row_number() over (order by LineID) as [row_number] from [" + BP.DBName+"].dbo.[@RDD_MR_BPWBSP] where code='"+BP.Code+"') x");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        k = Convert.ToInt32(ds.Tables[0].Rows[0]["LinedId"].ToString());
                    }
                    Int32 k1 = 0;
                    if (BP.MR_BPWBSP1List[0].BSPRJCD is not null)
                    {
                        DataSet ds1 = Com.ExecuteDataSet("select isnull(x.[row_number],0) as LinedId from( select -1+row_number() over (order by LineID) as [row_number],U_BSPRJCD from [" + BP.DBName + "].dbo.[@RDD_MR_BPWBSP] where code='" + BP.Code + "') x where x.U_BSPRJCD='" + BP.MR_BPWBSP1List[0].BSPRJCD + "'");
                        if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                        {
                            k1 = Convert.ToInt32(ds1.Tables[0].Rows[0]["LinedId"].ToString());
                        }
                        else
                        {
                            str.Clear();
                            str.Add(new Outcls1
                            {
                                Cardcode = "",
                                Outtf = false,
                                Id = -1,
                                Responsemsg = "Project Code-" + BP.MR_BPWBSP1List[0].BSPRJCD + " - ErrMsg- Invalid"
                            });
                            return str;
                        }
                    }
                    if (k == 0)
                    {                      
                        oGeneralData.SetProperty("Code", BP.Code);
                        oGeneralData.SetProperty("Name", BP.Name);
                        oGeneralData.SetProperty("U_DBNAME", BP.DBName);
                        oGeneralData.SetProperty("U_STATUS", BP.Status);
                    }
                    else
                    {                                               
                        oGeneralParams = ((SAPbobsCOM.GeneralDataParams)(oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams)));
                        oGeneralParams.SetProperty("Code", BP.Code);                       
                        oGeneralData = oGeneralService.GetByParams(oGeneralParams);
                    }
                        oSons = oGeneralData.Child("RDD_MR_BPWBSP");
                  
                    for (int i = 0; i < BP.MR_BPWBSP1List.Count; i++)
                    {
                        if (k == 0)
                        {
                            oSon = oSons.Add();
                        }else if (k1 > 0)
                        {
                            oSon = oSons.Item(k1);
                        }
                        else
                        {
                            oSon = oSons.Add();
                            oSon    =oSons.Item(k+1);                           
                        }
                        DateTime? dt=null; 
                        if(string.IsNullOrEmpty(BP.BSPKType)==true )
                        {
                            if (string.IsNullOrEmpty(BP.MR_BPWBSP1List[i].BSPRJNM) == true || string.IsNullOrEmpty(BP.MR_BPWBSP1List[i].RSTATUS) == true
                                || string.IsNullOrEmpty(BP.MR_BPWBSP1List[i].BSCDLVAL) == true || string.IsNullOrEmpty(BP.MR_BPWBSP1List[i].TNDNO) == true
                                || string.IsNullOrEmpty(BP.MR_BPWBSP1List[i].PAYTERM) == true || string.IsNullOrEmpty(BP.MR_BPWBSP1List[i].PAYTERMC) == true
                                || string.IsNullOrEmpty(BP.MR_BPWBSP1List[i].STATUS) == true || string.IsNullOrEmpty(BP.MR_BPWBSP1List[i].SCRTYPE) == true
                                || BP.MR_BPWBSP1List[i].EXPDT ==dt || BP.MR_BPWBSP1List[i].ISSUEDT==dt
                                )
                            {
                                str.Clear();
                                str.Add(new Outcls1
                                {
                                    Cardcode = "",
                                    Outtf = false,
                                    Id = -1,
                                    Responsemsg = "Please Filled Mandatory Fields"
                                });
                                return str;
                            }
                            oSon.SetProperty("U_BSPRJNM", BP.MR_BPWBSP1List[i].BSPRJNM);
                            oSon.SetProperty("U_EXPDT", Convert.ToDateTime(BP.MR_BPWBSP1List[i].EXPDT));
                            oSon.SetProperty("U_RSTATUS", BP.MR_BPWBSP1List[i].RSTATUS);
                            oSon.SetProperty("U_BSCDLVAL", BP.MR_BPWBSP1List[i].BSCDLVAL);
                            oSon.SetProperty("U_TNDNO", BP.MR_BPWBSP1List[i].TNDNO);
                            oSon.SetProperty("U_ISSUEDT", Convert.ToDateTime(BP.MR_BPWBSP1List[i].ISSUEDT));
                            oSon.SetProperty("U_PAYTERM", BP.MR_BPWBSP1List[i].PAYTERM);
                            oSon.SetProperty("U_PAYTERMC", BP.MR_BPWBSP1List[i].PAYTERMC);
                            oSon.SetProperty("U_STATUS", BP.MR_BPWBSP1List[i].STATUS);
                            oSon.SetProperty("U_SCRTYPE", BP.MR_BPWBSP1List[i].SCRTYPE);                            
                        }else if (BP.BSPKType == "BSPKSTATUS" && string.IsNullOrEmpty(BP.MR_BPWBSP1List[i].STATUS)==false && string.IsNullOrEmpty(BP.MR_BPWBSP1List[i].BSPRJCD)==false)
                        {

                            
                            oSon.SetProperty("U_STATUS", BP.MR_BPWBSP1List[i].STATUS);                            
                        }
                        else if (BP.BSPKType == "BSPKPROJSTATUS" && string.IsNullOrEmpty(BP.MR_BPWBSP1List[i].RSTATUS) == false && string.IsNullOrEmpty(BP.MR_BPWBSP1List[i].BSPRJCD) == false)
                        {                            
                            oSon.SetProperty("U_RSTATUS", BP.MR_BPWBSP1List[i].RSTATUS);
                        }
                        else if (BP.BSPKType == "BSPKEXPDATE" && BP.MR_BPWBSP1List[i].EXPDT != dt && string.IsNullOrEmpty(BP.MR_BPWBSP1List[i].BSPRJCD) == false)
                        {
                            oSon.SetProperty("U_EXPDT", Convert.ToDateTime(BP.MR_BPWBSP1List[i].EXPDT));
                        }
                        else if (BP.BSPKType == "BSPKTEMP" && BP.MR_BPWBSP1List[i].LIMTEXPDT != dt && string.IsNullOrEmpty(BP.MR_BPWBSP1List[i].LIMITAMT) == false && string.IsNullOrEmpty(BP.MR_BPWBSP1List[i].BSPRJCD) == false)
                        {
                            oSon.SetProperty("U_BTLIMIT", BP.MR_BPWBSP1List[i].LIMITAMT);
                            oSon.SetProperty("U_TVDATE", Convert.ToDateTime(BP.MR_BPWBSP1List[i].LIMTEXPDT));
                        }
                        else
                        {
                            str.Clear();
                            str.Add(new Outcls1
                            {
                                Cardcode = "",
                                Outtf = false,
                                Id = -1,
                                Responsemsg = "Please Filled Mandatory Fields :"+ BP.BSPKType
                            });
                            return str;
                        }
                    }
                    if (k > 0)
                    {
                        oGeneralService.Update(oGeneralData);                        
                    }
                    else
                    {
                        oGeneralService.Add(oGeneralData);
                    }                            
                    if (Result != 0 && ResultR!=0)
                    {
                        SAP_ConnectToCompany.mCompany.GetLastError(out ErrorCode, out ErrMessage);

                        string sUpdateLog;
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oGeneralData);

                        sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                        + " Values('" + BP.DBName + "', 'RDD_BESPOKE_ODOO','" + BP.BSPKID + "', '" + ErrorCode.ToString().Replace("'","") + "','" + ErrMessage.ToString().Replace("'", "") + "',getdate())";

                        Com.ExecuteNonQuery(sUpdateLog);

                        str.Clear();
                        str.Add(new Outcls1
                        {
                            Cardcode = "",
                            Outtf = false,
                            Id = -1,
                            Responsemsg = "ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage
                        });

                    }
                    else
                    {
                        string dockey = SAP_ConnectToCompany.mCompany.GetNewObjectKey();
                        string docType = SAP_ConnectToCompany.mCompany.GetNewObjectType();

                        string sUpdateLog;
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oGeneralData);

                        sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                        + " Values('" + BP.DBName + "', 'RDD_BESPOKE_ODOO','" + BP.BSPKID + "', '" + BP.Username + "','Succcess',getdate())";

                        DataSet oDs;
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oGeneralData);
                        str.Clear();
                        str.Add(new Outcls1
                        {
                            Cardcode = dockey,
                            Outtf = true,
                            Id = BP.BSPKID,
                            Responsemsg = "Success"
                        });


                    }
                }
                else
                {
                    string sUpdateLog;
                    sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                    + " Values('" + BP.DBName + "', 'RDD_BESPOKE_ODOO','" + BP.Code + "', '" + "SAP-Connection Failed" + "','" + "SAP-Connection Failed" + "',getdate())";

                    Com.ExecuteNonQuery(sUpdateLog);

                    str.Clear();
                    str.Add(new Outcls1
                    {
                        Cardcode = "",
                        Outtf = false,
                        Id = -1,
                        Responsemsg = "ErrCode-00004 - ErrMsg-SAP Connection Failed"
                    });
                }

            }
            catch (Exception ex)
            {
                string sUpdateLog;
               

                sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                + " Values('" + BP.DBName + "', 'RDD_BESPOKE_ODOO','" + BP.BSPKID + "', '" + ex.Message.ToString().Replace("'", "") + "','" + ex.Message.ToString().Replace("'", "") + "',getdate())";

                Com.ExecuteNonQuery(sUpdateLog);

                str.Clear();
                str.Add(new Outcls1
                {
                    Cardcode = cardcode,
                    Outtf = false,
                    Id = -1,
                    Responsemsg = ex.Message 
                });
            }

            

            return str;
        }
    }
}
