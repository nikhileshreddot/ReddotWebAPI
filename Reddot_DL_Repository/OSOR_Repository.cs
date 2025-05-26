using Reddot_DL_Interface;
using Reddot_EF;
using Reddot_View_Model;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Reddot_DL_Repository
{
    public class OSOR_Repository : IOSOR
    {
        Commonfunction Com = new Commonfunction();

      

        public async Task<List<Outcls1>> SaveSOR_SAP(RDD_OSOR_VM bP)
        {
            List<Outcls1> str = new List<Outcls1>();

     
            int Result = 0;
            string Cardcode = "";
            try
            {
                if (SAP_ConnectToCompany.ConnectToSAP(bP.DBName) == true)
                {
                    string errItemCodes = "", errMachineNos = "";
                    bool errRowFlag = false;
                    int ErrorCode;
                    string ErrMessage;
                    int RefDocSeries, iOrd;
                    string RefDocNum = "", RefDocDate = "", DocNum = "", Usrid = "";

                    SAPbobsCOM.Documents oSalesOrder;
                    oSalesOrder = (SAPbobsCOM.Documents)SAP_ConnectToCompany.mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oOrders);


                    oSalesOrder.CardCode =bP.CardCode.ToString();
                    oSalesOrder.CardName = bP.CardName.ToString();
                    oSalesOrder.DocDate = Convert.ToDateTime(bP.PostingDate);
                    oSalesOrder.DocDueDate = Convert.ToDateTime(bP.DeliveryDate);
                    oSalesOrder.TaxDate = Convert.ToDateTime(bP.PostingDate);

                    //oSalesOrder.Series=0;
                    oSalesOrder.NumAtCard = bP.RefNo;

                    oSalesOrder.GroupNumber = Convert.ToInt16(bP.CustPayTerms);
                    oSalesOrder.PaymentGroupCode = Convert.ToInt16(bP.CustPayTerms);// 8;//Convert.ToInt16(DS.Tables[0].Rows[0]["CustPayTerms"].ToString());
                    oSalesOrder.SalesPersonCode = Convert.ToInt16(bP.SalesEmp);
                    oSalesOrder.Comments = bP.Remarks;
                    oSalesOrder.DocCurrency = bP.DocCur;

                    oSalesOrder.UserFields.Fields.Item("U_SO_ID").Value = bP.SO_ID;
                    oSalesOrder.UserFields.Fields.Item("U_PayTerm").Value = bP.InvPayTerms;
                    oSalesOrder.UserFields.Fields.Item("U_Project").Value = bP.RDD_Project;
                    oSalesOrder.UserFields.Fields.Item("U_BizType").Value = bP.BusinesType;
                    oSalesOrder.UserFields.Fields.Item("U_APInvoiceNo").Value = bP.APNumber;
                   oSalesOrder.UserFields.Fields.Item("U_approval_code").Value = bP.ApprovalCode;
                    oSalesOrder.UserFields.Fields.Item("U_isredeem").Value = bP.IsRedeemRequestOrder;
                    oSalesOrder.UserFields.Fields.Item("U_BZ_SEG").Value = bP.BZ_SEG;
                    oSalesOrder.UserFields.Fields.Item("U_ENDUSER").Value = bP.enduser;
                    oSalesOrder.UserFields.Fields.Item("U_AGNO").Value = bP.BeSpokeProjectCode;
                    oSalesOrder.UserFields.Fields.Item("U_BSPKPRJ").Value = bP.BeSpokeProjectCode;


                    oSalesOrder.UserFields.Fields.Item("U_WTVCODE").Value = bP.WHVATCODE;
                    oSalesOrder.UserFields.Fields.Item("U_WTPN").Value = bP.WHVATPER;
                    oSalesOrder.UserFields.Fields.Item("U_WTAMT").Value = Convert.ToDouble(bP.WHVATAMT);


                    oSalesOrder.UserFields.Fields.Item("U_WCode").Value = bP.WHTAXCODE;
                    oSalesOrder.UserFields.Fields.Item("U_WTTAX").Value = Convert.ToDouble(bP.WHTAXPER);
                    oSalesOrder.UserFields.Fields.Item("U_WTTAXAMT").Value = Convert.ToDouble(bP.WHTAXAMT);

                    if (bP.DBName.Trim().ToUpper() == "SAPAE" || bP.DBName.Trim().ToUpper() == "SAPTRI")
                    {
                        oSalesOrder.UserFields.Fields.Item("U_forwarder").Value = bP.Forwarder;
                    }
                    if (bP.DBName.Trim().ToUpper() == "SAPAE" || bP.DBName.Trim().ToUpper() == "SAPTRI" || bP.DBName.Trim().ToUpper() == "SAPTRI-TEST" )
                    {
                        oSalesOrder.UserFields.Fields.Item("U_OURREF").Value =bP.OurREf;
                        oSalesOrder.UserFields.Fields.Item("U_PrdTyp").Value = bP.ProductType;
                        oSalesOrder.UserFields.Fields.Item("U_CustTyp").Value = bP.CustomerType;

                    }
                    try
                    {




                    }
                    catch { }

                    if (bP.RDD_SOR2List.Count > 0)
                    {
                        for (int iRow = 0; iRow < bP.RDD_SOR2List.Count; iRow++)
                        {
                            oSalesOrder.UserFields.Fields.Item("U_paymethod" + (iRow + 1).ToString()).Value = bP.RDD_SOR2List[iRow].Pay_Method;
                            oSalesOrder.UserFields.Fields.Item("U_refno" + (iRow + 1).ToString()).Value = bP.RDD_SOR2List[iRow].Rcpt_Check_No;
                            oSalesOrder.UserFields.Fields.Item("U_refdate" + (iRow + 1).ToString()).Value = bP.RDD_SOR2List[iRow].Rcpt_Check_Date;
                            oSalesOrder.UserFields.Fields.Item("U_memo" + (iRow + 1).ToString()).Value = bP.RDD_SOR2List[iRow].Remark;
                            oSalesOrder.UserFields.Fields.Item("U_currency" + (iRow + 1).ToString()).Value = bP.RDD_SOR2List[iRow].Curr_Id;
                            oSalesOrder.UserFields.Fields.Item("U_amount" + (iRow + 1).ToString()).Value =Convert.ToDouble(bP.RDD_SOR2List[iRow].Allocated_Amt);
                        }
                    }


                    oSalesOrder.UserFields.Fields.Item("U_SOR_User").Value = bP.CreatedBy;
                    oSalesOrder.UserFields.Fields.Item("U_PAS").Value = bP.TopPayApproval;




                    if (bP.RDD_SOR1List.Count > 0)
                    {
                        for (int iRow = 0; iRow < bP.RDD_SOR1List.Count; iRow++)
                        {
                            oSalesOrder.Lines.ItemCode = bP.RDD_SOR1List[iRow].ItemCode;
                            oSalesOrder.Lines.ItemDescription = bP.RDD_SOR1List[iRow].Description;
                            oSalesOrder.Lines.Quantity = Convert.ToDouble(bP.RDD_SOR1List[iRow].Quantity);
                            if (Convert.ToDouble(bP.RDD_SOR1List[iRow].DiscPer) > 0)
                            {
                                oSalesOrder.Lines.UnitPrice = Convert.ToDouble(bP.RDD_SOR1List[iRow].UnitPrice);
                            }
                            else
                            {
                                oSalesOrder.Lines.Price = Convert.ToDouble(bP.RDD_SOR1List[iRow].UnitPrice);
                            }
                            oSalesOrder.Lines.DiscountPercent = Convert.ToDouble(bP.RDD_SOR1List[iRow].DiscPer);

                            if (bP.DBName.Trim().ToUpper() == "SAPAE" || bP.DBName.Trim().ToUpper() == "SAPTRI" || bP.DBName.Trim().ToUpper() == "SAPTRI-TEST")
                            {
                                oSalesOrder.Lines.VatGroup = bP.RDD_SOR1List[iRow].TaxCode;
                            }
                            else
                            {
                                oSalesOrder.Lines.TaxCode = bP.RDD_SOR1List[iRow].TaxCode;
                            }
                            // 

                            oSalesOrder.Lines.WarehouseCode = bP.RDD_SOR1List[iRow].WhsCode;

                            //oSalesOrder.Lines.UserFields.Fields.Item("U_QtyAval").Value = "";
                            oSalesOrder.Lines.UserFields.Fields.Item("U_GPValue").Value = Convert.ToDouble(bP.RDD_SOR1List[iRow].GP);
                            oSalesOrder.Lines.UserFields.Fields.Item("U_GPPercent").Value = Convert.ToDouble(bP.RDD_SOR1List[iRow].GPPer);
                            oSalesOrder.Lines.UserFields.Fields.Item("U_opgRefAlpha").Value = bP.RDD_SOR1List[iRow].OpgRefAlpha.ToString();
                            oSalesOrder.Lines.UserFields.Fields.Item("U_opgSellinIdLink").Value = "NA";

                            oSalesOrder.Lines.Add();

                        }
                    }

                     Result = oSalesOrder.Add();

                    if (Result != 0)
                    {
                        SAP_ConnectToCompany.mCompany.GetLastError(out ErrorCode, out ErrMessage);
                        
                        string sUpdateLog;
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oSalesOrder);

                        sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                        + " Values('" + bP.DBName + "', 'RDD_OSOR_ODOO','" + bP.SO_ID + "', '" + ErrorCode.ToString().Replace("'", "") + "','" + ErrMessage.ToString().Replace("'", "") + "',getdate())";

                        Com.ExecuteNonQuery(sUpdateLog);

                        str.Clear();
                        str.Add(new Outcls1
                        {
                            Cardcode = "",
                            Outtf = false,
                            Id = -1,
                            Responsemsg = "ErrCode-" + ErrorCode.ToString() + " - ErrMsg-" + ErrMessage
                        });


                        // return result_ds;

                    }
                    else
                    {
                        string dockey = SAP_ConnectToCompany.mCompany.GetNewObjectKey();
                        string docType = SAP_ConnectToCompany.mCompany.GetNewObjectType();
                        
                        DataSet oDs;
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oSalesOrder);
                        str.Clear();
                        str.Add(new Outcls1
                        {
                            Cardcode = dockey,
                            Outtf = true,
                            Id = bP.SO_ID,
                            Responsemsg = "Success"
                        });


                    }
                }
                else
                {
                    string sUpdateLog;
                    sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                    + " Values('" + bP.DBName + "', 'RDD_OSOR_ODOO','" + bP.SO_ID + "', '" + "SAP-Connection Failed" + "','" + "SAP-Connection Failed" + "',getdate())";

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
