using Business.Entities.OurProduct;
using Business.Interface.IOurProduct;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Business.Service.OurProductService
{
    public class OurProductService : IOurProductService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public OurProductService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<string> AddOurProduct(OurProduct ourProduct)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@ItemForRFQID", ourProduct.ItemForRFQID)
                    ,new SqlParameter("@CollectionID", ourProduct.ItemCollectionID)
                    ,new SqlParameter("@IsRFQDone", ourProduct.IsRFQDone)
                    ,new SqlParameter("@Item", ourProduct.Item)
                    ,new SqlParameter("@UOMID", ourProduct.UOMID)
                    ,new SqlParameter("@Quantity", ourProduct.Quantity)
                    ,new SqlParameter("@ItemDescription", ourProduct.ItemDescription)
                    ,new SqlParameter("@CreatedOrModifiedBy", ourProduct.CreatedOrModifiedBy)
                };

                string obj = (string)await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_ItemForRFQ", param);

                return obj != null ? obj : "";
            }
            catch
            {
                throw;
            }
        }

        public async Task<PagedDataTable<GoForRFQCardTable>> GetSessionGoForRFQCardTable(string CollectionID)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@CollectionID",CollectionID)
                        /*new SqlParameter("@PageNo",1)"59024634912373606133"CollectionID
                        ,new SqlParameter("@PageSize","0")
                        ,new SqlParameter("@SearchString","")
                        ,new SqlParameter("@OrderBy","")
                        ,new SqlParameter("@SortBy",1)*/
                        };

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_ItemForRFQ", param))
                {
                    if (ds.Tables.Count > 0)
                    {
                        table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            if (table.ContainColumn("TotalCount"))
                                totalItemCount = Convert.ToInt32(table.Rows[0]["TotalCount"]);
                            else
                                totalItemCount = table.Rows.Count;
                        }
                    }
                    PagedDataTable<GoForRFQCardTable> lst = table.ToPagedDataTableList<GoForRFQCardTable>
                        (1, 0, totalItemCount, string.Empty, string.Empty, "1");
                    return lst;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> GoForRFQItemInactive(GoForRFQCardTable goForRFQCardTable)
        {
            try
            {
                SqlParameter[] param = {

                    new SqlParameter("@ItemForRFQID", goForRFQCardTable.ItemForRFQID)
                    ,new SqlParameter("@CreatedOrModifiedBy", goForRFQCardTable.CreatedOrModifiedBy)
                    //,new SqlParameter("@IsActive", goForRFQCardTable.IsActive == true ? 1 : 0)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_U_ItemForRFQInActive", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> IGoForRFQCardItem(GoForRFQCard goForRFQCard)
        {
            try
            {
                SqlParameter[] param = {
                     new SqlParameter("@InquiryID", goForRFQCard.InquiryID)
                    ,new SqlParameter("@InquiryNo", goForRFQCard.InquiryNo)
                    ,new SqlParameter("@InquiryDate", goForRFQCard.InquiryDate)
                    ,new SqlParameter("@FinancialYear", goForRFQCard.FinYearID)
                    ,new SqlParameter("@InquiryTypeID", 1)
                    ,new SqlParameter("@CustomerID", goForRFQCard.CustomerID)
                    ,new SqlParameter("@CustomerInquiryNo", goForRFQCard.InquiryNo)
                    ,new SqlParameter("@CustomerInquiryDate", goForRFQCard.CustomerInquiryDate)
                    ,new SqlParameter("@MediatorName", goForRFQCard.Mediator)
                    ,new SqlParameter("@InquiredBy", goForRFQCard.InquiredBy)
                    //,new SqlParameter("@InquiryAllottedto", goForRFQCard.InquiryAllottedto)
                    //,new SqlParameter("@ReceiveModeTypeID", goForRFQCard.ReceiveModeTypeID)
                    ,new SqlParameter("@ReceiveDate", goForRFQCard.InquiryDate)
                    ,new SqlParameter("@QuotationDueDays", goForRFQCard.QuotationDueDate)
                    //,new SqlParameter("@RequiredThirdPartyInspection", goForRFQCard.RequiredThirdPartyInspection)
                    //,new SqlParameter("@InspectionAgencyID", goForRFQCard.InspectionAgencyID)
                    ,new SqlParameter("@Remark", goForRFQCard.Remark)
                    ,new SqlParameter("@CreatedByOrModifiedBy", goForRFQCard.CreatedOrModifiedBy)
                    ,new SqlParameter("@IsActive", goForRFQCard.IsActive)
                    //,new SqlParameter("@InqItems", goForRFQCard.InqItems)
                };

                int obj = (int)await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_RFQDetail", param);

                return obj != null ? obj : 0;
            }
            catch
            {
                throw;
            }
        }

        public dynamic IGoForRFQCardItemList(DataTable dataTable)
        {
            try
            {
                //SqlParameter[] param = { 
                //     new SqlParameter("@InqItems", dataTable)
                //};
                SqlParameter param = new SqlParameter();
                param.ParameterName = "@InqItems";
                param.Value = dataTable;
                param.SqlDbType = SqlDbType.Structured;
                param.Direction = ParameterDirection.Input;
                param.TypeName = "UDTT_InquiryItemDetail";

                //Parameters.AddWithValue("@arrayList", tvp);
                //SqlParameter[] param = {
                //     new SqlParameter("@InqItems", dataTable)
                //};
                //foreach(var test in param)
                //{
                //    test.SqlDbType = SqlDbType.Structured;
                //    test.Direction = ParameterDirection.Input;
                //    test.TypeName = "UDTT_InquiryItemDetailS";
                //}

                /*SqlConnection sqlConnection = new SqlConnection("Data Source=103.92.235.45;Initial Catalog=stihydra_AksharSource;User Id=stihydra_bal;Password=xW41wnE9Fsrnu@og;MultipleActiveResultSets=true;TrustServerCertificate=True;");
                SqlCommand cmd = new SqlCommand("Usp_IU_RFQDetail", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter tvparam = cmd.Parameters.AddWithValue("@InqItems", dataTable);
                tvparam.SqlDbType = SqlDbType.Structured;
                tvparam.Direction = ParameterDirection.Input;
                tvparam.TypeName = "@InqItems";
                sqlConnection.Open();
                SqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    var test = myReader;
                }*/

                var obj = SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_RFQItemDetail", param);
                //return myReader;
                return obj != null ? obj : 0;
            }
            catch
            {
                throw;
            }
        }
    }
}
