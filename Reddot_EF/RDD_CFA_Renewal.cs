using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_EF
{
    public class RDD_CAF_Renewal
    {

        public Int64 TotalCount { get; set; }
        public Int64 RowNum { get; set; }
        public string CAF_Type { get; set; }
        public string ImagePath { get; set; }
        public string DBName { get; set; }
        public string CountryCode { get; set; }
        public string Countries { get; set; }
        public string Database { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Country { get; set; }
        public int ProfileCompletionPercentage { get; set; }

        public string BusinessName { get; set; }
        public string TradingName { get; set; }
        public string PhysicalAddress { get; set; }


        //public string Building { get; set; }

        //public string POBOX { get; set; }
        public string PostalAddress { get; set; }
        public string TelephoneNo { get; set; }
        public string Cellphone { get; set; }

        public string Telephone_Code { get; set; }
        public string Cellphone_Code { get; set; }
        public string Email { get; set; }

        public DateTime? DateOfIncorporation { get; set; }
        public string CertificateofIncorporation { get; set; }


        

        public string CertificateofIncorporationFileType { get; set; }

        public int ProfileCompletePercentage { get; set; }
        public string CertificateofIncorporationFileName { get; set; }
        public byte[] CertificateofIncorporationFilePath1 { get; set; }
        public Boolean CertificateofIncorporationFileFlag { get; set; }
        public string VerificationRemark { get; set; }
        public int isApprovalFlag { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public string VerificationStatus { get; set; }
        public string VerificationDoneBy { get; set; }
        public DateTime ValidityDate { get; set; }
        public DateTime Verificationdate { get; set; }
        public string TradeLicenseNumberFileName { get; set; }
        public string VATNumberFileName { get; set; }
        public string VATNumber { get; set; }
        public string VATNumber_FilePath { get; set; }
        public int Creditdays { get; set; }
        public string VATNumber_FileType { get; set; }
        public decimal CreditAmount { get; set; }
        public int ApprovalFlag { get; set; }
        public byte[] VATNumber_FilePath1 { get; set; }
        public bool VATNumber_FileFlag { get; set; }
        public string PIN_TIN_TRN_Number { get; set; }
        public string PIN_TIN_TRN_NumberFileName { get; set; }
        public string PortalType { get; set; }

        public string PIN_TIN_TRN_NumberFilePath { get; set; }

        public string PIN_TIN_TRN_NumberFileType { get; set; }

        
        public byte[] PIN_TIN_TRN_NumberFilePath1 { get; set; }

        public Boolean PIN_TIN_TRN_NumberFileFlag { get; set; }
        public string TradeLicenseNumber { get; set; }

        public string TradeLicenseNumberFilePath { get; set; }

        public string TradeLicenseNumberFileType { get; set; }

        
        public byte[] TradeLicenseNumberFilePath1 { get; set; }
        public Boolean TradeLicenseNumberFileFlag { get; set; }



        public Int64 CAF_Renewal_Id { get; set; }
        public string InsuranceCompany { get; set; }
        public string RisksInsuredAgainst { get; set; }
        public decimal? SumInsured { get; set; }
        public DateTime? PolicyStartDate { get; set; }
        public DateTime? PolicyEndDate { get; set; }
        public decimal? RequestedCreditLimit { get; set; }
        public string RequestedCreditTerms { get; set; }
        public decimal? TotalAssetValue { get; set; }



        public string LatestAuditedFinancialStatementsFileName { get; set; }
        
        public byte[] LatestAuditedFinancialStatementsFilePath1 { get; set; }
        public string LatestAuditedFinancialStatementsFileType { get; set; }
        public byte[] LatestAuditedFinancialStatements { get; set; }

        public Boolean LatestAuditedFinancialStatementsFileFlag { get; set; }

        public string LastThreeMonthsBankStatementsFileName { get; set; }
        
        public byte[] LastThreeMonthsBankStatementsFilePath1 { get; set; }
        public string LastThreeMonthsBankStatementsFileType { get; set; }
        public byte[] LastThreeMonthsBankStatements { get; set; }


        public Boolean LastThreeMonthsBankStatementsFileFlag { get; set; }

        public string InventoryInsurancePolicyFileName { get; set; }
        
        public byte[] InventoryInsurancePolicyFilePath1 { get; set; }
        public string InventoryInsurancePolicyFileType { get; set; }
        public byte[] InventoryInsurancePolicy { get; set; }

        public Boolean InventoryInsurancePolicyFileFlag { get; set; }

        public string LatestAnnualReturnFileName { get; set; }
        public string LatestAnnualReturnFilePath { get; set; }
        public byte[] LatestAnnualReturnFilePath1 { get; set; }
        public string LatestAnnualReturnFileType { get; set; }
        public byte[] LatestAnnualReturn { get; set; }
        public Boolean CAFSignedFileFlag { get; set; }
        public string CAFSignedFileName { get; set; }
        
        public byte[] CAFSignedFilePath1 { get; set; }
        public string CAFSignedFileType { get; set; }
        public byte[] CAFSigned { get; set; }

        public Boolean LatestAnnualReturnFileFlag { get; set; }


        public string Bank1ReferenceLetterFileName { get; set; }
        
        public byte[] Bank1ReferenceLetterFilePath1 { get; set; }
        public string Bank1ReferenceLetterFileType { get; set; }
        public byte[] Bank1ReferenceLetter { get; set; }

        public Boolean Bank1ReferenceLetterFileFlag { get; set; }

        public string Bank2ReferenceLetterFileName { get; set; }
        
        public byte[] Bank2ReferenceLetterFilePath1 { get; set; }
        public string Bank2ReferenceLetterFileType { get; set; }
        public byte[] Bank2ReferenceLetter { get; set; }

        public Boolean Bank2ReferenceLetterFileFlag { get; set; }


        public string Director1IDFileName { get; set; }
        
        public byte[] Director1IDFilePath1 { get; set; }
        public string Director1IDFileType { get; set; }
        public byte[] Director1ID { get; set; }


        public Boolean Director1IDFileFlag { get; set; }

        public string Director2IDFileName { get; set; }
        
        public byte[] Director2IDFilePath1 { get; set; }
        public string Director2IDFileType { get; set; }
        public byte[] Director2ID { get; set; }

        public Boolean Director2IDFileFlag { get; set; }

        public string PurchaseOrderSignature1FileName { get; set; }
        
        public byte[] PurchaseOrderSignature1FilePath1 { get; set; }
        public string PurchaseOrderSignature1FileType { get; set; }
        public byte[] PurchaseOrderSignature1 { get; set; }

        public string PurchaseOrderSignature2FileName { get; set; }
        
        public byte[] PurchaseOrderSignature2FilePath1 { get; set; }
        public string PurchaseOrderSignature2FileType { get; set; }
        public byte[] PurchaseOrderSignature2 { get; set; }


        public string NatureOfBusiness { get; set; }
        public Boolean NatureOfBusiness1 { get; set; }
        public string TypeOfBusiness { get; set; }
        public string NoOfEmployees { get; set; }
        public string AnnualTurnover { get; set; }
        public bool PurchasedFromRDDBefore { get; set; }

        public bool IsNewCustomer { get; set; }
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }
        public bool Editflag { get; set; }
        public string ResponseMsg { get; set; }

        public List<RDD_CAF_BankDetails> BankDetails { get; set; }
        public List<RDD_CAF_BusinessStakeHolderDetails> BusinessStakeHolderDetails { get; set; }
        public List<RDD_CAF_DirectorDetails> DirectorDetails { get; set; }
        public List<RDD_CAF_PurchaseOrderDetails> PurchaseOrderDetails { get; set; }
        public List<RDD_CAF_SuuplierReferenceDetails> SuuplierReferenceDetails { get; set; }
    }

    public partial class RDD_CAF_BankDetails
    {

        public string BankCurrencyCode { get; set; }
      //  public int BankDetailsId { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string AccountNo { get; set; }
        public string Address { get; set; }




    }
    public partial class RDD_CAF_BusinessStakeHolderDetails
    {
       // public int StakeHolderDetailsId { get; set; }
        public string StakeHolderDesignation { get; set; }
        public string StakeHolderName { get; set; }
        public string Email { get; set; }
        public string CellPhoneNo { get; set; }

        public string CellPhoneNo_Code { get; set; }
    }

    public partial class RDD_CAF_DirectorDetails
    {
       // public int DirectorDetailId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Citizenship { get; set; }
        public string LOcalCitizenIdNo { get; set; }
        public string Address { get; set; }
        public string CellPhoneNo { get; set; }
        public string CellPhoneNo_Code { get; set; }
        //public string DirectorIDFileName { get; set; }
        //public byte DirectorIDFile { get; set; }

    }

    public partial class RDD_CAF_PurchaseOrderDetails
    {
        //public int PurchaseOrderDetailId { get; set; }
        public string FULLName { get; set; }
        public string Designation { get; set; }
        //public string Signatures { get; set; }

        //public string SignatureFileName { get; set; }
        
        //public string SignatureFileType { get; set; }
        //public byte[] Signature { get; set; }

    }

    public partial class RDD_CAF_SuuplierReferenceDetails
    {
      //  public int SupplierReferenceDetailId { get; set; }
        public string SupplierName { get; set; }
        public string Address { get; set; }
        public string CreditLimit { get; set; }
        public string PaymentTerms { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonDesignation { get; set; }
        public string ContactPersonEmail { get; set; }
        public string TelephoneNo { get; set; }
        public string TelephoneNo_Code { get; set; }

    }
}

