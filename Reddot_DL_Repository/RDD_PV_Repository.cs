using Reddot_DL_Interface;
using Reddot_EF;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Reddot_DL_Repository
{
    public class RDD_PV_Repository : IRDD_PV_Repository
    {

        Commonfunction Com = new Commonfunction();
        public async Task<List<Outcls1>> SavePV_SAP(List<RDD_PV_V1> RPV)
        {
            List<Outcls1> str = new List<Outcls1>();
            str.Clear();
            str.Add(new Outcls1
            {
                Cardcode = "",
                Outtf = true,
                Id = 0,
                Responsemsg = ""
            });
            try
            {                
                    for (int i= 0;i < RPV.Count;i++)
                    {                     
                        using (TransactionScope scope = new TransactionScope())
                        {
                            SqlParameter[] Para = {
                        new SqlParameter("@p_PVId",RPV[i].PVId),
                        new SqlParameter("@p_Country",RPV[i].Country),
                        new SqlParameter("@p_RefNo",RPV[i].RefNo),
                        new SqlParameter("@p_DocStatus","Open"),
                        new SqlParameter("@p_VType",RPV[i].VType),
                        new SqlParameter("@p_DBName",RPV[i].DBName),
                        new SqlParameter("@p_Currency",RPV[i].Currency),
                        new SqlParameter("@p_VendorCode",RPV[i].VendorCode),
                        new SqlParameter("@p_VendorEmployee",RPV[i].VendorEmployee),
                        new SqlParameter("@p_Benificiary",RPV[i].Benificiary),
                        new SqlParameter("@p_RequestedAmt",RPV[i].RequestedAmt),
                        new SqlParameter("@p_ApprovedAmt",RPV[i].ApprovedAmt),
                        new SqlParameter("@p_BeingPayOf",RPV[i].BeingPayOf),
                        new SqlParameter("@p_PayRequestDate",RPV[i].PayRequestDate),
                        new SqlParameter("@p_BankCode",RPV[i].BankCode),
                        new SqlParameter("@p_BankName",RPV[i].BankName),
                        new SqlParameter("@p_PayMethod",RPV[i].PayMethod),
                        new SqlParameter("@p_PayRefNo",RPV[i].PayRefNo),
                        new SqlParameter("@p_PayDate",RPV[i].PayDate),                                                                   
                        new SqlParameter("@p_CreatedOn",RPV[i].CreatedOn),
                        new SqlParameter("@p_CreatedBy",RPV[i].CreatedBy),                       
                        new SqlParameter("@p_type","Insert"),
                        new SqlParameter("@p_Purpose",RPV[i].Purpose),
                        new SqlParameter("@p_id",RPV[i].id),
                        new SqlParameter("@p_response",RPV[i].Erormsg)

                };

                            str = Com.ExecuteNonQueryListID("RDD_PV_Insert_Update_Delete", Para);
                            if (str[0].Outtf == true)
                            {
                                int k = 0;                              
                                while (RPV[i].RDD_PVLinesDetails.Count > k)
                                {
                                    SqlParameter[] ParaDet1 = {
                                                 new SqlParameter("@p_PVLineId",0),
                                            new SqlParameter("@p_PVId",str[0].Id),
                                            new SqlParameter("@p_LineRefNo",k+1),
                                            new SqlParameter("@p_Date",RPV[i].RDD_PVLinesDetails[k].Date),
                                            new SqlParameter("@p_Description",RPV[i].RDD_PVLinesDetails[k].Description),
                                            new SqlParameter("@p_Amount",RPV[i].RDD_PVLinesDetails[k].Amount),
                                            new SqlParameter("@p_Remarks",RPV[i].RDD_PVLinesDetails[k].Remarks),                                            
                                            new SqlParameter("@p_CreatedOn",RPV[i].CreatedOn),
                                            new SqlParameter("@p_CreatedBy",RPV[i].CreatedBy),
                                            new SqlParameter("@p_FilePath","#"),
                                            new SqlParameter("@p_typ","I")
                            };
                                    var det1 = Com.ExecuteNonQuery("RDD_PVLinesInsert_Update_Delete", ParaDet1);
                                    if (det1 == false)
                                    {
                                        str.Clear();
                                        str.Add(new Outcls1
                                        {
                                            Outtf = false,
                                            Id = -1,
                                            Responsemsg = "Error occured : All Row Mandatory Details "
                                        });
                                        return str;
                                    }
                                    k++;
                                }
                            }

                            scope.Complete();
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
