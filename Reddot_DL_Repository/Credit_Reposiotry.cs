using Reddot_DL_Interface;
using Reddot_EF;
using Reddot_View_Model;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_DL_Repository
{
    public class Credit_Reposiotry: ICredit_Reposiotry
    {
        Commonfunction Com = new Commonfunction();
        public async Task<List<Outcls1>> SaveBP_Credit_SAP(CreditBP bP)
        {
            List<Outcls1> str = new List<Outcls1>();
            DataSet result_ds = new DataSet();
            int priority = 0;
            int paytermcode = 0;
            string Cardcode = "";
            Int32 KYC_ID = 0;
            DataSet result_ds1 = new DataSet();
            DataTable t1 = new DataTable("table");
            t1.Columns.Add("Result");
            t1.Columns.Add("Message");
            string errormg=string.Empty;
            int Result = 0;
            if(bP.DBname=="" || bP.code == "" || bP.Username =="")
            {
                Result = -14;
                errormg = "Kindly add DBNAME , BPCODE,Username";

            }
            else 
            if (bP.CreditType.ToString().ToLower() == "paycredit")
            {                             
                if (bP.Priority == null || bP.Payterm == null )
                {
                    Result = -14;
                    errormg = "Kindly add Mandtory Field : Priority & Payterm ";
                }else if(bP.CreditAmount >0 &&( bP.CrdupdateDate is null || bP.CrdExpDate is null) && (bP.Priority!="CASH" ||bP.Priority!="CDC" ) )
                {
                    Result = -14;
                    errormg = "Kindly add Mandtory Field: Credit Amount ,Cr from date, Cr To date ";
                }
                else
                {
                    //Com.ExecuteDataSet("GETOdoo_Priority_Payterm")

                    SqlParameter[] Para = {
                        new SqlParameter("@dbname",bP.DBname),
                        new SqlParameter("@Priority",bP.Priority),
                         new SqlParameter("@Payterm",bP.Payterm)
                         
                };
                    DataSet DS = Com.ExecuteDataSet("GETOdoo_Priority_Payterm", CommandType.StoredProcedure, Para);
                    //DBName	Territory	Countrycode	Indcode	GroupCode
                    //Customer	NewCode	DBName

                    priority = Convert.ToInt16(DS.Tables[0].Rows[0]["Prioritys"].ToString());
                    paytermcode=Convert.ToInt16(DS.Tables[0].Rows[0]["Paytermcode"].ToString());


                    if(priority ==-2 || paytermcode == -2)
                    {
                        Result = -14;
                        errormg = "Invalid Payterm , Priority ";
                    }
                }
                
            }
            else if (bP.CreditType.ToString().ToLower() == "temp")
            {
                if((bP.TempRemark is null || bP.TempRemark=="") || bP.TempCrdExpDate is null || bP.TempCreditAmt <=0 )
                {
                    Result = -14;
                    errormg = "Kindly add Mandtory Field Temp : Exp date ,Amount,Remark ";
                }
            }
            else if (bP.CreditType.ToString().ToLower() == "tstatus")
            {
                if(bP.Tstatus is null || bP.Tstatus=="" || bP.TstatusRemark == "")
                {
                    Result = -14;
                    errormg = "Kindly add Mandtory Field : Transaction Status , Remark";
                }
            }
            else if (bP.CreditType.ToString().ToLower() == "clstatus")
            {
                if (bP.Clstatus is null || bP.Clstatus == "" || bP.ClstatusRemark == "")
                {
                    Result = -14;
                    errormg = "Kindly add Mandtory Field : Cl status , Remark";
                }
            }
            else
            {
                Result = -13;
                errormg = "Wrong Credit Type:" + bP.CreditType + " Correct Credit Type 'temp,tstatus,clstatus,clstatusexp'";
            }
            if (Result < 0) {
                str.Clear();
                str.Add(new Outcls1
                {
                    Cardcode = "",
                    Outtf = false,
                    Id = Result,
                    Responsemsg =errormg 
                });
                return str;
            }
            try
            {
                if (SAP_ConnectToCompany.ConnectToSAP(bP.DBname) == true)
                {
                    string errItemCodes = "", errMachineNos = "";
                    bool errRowFlag = false;
                    int ErrorCode;
                    string ErrMessage;
                    int RefDocSeries, iOrd;
                    string RefDocNum = "", RefDocDate = "", DocNum = "", Usrid = "";
                    int countB = 0;
                    int CountS = 0;

                    SAPbobsCOM.BusinessPartners oBP = null;
                    oBP = (SAPbobsCOM.BusinessPartners)SAP_ConnectToCompany.mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
                    SAPbobsCOM.BPAddresses oBpA = null;
                    if (oBP.GetByKey(bP.code.ToString()))
                    {

                        DateTime? myTime = null;


                        if (bP.CreditType.ToString().ToLower() == "paycredit")
                        {
                            oBP.UserFields.Fields.Item("GroupNum").Value = Convert.ToInt16(paytermcode);
                            oBP.Priority = Convert.ToInt32(priority);
                            oBP.CreditLimit =Convert.ToDouble(bP.CreditAmount);
                            
                            oBP.UserFields.Fields.Item("U_cl_expiry").Value = bP.CrdExpDate is null ? Convert.ToDateTime(myTime) : Convert.ToDateTime(bP.CrdExpDate);
                            oBP.UserFields.Fields.Item("U_CLUpdateDate").Value = bP.CrdupdateDate is null ? Convert.ToDateTime(myTime) : Convert.ToDateTime(bP.CrdupdateDate);
                            oBP.UserFields.Fields.Item("U_CLExpiryExntension").Value = bP.Clextesionmonth is null ? 0 : Convert.ToInt16(bP.Clextesionmonth);
                        }
                        else
                        if (bP.CreditType.ToString().ToLower() == "temp")
                        {
                            oBP.UserFields.Fields.Item("U_temp_expdate").Value = bP.TempCrdExpDate is null ? Convert.ToDateTime(myTime) : Convert.ToDateTime(bP.TempCrdExpDate);
                            oBP.UserFields.Fields.Item("U_temp_cl").Value = bP.TempCreditAmt is null ? 0 :  Convert.ToDouble(bP.TempCreditAmt);
                            oBP.UserFields.Fields.Item("U_tempCLRemark").Value = bP.TempRemark;
                        }
                        else if (bP.CreditType.ToString().ToLower() == "tstatus")
                        {
                            oBP.UserFields.Fields.Item("U_status").Value = bP.Tstatus is null ? "" : bP.Tstatus;
                            oBP.UserFields.Fields.Item("U_StatusRemark").Value = bP.TstatusRemark is null ? "" : bP.TstatusRemark;

                        }
                        else if (bP.CreditType.ToString().ToLower() == "clstatus")
                        {
                            oBP.UserFields.Fields.Item("U_CLStatus").Value = bP.Clstatus is null ? "O" : bP.Clstatus;
                            oBP.UserFields.Fields.Item("U_CLStatusRemark").Value = bP.ClstatusRemark is null ? "" : bP.ClstatusRemark;
                        }

                        else
                        {
                            str.Clear();
                            str.Add(new Outcls1
                            {
                                Cardcode = "",
                                Outtf = false,
                                Id = -13,
                                Responsemsg = "Wrong Credit Type:" + bP.CreditType + " Correct Credit Type 'temp,tstatus,clstatus,clstatusexp'"
                            });
                            return str;
                        }




                        Result = oBP.Update();
                    }
                    else
                    {
                        Result = -12;
                    }
                    if (Result == -12)
                    {
                        string sCardCode = SAP_ConnectToCompany.mCompany.GetNewObjectKey();
                        string docType = SAP_ConnectToCompany.mCompany.GetNewObjectType();
                        t1.Rows.Add("False", "This customer does not exits in SAP");
                        t1.Rows.Add("Err", "This customer does not exits in SAP");
                        DataSet oDs;

                        SAPbobsCOM.Recordset oRs;
                        oRs = (SAPbobsCOM.Recordset)SAP_ConnectToCompany.mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                        string sUpdateLog;
                        sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                        + " Values('" + bP.DBname + "', 'Credit Odoo -" + bP.CreditType + "','" + bP.code + "-" + bP.Username.ToString() + "', '00','This customer does not exits in SAP',getdate())";
                        oRs.DoQuery(sUpdateLog);

                        str.Clear();
                        str.Add(new Outcls1
                        {
                            Cardcode = "",
                            Outtf = false,
                            Id = 1,
                            Responsemsg = "This customer does not exits in SAP"
                        });

                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oBP);

                    }
                    else if (Result != 0)
                    {
                        SAP_ConnectToCompany.mCompany.GetLastError(out ErrorCode, out ErrMessage);
                        t1.Rows.Add("False", "Credit Customer Posting Failed in SAP");
                        t1.Rows.Add("Err", "ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                        // result_ds.Tables.Add(t1);

                        SAPbobsCOM.Recordset oRs;
                        oRs = (SAPbobsCOM.Recordset)SAP_ConnectToCompany.mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                        string sUpdateLog;
                        sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                        + " Values('" + bP.DBname + "', 'Credit Odoo-" + bP.CreditType + "','" + bP.code + "-" + bP.Username.ToString() + "', '" + ErrorCode.ToString() + "','" + ErrMessage.Replace("'","") + "',getdate())";
                        oRs.DoQuery(sUpdateLog);
                        str.Clear();
                        str.Add(new Outcls1
                        {
                            Cardcode = bP.code,
                            Outtf = false,
                            Id = -1,
                            Responsemsg = "ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage
                        });

                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oBP);

                    }
                    else
                    {
                        string sCardCode = SAP_ConnectToCompany.mCompany.GetNewObjectKey();
                        string docType = SAP_ConnectToCompany.mCompany.GetNewObjectType();

                        DataSet oDs;

                        SAPbobsCOM.Recordset oRs;
                        oRs = (SAPbobsCOM.Recordset)SAP_ConnectToCompany.mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                        string sUpdateLog;
                        sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                        + " Values('" + bP.DBname + "', 'Credit Odoo-"+bP.CreditType.ToString()+ "','"+bP.code+ "-" + bP.Username.ToString()+"', '00','Success',getdate())";
                        oRs.DoQuery(sUpdateLog);
                        // sUpdateLog = "insert into tejSAP.DBO.BPStatus_StatusChangeLog (DBName,CardCode,CardName,StatusChangeType,OldTransStatus," +
                        //     "OldCLStatus,NewTransStatus,NewCLStatus,TransStatusChangeRemark,NewCLStatus,CLStatusChangeRemark," +
                        //     "TransMailBody,CLMailBody,CreatedOn) "
                        //+ " Values('" + bP.DBname + "', 'Credit','1', '00','Success',getdate())";
                        // oRs.DoQuery(sUpdateLog);

                        str.Clear();
                        str.Add(new Outcls1
                        {
                            Cardcode = "",
                            Outtf = true,
                            Id = 1,
                            Responsemsg = "Success"
                        });
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oBP);

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
    }
}
