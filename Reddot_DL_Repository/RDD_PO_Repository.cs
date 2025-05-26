using Reddot_DL_Interface;
using Reddot_EF;
using SAPbobsCOM;
using System.Data;
using System.Data.SqlClient;
using RDD_PO1 = Reddot_EF.RDD_PO1;
using RDD_PO2 = Reddot_EF.RDD_PO2;


namespace Reddot_DL_Repository
{
    public class RDD_PO_Repository : IRDD_PO_Repository
    {

        Commonfunction Com = new Commonfunction();

        public async Task<List<RDD_PO>> GetItem_GRV_SAP_LIST(string DbName, long? pagesize, int? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username)
        {
            string str = string.Empty;
            RDD_PO Itm1 = new RDD_PO();
            List<RDD_PO> item = new List<RDD_PO>();
            SqlParameter[] sqlParameters = { new SqlParameter("@DBName",DbName),
                                    new SqlParameter("@p_pageno",pageno),
                                    new SqlParameter("@p_pagesize",pagesize),
                                    new SqlParameter("@p_SortColumn",sortcoloumn),
                                    new SqlParameter("@p_SortOrder",sortorder),
                                    new SqlParameter("@s_date",s_date),
                                    new SqlParameter("@e_date",e_date),
                                    new SqlParameter("@p_UserName",username),


                                    };
            DataSet ds = Com.ExecuteDataSet("Odoo_GRVAPI_get", CommandType.StoredProcedure, sqlParameters);
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

                    DataTable dtModule2;
                    DataRowCollection drc2;

                    foreach (DataRow dr in drc)
                    {
                        List<RDD_PO1> itemdetails = new List<RDD_PO1>();
                        dtModule1 = ds.Tables[1].Select("DocEntry=" + dr["DocEntry"].ToString()).CopyToDataTable();
                        drc1 = dtModule1.Rows;
                        foreach (DataRow dr1 in drc1)
                        {
                            itemdetails.Add(new RDD_PO1
                            {
                                //docEntry	lineNum	Itemcode	dscription	quantity	price	Currency	LineTotal	TaxCode	WhsCode
                                lineNum = !string.IsNullOrWhiteSpace(dr1["lineNum"].ToString()) ? Convert.ToInt32(dr1["lineNum"].ToString()) : 0,
                                ItemCode = !string.IsNullOrWhiteSpace(dr1["Itemcode"].ToString()) ? dr1["Itemcode"].ToString() : "",
                                Description = !string.IsNullOrWhiteSpace(dr1["dscription"].ToString()) ? dr1["dscription"].ToString() : "",
                                Quantity = !string.IsNullOrWhiteSpace(dr1["quantity"].ToString()) ? Convert.ToDecimal(dr1["quantity"].ToString()) : 0,
                                // Currency = !string.IsNullOrWhiteSpace(dr1["Currency"].ToString()) ? dr1["Currency"].ToString() : "",
                                WhsCode = !string.IsNullOrWhiteSpace(dr1["WhsCode"].ToString()) ? dr1["WhsCode"].ToString() : "",
                                TaxCode = !string.IsNullOrWhiteSpace(dr1["TaxCode"].ToString()) ? dr1["TaxCode"].ToString() : "",
                                LineTotal = !string.IsNullOrWhiteSpace(dr1["LineTotal"].ToString()) ? Convert.ToDecimal(dr1["LineTotal"].ToString()) : 0,
                                UnitPrice = !string.IsNullOrWhiteSpace(dr1["price"].ToString()) ? Convert.ToDecimal(dr1["price"].ToString()) : 0,


                            });
                        }

                        List<RDD_PO2> itemdetail1 = new List<RDD_PO2>();
                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            dtModule2 = ds.Tables[2].Select("DOCENTRY=" + dr["DocEntry"].ToString()).CopyToDataTable();
                            drc2 = dtModule2.Rows;
                            foreach (DataRow dr1 in drc2)
                            {
                                itemdetail1.Add(new RDD_PO2
                                {
                                    //DOCENTRY, paymethod,refno, refdate,memo,currency, amount
                                    //docEntry	lineNum	Itemcode	dscription	quantity	price	Currency	LineTotal	TaxCode	WhsCode

                                    paymethod = !string.IsNullOrWhiteSpace(dr1["paymethod"].ToString()) ? dr1["paymethod"].ToString() : "",
                                    Chequeno = !string.IsNullOrWhiteSpace(dr1["refno"].ToString()) ? dr1["refno"].ToString() : "",
                                    Paydate = !string.IsNullOrWhiteSpace(dr1["refdate"].ToString()) ? Convert.ToDateTime(dr1["refdate"].ToString()) : DateTime.Now,
                                    Remark = !string.IsNullOrWhiteSpace(dr1["memo"].ToString()) ? dr1["memo"].ToString() : "",
                                    Currency = !string.IsNullOrWhiteSpace(dr1["currency"].ToString()) ? dr1["currency"].ToString() : "",
                                    Amount = !string.IsNullOrWhiteSpace(dr1["amount"].ToString()) ? Convert.ToDecimal(dr1["amount"].ToString()) : 0,



                                });
                            }
                        }

                        item.Add(new RDD_PO
                        {

                            //TotalCount,RowNum,Source,remarks,enduser,bZ_SEG,ourREf,postingDate,deliveryDate,cardCode,cardName,
                            ////refNo,RDD_Project,businesType,invPayTerms,custPayTerms,salesEmp,docCur,DocEntry
                            RefNo = !string.IsNullOrWhiteSpace(dr["refNo"].ToString()) ? dr["refNo"].ToString() : "",
                            DocStatus = !string.IsNullOrWhiteSpace(dr["DocStatus"].ToString()) ? dr["DocStatus"].ToString() : "",
                            RDD_Project = !string.IsNullOrWhiteSpace(dr["RDD_Project"].ToString()) ? dr["RDD_Project"].ToString() : "",
                            OurREf = !string.IsNullOrWhiteSpace(dr["Source"].ToString()) ? dr["Source"].ToString() : "",
                            RowNum = !string.IsNullOrWhiteSpace(dr["RowNum"].ToString()) ? Convert.ToInt64(dr["RowNum"].ToString()) : 0,
                            TotalCount = !string.IsNullOrWhiteSpace(dr["TotalCount"].ToString()) ? Convert.ToInt64(dr["TotalCount"].ToString()) : 0,
                            SalesEmp = !string.IsNullOrWhiteSpace(dr["salesEmp"].ToString()) ? dr["salesEmp"].ToString() : "0",
                            enduser = !string.IsNullOrWhiteSpace(dr["enduser"].ToString()) ? dr["enduser"].ToString() : "",
                            bZ_SEG = !string.IsNullOrWhiteSpace(dr["bZ_SEG"].ToString()) ? dr["bZ_SEG"].ToString() : "",
                            CardCode = !string.IsNullOrWhiteSpace(dr["cardCode"].ToString()) ? dr["cardCode"].ToString() : "",
                            CardName = !string.IsNullOrWhiteSpace(dr["cardName"].ToString()) ? dr["cardName"].ToString() : "",
                            CustPayTerms = !string.IsNullOrWhiteSpace(dr["custPayTerms"].ToString()) ? dr["custPayTerms"].ToString() : "",
                            InvPayTerms = !string.IsNullOrWhiteSpace(dr["invPayTerms"].ToString()) ? dr["invPayTerms"].ToString() : "",
                            DocCur = !string.IsNullOrWhiteSpace(dr["docCur"].ToString()) ? dr["docCur"].ToString() : "",
                            BusinesType = !string.IsNullOrWhiteSpace(dr["businesType"].ToString()) ? dr["businesType"].ToString() : "",
                            shipto = !string.IsNullOrWhiteSpace(dr["shipto"].ToString()) ? dr["shipto"].ToString() : "",
                            Source = !string.IsNullOrWhiteSpace(dr["Source"].ToString()) ? dr["Source"].ToString() : "",
                            docentry = !string.IsNullOrWhiteSpace(dr["DocEntry"].ToString()) ? Convert.ToInt64(dr["DocEntry"].ToString()) : 0,

                            DocTotal = !string.IsNullOrWhiteSpace(dr["DocTotalSy"].ToString()) ? Convert.ToDecimal(dr["DocTotalSy"].ToString()) : 0,
                            PostingDate = !string.IsNullOrWhiteSpace(dr["postingDate"].ToString()) ? Convert.ToDateTime(dr["postingDate"].ToString()) : DateTime.Now,
                            DeliveryDate = !string.IsNullOrWhiteSpace(dr["deliveryDate"].ToString()) ? Convert.ToDateTime(dr["deliveryDate"].ToString()) : DateTime.Now,

                            RDD_PO1List = itemdetails,
                            RDD_PO2List = itemdetail1
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

        public async Task<DataSet> GetItem_INV_RECON_SAP_LIST(string DbName, long? pagesize, int? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username)
        {
            string str = string.Empty;
            RDD_PO Itm1 = new RDD_PO();
            List<RDD_PO> item = new List<RDD_PO>();
            SqlParameter[] sqlParameters = { new SqlParameter("@DBName",DbName),
                                    new SqlParameter("@p_pageno",pageno),
                                    new SqlParameter("@p_pagesize",pagesize),
                                    new SqlParameter("@p_SortColumn",sortcoloumn),
                                    new SqlParameter("@p_SortOrder",sortorder),
                                    new SqlParameter("@s_date",s_date),
                                    new SqlParameter("@e_date",e_date),
                                    new SqlParameter("@p_UserName",username),


                                    };
            DataSet ds = Com.ExecuteDataSet("Odoo_INVOICE_REC_API_get", CommandType.StoredProcedure, sqlParameters);
            return ds;
        }

        public async Task<List<RDD_PO>> GetItem_PO_SAP_LIST(string DbName, long? pagesize, int? pageno, DateTime s_date, DateTime e_date, string? sortcoloumn, string? sortorder, string username)
        {
            string str = string.Empty;
            RDD_PO Itm1 = new RDD_PO();
            List<RDD_PO> item = new List<RDD_PO>();
            SqlParameter[] sqlParameters = { new SqlParameter("@DBName",DbName),
                                    new SqlParameter("@p_pageno",pageno),
                                    new SqlParameter("@p_pagesize",pagesize),
                                    new SqlParameter("@p_SortColumn",sortcoloumn),
                                    new SqlParameter("@p_SortOrder",sortorder),
                                    new SqlParameter("@s_date",s_date),
                                    new SqlParameter("@e_date",e_date),
                                    new SqlParameter("@p_UserName",username),


                                    };
            DataSet ds = Com.ExecuteDataSet("Odoo_PurchaseAPI_get", CommandType.StoredProcedure, sqlParameters);
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

                    DataTable dtModule2;
                    DataRowCollection drc2;

                    foreach (DataRow dr in drc)
                    {
                        List<RDD_PO1> itemdetails = new List<RDD_PO1>();
                        dtModule1 = ds.Tables[1].Select("DocEntry=" + dr["DocEntry"].ToString()).CopyToDataTable();
                        drc1 = dtModule1.Rows;
                        foreach (DataRow dr1 in drc1)
                        {
                            itemdetails.Add(new RDD_PO1
                            {
                                //docEntry	lineNum	Itemcode	dscription	quantity	price	Currency	LineTotal	TaxCode	WhsCode
                                lineNum = !string.IsNullOrWhiteSpace(dr1["lineNum"].ToString()) ? Convert.ToInt32(dr1["lineNum"].ToString()) : 0,
                                ItemCode = !string.IsNullOrWhiteSpace(dr1["Itemcode"].ToString()) ? dr1["Itemcode"].ToString() : "",
                                Description = !string.IsNullOrWhiteSpace(dr1["dscription"].ToString()) ? dr1["dscription"].ToString() : "",
                                Quantity = !string.IsNullOrWhiteSpace(dr1["quantity"].ToString()) ? Convert.ToDecimal(dr1["quantity"].ToString()) : 0,
                                // Currency = !string.IsNullOrWhiteSpace(dr1["Currency"].ToString()) ? dr1["Currency"].ToString() : "",
                                WhsCode = !string.IsNullOrWhiteSpace(dr1["WhsCode"].ToString()) ? dr1["WhsCode"].ToString() : "",
                                TaxCode = !string.IsNullOrWhiteSpace(dr1["TaxCode"].ToString()) ? dr1["TaxCode"].ToString() : "",
                                LineTotal = !string.IsNullOrWhiteSpace(dr1["LineTotal"].ToString()) ? Convert.ToDecimal(dr1["LineTotal"].ToString()) : 0,
                                UnitPrice = !string.IsNullOrWhiteSpace(dr1["price"].ToString()) ? Convert.ToDecimal(dr1["price"].ToString()) : 0,


                            });
                        }

                        List<RDD_PO2> itemdetail1 = new List<RDD_PO2>();
                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            dtModule2 = ds.Tables[2].Select("DOCENTRY=" + dr["DocEntry"].ToString()).CopyToDataTable();
                            drc2 = dtModule2.Rows;
                            foreach (DataRow dr1 in drc2)
                            {
                                itemdetail1.Add(new RDD_PO2
                                {
                                    //DOCENTRY, paymethod,refno, refdate,memo,currency, amount
                                    //docEntry	lineNum	Itemcode	dscription	quantity	price	Currency	LineTotal	TaxCode	WhsCode

                                    paymethod = !string.IsNullOrWhiteSpace(dr1["paymethod"].ToString()) ? dr1["paymethod"].ToString() : "",
                                    Chequeno = !string.IsNullOrWhiteSpace(dr1["refno"].ToString()) ? dr1["refno"].ToString() : "",
                                    Paydate = !string.IsNullOrWhiteSpace(dr1["refdate"].ToString()) ? Convert.ToDateTime(dr1["refdate"].ToString()) : DateTime.Now,
                                    Remark = !string.IsNullOrWhiteSpace(dr1["memo"].ToString()) ? dr1["memo"].ToString() : "",
                                    Currency = !string.IsNullOrWhiteSpace(dr1["currency"].ToString()) ? dr1["currency"].ToString() : "",
                                    Amount = !string.IsNullOrWhiteSpace(dr1["amount"].ToString()) ? Convert.ToDecimal(dr1["amount"].ToString()) : 0,



                                });
                            }
                        }

                        item.Add(new RDD_PO
                        {

                            //TotalCount,RowNum,Source,remarks,enduser,bZ_SEG,ourREf,postingDate,deliveryDate,cardCode,cardName,
                            ////refNo,RDD_Project,businesType,invPayTerms,custPayTerms,salesEmp,docCur,DocEntry
                            RefNo = !string.IsNullOrWhiteSpace(dr["refNo"].ToString()) ? dr["refNo"].ToString() : "",
                            DocStatus = !string.IsNullOrWhiteSpace(dr["DocStatus"].ToString()) ? dr["DocStatus"].ToString() : "",
                            RDD_Project = !string.IsNullOrWhiteSpace(dr["RDD_Project"].ToString()) ? dr["RDD_Project"].ToString() : "",
                            OurREf = !string.IsNullOrWhiteSpace(dr["Source"].ToString()) ? dr["Source"].ToString() : "",
                            RowNum = !string.IsNullOrWhiteSpace(dr["RowNum"].ToString()) ? Convert.ToInt64(dr["RowNum"].ToString()) : 0,
                            TotalCount = !string.IsNullOrWhiteSpace(dr["TotalCount"].ToString()) ? Convert.ToInt64(dr["TotalCount"].ToString()) : 0,
                            SalesEmp = !string.IsNullOrWhiteSpace(dr["salesEmp"].ToString()) ? dr["salesEmp"].ToString() : "0",
                            enduser = !string.IsNullOrWhiteSpace(dr["enduser"].ToString()) ? dr["enduser"].ToString() : "",
                            bZ_SEG = !string.IsNullOrWhiteSpace(dr["bZ_SEG"].ToString()) ? dr["bZ_SEG"].ToString() : "",
                            CardCode = !string.IsNullOrWhiteSpace(dr["cardCode"].ToString()) ? dr["cardCode"].ToString() : "",
                            CardName = !string.IsNullOrWhiteSpace(dr["cardName"].ToString()) ? dr["cardName"].ToString() : "",
                            CustPayTerms = !string.IsNullOrWhiteSpace(dr["custPayTerms"].ToString()) ? dr["custPayTerms"].ToString() : "",
                            InvPayTerms = !string.IsNullOrWhiteSpace(dr["invPayTerms"].ToString()) ? dr["invPayTerms"].ToString() : "",
                            DocCur = !string.IsNullOrWhiteSpace(dr["docCur"].ToString()) ? dr["docCur"].ToString() : "",
                            BusinesType = !string.IsNullOrWhiteSpace(dr["businesType"].ToString()) ? dr["businesType"].ToString() : "",
                            Source = !string.IsNullOrWhiteSpace(dr["Source"].ToString()) ? dr["Source"].ToString() : "",
                            docentry = !string.IsNullOrWhiteSpace(dr["DocEntry"].ToString()) ? Convert.ToInt64(dr["DocEntry"].ToString()) : 0,

                            DocTotal = !string.IsNullOrWhiteSpace(dr["DocTotalSy"].ToString()) ? Convert.ToDecimal(dr["DocTotalSy"].ToString()) : 0,
                            PostingDate = !string.IsNullOrWhiteSpace(dr["postingDate"].ToString()) ? Convert.ToDateTime(dr["postingDate"].ToString()) : DateTime.Now,
                            DeliveryDate = !string.IsNullOrWhiteSpace(dr["deliveryDate"].ToString()) ? Convert.ToDateTime(dr["deliveryDate"].ToString()) : DateTime.Now,

                            RDD_PO1List = itemdetails,
                            RDD_PO2List = itemdetail1
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

        public async Task<List<Outcls1>> SaveGRV_SAP(RDD_GRV bP)
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

                    SAPbobsCOM.Documents oPoOrder;
                    oPoOrder = (SAPbobsCOM.Documents)SAP_ConnectToCompany.mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseDeliveryNotes);


                    oPoOrder.CardCode = bP.CardCode;
                    oPoOrder.CardName = bP.CardName;
                    oPoOrder.DocDate = Convert.ToDateTime(bP.PostingDate);
                    oPoOrder.DocDueDate = Convert.ToDateTime(bP.DeliveryDate);
                    oPoOrder.TaxDate = Convert.ToDateTime(bP.PostingDate);

                    //oPoOrder.Series=0;
                    oPoOrder.NumAtCard = bP.RefNo;

                    oPoOrder.GroupNumber = Convert.ToInt16(bP.CustPayTerms);
                    oPoOrder.PaymentGroupCode = Convert.ToInt16(bP.CustPayTerms);// 8;//Convert.ToInt16(DS.Tables[0].Rows[0]["CustPayTerms"].ToString());
                    oPoOrder.SalesPersonCode = Convert.ToInt16(bP.SalesEmp);
                    oPoOrder.Comments = bP.Remarks;
                    oPoOrder.DocCurrency = bP.DocCur;

                    oPoOrder.UserFields.Fields.Item("U_SO_ID").Value = bP.PO_ID;
                    oPoOrder.UserFields.Fields.Item("U_PayTerm").Value = bP.InvPayTerms;
                    oPoOrder.UserFields.Fields.Item("U_Project").Value = bP.RDD_Project;
                    oPoOrder.UserFields.Fields.Item("U_BizType").Value = bP.BusinesType;
                    // oPoOrder.UserFields.Fields.Item("U_APInvoiceNo").Value = bP.APNumber;
                    //oPoOrder.UserFields.Fields.Item("U_approval_code").Value = bP.ApprovalCode;
                    //oPoOrder.UserFields.Fields.Item("U_isredeem").Value = bP.IsRedeemRequestOrder;
                    // oPoOrder.UserFields.Fields.Item("U_BZ_SEG").Value = bP.BZ_SEG;
                    oPoOrder.UserFields.Fields.Item("U_ENDUSER").Value = bP.enduser;
                    // oPoOrder.UserFields.Fields.Item("U_AGNO").Value = bP.BeSpokeProjectCode;
                    //oPoOrder.UserFields.Fields.Item("U_BSPKPRJ").Value = bP.BeSpokeProjectCode;


                    //oPoOrder.UserFields.Fields.Item("U_WTVCODE").Value = bP.WHVATCODE;
                    //oPoOrder.UserFields.Fields.Item("U_WTPN").Value = bP.WHVATPER;
                    //oPoOrder.UserFields.Fields.Item("U_WTAMT").Value = Convert.ToDouble(bP.WHVATAMT);


                    //oPoOrder.UserFields.Fields.Item("U_WCode").Value = bP.WHTAXCODE;
                    //oPoOrder.UserFields.Fields.Item("U_WTTAX").Value = Convert.ToDouble(bP.WHTAXPER);
                    //oPoOrder.UserFields.Fields.Item("U_WTTAXAMT").Value = Convert.ToDouble(bP.WHTAXAMT);




                    //if (bP.DBName.Trim().ToUpper() == "SAPAE" || bP.DBName.Trim().ToUpper() == "SAPTRI")
                    //{
                    //    oPoOrder.UserFields.Fields.Item("U_forwarder").Value = bP.Forwarder;
                    //}
                    if (bP.DBName.Trim().ToUpper() == "SAPAE" || bP.DBName.Trim().ToUpper() == "SAPTRI" || bP.DBName.Trim().ToUpper() == "SAPTRI-TEST")
                    {
                        oPoOrder.UserFields.Fields.Item("U_OURREF").Value = bP.OurREf;
                        // oPoOrder.UserFields.Fields.Item("U_PrdTyp").Value = bP.ProductType;
                        //oPoOrder.UserFields.Fields.Item("U_CustTyp").Value = bP.CustomerType;

                    }
                    try
                    {




                    }
                    catch { }

                    //if (bP.RDD_PO2List.Count > 0)
                    //{
                    //    for (int iRow = 0; iRow < bP.RDD_PO1List.Count; iRow++)
                    //    {
                    //        oPoOrder.UserFields.Fields.Item("U_paymethod" + (iRow + 1).ToString()).Value = bP.RDD_PO2List[iRow].paymethod;
                    //        oPoOrder.UserFields.Fields.Item("U_refno" + (iRow + 1).ToString()).Value = bP.RDD_PO2List[iRow].Chequeno;
                    //        oPoOrder.UserFields.Fields.Item("U_refdate" + (iRow + 1).ToString()).Value = bP.RDD_PO2List[iRow].Paydate;
                    //        oPoOrder.UserFields.Fields.Item("U_memo" + (iRow + 1).ToString()).Value = bP.RDD_PO2List[iRow].Remark;
                    //        oPoOrder.UserFields.Fields.Item("U_currency" + (iRow + 1).ToString()).Value = bP.RDD_PO2List[iRow].Currency;
                    //        oPoOrder.UserFields.Fields.Item("U_amount" + (iRow + 1).ToString()).Value = Convert.ToDouble(bP.RDD_PO2List[iRow].Amount);
                    //    }
                    //}


                    oPoOrder.UserFields.Fields.Item("U_SOR_User").Value = bP.CreatedBy;



                    if (bP.RDD_GRV1List.Count > 0)
                    {
                        for (int iRow = 0; iRow < bP.RDD_GRV1List.Count; iRow++)
                        {
                            oPoOrder.Lines.ItemCode = bP.RDD_GRV1List[iRow].ItemCode;
                            oPoOrder.Lines.ItemDescription = bP.RDD_GRV1List[iRow].Description;
                            oPoOrder.Lines.Quantity = Convert.ToDouble(bP.RDD_GRV1List[iRow].Quantity);
                            if (Convert.ToDouble(bP.RDD_GRV1List[iRow].DiscPer) > 0)
                            {
                                oPoOrder.Lines.UnitPrice = Convert.ToDouble(bP.RDD_GRV1List[iRow].UnitPrice);
                            }
                            else
                            {
                                oPoOrder.Lines.Price = Convert.ToDouble(bP.RDD_GRV1List[iRow].UnitPrice);
                            }
                            oPoOrder.Lines.DiscountPercent = Convert.ToDouble(bP.RDD_GRV1List[iRow].DiscPer);

                            if (bP.DBName.Trim().ToUpper() == "SAPAE" || bP.DBName.Trim().ToUpper() == "SAPTRI" || bP.DBName.Trim().ToUpper() == "SAPTRI-TEST")
                            {
                                oPoOrder.Lines.VatGroup = bP.RDD_GRV1List[iRow].TaxCode;
                            }
                            else
                            {
                                oPoOrder.Lines.TaxCode = bP.RDD_GRV1List[iRow].TaxCode;
                            }
                            // 

                            oPoOrder.Lines.WarehouseCode = bP.RDD_GRV1List[iRow].WhsCode;

                            //oPoOrder.Lines.UserFields.Fields.Item("U_QtyAval").Value = "";


                            oPoOrder.Lines.Add();

                        }
                    }

                    Result = oPoOrder.Add();

                    if (Result != 0)
                    {
                        SAP_ConnectToCompany.mCompany.GetLastError(out ErrorCode, out ErrMessage);

                        string sUpdateLog;
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oPoOrder);

                        sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                        + " Values('" + bP.DBName + "', 'RDD_GRV_ODOO','" + bP.PO_ID + "', '" + ErrorCode + "','" + ErrMessage.ToString().Replace("'", "") + "',getdate())";

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
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oPoOrder);
                        str.Clear();
                        str.Add(new Outcls1
                        {
                            Cardcode = dockey,
                            Outtf = true,
                            Id = bP.PO_ID,
                            Responsemsg = "Success"
                        });


                    }
                }
                else
                {
                    string sUpdateLog;
                    sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                    + " Values('" + bP.DBName + "', 'RDD_GRV_ODOO','" + bP.PO_ID + "', '" + "SAP-Connection Failed" + "','" + "SAP-Connection Failed" + "',getdate())";

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

        public async Task<List<Outcls1>> SavePO_SAP(RDD_PO bP)
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

                    SAPbobsCOM.Documents oPoOrder;
                    oPoOrder = (SAPbobsCOM.Documents)SAP_ConnectToCompany.mCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseOrders);

                    



                    oPoOrder.CardCode = bP.CardCode;
                    oPoOrder.CardName = bP.CardName;
                    oPoOrder.DocDate = Convert.ToDateTime(bP.PostingDate);
                    oPoOrder.DocDueDate = Convert.ToDateTime(bP.DeliveryDate);
                    oPoOrder.TaxDate = Convert.ToDateTime(bP.PostingDate);

                    //oPoOrder.Series=0;
                    oPoOrder.NumAtCard = bP.RefNo;

                    oPoOrder.GroupNumber = Convert.ToInt16(bP.CustPayTerms);
                    oPoOrder.PaymentGroupCode = Convert.ToInt16(bP.CustPayTerms);// 8;//Convert.ToInt16(DS.Tables[0].Rows[0]["CustPayTerms"].ToString());
                    oPoOrder.SalesPersonCode = Convert.ToInt16(bP.SalesEmp);
                    oPoOrder.Comments = bP.Remarks;
                    oPoOrder.DocCurrency = bP.DocCur;

                    oPoOrder.UserFields.Fields.Item("U_SO_ID").Value = bP.PO_ID;
                    oPoOrder.UserFields.Fields.Item("U_PayTerm").Value = bP.InvPayTerms;
                    oPoOrder.UserFields.Fields.Item("U_Project").Value = bP.RDD_Project;
                    oPoOrder.UserFields.Fields.Item("U_BizType").Value = bP.BusinesType;
                    oPoOrder.UserFields.Fields.Item("U_shipto").Value = bP.shipto;
                    // oPoOrder.UserFields.Fields.Item("U_APInvoiceNo").Value = bP.APNumber;
                    //oPoOrder.UserFields.Fields.Item("U_approval_code").Value = bP.ApprovalCode;
                    //oPoOrder.UserFields.Fields.Item("U_isredeem").Value = bP.IsRedeemRequestOrder;
                    // oPoOrder.UserFields.Fields.Item("U_BZ_SEG").Value = bP.BZ_SEG;
                    oPoOrder.UserFields.Fields.Item("U_ENDUSER").Value = bP.enduser;
                    // oPoOrder.UserFields.Fields.Item("U_AGNO").Value = bP.BeSpokeProjectCode;
                    //oPoOrder.UserFields.Fields.Item("U_BSPKPRJ").Value = bP.BeSpokeProjectCode;


                    //oPoOrder.UserFields.Fields.Item("U_WTVCODE").Value = bP.WHVATCODE;
                    //oPoOrder.UserFields.Fields.Item("U_WTPN").Value = bP.WHVATPER;
                    //oPoOrder.UserFields.Fields.Item("U_WTAMT").Value = Convert.ToDouble(bP.WHVATAMT);


                    //oPoOrder.UserFields.Fields.Item("U_WCode").Value = bP.WHTAXCODE;
                    //oPoOrder.UserFields.Fields.Item("U_WTTAX").Value = Convert.ToDouble(bP.WHTAXPER);
                    //oPoOrder.UserFields.Fields.Item("U_WTTAXAMT").Value = Convert.ToDouble(bP.WHTAXAMT);


                    oPoOrder.UserFields.Fields.Item("U_ActualIntraPO").Value = bP.ActualOdoono;

                    //if (bP.DBName.Trim().ToUpper() == "SAPAE" || bP.DBName.Trim().ToUpper() == "SAPTRI")
                    //{
                    //    oPoOrder.UserFields.Fields.Item("U_forwarder").Value = bP.Forwarder;
                    //}
                    if (bP.DBName.Trim().ToUpper() == "SAPAE" || bP.DBName.Trim().ToUpper() == "SAPTRI" || bP.DBName.Trim().ToUpper() == "SAPTRI-TEST")
                    {
                        oPoOrder.UserFields.Fields.Item("U_OURREF").Value = bP.OurREf;
                        // oPoOrder.UserFields.Fields.Item("U_PrdTyp").Value = bP.ProductType;
                        //oPoOrder.UserFields.Fields.Item("U_CustTyp").Value = bP.CustomerType;

                    }
                    try
                    {




                    }
                    catch { }

                    if (bP.RDD_PO2List.Count > 0)
                    {
                        for (int iRow = 0; iRow < bP.RDD_PO2List.Count; iRow++)
                        {
                            oPoOrder.UserFields.Fields.Item("U_paymethod" + (iRow + 1).ToString()).Value = bP.RDD_PO2List[iRow].paymethod;
                            oPoOrder.UserFields.Fields.Item("U_refno" + (iRow + 1).ToString()).Value = bP.RDD_PO2List[iRow].Chequeno;
                            oPoOrder.UserFields.Fields.Item("U_refdate" + (iRow + 1).ToString()).Value = bP.RDD_PO2List[iRow].Paydate;
                            oPoOrder.UserFields.Fields.Item("U_memo" + (iRow + 1).ToString()).Value = bP.RDD_PO2List[iRow].Remark;
                            oPoOrder.UserFields.Fields.Item("U_currency" + (iRow + 1).ToString()).Value = bP.RDD_PO2List[iRow].Currency;
                            oPoOrder.UserFields.Fields.Item("U_amount" + (iRow + 1).ToString()).Value = Convert.ToDouble(bP.RDD_PO2List[iRow].Amount);
                        }
                    }


                    oPoOrder.UserFields.Fields.Item("U_SOR_User").Value = bP.CreatedBy;



                    if (bP.RDD_PO1List.Count > 0)
                    {
                        for (int iRow = 0; iRow < bP.RDD_PO1List.Count; iRow++)
                        {
                            oPoOrder.Lines.ItemCode = bP.RDD_PO1List[iRow].ItemCode;
                            oPoOrder.Lines.ItemDescription = bP.RDD_PO1List[iRow].Description;
                            oPoOrder.Lines.Quantity = Convert.ToDouble(bP.RDD_PO1List[iRow].Quantity);
                            if (Convert.ToDouble(bP.RDD_PO1List[iRow].DiscPer) > 0)
                            {
                                oPoOrder.Lines.UnitPrice = Convert.ToDouble(bP.RDD_PO1List[iRow].UnitPrice);
                            }
                            else
                            {
                                oPoOrder.Lines.Price = Convert.ToDouble(bP.RDD_PO1List[iRow].UnitPrice);
                            }
                            oPoOrder.Lines.DiscountPercent = Convert.ToDouble(bP.RDD_PO1List[iRow].DiscPer);

                            if (bP.DBName.Trim().ToUpper() == "SAPAE" || bP.DBName.Trim().ToUpper() == "SAPTRI" || bP.DBName.Trim().ToUpper() == "SAPTRI-TEST")
                            {
                                oPoOrder.Lines.VatGroup = bP.RDD_PO1List[iRow].TaxCode;
                            }
                            else
                            {
                                oPoOrder.Lines.TaxCode = bP.RDD_PO1List[iRow].TaxCode;
                            }
                            // 

                            oPoOrder.Lines.WarehouseCode = bP.RDD_PO1List[iRow].WhsCode;

                            //oPoOrder.Lines.UserFields.Fields.Item("U_QtyAval").Value = "";


                            oPoOrder.Lines.Add();

                        }
                    }

                    Result = oPoOrder.Add();

                    if (Result != 0)
                    {
                        SAP_ConnectToCompany.mCompany.GetLastError(out ErrorCode, out ErrMessage);

                        string sUpdateLog;
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oPoOrder);

                        sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                        + " Values('" + bP.DBName + "', 'RDD_PO_ODOO','" + bP.PO_ID + "', '" + ErrorCode.ToString().Replace("'","") + "','" + ErrMessage.ToString().Replace("'", "") + "',getdate())";

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
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oPoOrder);
                        str.Clear();
                        str.Add(new Outcls1
                        {
                            Cardcode = dockey,
                            Outtf = true,
                            Id = bP.PO_ID,
                            Responsemsg = "Success"
                        });


                    }
                }
                else
                {
                    string sUpdateLog;
                    sUpdateLog = "insert into tejSAP.DBO.RDD_TBL_SAPErrorlog (DBName,BaseType,BaseEntry,ErrorCode,ErrorMessage,CreateDate) "
                    + " Values('" + bP.DBName + "', 'RDD_PO_ODOO','" + bP.PO_ID + "', '" + "SAP-Connection Failed" + "','" + "SAP-Connection Failed" + "',getdate())";

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

        public async Task<List<Outcls1>> SavePurchaseAgreement(PurchaseAgreement purchaseAgreement)
        {
                        List <Outcls1> str = new List<Outcls1>();
            int Result = 0;
            string Cardcode = "";
            try
            {
                if (SAP_ConnectToCompany.ConnectToSAP(purchaseAgreement.DBname) == true)
                {
                    string errItemCodes = "", errMachineNos = "";
                    bool errRowFlag = false;
                    int ErrorCode;
                    string ErrMessage;
                    int RefDocSeries, iOrd;
                    string RefDocNum = "", RefDocDate = "", DocNum = "", Usrid = "";                   
                    SAPbobsCOM.CompanyService oCompanyService;
                    SAPbobsCOM.BlanketAgreementsService oBlanketAgreementsService;

                    SAPbobsCOM.BlanketAgreement oBlanketAgreement;
                    oCompanyService = SAP_ConnectToCompany.mCompany.GetCompanyService();

                    oBlanketAgreementsService = oCompanyService.GetBusinessService(SAPbobsCOM.ServiceTypes.BlanketAgreementsService) as SAPbobsCOM.BlanketAgreementsService;

                    oBlanketAgreement = oBlanketAgreementsService.GetDataInterface(SAPbobsCOM.BlanketAgreementsServiceDataInterfaces.basBlanketAgreement) as SAPbobsCOM.BlanketAgreement;

                    oBlanketAgreement.BPCode = purchaseAgreement.BpCode;
                    oBlanketAgreement.BPCurrency = "AED";
                    oBlanketAgreement.StartDate = Convert.ToDateTime(purchaseAgreement.StartDate);  
                    oBlanketAgreement.EndDate = Convert.ToDateTime(purchaseAgreement.EndDate);  
                    
                    oBlanketAgreement.Description = purchaseAgreement.Descript;
                    oBlanketAgreement.AgreementMethod = SAPbobsCOM.BlanketAgreementMethodEnum.amItem;
                    DateTime? myTime = null;
                    oBlanketAgreement.TerminateDate = purchaseAgreement.TermDate is null ? Convert.ToDateTime(myTime) : Convert.ToDateTime(purchaseAgreement.TermDate);
                    oBlanketAgreement.Renewal =    SAPbobsCOM.BoYesNoEnum.tYES;
                    if (purchaseAgreement.Status == "A")
                    {
                        oBlanketAgreement.Status = SAPbobsCOM.BlanketAgreementStatusEnum.asApproved;
                    }
                    else if (purchaseAgreement.Status == "D")
                    {
                        oBlanketAgreement.Status = SAPbobsCOM.BlanketAgreementStatusEnum.asDraft;
                    }else if (purchaseAgreement.Status == "T")
                    {
                        oBlanketAgreement.Status = SAPbobsCOM.BlanketAgreementStatusEnum.asTerminated;
                    }
                    else if (purchaseAgreement.Status == "H")
                    {
                        oBlanketAgreement.Status = SAPbobsCOM.BlanketAgreementStatusEnum.asOnHold;
                    }
                    oBlanketAgreement.Renewal = SAPbobsCOM.BoYesNoEnum.tNO;

                    oBlanketAgreement.SigningDate = DateTime.Now;
                    int i = 0;
                    while (purchaseAgreement.PurchaseAgreementDetails.Count > i)
                    {
                        SAPbobsCOM.BlanketAgreements_ItemsLine oBlanketAgreements_ItemsLines = oBlanketAgreement.BlanketAgreements_ItemsLines.Add();
                        
                        oBlanketAgreements_ItemsLines.ItemNo = purchaseAgreement.PurchaseAgreementDetails[i].ItemCode;
                        oBlanketAgreements_ItemsLines.ItemDescription = purchaseAgreement.PurchaseAgreementDetails[i].ItemName;                        
                        oBlanketAgreements_ItemsLines.PlannedQuantity = purchaseAgreement.PurchaseAgreementDetails[i].PlanQty;
                        oBlanketAgreements_ItemsLines.UnitPrice = 0;
                        oBlanketAgreements_ItemsLines.PlannedAmountFC = 0;
                        
                        oBlanketAgreements_ItemsLines.PlannedVATAmountLC = 0;
                        oBlanketAgreements_ItemsLines.FreeText = purchaseAgreement.PurchaseAgreementDetails[i].FreeTxt;
                        oBlanketAgreements_ItemsLines.UserFields.Item("U_RebateAE").Value = purchaseAgreement.PurchaseAgreementDetails[i].U_RebateAE;
                        oBlanketAgreements_ItemsLines.UserFields.Item("U_RebateKE").Value = purchaseAgreement.PurchaseAgreementDetails[i].U_RebateKE;
                        oBlanketAgreements_ItemsLines.UserFields.Item("U_RebateUG").Value = purchaseAgreement.PurchaseAgreementDetails[i].U_RebateUG;
                        oBlanketAgreements_ItemsLines.UserFields.Item("U_RebateTZ").Value = purchaseAgreement.PurchaseAgreementDetails[i].U_RebateTZ;
                        oBlanketAgreements_ItemsLines.UserFields.Item("U_RebateZM").Value = purchaseAgreement.PurchaseAgreementDetails[i].U_RebateZM;
                        oBlanketAgreements_ItemsLines.UserFields.Item("U_RebateTRI").Value = purchaseAgreement.PurchaseAgreementDetails[i].U_RebateTRI;
                        oBlanketAgreements_ItemsLines.UserFields.Item("U_RebateML").Value = purchaseAgreement.PurchaseAgreementDetails[i].U_RebateML;
                        oBlanketAgreements_ItemsLines.UserFields.Item("U_RebateBT").Value = purchaseAgreement.PurchaseAgreementDetails[i].U_RebateBT;
                        oBlanketAgreements_ItemsLines.UserFields.Item("U_RebateMAU").Value = purchaseAgreement.PurchaseAgreementDetails[i].U_RebateMAU;
                        oBlanketAgreements_ItemsLines.UserFields.Item("U_RebateSA").Value = purchaseAgreement.PurchaseAgreementDetails[i].U_RebateSA;
                        oBlanketAgreements_ItemsLines.UserFields.Item("U_RebateLLC").Value = purchaseAgreement.PurchaseAgreementDetails[i].U_RebateLLC;
                        i++;
                    }
                    // Save changes
                    oBlanketAgreementsService.AddBlanketAgreement(oBlanketAgreement);                                          
                    }                                      
            }
            catch (Exception ex)
            {

                str.Clear();
                str.Add(new Outcls1
                {
                    Cardcode = purchaseAgreement.BpCode,
                    Outtf = false,
                    Id = -1,
                    Responsemsg = ex.Message
                });
                return str;
            }
            
            str.Clear();
            str.Add(new Outcls1
            {
                Cardcode = purchaseAgreement.BpCode,
                Outtf = true,
                Id = 1,
                Responsemsg = "Record Succesfully Added"
            });
            return str;
        }
    }
}
