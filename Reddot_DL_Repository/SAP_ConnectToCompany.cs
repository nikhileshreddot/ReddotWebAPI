using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reddot_DL_Repository
{
    public class SAP_ConnectToCompany
    {
        public static string sCode = "", sIPAdress = "", sServerName = "", sDBName1 = "", sDBPassword = "", sDBUserName = "", sB1Password = "", sB1UserName = "";
        public static string SAPServer = "", LICServer = "";
        public static SAPbobsCOM.Company mCompany;
       
        public static bool ConnectToSAP(string sDBName)
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
                else if (sDBName == "SAPAETEST")
                {
                    SAPconstring = configuation.GetSection("AppSettings").GetSection("SAPCompanyConnectCredsSAPAETEST").Value;
                }
                else if (sDBName == "SAPAE_TEST")
                {
                    SAPconstring = configuation.GetSection("AppSettings").GetSection("SAPCompanyConnectCredsSAPAETEST").Value;
                }
                else if (sDBName == "SAPKE")
                {
                    SAPconstring = configuation.GetSection("AppSettings").GetSection("SAPCompanyConnectCredsKE").Value;
                }
                else if (sDBName == "SAPKE-Test")
                {
                    SAPconstring = configuation.GetSection("AppSettings").GetSection("SAPCompanyConnectCredsKETEST").Value;
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
                else if (sDBName == "SAPSA")
                {
                    SAPconstring = configuation.GetSection("AppSettings").GetSection("SAPCompanyConnectCredsSA").Value;
                }
                else if (sDBName == "SAPMAU")
                {
                    SAPconstring = configuation.GetSection("AppSettings").GetSection("SAPCompanyConnectCredsMAU").Value;
                }
                else if (sDBName == "SAPRDDLLC")
                {
                    SAPconstring = configuation.GetSection("AppSettings").GetSection("SAPCompanyConnectCredsRDLLC").Value;
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

                    mCompany = new SAPbobsCOM.Company();//.Company();

                    mCompany.UseTrusted = false;
                    mCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2019;
                    mCompany.Server = SAPServer;
                    mCompany.LicenseServer = LICServer;
                    mCompany.CompanyDB = sDBName1;
                    mCompany.UserName = sB1UserName;
                    mCompany.Password = sB1Password;
                    mCompany.language = SAPbobsCOM.BoSuppLangs.ln_English;
                    mCompany.DbUserName = sDBUserName;
                    mCompany.DbPassword = sDBPassword;


                    int iErrCode = 0;
                    int iCounter = 0;

                    do
                    {

                        iErrCode = mCompany.Connect();
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
                    while (mCompany.Connected == false);

                    if (iErrCode != 0)
                    {
                        string strErr;
                        mCompany.GetLastError(out iErrCode, out strErr);
                        string str=strErr+iErrCode.ToString();
                        
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
    }
}
