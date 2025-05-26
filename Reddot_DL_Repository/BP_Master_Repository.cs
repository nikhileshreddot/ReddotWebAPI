using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Reddot_DL_Interface;
using Reddot_EF;
using Reddot_View_Model;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Reddot_DL_Repository.Commonfunction;

namespace Reddot_DL_Repository
{
    public class BP_Master_Repository : IBP_Master
    {

        Commonfunction Com = new Commonfunction();
        public List<BP_Master> GetBP(string DBName)
        {

            List<BP_Master> bP_Masters = new List<BP_Master>();
            DataSet ds = new DataSet();
            DataTable dtModule;
            ds = Com.ExecuteDataSet("select cardCode,CardName from [" + DBName + "].dbo.OCRD  where CardType='C' and  isnull(U_IIODOO,0) in (1) order by  CardCode");
            dtModule = ds.Tables[0];
            DataRowCollection drc;
            drc = dtModule.Rows;
            foreach (DataRow dr in drc) {
                bP_Masters.Add(new BP_Master()
                {
                    CardCode = !string.IsNullOrWhiteSpace(dr["cardCode"].ToString()) ? dr["cardCode"].ToString() : "",
                    CardName = !string.IsNullOrWhiteSpace(dr["CardName"].ToString()) ? dr["CardName"].ToString() : "",
                });
            }
            return bP_Masters;
        }

        public async Task<DataSet> GetBP_SAP(string DbName, string Cardtype, Int64? pagesize, Int32? pageno, string type, string? Cardcode, string username)
        {
            SqlParameter[] Para = {
                        new SqlParameter("@p_dbname",DbName),
                        new SqlParameter("@p_pagesize",pagesize),
                         new SqlParameter("@p_pageno",pageno),
                        new SqlParameter("@p_type",type),
                        new SqlParameter("@cardCode",Cardcode),
                        new SqlParameter("@cardtype",Cardtype),
                        new SqlParameter("@p_UserName",username),
                };
            return Com.ExecuteDataSet("GetBPMasterOdoo", CommandType.StoredProcedure, Para);
        }

