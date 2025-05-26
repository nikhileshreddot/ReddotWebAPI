using Reddot_DL_Interface;
using Reddot_EF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Web.Helpers;
using System.Reflection.PortableExecutable;
using System.IO;
using SAPbobsCOM;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Reddot_DL_Repository
{
    public class CAF_Repository : ICAF_Repository
    {

        Commonfunction Com = new Commonfunction();

        public async Task<List<RDD_CAF_Renewal>> GetCAF_LIST(string DbName, long? pagesize, int? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username)
        {
            string str = string.Empty;
            RDD_CAF_Renewal Itm1 = new RDD_CAF_Renewal();
            List<RDD_CAF_Renewal> item = new List<RDD_CAF_Renewal>();
            SqlParameter[] sqlParameters = { new SqlParameter("@DBName",DbName),
                                    new SqlParameter("@p_pageno",pageno),
                                    new SqlParameter("@p_pagesize",pagesize),
                                    new SqlParameter("@p_SortColumn",sortcoloumn),
                                    new SqlParameter("@p_SortOrder",sortorder),
                                    new SqlParameter("@s_date",s_date),
                                    new SqlParameter("@e_date",e_date),
                                    new SqlParameter("@p_UserName",username),


                                    };
            DataSet ds = Com.ExecuteDataSet("Odoo_CAF_API_get", CommandType.StoredProcedure, sqlParameters);
            try
            {
                if (ds.Tables.Count > 0)
                {
                    DateTime? myTime = null;
                    DataTable dtModule;
                    DataRowCollection drc;



                    DataTable dtModule1;
                    DataRowCollection drc1;

                    DataTable dtModule2;
                    DataRowCollection drc2;

                    DataTable dtModule3;
                    DataRowCollection drc3;

                    DataTable dtModule4;
                    DataRowCollection drc4;

                    DataTable dtModule5;
                    DataRowCollection drc5;
                    List<RDD_CAF_PurchaseOrderDetails> itemdetails4 = new List<RDD_CAF_PurchaseOrderDetails>();
                    List<RDD_CAF_SuuplierReferenceDetails> itemdetails3 = new List<RDD_CAF_SuuplierReferenceDetails>();
                    List<RDD_CAF_DirectorDetails> itemdetails2 = new List<RDD_CAF_DirectorDetails>();
                    List<RDD_CAF_BankDetails> itemdetails = new List<RDD_CAF_BankDetails>();
                    List<RDD_CAF_BusinessStakeHolderDetails> itemdetail1 = new List<RDD_CAF_BusinessStakeHolderDetails>();
                    /* public List<RDD_CAF_BankDetails> BankDetails { get; set; }
        public List<RDD_CAF_BusinessStakeHolderDetails> BusinessStakeHolderDetails { get; set; }
        public List<RDD_CAF_DirectorDetails> DirectorDetails { get; set; }
        public List<RDD_CAF_PurchaseOrderDetails> PurchaseOrderDetails { get; set; }
        public List<RDD_CAF_SuuplierReferenceDetails> SuuplierReferenceDetails { get; set; }*/

                    dtModule = ds.Tables[0].Select().CopyToDataTable();
                    // dtModule = ds.Tables[0].Clone();

                    drc = dtModule.Rows;
                    foreach (DataRow dr in drc)
                    {
                        /*  BankDetails = itemdetails,
                            BusinessStakeHolderDetails = itemdetail1,
                            DirectorDetails=itemdetails2,
                            SuuplierReferenceDetails=itemdetails3,
                            PurchaseOrderDetails=itemdetails4*/
                        var k = 1;

                        k = 2;


                        if (ds.Tables[5].Rows.Count > 0)
                        {
                            //dtModule1 = ds.Tables[5].Select("CAFId=" + dr["CAF_Renewal_Id"].ToString()).CopyToDataTable();

                            var table = ds.Tables[5].Select("CAFId=" + dr["CAF_Renewal_Id"].ToString());
                            if (table.Length > 0)
                            {
                                dtModule1 = table.CopyToDataTable();

                                drc1 = dtModule1.Rows;
                                foreach (DataRow dr1 in drc1)
                                {
                                    itemdetails4.Add(new RDD_CAF_PurchaseOrderDetails
                                    {
                                        FULLName = !string.IsNullOrWhiteSpace(dr1["FullName"].ToString()) ? dr1["FullName"].ToString() : "",
                                        Designation = !string.IsNullOrWhiteSpace(dr1["Designation"].ToString()) ? dr1["Designation"].ToString() : "",
                                    });
                                }
                            }
                        }

                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            // dtModule2 = ds.Tables[2].Select("CAFId=" + dr["CAF_Renewal_Id"].ToString()).CopyToDataTable();
                            var table = ds.Tables[2].Select("CAFId=" + dr["CAF_Renewal_Id"].ToString());
                            if (table.Length > 0)
                            {
                                dtModule2 = table.CopyToDataTable();
                                drc2 = dtModule2.Rows;

                                foreach (DataRow dr1 in drc2)
                                {
                                    itemdetails3.Add(new RDD_CAF_SuuplierReferenceDetails
                                    {
                                        //SupplierDetailId	SupplierName	Address	CreditLimit	PaymentTerms	ContactPersonName	ContactPersonDesignation	ContactPersonEmail	
                                        //TelephoneNo_code	TelephoneNo	CAFId	CAF_Type

                                        SupplierName = !string.IsNullOrWhiteSpace(dr1["SupplierName"].ToString()) ? dr1["SupplierName"].ToString() : "",
                                        Address = !string.IsNullOrWhiteSpace(dr1["Address"].ToString()) ? dr1["Address"].ToString() : "",
                                        CreditLimit = !string.IsNullOrWhiteSpace(dr1["CreditLimit"].ToString()) ? dr1["CreditLimit"].ToString() : "",
                                        PaymentTerms = !string.IsNullOrWhiteSpace(dr1["PaymentTerms"].ToString()) ? dr1["PaymentTerms"].ToString() : "",
                                        ContactPersonName = !string.IsNullOrWhiteSpace(dr1["ContactPersonName"].ToString()) ? dr1["ContactPersonName"].ToString() : "",
                                        ContactPersonDesignation = !string.IsNullOrWhiteSpace(dr1["ContactPersonDesignation"].ToString()) ? dr1["ContactPersonDesignation"].ToString() : "",
                                        ContactPersonEmail = !string.IsNullOrWhiteSpace(dr1["ContactPersonEmail"].ToString()) ? dr1["ContactPersonEmail"].ToString() : "",
                                        TelephoneNo_Code = !string.IsNullOrWhiteSpace(dr1["TelephoneNo_code"].ToString()) ? dr1["TelephoneNo_code"].ToString() : "",
                                        TelephoneNo = !string.IsNullOrWhiteSpace(dr1["TelephoneNo"].ToString()) ? dr1["TelephoneNo"].ToString() : ""



                                    });
                                }
                            }
                        }


                        if (ds.Tables[4].Rows.Count > 0)
                        {
                            // dtModule3 = ds.Tables[4].Select("CAFId=" + dr["CAF_Renewal_Id"].ToString()).CopyToDataTable();
                            var table = ds.Tables[4].Select("CAFId=" + dr["CAF_Renewal_Id"].ToString());
                            if (table.Length > 0)
                            {
                                dtModule3 = table.CopyToDataTable();
                                drc3 = dtModule3.Rows;

                                foreach (DataRow dr1 in drc3)
                                {
                                    itemdetails2.Add(new RDD_CAF_DirectorDetails
                                    {
                                        // DirectorDetailId    FulName Email   Citizenship LocalCitizenIDNo    ResidentialAddress  CellPhoneNo_code    CellPhoneNo DirectorIDFile
                                        // --DirectorIDFileName  DirectorIDFileType  CAFId   CAF_Type

                                        Fullname = !string.IsNullOrWhiteSpace(dr1["FulName"].ToString()) ? dr1["FulName"].ToString() : "",
                                        Email = !string.IsNullOrWhiteSpace(dr1["Email"].ToString()) ? dr1["Email"].ToString() : "",
                                        Citizenship = !string.IsNullOrWhiteSpace(dr1["Citizenship"].ToString()) ? dr1["Citizenship"].ToString() : "",
                                        LOcalCitizenIdNo = !string.IsNullOrWhiteSpace(dr1["LocalCitizenIDNo"].ToString()) ? dr1["LocalCitizenIDNo"].ToString() : "",
                                        Address = !string.IsNullOrWhiteSpace(dr1["ResidentialAddress"].ToString()) ? dr1["ResidentialAddress"].ToString() : "",
                                        CellPhoneNo_Code = !string.IsNullOrWhiteSpace(dr1["CellPhoneNo_code"].ToString()) ? dr1["CellPhoneNo_code"].ToString() : "",
                                        CellPhoneNo = !string.IsNullOrWhiteSpace(dr1["CellPhoneNo"].ToString()) ? dr1["CellPhoneNo"].ToString() : "",
                                        // DirectorIDFile = !string.IsNullOrWhiteSpace(dr["DirectorIDFile"].ToString()) ? reader.ReadBytes([dr["DirectorIDFile"].ToString()] : new byte[0],
                                        // DirectorIDFileName = !string.IsNullOrWhiteSpace(dr["DirectorIDFileName"].ToString()) ? dr["DirectorIDFileName"].ToString() : "",





                                        // DirectorDetailId = !string.IsNullOrWhiteSpace(dr["DirectorDetailId"].ToString()) ? Convert.ToInt32(dr["DirectorDetailId"].ToString()) : 0,

                                    });
                                }
                            }
                        }

                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            //dtModule4 = ds.Tables[1].Select("CAFId=" + dr["CAF_Renewal_Id"].ToString()).CopyToDataTable();
                            var table = ds.Tables[1].Select("CAFId=" + dr["CAF_Renewal_Id"].ToString());
                            if (table.Length > 0)
                            {
                                dtModule4 = table.CopyToDataTable();
                                drc4 = dtModule4.Rows;
                                foreach (DataRow dr1 in drc4)
                                {
                                    itemdetails.Add(new RDD_CAF_BankDetails
                                    {

                                        //BankDetailsId	BankName	BranchName	Address	BankCurrencyCode	AccountNo	BankReferenceLetterFile	BankReferenceLetterFileName
                                        //	BankReferenceLetterFileType	CAFId	CAF_Type

                                        BankName = !string.IsNullOrWhiteSpace(dr1["BankName"].ToString()) ? dr1["BankName"].ToString() : "",
                                        BranchName = !string.IsNullOrWhiteSpace(dr1["BranchName"].ToString()) ? dr1["BranchName"].ToString() : "",
                                        Address = !string.IsNullOrWhiteSpace(dr1["Address"].ToString()) ? dr1["Address"].ToString() : "",
                                        BankCurrencyCode = !string.IsNullOrWhiteSpace(dr1["BankCurrencyCode"].ToString()) ? dr1["BankCurrencyCode"].ToString() : "",
                                        AccountNo = !string.IsNullOrWhiteSpace(dr1["AccountNo"].ToString()) ? dr1["AccountNo"].ToString() : "",
                                        //  BankReferenceLetterFile = !string.IsNullOrWhiteSpace(dr["Citizenship"].ToString()) ? dr["Citizenship"].ToString() : "",
                                        //BankReferenceLetterFileName = !string.IsNullOrWhiteSpace(dr["LocalCitizenIDNo"].ToString()) ? dr["LocalCitizenIDNo"].ToString() : "",

                                    });
                                }

                            }
                        }

                        if (ds.Tables[3].Rows.Count > 0)
                        {
                            // dtModule5 = ds.Tables[3].Select("CAFId=" + dr["CAF_Renewal_Id"].ToString()).CopyToDataTable();
                            var table = ds.Tables[3].Select("CAFId=" + dr["CAF_Renewal_Id"].ToString());
                            if (table.Length > 0)
                            {
                                dtModule5 = table.CopyToDataTable();
                                drc5 = dtModule5.Rows;
                                foreach (DataRow dr1 in drc5)
                                {
                                    itemdetail1.Add(new RDD_CAF_BusinessStakeHolderDetails
                                    {
                                        //StakeHolderDetailsId	StakeHolderName	Designation	Email	CellPhoneNo_Code	CellPhoneNo	CAFId	CAF_Type

                                        StakeHolderName = !string.IsNullOrWhiteSpace(dr1["StakeHolderName"].ToString()) ? dr1["StakeHolderName"].ToString() : "",
                                        StakeHolderDesignation = !string.IsNullOrWhiteSpace(dr1["Designation"].ToString()) ? dr1["Designation"].ToString() : "",
                                        Email = !string.IsNullOrWhiteSpace(dr1["Email"].ToString()) ? dr1["Email"].ToString() : "",
                                        CellPhoneNo_Code = !string.IsNullOrWhiteSpace(dr1["CellPhoneNo_Code"].ToString()) ? dr1["CellPhoneNo_Code"].ToString() : "",
                                        CellPhoneNo = !string.IsNullOrWhiteSpace(dr1["CellPhoneNo"].ToString()) ? dr1["CellPhoneNo"].ToString() : "",


                                    });
                                }
                            }
                        }

                        item.Add(new RDD_CAF_Renewal
                        {




                            TotalCount = !string.IsNullOrWhiteSpace(dr["TotalCount"].ToString()) ? Convert.ToInt64(dr["TotalCount"].ToString()) : 0,
                            RowNum = !string.IsNullOrWhiteSpace(dr["RowNum"].ToString()) ? Convert.ToInt64(dr["RowNum"].ToString()) : 0,
                            CAF_Renewal_Id = !string.IsNullOrWhiteSpace(dr["CAF_Renewal_Id"].ToString()) ? Convert.ToInt64(dr["CAF_Renewal_Id"].ToString()) : 0,
                            DBName = !string.IsNullOrWhiteSpace(dr["DBName"].ToString()) ? dr["DBName"].ToString() : "",
                            CustomerCode = !string.IsNullOrWhiteSpace(dr["CustomerCode"].ToString()) ? dr["CustomerCode"].ToString() : "",
                            CustomerName = !string.IsNullOrWhiteSpace(dr["CustomerName"].ToString()) ? dr["CustomerName"].ToString() : "",
                            Country = !string.IsNullOrWhiteSpace(dr["Country"].ToString()) ? dr["Country"].ToString() : "",
                            TradingName = !string.IsNullOrWhiteSpace(dr["TradingName"].ToString()) ? dr["TradingName"].ToString() : "",
                            PhysicalAddress = !string.IsNullOrWhiteSpace(dr["PhysicalAddress"].ToString()) ? dr["PhysicalAddress"].ToString() : "",
                            PostalAddress = !string.IsNullOrWhiteSpace(dr["PostalAddress"].ToString()) ? dr["PostalAddress"].ToString() : "",
                            TelephoneNo = !string.IsNullOrWhiteSpace(dr["TelephoneNo"].ToString()) ? dr["TelephoneNo"].ToString() : "",
                            Cellphone = !string.IsNullOrWhiteSpace(dr["Cellphone"].ToString()) ? dr["Cellphone"].ToString() : "",
                            Email = !string.IsNullOrWhiteSpace(dr["Email"].ToString()) ? dr["Email"].ToString() : "",
                            DateOfIncorporation = !string.IsNullOrWhiteSpace(dr["DateOfIncorporation"].ToString()) ? Convert.ToDateTime(dr["DateOfIncorporation"].ToString()) : Convert.ToDateTime(myTime),
                            CertificateofIncorporation = !string.IsNullOrWhiteSpace(dr["CertificateofIncorporation"].ToString()) ? dr["CertificateofIncorporation"].ToString() : "",
                            CertificateofIncorporationFilePath1 = !string.IsNullOrWhiteSpace(dr["CertificateofIncorporationFile"].ToString()) ? Encoding.ASCII.GetBytes(dr["CertificateofIncorporationFile"].ToString()) : new byte[0],
                            CertificateofIncorporationFileName = !string.IsNullOrWhiteSpace(dr["CertificateofIncorporationFileName"].ToString()) ? dr["CertificateofIncorporationFileName"].ToString() : "",
                            CertificateofIncorporationFileType = !string.IsNullOrWhiteSpace(dr["CertificateofIncorporationFileType"].ToString()) ? dr["CertificateofIncorporationFileType"].ToString() : "",
                            VATNumber = !string.IsNullOrWhiteSpace(dr["VATNumber"].ToString()) ? dr["VATNumber"].ToString() : "",
                            VATNumber_FilePath1 = !string.IsNullOrWhiteSpace(dr["VATNumberFile"].ToString()) ? Encoding.ASCII.GetBytes(dr["VATNumberFile"].ToString()) : new byte[0],
                            VATNumberFileName = !string.IsNullOrWhiteSpace(dr["VATNumberFileName"].ToString()) ? dr["VATNumberFileName"].ToString() : "",
                            VATNumber_FileType = !string.IsNullOrWhiteSpace(dr["VATNumberFileType"].ToString()) ? dr["VATNumberFileType"].ToString() : "",
                            PIN_TIN_TRN_Number = !string.IsNullOrWhiteSpace(dr["PIN_TIN_TRN_Number"].ToString()) ? dr["PIN_TIN_TRN_Number"].ToString() : "",
                            PIN_TIN_TRN_NumberFilePath1 = !string.IsNullOrWhiteSpace(dr["PIN_TIN_TRN_NumberFile"].ToString()) ? Encoding.ASCII.GetBytes(dr["PIN_TIN_TRN_NumberFile"].ToString()) :new byte[0],
                            PIN_TIN_TRN_NumberFileName = !string.IsNullOrWhiteSpace(dr["PIN_TIN_TRN_NumberFileName"].ToString()) ? dr["PIN_TIN_TRN_NumberFileName"].ToString() : "",
                            PIN_TIN_TRN_NumberFileType = !string.IsNullOrWhiteSpace(dr["PIN_TIN_TRN_NumberFileType"].ToString()) ? dr["PIN_TIN_TRN_NumberFileType"].ToString() : "",
                            TradeLicenseNumber = !string.IsNullOrWhiteSpace(dr["TradeLicenseNumber"].ToString()) ? dr["TradeLicenseNumber"].ToString() : "",
                            TradeLicenseNumberFilePath1 = !string.IsNullOrWhiteSpace(dr["TradeLicenseNumberFile"].ToString()) ? Encoding.ASCII.GetBytes(dr["TradeLicenseNumberFile"].ToString()) : new byte[0],
                            TradeLicenseNumberFileName = !string.IsNullOrWhiteSpace(dr["TradeLicenseNumberFileName"].ToString()) ? dr["TradeLicenseNumberFileName"].ToString() : "",
                            TradeLicenseNumberFileType = !string.IsNullOrWhiteSpace(dr["TradeLicenseNumberFileType"].ToString()) ? dr["TradeLicenseNumberFileType"].ToString() : "",
                            NatureOfBusiness = !string.IsNullOrWhiteSpace(dr["NatureOfBusiness"].ToString()) ? dr["NatureOfBusiness"].ToString() : "",
                            TypeOfBusiness = !string.IsNullOrWhiteSpace(dr["TypeOfBusiness"].ToString()) ? dr["TypeOfBusiness"].ToString() : "",
                            NoOfEmployees = !string.IsNullOrWhiteSpace(dr["NoOfEmployees"].ToString()) ? dr["NoOfEmployees"].ToString() : "",
                            PurchasedFromRDDBefore = !string.IsNullOrWhiteSpace(dr["PurchasedFromRDDBefore"].ToString()) ? Convert.ToBoolean(dr["PurchasedFromRDDBefore"].ToString()) : false,
                            Telephone_Code = !string.IsNullOrWhiteSpace(dr["Telephone_code"].ToString()) ? dr["Telephone_code"].ToString() : "",
                            Cellphone_Code = !string.IsNullOrWhiteSpace(dr["Cellphone_code"].ToString()) ? dr["Cellphone_code"].ToString() : "",
                            InsuranceCompany = !string.IsNullOrWhiteSpace(dr["InsuranceCompany"].ToString()) ? dr["InsuranceCompany"].ToString() : "",
                            RisksInsuredAgainst = !string.IsNullOrWhiteSpace(dr["RisksInsuredAgainst"].ToString()) ? dr["RisksInsuredAgainst"].ToString() : "",
                            SumInsured = !string.IsNullOrWhiteSpace(dr["SumInsured"].ToString()) ? Convert.ToDecimal(dr["SumInsured"].ToString()) : 0,
                            PolicyStartDate = !string.IsNullOrWhiteSpace(dr["PolicyStartdate"].ToString()) ? Convert.ToDateTime(dr["PolicyStartdate"].ToString()) : Convert.ToDateTime(myTime),
                            PolicyEndDate = !string.IsNullOrWhiteSpace(dr["PolicyEnddate"].ToString()) ? Convert.ToDateTime(dr["PolicyEnddate"].ToString()) : Convert.ToDateTime(myTime),
                            RequestedCreditLimit = !string.IsNullOrWhiteSpace(dr["RequestedCreditLimit"].ToString()) ? Convert.ToDecimal(dr["RequestedCreditLimit"].ToString()) : 0,
                            RequestedCreditTerms = !string.IsNullOrWhiteSpace(dr["RequestedCreditTerms"].ToString()) ? dr["RequestedCreditTerms"].ToString() : "",
                            TotalAssetValue = !string.IsNullOrWhiteSpace(dr["TotalAssetValue"].ToString()) ? Convert.ToDecimal(dr["TotalAssetValue"].ToString()) : 0,
                            LatestAuditedFinancialStatements = !string.IsNullOrWhiteSpace(dr["LatestAuditedFinancialStatements"].ToString()) ? Encoding.ASCII.GetBytes(dr["LatestAuditedFinancialStatements"].ToString()) : new byte[0],
                            LatestAuditedFinancialStatementsFileName = !string.IsNullOrWhiteSpace(dr["LatestAuditedFinancialStatementFileName"].ToString()) ? dr["LatestAuditedFinancialStatementFileName"].ToString() : "",
                            LatestAuditedFinancialStatementsFileType = !string.IsNullOrWhiteSpace(dr["LatedstAuditedFinancialStatementsFileType"].ToString()) ? dr["LatedstAuditedFinancialStatementsFileType"].ToString() : "",
                            LastThreeMonthsBankStatements = !string.IsNullOrWhiteSpace(dr["LastThreeMonthsBankStatements"].ToString()) ? Encoding.ASCII.GetBytes(dr["LastThreeMonthsBankStatements"].ToString()) : new byte[0],
                            LastThreeMonthsBankStatementsFileName = !string.IsNullOrWhiteSpace(dr["LastThreeMonthsBankStatementsFileName"].ToString()) ? dr["LastThreeMonthsBankStatementsFileName"].ToString() : "",
                            LastThreeMonthsBankStatementsFileType = !string.IsNullOrWhiteSpace(dr["LastThreeMonthsBankStatementsFileType"].ToString()) ? dr["LastThreeMonthsBankStatementsFileType"].ToString() : "",
                             InventoryInsurancePolicy = !string.IsNullOrWhiteSpace(dr["InventoryInsurancePolicy"].ToString()) ? Encoding.ASCII.GetBytes(dr["InventoryInsurancePolicy"].ToString()) : new byte[0],
                            InventoryInsurancePolicyFileName = !string.IsNullOrWhiteSpace(dr["InventoryInsurancePolicyFileName"].ToString()) ? dr["InventoryInsurancePolicyFileName"].ToString() : "",
                            InventoryInsurancePolicyFileType = !string.IsNullOrWhiteSpace(dr["InventoryInsurancePolicyFileType"].ToString()) ? dr["InventoryInsurancePolicyFileType"].ToString() : "",
                             LatestAnnualReturnFilePath1 = !string.IsNullOrWhiteSpace(dr["LatestAnnualReturnFile"].ToString()) ? Encoding.ASCII.GetBytes(dr["LatestAnnualReturnFile"].ToString()) : new byte[0],
                            LatestAnnualReturnFileName = !string.IsNullOrWhiteSpace(dr["LatestAnnualReturnFileName"].ToString()) ? dr["LatestAnnualReturnFileName"].ToString() : "",
                            LatestAnnualReturnFileType = !string.IsNullOrWhiteSpace(dr["LatestAnnualReturnFileType"].ToString()) ? dr["LatestAnnualReturnFileType"].ToString() : "",
                            ProfileCompletePercentage = !string.IsNullOrWhiteSpace(dr["ProfileCompletePercentage"].ToString()) ? Convert.ToInt16(dr["ProfileCompletePercentage"].ToString()) : 0,
                            CreatedBy = !string.IsNullOrWhiteSpace(dr["CreatedBy"].ToString()) ? dr["CreatedBy"].ToString() : "",
                            CreatedOn = !string.IsNullOrWhiteSpace(dr["CreatedOn"].ToString()) ? Convert.ToDateTime(dr["CreatedOn"].ToString()) : Convert.ToDateTime(myTime),
                            LastUpdatedBy = !string.IsNullOrWhiteSpace(dr["LastUpdatedBy"].ToString()) ? dr["LastUpdatedBy"].ToString() : "",
                            LastUpdatedOn = !string.IsNullOrWhiteSpace(dr["LastUpdatedOn"].ToString()) ? Convert.ToDateTime(dr["LastUpdatedOn"].ToString()) : Convert.ToDateTime(myTime),
                            CAFSignedFilePath1 = !string.IsNullOrWhiteSpace(dr["CAFSignedFile"].ToString()) ? Encoding.ASCII.GetBytes(dr["CAFSignedFile"].ToString()) : new byte[0],
                            CAFSignedFileName = !string.IsNullOrWhiteSpace(dr["CAFSignedFileName"].ToString()) ? dr["CAFSignedFileName"].ToString() : "",
                            CAFSignedFileType = !string.IsNullOrWhiteSpace(dr["CAFSignedFileType"].ToString()) ? dr["CAFSignedFileType"].ToString() : "",
                            VerificationStatus = !string.IsNullOrWhiteSpace(dr["VerificationStatus"].ToString()) ? dr["VerificationStatus"].ToString() : "",
                            VerificationDoneBy = !string.IsNullOrWhiteSpace(dr["VerificationDoneBy"].ToString()) ? dr["VerificationDoneBy"].ToString() : "",
                            Verificationdate = !string.IsNullOrWhiteSpace(dr["Verificationdate"].ToString()) ? Convert.ToDateTime(dr["Verificationdate"].ToString()) : Convert.ToDateTime(myTime),
                            VerificationRemark = !string.IsNullOrWhiteSpace(dr["VerificationRemark"].ToString()) ? dr["VerificationRemark"].ToString() : "",
                            isApprovalFlag = !string.IsNullOrWhiteSpace(dr["isApprovalFlag"].ToString()) ? Convert.ToInt16(dr["isApprovalFlag"].ToString()) : 0,
                            ValidityDate = !string.IsNullOrWhiteSpace(dr["ValidityDate"].ToString()) ? Convert.ToDateTime(dr["ValidityDate"].ToString()) : Convert.ToDateTime(myTime),
                            Creditdays = !string.IsNullOrWhiteSpace(dr["Creditdays"].ToString()) ? Convert.ToInt16(dr["Creditdays"].ToString()) : 0,
                            CreditAmount = !string.IsNullOrWhiteSpace(dr["CreditAmount"].ToString()) ? Convert.ToDecimal(dr["CreditAmount"].ToString()) : 0,
                            ApprovalFlag = !string.IsNullOrWhiteSpace(dr["ApprovalFlag"].ToString()) ? Convert.ToInt16(dr["ApprovalFlag"].ToString()) : 0,


                            BankDetails = itemdetails,
                            BusinessStakeHolderDetails = itemdetail1,
                            DirectorDetails = itemdetails2,
                            SuuplierReferenceDetails = itemdetails3,
                            PurchaseOrderDetails = itemdetails4
                        });

                        //   dtModule.ImportRow(dr);
                        itemdetails4 = new List<RDD_CAF_PurchaseOrderDetails>();
                        itemdetails3 = new List<RDD_CAF_SuuplierReferenceDetails>();
                        itemdetails2 = new List<RDD_CAF_DirectorDetails>();
                        itemdetails = new List<RDD_CAF_BankDetails>();
                        itemdetail1 = new List<RDD_CAF_BusinessStakeHolderDetails>();

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

        public async Task<DataSet> GetKYC(string TransType, long? pagesize, int? pageno, string username)
        {
            SqlParameter[] sqlParameters = { new SqlParameter("@TransType",TransType),
                                    new SqlParameter("@p_pageno",pageno),
                                    new SqlParameter("@p_pagesize",pagesize),
                                   
                                    new SqlParameter("@p_UserName",username),


                                    };
            DataSet ds = Com.ExecuteDataSet("GetKYCOdoo", CommandType.StoredProcedure, sqlParameters);
            return ds;
        }
    }
}
