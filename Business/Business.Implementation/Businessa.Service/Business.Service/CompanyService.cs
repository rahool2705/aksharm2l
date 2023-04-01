using Business.Entities;
using Business.Entities.Company;
using Business.Interface;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Business.Service
{
    public class CompanyService : ISiteCompanyRepository
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public CompanyService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }


        #region "Add Company Slider"
        public async Task<CompanyMasterMetadata> GetCompanyAsync(int CompanyID)
        {
            CompanyMasterMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@CompanyID", CompanyID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CompanyMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CompanyMasterMetadata>();
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
        #endregion "Add Company Slider"

        #region "Company Master"
        public async Task<PagedDataTable<CompanyMasterMetadata>> GetAllCompanyAsync(int pageNo, int pageSize, string searchString = "", string orderBy = "CompanyName", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<CompanyMasterMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CompanyMaster", param))
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
                    lst = table.ToPagedDataTableList<CompanyMasterMetadata>(pageNo, pageSize, totalItemCount);
                    return lst;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (table != null)
                    table.Dispose();
            }
        }

        public async Task<int> CreateOrUpdateCompanyAsync(CompanyMasterMetadata compnay)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@CompanyID", compnay.CompanyID)
                ,new SqlParameter("@CompanyCode", compnay.CompanyCode)
                ,new SqlParameter("@CompanyName", compnay.CompanyName)
                ,new SqlParameter("@IsActive", compnay.IsActive)
                ,new SqlParameter("@CompanyWebsiteText", compnay.CompanyWebsiteText)
                ,new SqlParameter("@CompanyLogoName", compnay.CompanyLogoName)
                ,new SqlParameter("@CompanyLogoImagePath", compnay.CompanyLogoImagePath)
                ,new SqlParameter("@CompanyGroupName", compnay.CompanyGroupName)
                ,new SqlParameter("@UnitName", compnay.UnitName)
                ,new SqlParameter("@IndustryTypeID", compnay.IndustryTypeID)
                ,new SqlParameter("@BusinessTypeID", compnay.BusinessTypeID)
                ,new SqlParameter("@CreatedOrModifiedBy", compnay.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CompanyMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch
            {
                throw;
            }
        }

        public async Task<CompanyMasterMetadata> GetCompnayAsync(string companyID)
        {
            CompanyMasterMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@CompanyID", companyID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CompanyMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CompanyMasterMetadata>();
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<CompanyLogoMasterMetadata> GetCompanyLogoMaster(int companyID)
        {
            DataTable table = new DataTable();
            CompanyLogoMasterMetadata item = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@CompanyID", companyID) };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "usp_Get_CompanyLogoMaster", param))
                {
                    if (ds.Tables.Count > 0)
                    {
                        table = ds.Tables[0];
                        if (table.Rows.Count > 0)
                        {
                            DataRow dr = table.Rows[0];
                            item = dr.ToPagedDataTableList<CompanyLogoMasterMetadata>();
                        }
                    }
                }
                return item;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (table != null)
                    table.Dispose();
            }
        }
        #endregion

        #region "Compnay contact"
        public async Task<PagedDataTable<CompanyContactTxnMetadata>> GetAllCompanyContactAsync(int companyID)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<CompanyContactTxnMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@CompanyID",companyID)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CompanyContactPersons", param))
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
                    lst = table.ToPagedDataTableList<CompanyContactTxnMetadata>();
                    return lst;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (table != null)
                    table.Dispose();
            }
        }

        public async Task<int> CreateOrUpdateCompanyContactAsync(CompanyContactTxnMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@CompanyContactPersonsID", item.CompanyContactPersonsID)
                ,new SqlParameter("@CompanyID", item.CompanyID)
                        ,new SqlParameter("@DepartmentID", item.DepartmentID)
                        ,new SqlParameter("@DesignationID", item.DesignationID)
                        ,new SqlParameter("@Prefix", item.Prefix)
                        ,new SqlParameter("@PersonName", item.PersonName)
                        ,new SqlParameter("@PersonalMobileNo", item.PersonalMobileNo)
                        ,new SqlParameter("@OfficeMobileNo", item.OfficeMobileNo)
                        ,new SqlParameter("@AlternetMobileNo", item.AlternetMobileNo)
                        ,new SqlParameter("@EmailGroupID", item.EmailGroupID)
                        ,new SqlParameter("@PersonEmail", item.PersonEmail)
                        ,new SqlParameter("@OfficeEmail", item.OfficeEmail)
                        ,new SqlParameter("@Birthdate", item.Birthdate)
                        ,new SqlParameter("@Religion", item.Religion)
                        ,new SqlParameter("@IsActive", item.IsActive)
                        ,new SqlParameter("@IsResigned", item.IsResigned)
                        ,new SqlParameter("@Note", item.Note)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CompanyContactPersons", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch
            {
                throw;
            }
        }

        public async Task<CompanyContactTxnMetadata> GetCompnayContactAsync(int companyID, int companyContactPersonsID)
        {
            CompanyContactTxnMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@companyContactPersonsID", companyContactPersonsID)
                ,new SqlParameter("@CompanyID", companyID)};
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CompanyContactPersons", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CompanyContactTxnMetadata>();
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region "Compnay Address"
        public async Task<PagedDataTable<CompanyAddressTxnMetadata>> GetAllCompanyAddressAsync(int companyID)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<CompanyAddressTxnMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@CompanyID",companyID)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CompanyAddressTxn", param))
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
                    lst = table.ToPagedDataTableList<CompanyAddressTxnMetadata>();
                    return lst;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (table != null)
                    table.Dispose();
            }
        }

        public async Task<int> CreateOrUpdateCompanyAddressAsync(CompanyAddressTxnMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CompanyAddressTxnID", item.CompanyAddressTxnID)
                    ,new SqlParameter("@CompanyID", item.CompanyID)
                    ,new SqlParameter("@Address1", item.Address1)
                    ,new SqlParameter("@Address2", item.Address2)
                    ,new SqlParameter("@Address3", item.Address3)
                    ,new SqlParameter("@Area", item.Area)
                    ,new SqlParameter("@ZIPCodeID", item.ZIPCodeID)
                    ,new SqlParameter("@IsActive", item.IsActive)
                    ,new SqlParameter("@CityID", item.CityID)
                    ,new SqlParameter("@DistrictID", item.DistrictID)
                    ,new SqlParameter("@TalukaID", item.TalukaID)
                    ,new SqlParameter("@AddressTypeID", item.AddressTypeID)
                    ,new SqlParameter("@CreatedOrModifiedBy",item.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CompanyAddressTxn", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CompanyAddressTxnMetadata> GetCompnayAddressAsync(int companyID, int companyAddressTxnID)
        {
            CompanyAddressTxnMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@CompanyAddressTxnID", companyAddressTxnID)
                ,new SqlParameter("@CompanyID", companyID)};
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CompanyAddressTxn", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CompanyAddressTxnMetadata>();
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region "Company Registration"

        public async Task<CompanyRegistration> GetCompanyRegistration(int CompanyId, int CompanyRegistrationID)
        {
            CompanyRegistration result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CompanyRegistrationID", CompanyRegistrationID),
                    new SqlParameter("@CompanyId", CompanyId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CompanyRegistration", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CompanyRegistration>();
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> AddUpdateCompanyRegistration(CompanyRegistration companyRegistration)
        {
            try
            {
                SqlParameter[] param = {
                    /*new SqlParameter("@CompanyRegistrationID", CompanyRegistration.CompanyRegistrationID)
                    ,new SqlParameter("@CompanyID", CompanyRegistration.CompanyID)
                    ,new SqlParameter("@PANNo", CompanyRegistration.PANNo)
                    ,new SqlParameter("@GSTINNo", CompanyRegistration.GSTINNo)
                    ,new SqlParameter("@GSTINType", CompanyRegistration.GSTINType)
                    ,new SqlParameter("@FactoryLicenseNo", CompanyRegistration.FactoryLicenseNo)
                    ,new SqlParameter("@FactoryRegNo", CompanyRegistration.FactoryRegNo)
                    ,new SqlParameter("@ARNNo", CompanyRegistration.ARNNo)
                    ,new SqlParameter("@ECCNo", CompanyRegistration.ECCNo)
                    ,new SqlParameter("@MSMENo", CompanyRegistration.MSMENo)
                    ,new SqlParameter("@SSINo", CompanyRegistration.SSINo)
                    ,new SqlParameter("@TANNo", CompanyRegistration.TANNo)
                    ,new SqlParameter("@ExportNo", CompanyRegistration.ExportNo)
                    ,new SqlParameter("@ImportNo", CompanyRegistration.ImportNo)
                    ,new SqlParameter("@TaxRange", CompanyRegistration.TaxRange)
                    ,new SqlParameter("@TaxDivisio", CompanyRegistration.TaxDivisio)
                    ,new SqlParameter("@TaxCommisionerate", CompanyRegistration.TaxCommisionerate)
                    ,new SqlParameter("@CreatedByORModifiedBy", CompanyRegistration.CreatedOrModifiedBy)
                    ,new SqlParameter("@LabourContratorNo", CompanyRegistration.LabourContratorNo)*/

                    new SqlParameter("@CompanyRegistrationID",companyRegistration.CompanyRegistrationID )
                    ,new SqlParameter("@CompanyID",companyRegistration.CompanyID)
                    ,new SqlParameter("@PANNo",companyRegistration.PANNo)
                    ,new SqlParameter("@GSTINNo",companyRegistration.GSTINNo)
                    ,new SqlParameter("@GSTINType",companyRegistration.GSTINType)
                    ,new SqlParameter("@LabourContratorNo", companyRegistration.LabourContratorNo)
                    ,new SqlParameter("@FactoryLicenseNo",companyRegistration.FactoryLicenseNo)
                    ,new SqlParameter("@FactoryRegNo",companyRegistration.FactoryRegNo)
                    ,new SqlParameter("@ARNNo",companyRegistration.ARNNo)
                    ,new SqlParameter("@ECCNo",companyRegistration.ECCNo)
                    ,new SqlParameter("@MSMENo",companyRegistration.MSMENo)
                    ,new SqlParameter("@SSINo",companyRegistration.SSINo)
                    ,new SqlParameter("@TANNo",companyRegistration.TANNo)
                    ,new SqlParameter("@ExportNo",companyRegistration.ExportNo)
                    ,new SqlParameter("@ImportNo",companyRegistration.ImportNo)
                    ,new SqlParameter("@TaxRange",companyRegistration.TaxRange)
                    ,new SqlParameter("@TaxDivisio",companyRegistration.TaxDivisio)
                    ,new SqlParameter("@TaxCommisionerate",companyRegistration.TaxCommisionerate)

                };
                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CompanyRegistration", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }


        public async Task<int> CreateOrUpdateCompanyRegistrationAsync(CompanyMasterMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@CompanyRegistrationID", item.CompanyRegistrationID)
                ,new SqlParameter("@CompanyID", item.CompanyID)
                ,new SqlParameter("@PANNo", item.PANNo)
                ,new SqlParameter("@GSTINNo", item.GSTINNo)
                ,new SqlParameter("@GSTINType", item.GSTINType)
                ,new SqlParameter("@FactoryLicenseNo", item.FactoryLicenseNo)
                ,new SqlParameter("@FactoryRegNo", item.FactoryRegNo)
                ,new SqlParameter("@ARNNo", item.ARNNo)
                ,new SqlParameter("@ECCNo", item.ECCNo)
                ,new SqlParameter("@MSMENo", item.MSMENo)
                ,new SqlParameter("@SSINo", item.SSINo)
                ,new SqlParameter("@TANNo", item.TANNo)
                ,new SqlParameter("@ExportNo", item.ExportNo)
                ,new SqlParameter("@ImportNo", item.ImportNo)
                ,new SqlParameter("@TaxRange", item.TaxRange)
                ,new SqlParameter("@TaxDivisio", item.TaxDivisio)
                ,new SqlParameter("@TaxCommisionerate", item.TaxCommisionerate)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CompanyRegistration", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region "Company Banking"

        #region "SuperAdmin Bank List"

        public async Task<PagedDataTable<CompanyBankDetails>> GetAllCompanyBankingAsync(int companyID)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<CompanyBankDetails> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@CompanyID",companyID)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CompanyBanking", param))
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
                    lst = table.ToPagedDataTableList<CompanyBankDetails>();
                    return lst;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (table != null)
                    table.Dispose();
            }
        }

        public async Task<CompanyBankDetails> GetCompanyBankAccount(int CompanyBankingID, int CompanyId)
        {
            CompanyBankDetails result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CompanyBankingID", CompanyBankingID),
                    new SqlParameter("@CompanyID", CompanyId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CompanyBanking", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CompanyBankDetails>();
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> AddUpdateCompanyBankDetails(CompanyBankDetails companyBankDetails)
        {
            try
            {
                SqlParameter[] param = {
                     new SqlParameter("@CompanyBankingID",companyBankDetails.CompanyBankingID)
                    ,new SqlParameter("@CompanyID",companyBankDetails.CompanyID)
                    ,new SqlParameter("@BankName",companyBankDetails.BankName)
                    ,new SqlParameter("@AccountName",companyBankDetails.AccountName)
                    ,new SqlParameter("@IFCICode",companyBankDetails.IFCICode)
                    ,new SqlParameter("@AccountNO",companyBankDetails.AccountNO)
                    ,new SqlParameter("@BranchLocation",companyBankDetails.BranchLocation)
                    ,new SqlParameter("@City",companyBankDetails.City)
                    ,new SqlParameter("@BankCode",companyBankDetails.BankCode)
                    ,new SqlParameter("@BIC_SWIFTCode",companyBankDetails.BIC_SWIFTCode)
                    ,new SqlParameter("@CountryID",companyBankDetails.CountryID)
                    ,new SqlParameter("@IsDefaultBankAccount",companyBankDetails.IsDefaultBankAccount)
                    ,new SqlParameter("@IsActive",companyBankDetails.IsActive)
                    ,new SqlParameter("@CreatedOrModifiedBy",companyBankDetails.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CompanyBanking", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        #endregion "SuperAdmin Bank List"

        public async Task<CompanyBankingMetadata> GetCompanyBankingAsync(int companyID, int companyBankingID)
        {
            CompanyBankingMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@CompanyBankingID", companyBankingID)
                ,new SqlParameter("@CompanyID", companyID)};
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CompanyBanking", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CompanyBankingMetadata>();
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> CreateOrUpdateCompanyBankingAsync(CompanyBankingMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@CompanyBankingID", item.CompanyBankingID)
                ,new SqlParameter("@CompanyID", item.CompanyID)
                ,new SqlParameter("@BankName", item.BankName)
                ,new SqlParameter("@BankCode", item.BankCode)
                ,new SqlParameter("@AccountNo", item.AccountNo)
                ,new SqlParameter("@BIC_SWIFTCode", item.BIC_SWIFTCode)
                ,new SqlParameter("@AccountName", item.AccountName)
                ,new SqlParameter("@IFCICode", item.IFCICode)
                ,new SqlParameter("@Branch", item.Branch)
                ,new SqlParameter("@CityID", item.CityID)
                ,new SqlParameter("@CountryID", item.CountryID)
                ,new SqlParameter("@IsActive", item.IsActive)
                ,new SqlParameter("@CreatedOrModifyBy", item.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CompanyBanking", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch
            {
                throw;
            }
        }
        /*public async Task<PagedDataTable<CompanyBankingMetadata>> GetAllCompanyBankingAsync(int companyID)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<CompanyBankingMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@CompanyID",companyID)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CompanyBanking", param))
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
                    lst = table.ToPagedDataTableList<CompanyBankingMetadata>();
                    return lst;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (table != null)
                    table.Dispose();
            }
        }*/
        #endregion

        #region "Company Document"

        #region "SuperAdmin Document"

        public async Task<PagedDataTable<CompanyDocument>> GetCompanyAllDocuments(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int CompanyId = 0)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            try
            {
                SqlParameter[] param = {
                         new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1),
                        new SqlParameter("@CompanyID",CompanyId)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CompanyDocuments", param))
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
                    PagedDataTable<CompanyDocument> lst = table.ToPagedDataTableList<CompanyDocument>
                        (pageNo, pageSize, totalItemCount, searchString, orderBy, sortBy);
                    return lst;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (table != null)
                    table.Dispose();
            }
        }

        #region Company Document

        /*public async Task<PagedDataTable<CompanyDocument>> GetCompanyAllDocuments(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int CompanyId = 0)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)
                        ,new SqlParameter("@CompanyID",CompanyId)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CompanyDocument", param))
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
                    PagedDataTable<CompanyDocument> lst = table.ToPagedDataTableList<CompanyDocument>
                        (pageNo, pageSize, totalItemCount, searchString, orderBy, sortBy);
                    return lst;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (table != null)
                    table.Dispose();
            }
        }*/

        public async Task<CompanyDocument> GetCompanyDocument(int CompanyDocumentId, int CompanyId)
        {
            CompanyDocument result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CompanyDocumentsID", CompanyDocumentId),
                    new SqlParameter("@CompanyID", CompanyId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CompanyDocuments", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CompanyDocument>();
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> AddUpdateCompanyDocument(CompanyDocument CompanyDocument)
        {
            try
            {
                SqlParameter[] param = {
                     new SqlParameter("@CompanyDocumentsID",    CompanyDocument.CompanyDocumentsID)
                    ,new SqlParameter("@CompanyID",             CompanyDocument.CompanyID)
                    ,new SqlParameter("@DocumentName",          CompanyDocument.DocumentName)
                    ,new SqlParameter("@DocumentExtension",     CompanyDocument.DocumentExtension)
                    ,new SqlParameter("@DocumentTypeID",        CompanyDocument.DocumentTypeID)                    
                    ,new SqlParameter("@DocumentDesc",          CompanyDocument.DocumentDesc)
                    ,new SqlParameter("@DocumentPath",          CompanyDocument.DocumentPath)
                    ,new SqlParameter("@IsActive",              CompanyDocument.IsActive)
                    //,new SqlParameter("@IsDeleted", CompanyDocument.IsDeleted)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CompanyDocuments", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        #endregion Company Document


        #endregion "SuperAdmin Document"

        public async Task<CompanyDocumentMetadata> GetDocumentAsync(int companyID, int companyDocumentsID)
        {
            CompanyDocumentMetadata result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@CompanyDocumentsID", companyDocumentsID)
                ,new SqlParameter("@CompanyID", companyID)};
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CompanyDocuments", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CompanyDocumentMetadata>();
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }
        public async Task<int> CreateOrUpdateCompanyDocumentAsync(CompanyDocumentMetadata item)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@CompanyDocumentsID", item.CompanyDocumentsID)
                ,new SqlParameter("@CompanyID", item.CompanyID)
                ,new SqlParameter("@DocumentTypeID", item.DocumentTypeID)
                ,new SqlParameter("@DocumentName", item.DocumentName)
                ,new SqlParameter("@DocumentDesc", item.DocumentDesc)
                ,new SqlParameter("@IsActive", item.IsActive)
                ,new SqlParameter("@DocumentPath", item.DocumentPath)

                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CompanyDocuments", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch
            {
                throw;
            }
        }
        public async Task<PagedDataTable<CompanyDocumentMetadata>> GetAllCompanyDocumentAsync(int companyID)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<CompanyDocumentMetadata> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@CompanyID",companyID)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CompanyDocuments", param))
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
                    lst = table.ToPagedDataTableList<CompanyDocumentMetadata>();
                    return lst;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (table != null)
                    table.Dispose();
            }
        }
        #endregion

        
    }
}