        public async Task<List<Outcls1>> SaveBP_SAP(BP_Master_SAP_VM bP)
        {
            List<Outcls1> str = new List<Outcls1>();

            DataSet result_ds = new DataSet();


            int Result = 0;
            string Cardcode = "";
            try
            {
                SqlParameter[] Para = {
                        new SqlParameter("@CustomerName",bP.CustomerName),
                        new SqlParameter("@Country",bP.Country),
                         new SqlParameter("@p_dbname",bP.DbName),
                };
                DataSet DS = Com.ExecuteDataSet("Odoo_Customer_Code", CommandType.StoredProcedure, Para);
                //DBName	Territory	Countrycode	Indcode	GroupCode
                //Customer	NewCode	DBName

                string Dbname = DS.Tables[0].Rows[0]["DBName"].ToString();

                if (SAP_ConnectToCompany.ConnectToSAP(Dbname) == true)
                {


                    // need to create this SP

                    if (DS.Tables.Count > 0)
                    {
                        string errItemCodes = "", errMachineNos = "";
                        bool errRowFlag = false;
                        int ErrorCode;
                        string ErrMessage;
                        int RefDocSeries, iOrd;
                        string RefDocNum = "", RefDocDate = "", DocNum = "", Usrid = "";

                        SAPbobsCOM.BusinessPartners oBP = null;
                        

                        oBP = (SAPbobsCOM.BusinessPartners)SAP_ConnectToCompany.mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
                        oBP.CardCode = DS.Tables[1].Rows[0]["NewCode"].ToString().Trim();
                        Cardcode = DS.Tables[1].Rows[0]["NewCode"].ToString().Trim();
                        oBP.CardName = bP.CustomerName;
                        oBP.CardForeignName = bP.CustomerName;


                        if (bP.CardType == "C")
                        {
                            oBP.CardType = BoCardTypes.cCustomer;
                        }
                        else if (bP.CardType == "S")
                        {
                            oBP.CardType = BoCardTypes.cSupplier;
                        }
                        else
                        {
                            oBP.CardType = BoCardTypes.cLid;
                        }

                        oBP.GroupCode = Convert.ToInt32(DS.Tables[0].Rows[0]["GroupCode"].ToString());
                        oBP.Currency = "##";
                        oBP.UserFields.Fields.Item("U_email").Value = bP.Email;
                        oBP.Phone1 = bP.TelephoneNo;
                        oBP.Phone2 = "";
                        oBP.Cellular = "";
                        oBP.Fax = "";
                        oBP.EmailAddress = bP.Email;
                        oBP.Website = "";
                        oBP.Territory = Convert.ToInt32(DS.Tables[0].Rows[0]["Territory"].ToString());
                        // oBP.BusinessType = "C";
                        oBP.Industry = Convert.ToInt32(DS.Tables[0].Rows[0]["IndCode"].ToString());
                        oBP.CompanyPrivate = BoCardCompanyTypes.cCompany;
                        // oBP.VATRegistrationNumber= DS.Tables[0].Rows[0]["PIN_TIN_TRN_Number"].ToString().Trim();
                        oBP.UnifiedFederalTaxID = bP.PIN_TIN_TRN_Number;
                        // oBP.CompanyRegistrationNumber = DS.Tables[0].Rows[0]["PIN_TIN_TRN_Number"].ToString().Trim();
                        // oBP.VatIDNum = DS.Tables[0].Rows[0]["PIN_TIN_TRN_Number"].ToString().Trim();
                        oBP.UserFields.Fields.Item("U_CERTFIC").Value = bP.CertificateofIncorporation;
                        oBP.UserFields.Fields.Item("U_VATNO").Value = bP.VATNumber;
                        oBP.CompanyRegistrationNumber = bP.VATNumber;

                        oBP.PayTermsGrpCode = 1;
                        oBP.Priority = 1;
                        //Address bill to                       

                        oBP.Addresses.AddressName = bP.Address;
                        oBP.Addresses.AddressType = BoAddressType.bo_BillTo;

                        oBP.Addresses.AddressName2 = "";
                        oBP.Addresses.AddressName3 = "";
                        oBP.Addresses.Street = bP.Street;
                        oBP.Addresses.Block = "";
                        oBP.Addresses.City = bP.City;

                        oBP.Addresses.Country = DS.Tables[0].Rows[0]["CountryCode"].ToString();
                        oBP.Addresses.StreetNo = bP.POBOX;
                        oBP.Addresses.BuildingFloorRoom = "";
                        oBP.Addresses.Add();
                        //Address ship to
                        oBP.Addresses.AddressName = bP.Address;
                        oBP.Addresses.AddressType = BoAddressType.bo_ShipTo;

                        oBP.Addresses.AddressName2 = "";
                        oBP.Addresses.AddressName3 = "";
                        oBP.Addresses.Street = bP.Street;
                        oBP.Addresses.Block = "";
                        oBP.Addresses.City = bP.City;

                        oBP.Addresses.Country = DS.Tables[0].Rows[0]["CountryCode"].ToString();
                        oBP.Addresses.StreetNo = bP.POBOX;
                        oBP.Addresses.BuildingFloorRoom = "";
                        if (Dbname == "SAPAE" || Dbname == "SAPMAU" || Dbname == "SAPRDDLLC" || Dbname == "SAPMAU")
                        {
                            oBP.UserFields.Fields.Item("U_QBEDT").Value = bP.QBEDate;
                        }
                        oBP.UserFields.Fields.Item("U_C_SEG").Value = bP.NatureOfBusiness;
                        oBP.Addresses.Add();

                        Result = oBP.Add();

                        if (Result != 0)
                        {
                            SAP_ConnectToCompany.mCompany.GetLastError(out ErrorCode, out ErrMessage);

                            // result_ds.Tables.Add(t1);

                            SAPbobsCOM.Recordset oRs;
                            oRs = (SAPbobsCOM.Recordset)SAP_ConnectToCompany.mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            string sUpdateLog;
                            sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                            + " Values('" + Dbname + "', 'RDD_KYC_Odoo','" + bP.odooid + "', '" + ErrorCode.ToString().Replace("'", "") + "','" + ErrMessage.ToString().Replace("'", "") + "',getdate())";
                            oRs.DoQuery(sUpdateLog);

                            str.Clear();
                            str.Add(new Outcls1
                            {
                                Cardcode = "",
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

                            str.Clear();
                            str.Add(new Outcls1
                            {
                                Cardcode = Cardcode,
                                Outtf = true,
                                Id = bP.odooid,
                                Responsemsg = "Success"
                            });


                            sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                            + " Values('" + Dbname + "', 'RDD_KYC_Odoo'+'" + Cardcode + "','" + bP.odooid + "', '00','Success',getdate())";
                            oRs.DoQuery(sUpdateLog);

                            //SAPbobsCOM.Recordset oRsUK;
                            //oRsUK = (SAPbobsCOM.Recordset)SAP_ConnectToCompany.mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            //string sUpdateKYC;
                            //sUpdateKYC = "Update tejSAP.dbo.RDD_KYC Set TransType='KYC',DBNAME='" + dbname + "',CustomerCode='" + sCardCode + "' "
                            //+ " Where KYC_ID=" + KYC_ID + "";
                            //oRsUK.DoQuery(sUpdateKYC);
                            //sUpdateKYC = "Insert into tejSAP.dbo.RDD_KYC_Log(KeyId, TableName, ColName, ColDescription, OldValue, NewValue, ChangedBy, ChangedOn, Portal)";
                            //sUpdateKYC += "select " + KYC_ID + ", 'RDD_KYC', 'DBName', 'DBName','', '" + dbname + "','" + UserName + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "', 'Staff Portal'";
                            ////oRsUK.DoQuery(sUpdateKYC);
                            ////sUpdateKYC = "Insert into RDD_KYC_Log(KeyId, TableName, ColName, ColDescription, OldValue, NewValue, ChangedBy, ChangedOn, Portal)" +
                            //sUpdateKYC += "union all select " + KYC_ID + ", 'RDD_KYC', 'CustomerCode', 'CustomerCode','', '" + sCardCode + "','" + UserName + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "', 'Staff Portal'";
                            ////oRsUK.DoQuery(sUpdateKYC);
                            ////sUpdateKYC = "Insert into RDD_KYC_Log(KeyId, TableName, ColName, ColDescription, OldValue, NewValue, ChangedBy, ChangedOn, Portal)" +
                            //sUpdateKYC += "union all select " + KYC_ID + ", 'RDD_KYC', 'TransType', 'TransType','CustomerOnBoarding', 'KYC','" + UserName + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "', 'Staff Portal'";
                            //oRsUK.DoQuery(sUpdateKYC);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(oBP);
                            // return result_ds;
                        }
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


        public async Task<DataSet> Get_CustomersDue_Info(string Dbanme, string cardcode)
        {
            SqlParameter[] Para = {
                        new SqlParameter("@DBName",Dbanme),
                        new SqlParameter("@BP",cardcode),


                };
            return Com.ExecuteDataSet("RDD_SOR_Get_CustomerDue_KYC", CommandType.StoredProcedure, Para);
        }

        public BP_Master_VM SaveBrch(BP_Master_VM BP)
        {
            throw new NotImplementedException();
        }

        public async Task<DataSet> GetBESPOKE_SAP(string DbName, long? pagesize, int? pageno, string type, string? Cardcode, string username)
        {
            SqlParameter[] Para = {
                        new SqlParameter("@p_DBANME",DbName),
                        new SqlParameter("@p_pagesize",pagesize),
                         new SqlParameter("@p_pageno",pageno),
                        new SqlParameter("@p_type",type),
                        new SqlParameter("@CardCode",Cardcode),

                        new SqlParameter("@p_UserName",username),
                };
            return Com.ExecuteDataSet("BESPOKE_ODOO_GET", CommandType.StoredProcedure, Para);
        }

        public async Task<List<Outcls1>> UpdateBP_SAP(BP_Master_SAP_VM bP)
        {

            List<Outcls1> str = new List<Outcls1>();
            try
            {
                int ErrorCode;
                string ErrMessage;


                int Result = 0;
                string Cardcode = "";
                
                    SqlParameter[] Para = {
                        new SqlParameter("@p_cardcode",bP.Code),
                        new SqlParameter("@p_DbName",bP.DbName),
                        new SqlParameter("@p_country",bP.Country)
                };
                    DataSet DS = Com.ExecuteDataSet("RDD_ODOO_KYC_Vendor", CommandType.StoredProcedure, Para);
                if (DS.Tables[0].Rows.Count == 0)
                {
                    str= await SaveBP_SAP(bP);
                    //str.Clear();
                    //str.Add(new Outcls1
                    //{
                    //    Cardcode = "",
                    //    Outtf = false,
                    //    Id = -1,
                    //    Responsemsg = "ErrCode-This customer does not exits in SAP - ErrMsg-This customer does not exits in SAP"
                    //});
                    //return str;
                }
                    if (DS.Tables[0].Rows.Count > 0) {

                        if (SAP_ConnectToCompany.ConnectToSAP(bP.DbName) == true)
                        {



                            SAPbobsCOM.BusinessPartners oBP = null;
                            oBP = (SAPbobsCOM.BusinessPartners)SAP_ConnectToCompany.mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
                            SAPbobsCOM.BPAddresses oBpA = null;
                            
                            if (oBP.GetByKey(bP.Code))
                            {

                            bP.Country = DS.Tables[0].Rows[0]["Countrycode"].ToString();
                                if (bP.CardType == "C")
                                {
                                    oBP.CardType = BoCardTypes.cCustomer;
                                }
                                else if (bP.CardType == "S")
                                {
                                    oBP.CardType = BoCardTypes.cSupplier;
                                }
                                else
                                {
                                    oBP.CardType = BoCardTypes.cLid;
                                }


                                oBP.UserFields.Fields.Item("U_email").Value = bP.Email;
                                oBP.Phone1 = bP.TelephoneNo;

                                oBP.EmailAddress = bP.Email;

                                oBP.CompanyPrivate = BoCardCompanyTypes.cCompany;

                                oBP.UnifiedFederalTaxID = bP.PIN_TIN_TRN_Number;

                                oBP.UserFields.Fields.Item("U_CERTFIC").Value = bP.CertificateofIncorporation;
                                oBP.UserFields.Fields.Item("U_VATNO").Value = bP.VATNumber;
                                oBP.CompanyRegistrationNumber = bP.VATNumber;


                                if (bP.DbName == "SAPAE" || bP.DbName == "SAPTRI" || bP.DbName == "SAPMAU" || bP.DbName == "SAPRDDRLLC")
                                {
                                    oBP.UserFields.Fields.Item("U_QBEDT").Value = bP.QBEDate;
                                }
                                oBP.UserFields.Fields.Item("U_C_SEG").Value = bP.NatureOfBusiness;

                                Result = oBP.Update();
                                int countB = 0;
                                if (oBP.GetByKey(bP.Code) && Result == 0)
                                {
                                    string addresB = "";
                                    SAPbobsCOM.BPAddresses addr = oBP.Addresses;
                                   // int a = addr.Count - 1;
                                    DataRow[] ADDresB = DS.Tables[1].Select("AdresType='B' and Address='" + bP.Address + "'");
                                    DataRow[] ADDresB1 = DS.Tables[1].Select("AdresType='B' ");

                                    if (ADDresB.Length > 0)
                                    {
                                        countB = Convert.ToInt32(ADDresB[0]["LineNum"].ToString());
                                        addresB = ADDresB[0]["Address"].ToString();
                                                 oBP.Addresses.SetCurrentLine(countB);
                                                
                                        
                                        oBP.Addresses.AddressType = BoAddressType.bo_BillTo;

                                    }
                                    else if (ADDresB1.Length == 0)
                                    {
                                        addr.Add();

                                        oBP.Addresses.AddressName = "Bill to";
                                        oBP.Addresses.AddressType = BoAddressType.bo_BillTo;

                                    }

                                    else
                                    {
                                        addr.Add();

                                        oBP.Addresses.AddressName = bP.Address;
                                        oBP.Addresses.AddressType = BoAddressType.bo_BillTo;

                                    }


                                    oBP.Addresses.Street = bP.Street;

                                    oBP.Addresses.City = bP.City;

                                    oBP.Addresses.Country = bP.Country;
                                    oBP.Addresses.StreetNo = bP.POBOX;


                                    Result = oBP.Update();



                                }


                                if (oBP.GetByKey(bP.Code))
                                {
                                    int CountS = 0;
                                    string addresS = "";
                                    SAPbobsCOM.BPAddresses addr = oBP.Addresses;
                                   // /int a = addr.Count - 1;
                                    DataRow[] ADDresS = DS.Tables[1].Select("AdresType='S' and Address='" + bP.Address + "'");
                                    DataRow[] ADDresS1 = DS.Tables[1].Select("AdresType='S' ");
                                    if (ADDresS.Length > 0)
                                    {
                                        CountS = Convert.ToInt32(ADDresS[0]["LineNum"].ToString());
                                    
                                        addresS = ADDresS[0]["Address"].ToString();
                                            
                                                oBP.Addresses.SetCurrentLine(CountS);
                                           

                                    }
                                    else
                                    {
                                        addr.Add();
                                        oBP.Addresses.AddressName = bP.Address;

                                    }
                                    oBP.Addresses.AddressType = BoAddressType.bo_ShipTo;
                                    //oBP.Addresses.AddressName2 = "";
                                    //oBP.Addresses.AddressName3 = "";
                                    oBP.Addresses.Street = bP.Street;
                                    //oBP.Addresses.Block = "";
                                    oBP.Addresses.City = bP.City;

                                    oBP.Addresses.Country = bP.Country;
                                    oBP.Addresses.StreetNo = bP.Street;
                                    //oBP.Addresses.BuildingFloorRoom = "";



                                    Result = oBP.Update();

                                }








                            }
                            else
                            {
                                Result = -12;
                            }


                            if (Result == -12)
                            {
                                string sCardCode = SAP_ConnectToCompany.mCompany.GetNewObjectKey();
                                string docType = SAP_ConnectToCompany.mCompany.GetNewObjectType();
                                // t1.Rows.Add("False", "This customer does not exits in SAP");
                                //t1.Rows.Add("Err", "This customer does not exits in SAP");
                                DataSet oDs;

                                SAPbobsCOM.Recordset oRs;
                                oRs = (SAPbobsCOM.Recordset)SAP_ConnectToCompany.mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                string sUpdateLog;
                                sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                                + " Values('" + bP.DbName + "', 'RDD_Vendor_Odoo','" + bP.odooid + "', '00','This customer does not exits in SAP',getdate())";
                                oRs.DoQuery(sUpdateLog);

                                str.Clear();
                                str.Add(new Outcls1
                                {
                                    Cardcode = "",
                                    Outtf = false,
                                    Id = -1,
                                    Responsemsg = "ErrCode-This customer does not exits in SAP - ErrMsg-This customer does not exits in SAP"
                                });


                                System.Runtime.InteropServices.Marshal.ReleaseComObject(oBP);

                            }
                            else if (Result != 0)
                            {
                                SAP_ConnectToCompany.mCompany.GetLastError(out ErrorCode, out ErrMessage);
                                //t1.Rows.Add("False", "KYC Customer Posting Failed in SAP");
                                //t1.Rows.Add("Err", "ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage);
                                // result_ds.Tables.Add(t1);

                                SAPbobsCOM.Recordset oRs;
                                oRs = (SAPbobsCOM.Recordset)SAP_ConnectToCompany.mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                                string sUpdateLog;
                                sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                                + " Values('" + bP.DbName + "', 'RDD_Vendor_odoo','" + bP.odooid + "', '" + ErrorCode.ToString().Replace("'", "") + "','" + ErrMessage.ToString().Replace("'", "") + "',getdate())";
                                oRs.DoQuery(sUpdateLog);

                            str.Clear();
                            str.Add(new Outcls1
                            {
                                Cardcode = "",
                                Outtf = false,
                                Id = -1,
                                Responsemsg = "ErrCode- "+ErrorCode.ToString()+" - ErrMsg- "+ErrMessage+""
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
                                + " Values('" + bP.DbName + "', 'RDD_Vendor_odoo','" + bP.odooid + "', '00','Success',getdate())";
                                oRs.DoQuery(sUpdateLog);

                                str.Clear();
                                str.Add(new Outcls1
                                {
                                    Cardcode = bP.Code.ToString(),
                                    Outtf = true,
                                    Id = bP.odooid,
                                    Responsemsg = "Vendor Updated Succesfuly"
                                });
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(oBP);

                            }
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
                    Responsemsg = " ErrMsg-"+ex.Message+""
                });
            }
            return str;
            }

        public async Task<List<Outcls1>> SaveBP_Vendor_SAP(BP_Master_SAP_VENDOR_VM bP)
        {
            List<Outcls1> str = new List<Outcls1>();

            DataSet result_ds = new DataSet();


            int Result = 0;
            string Cardcode = "";
            try
            {
                SqlParameter[] Para = {
                        new SqlParameter("@CustomerName",bP.CustomerName),
                        new SqlParameter("@Country",bP.Country),
                         new SqlParameter("@p_dbname",bP.DbName),
                };
                DataSet DS = Com.ExecuteDataSet("Odoo_Vendor_Code", CommandType.StoredProcedure, Para);
                //DBName	Territory	Countrycode	Indcode	GroupCode
                //Customer	NewCode	DBName

                string Dbname = DS.Tables[0].Rows[0]["DBName"].ToString();

                if (SAP_ConnectToCompany.ConnectToSAP(Dbname) == true)
                {
                    DateTime? myTime = null;
                    // need to create this SP

                    if (DS.Tables.Count > 0)
                    {
                        string errItemCodes = "", errMachineNos = "";
                        bool errRowFlag = false;
                        int ErrorCode;
                        string ErrMessage;
                        int RefDocSeries, iOrd;
                        string RefDocNum = "", RefDocDate = "", DocNum = "", Usrid = "";

                        SAPbobsCOM.BusinessPartners oBP = null;


                        oBP = (SAPbobsCOM.BusinessPartners)SAP_ConnectToCompany.mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
                        oBP.CardCode = DS.Tables[1].Rows[0]["NewCode"].ToString().Trim();
                        Cardcode = DS.Tables[1].Rows[0]["NewCode"].ToString().Trim();
                        oBP.CardName = bP.CustomerName;
                        oBP.CardForeignName = bP.CustomerName;


                        if (bP.CardType == "C")
                        {
                            oBP.CardType = BoCardTypes.cCustomer;
                        }
                        else if (bP.CardType == "S")
                        {
                            oBP.CardType = BoCardTypes.cSupplier;
                        }
                        else
                        {
                            oBP.CardType = BoCardTypes.cLid;
                        }

                        oBP.GroupCode = Convert.ToInt32(DS.Tables[0].Rows[0]["GroupCode"].ToString());
                        oBP.Currency = "##";
                        oBP.UserFields.Fields.Item("U_email").Value = bP.Email;
                        oBP.Phone1 = bP.TelephoneNo;
                        oBP.Phone2 = "";
                        oBP.Cellular = "";
                        oBP.Fax = "";
                        oBP.EmailAddress = bP.Email;
                        oBP.Website = "";
                        oBP.Territory = Convert.ToInt32(DS.Tables[0].Rows[0]["Territory"].ToString());
                        // oBP.BusinessType = "C";
                        oBP.Industry = Convert.ToInt32(DS.Tables[0].Rows[0]["IndCode"].ToString());
                        oBP.CompanyPrivate = BoCardCompanyTypes.cCompany;
                        // oBP.VATRegistrationNumber= DS.Tables[0].Rows[0]["PIN_TIN_TRN_Number"].ToString().Trim();
                       

                        oBP.PayTermsGrpCode = 1;
                        oBP.Priority = 1;
                        //Address bill to                       

                        oBP.Addresses.AddressName = bP.Address;
                        oBP.Addresses.AddressType = BoAddressType.bo_BillTo;

                        oBP.Addresses.AddressName2 = "";
                        oBP.Addresses.AddressName3 = "";
                        oBP.Addresses.Street = bP.Street;
                        oBP.Addresses.Block = "";
                        oBP.Addresses.City = bP.City;

                        oBP.Addresses.Country = DS.Tables[0].Rows[0]["CountryCode"].ToString();
                        oBP.Addresses.StreetNo = bP.POBOX;
                        oBP.Addresses.BuildingFloorRoom = "";
                        oBP.Addresses.Add();
                        //Address ship to
                        oBP.Addresses.AddressName = bP.Address;
                        oBP.Addresses.AddressType = BoAddressType.bo_ShipTo;

                        oBP.Addresses.AddressName2 = "";
                        oBP.Addresses.AddressName3 = "";
                        oBP.Addresses.Street = bP.Street;
                        oBP.Addresses.Block = "";
                        oBP.Addresses.City = bP.City;

                        oBP.Addresses.Country = DS.Tables[0].Rows[0]["CountryCode"].ToString();
                        oBP.Addresses.StreetNo = bP.POBOX;
                        oBP.Addresses.BuildingFloorRoom = "";
                        
                        oBP.UserFields.Fields.Item("U_TrdName").Value = bP.TradingName ;
                        oBP.UserFields.Fields.Item("U_Org").Value = bP.TypeofOrganisation;
                        if(bP.PIN_TIN_TRN_Number is not null)
                        {                        
                            oBP.UserFields.Fields.Item("U_Trm1").Value = bP.PIN_TIN_TRN_Number is null ?"3":bP.PIN_TIN_TRN_Number;
                            oBP.UserFields.Fields.Item("U_Trm2").Value = bP.VATNumber is null ? "3" : bP.VATNumber;
                            oBP.UserFields.Fields.Item("U_Trm3").Value = bP.TradeLicense is null ? "3" : bP.TradeLicense;
                            oBP.UserFields.Fields.Item("U_Trm4").Value = bP.RegularLicense is null ? "3" : bP.RegularLicense;
                            oBP.UserFields.Fields.Item("U_Trm5").Value = bP.Govtid is null ? "3" : bP.Govtid;
                            oBP.UserFields.Fields.Item("U_KYSPRD").Value = bP.KYCperiod is null ? "3" : bP.KYCperiod;
                            oBP.UserFields.Fields.Item("U_KYSSTS").Value = bP.KYSStatus is null ? "3" : bP.KYSStatus;                                              
                            oBP.UserFields.Fields.Item("U_KYSSDT").Value = bP.KYCstartdate is null ? Convert.ToDateTime(myTime) : Convert.ToDateTime(bP.KYCstartdate);
                            oBP.UserFields.Fields.Item("U_KYSEXDT").Value = bP.KYCexpirydate is null ? Convert.ToDateTime(myTime) : Convert.ToDateTime(bP.KYCexpirydate);
                            oBP.UserFields.Fields.Item("U_KYSEXP").Value = bP.KYCexpirystatus is null?"3":bP.KYCexpirystatus;
                        }
                        oBP.UserFields.Fields.Item("U_No_Dir").Value = bP.Noofdirectores is null ?"":bP.Noofdirectores;

                        
                        oBP.Addresses.Add();

                        Result = oBP.Add();

                        if (Result != 0)
                        {
                            SAP_ConnectToCompany.mCompany.GetLastError(out ErrorCode, out ErrMessage);

                            // result_ds.Tables.Add(t1);

                            SAPbobsCOM.Recordset oRs;
                            oRs = (SAPbobsCOM.Recordset)SAP_ConnectToCompany.mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            string sUpdateLog;
                            sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                            + " Values('" + Dbname + "', 'RDD_Vendor_Odoo','" + bP.odooid + "', '" + ErrorCode.ToString().Replace("'", "") + "','" + ErrMessage.ToString().Replace("'", "") + "',getdate())";
                            oRs.DoQuery(sUpdateLog);

                            str.Clear();
                            str.Add(new Outcls1
                            {
                                Cardcode = "",
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

                            str.Clear();
                            str.Add(new Outcls1
                            {
                                Cardcode = Cardcode,
                                Outtf = true,
                                Id = bP.odooid,
                                Responsemsg = "Success"
                            });


                            sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                            + " Values('" + Dbname + "', 'RDD_Vendor_Odoo'+'" + Cardcode + "','" + bP.odooid + "', '00','Success',getdate())";
                            oRs.DoQuery(sUpdateLog);

                            //SAPbobsCOM.Recordset oRsUK;
                            //oRsUK = (SAPbobsCOM.Recordset)SAP_ConnectToCompany.mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            //string sUpdateKYC;
                            //sUpdateKYC = "Update tejSAP.dbo.RDD_KYC Set TransType='KYC',DBNAME='" + dbname + "',CustomerCode='" + sCardCode + "' "
                            //+ " Where KYC_ID=" + KYC_ID + "";
                            //oRsUK.DoQuery(sUpdateKYC);
                            //sUpdateKYC = "Insert into tejSAP.dbo.RDD_KYC_Log(KeyId, TableName, ColName, ColDescription, OldValue, NewValue, ChangedBy, ChangedOn, Portal)";
                            //sUpdateKYC += "select " + KYC_ID + ", 'RDD_KYC', 'DBName', 'DBName','', '" + dbname + "','" + UserName + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "', 'Staff Portal'";
                            ////oRsUK.DoQuery(sUpdateKYC);
                            ////sUpdateKYC = "Insert into RDD_KYC_Log(KeyId, TableName, ColName, ColDescription, OldValue, NewValue, ChangedBy, ChangedOn, Portal)" +
                            //sUpdateKYC += "union all select " + KYC_ID + ", 'RDD_KYC', 'CustomerCode', 'CustomerCode','', '" + sCardCode + "','" + UserName + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "', 'Staff Portal'";
                            ////oRsUK.DoQuery(sUpdateKYC);
                            ////sUpdateKYC = "Insert into RDD_KYC_Log(KeyId, TableName, ColName, ColDescription, OldValue, NewValue, ChangedBy, ChangedOn, Portal)" +
                            //sUpdateKYC += "union all select " + KYC_ID + ", 'RDD_KYC', 'TransType', 'TransType','CustomerOnBoarding', 'KYC','" + UserName + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "', 'Staff Portal'";
                            //oRsUK.DoQuery(sUpdateKYC);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(oBP);
                            // return result_ds;
                        }
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


        public async Task<List<Outcls1>> UpdateBP_Vendor_SAP(BP_Master_SAP_VENDOR_VM bP)
        {
            List<Outcls1> str = new List<Outcls1>();

            DataSet result_ds = new DataSet();


            int Result = 0;
            string Cardcode = "";
            try
            {
                SqlParameter[] Para = {
                        new SqlParameter("@CustomerName",bP.CustomerName),
                        new SqlParameter("@Country",bP.Country),
                         new SqlParameter("@p_dbname",bP.DbName),
                };
                DataSet DS = Com.ExecuteDataSet("Odoo_Vendor_Code", CommandType.StoredProcedure, Para);
                //DBName	Territory	Countrycode	Indcode	GroupCode
                //Customer	NewCode	DBName

                string Dbname = DS.Tables[0].Rows[0]["DBName"].ToString();

                if (SAP_ConnectToCompany.ConnectToSAP(Dbname) == true)
                {
                    DateTime? myTime = null;
                    // need to create this SP

                    if (DS.Tables.Count > 0)
                    {
                        string errItemCodes = "", errMachineNos = "";
                        bool errRowFlag = false;
                        int ErrorCode;
                        string ErrMessage;
                        int RefDocSeries, iOrd;
                        string RefDocNum = "", RefDocDate = "", DocNum = "", Usrid = "";

                        SAPbobsCOM.BusinessPartners oBP = null;


                        oBP = (SAPbobsCOM.BusinessPartners)SAP_ConnectToCompany.mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oBusinessPartners);
                        oBP.CardCode = DS.Tables[1].Rows[0]["NewCode"].ToString().Trim();
                        Cardcode = DS.Tables[1].Rows[0]["NewCode"].ToString().Trim();
                        oBP.CardName = bP.CustomerName;
                        oBP.CardForeignName = bP.CustomerName;


                        if (bP.CardType == "C")
                        {
                            oBP.CardType = BoCardTypes.cCustomer;
                        }
                        else if (bP.CardType == "S")
                        {
                            oBP.CardType = BoCardTypes.cSupplier;
                        }
                        else
                        {
                            oBP.CardType = BoCardTypes.cLid;
                        }

                        oBP.GroupCode = Convert.ToInt32(DS.Tables[0].Rows[0]["GroupCode"].ToString());
                        oBP.Currency = "##";
                        oBP.UserFields.Fields.Item("U_email").Value = bP.Email;
                        oBP.Phone1 = bP.TelephoneNo;
                        oBP.Phone2 = "";
                        oBP.Cellular = "";
                        oBP.Fax = "";
                        oBP.EmailAddress = bP.Email;
                        oBP.Website = "";
                        oBP.Territory = Convert.ToInt32(DS.Tables[0].Rows[0]["Territory"].ToString());
                        // oBP.BusinessType = "C";
                        oBP.Industry = Convert.ToInt32(DS.Tables[0].Rows[0]["IndCode"].ToString());
                        oBP.CompanyPrivate = BoCardCompanyTypes.cCompany;
                        // oBP.VATRegistrationNumber= DS.Tables[0].Rows[0]["PIN_TIN_TRN_Number"].ToString().Trim();


                        oBP.PayTermsGrpCode = 1;
                        oBP.Priority = 1;
                        //Address bill to                       

                        oBP.Addresses.AddressName = bP.Address;
                        oBP.Addresses.AddressType = BoAddressType.bo_BillTo;

                        oBP.Addresses.AddressName2 = "";
                        oBP.Addresses.AddressName3 = "";
                        oBP.Addresses.Street = bP.Street;
                        oBP.Addresses.Block = "";
                        oBP.Addresses.City = bP.City;

                        oBP.Addresses.Country = DS.Tables[0].Rows[0]["CountryCode"].ToString();
                        oBP.Addresses.StreetNo = bP.POBOX;
                        oBP.Addresses.BuildingFloorRoom = "";
                        oBP.Addresses.Add();
                        //Address ship to
                        oBP.Addresses.AddressName = bP.Address;
                        oBP.Addresses.AddressType = BoAddressType.bo_ShipTo;

                        oBP.Addresses.AddressName2 = "";
                        oBP.Addresses.AddressName3 = "";
                        oBP.Addresses.Street = bP.Street;
                        oBP.Addresses.Block = "";
                        oBP.Addresses.City = bP.City;

                        oBP.Addresses.Country = DS.Tables[0].Rows[0]["CountryCode"].ToString();
                        oBP.Addresses.StreetNo = bP.POBOX;
                        oBP.Addresses.BuildingFloorRoom = "";

                        oBP.UserFields.Fields.Item("U_TrdName").Value = bP.TradingName;
                        oBP.UserFields.Fields.Item("U_Org").Value = bP.TypeofOrganisation;
                        oBP.UserFields.Fields.Item("U_Trm1").Value = bP.PIN_TIN_TRN_Number is null ? "3" : bP.PIN_TIN_TRN_Number;
                        oBP.UserFields.Fields.Item("U_Trm2").Value = bP.VATNumber is null ? "3" : bP.VATNumber;
                        oBP.UserFields.Fields.Item("U_Trm3").Value = bP.TradeLicense is null ? "3" : bP.TradeLicense;
                        oBP.UserFields.Fields.Item("U_Trm4").Value = bP.RegularLicense is null ? "3" : bP.RegularLicense;
                        oBP.UserFields.Fields.Item("U_Trm5").Value = bP.Govtid is null ? "3" : bP.Govtid;
                        oBP.UserFields.Fields.Item("U_KYSPRD").Value = bP.KYCperiod is null ? "3" : bP.KYCperiod;
                        oBP.UserFields.Fields.Item("U_KYSSTS").Value = bP.KYSStatus is null ? "3" : bP.KYSStatus;
                        oBP.UserFields.Fields.Item("U_KYSSDT").Value = bP.KYCstartdate is null ? Convert.ToDateTime(myTime) : Convert.ToDateTime(bP.KYCstartdate);
                        oBP.UserFields.Fields.Item("U_KYSEXDT").Value = bP.KYCexpirydate is null ? Convert.ToDateTime(myTime) : Convert.ToDateTime(bP.KYCexpirydate);
                        oBP.UserFields.Fields.Item("U_KYSEXP").Value = bP.KYCexpirystatus is null ? "3" : bP.KYCexpirystatus;
                        oBP.UserFields.Fields.Item("U_No_Dir").Value = bP.Noofdirectores is null ? "" : bP.Noofdirectores;


                        oBP.Addresses.Add();

                        Result = oBP.Add();

                        if (Result != 0)
                        {
                            SAP_ConnectToCompany.mCompany.GetLastError(out ErrorCode, out ErrMessage);

                            // result_ds.Tables.Add(t1);

                            SAPbobsCOM.Recordset oRs;
                            oRs = (SAPbobsCOM.Recordset)SAP_ConnectToCompany.mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            string sUpdateLog;
                            sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                            + " Values('" + Dbname + "', 'RDD_KYC_Odoo','" + bP.odooid + "', '" + ErrorCode.ToString().Replace("'", "") + "','" + ErrMessage.ToString().Replace("'", "") + "',getdate())";
                            oRs.DoQuery(sUpdateLog);

                            str.Clear();
                            str.Add(new Outcls1
                            {
                                Cardcode = "",
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

                            str.Clear();
                            str.Add(new Outcls1
                            {
                                Cardcode = Cardcode,
                                Outtf = true,
                                Id = bP.odooid,
                                Responsemsg = "Success"
                            });


                            sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                            + " Values('" + Dbname + "', 'RDD_KYC_Odoo'+'" + Cardcode + "','" + bP.odooid + "', '00','Success',getdate())";
                            oRs.DoQuery(sUpdateLog);

                            //SAPbobsCOM.Recordset oRsUK;
                            //oRsUK = (SAPbobsCOM.Recordset)SAP_ConnectToCompany.mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            //string sUpdateKYC;
                            //sUpdateKYC = "Update tejSAP.dbo.RDD_KYC Set TransType='KYC',DBNAME='" + dbname + "',CustomerCode='" + sCardCode + "' "
                            //+ " Where KYC_ID=" + KYC_ID + "";
                            //oRsUK.DoQuery(sUpdateKYC);
                            //sUpdateKYC = "Insert into tejSAP.dbo.RDD_KYC_Log(KeyId, TableName, ColName, ColDescription, OldValue, NewValue, ChangedBy, ChangedOn, Portal)";
                            //sUpdateKYC += "select " + KYC_ID + ", 'RDD_KYC', 'DBName', 'DBName','', '" + dbname + "','" + UserName + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "', 'Staff Portal'";
                            ////oRsUK.DoQuery(sUpdateKYC);
                            ////sUpdateKYC = "Insert into RDD_KYC_Log(KeyId, TableName, ColName, ColDescription, OldValue, NewValue, ChangedBy, ChangedOn, Portal)" +
                            //sUpdateKYC += "union all select " + KYC_ID + ", 'RDD_KYC', 'CustomerCode', 'CustomerCode','', '" + sCardCode + "','" + UserName + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "', 'Staff Portal'";
                            ////oRsUK.DoQuery(sUpdateKYC);
                            ////sUpdateKYC = "Insert into RDD_KYC_Log(KeyId, TableName, ColName, ColDescription, OldValue, NewValue, ChangedBy, ChangedOn, Portal)" +
                            //sUpdateKYC += "union all select " + KYC_ID + ", 'RDD_KYC', 'TransType', 'TransType','CustomerOnBoarding', 'KYC','" + UserName + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "', 'Staff Portal'";
                            //oRsUK.DoQuery(sUpdateKYC);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(oBP);
                            // return result_ds;
                        }
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