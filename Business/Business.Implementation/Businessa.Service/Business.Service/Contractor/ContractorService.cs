using Business.Entities.Contractor;
using Business.Interface.IContractor;
using Business.SQL;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Business.Service.Contractor
{
    public class ContractorService : IContractorService

    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public ContractorService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }


        #region Contractor Index Page List

        public async Task<PagedDataTable<ContractorMaster>> GetAllContractorAsync(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "ContractorID", string sortBy = "ASC")
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
                        };

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_ContractorMaster", param))
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
                    PagedDataTable<ContractorMaster> lst = table.ToPagedDataTableList<ContractorMaster>
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


        #endregion Contractor Index Page List

        #region Contractor Basic Details

        public async Task<ContractorMaster> GetContractorAsync(int contractorID)
        {
            ContractorMaster result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@ContractorID", contractorID) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_ContractorMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<ContractorMaster>();
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

        public async Task<int> AddUpdateContractor(ContractorMaster contractorMaster)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@ContractorID", contractorMaster.ContractorID),
                    new SqlParameter("@ContractorCode", contractorMaster.ContractorCode),
                    new SqlParameter("@ContractorName", contractorMaster.ContractorName),
                    new SqlParameter("@GroupName", contractorMaster.GroupName),
                    new SqlParameter("@OwnerName", contractorMaster.OwnerName),
                    new SqlParameter("@UnitNoName", contractorMaster.UnitNoName),
                    new SqlParameter("@ContactPhone1", contractorMaster.ContactPhone1),
                    new SqlParameter("@Mobile1", contractorMaster.Mobile1),
                    new SqlParameter("@FaxNo", contractorMaster.FaxNo),
                    new SqlParameter("@IndustryTypeID", contractorMaster.IndustryTypeID),
                    new SqlParameter("@BusinessTypeID", contractorMaster.BusinessTypeID),
                    new SqlParameter("@IsActive", contractorMaster.IsActive),
                    new SqlParameter("@CreatedOrModifiedBy", contractorMaster.CreatedOrModifiedBy),
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_ContractorMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateContractorLogoImage(LeadLogoImage contractorLogoImage)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@ContractorID", contractorLogoImage.ContractorID),
                new SqlParameter("@LogoImageName", contractorLogoImage.LogoImageName),
                new SqlParameter("@LogoImagePath", contractorLogoImage.LogoImagePath),
                new SqlParameter("@IsActive", contractorLogoImage.IsActive),
                new SqlParameter("@CreatedOrModifiedBy", contractorLogoImage.CreatedOrModifiedBy ),

                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_contractorLogoImage", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Contractor Basic Details

        #region Contractor Contact Details
        public async Task<PagedDataTable<ContractorContactTxn>> GetContractorAllContactPerson(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int contractorId = 0)
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
                        ,new SqlParameter("@ContractorID",contractorId)
                        };

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_ContractorContactTxn", param))
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
                    PagedDataTable<ContractorContactTxn> lst = table.ToPagedDataTableList<ContractorContactTxn>
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

        public async Task<ContractorContactTxn> GetContractorContactPerson(int contractorContactID, int contractorId)
        {
            ContractorContactTxn result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@contractorContactID", contractorContactID),
                    new SqlParameter("@contractorID", contractorId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_ContractorContactTxn", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<ContractorContactTxn>();
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

        public async Task<int> AddUpdateContractorContactPerson(ContractorContactTxn contractorContactTxn)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@ContractorContactID", contractorContactTxn.ContractorContactID)
                    ,new SqlParameter("@ContractorID", contractorContactTxn.ContractorID)
                    ,new SqlParameter("@Prefix", contractorContactTxn.Prefix)
                    ,new SqlParameter("@ContactPerson", contractorContactTxn.ContactPersonName)
                    //,new SqlParameter("@DesignationID", contractorContactTxn.DesignationID)
                    //,new SqlParameter("@DepartmentID", contractorContactTxn.DepartmentID)
                    ,new SqlParameter("@PersonalMobile", contractorContactTxn.PersonalMobile)
                    ,new SqlParameter("@OfficeMobile", contractorContactTxn.OfficeMobile)
                    ,new SqlParameter("@PersonalEmailID", contractorContactTxn.PersonalEmailID)
                    ,new SqlParameter("@OfficeEmailID", contractorContactTxn.OfficeEmailID)
                    ,new SqlParameter("@AlternativeMobile", contractorContactTxn.AlternativeMobile)
                    ,new SqlParameter("@EmailGroupName", contractorContactTxn.EmailGroupName)
                    ,new SqlParameter("@BirthDate", contractorContactTxn.BirthDate)
                    ,new SqlParameter("@Religion", contractorContactTxn.Religion)
                    ,new SqlParameter("@IsActive", contractorContactTxn.IsActive)
                    ,new SqlParameter("@Notes", contractorContactTxn.Notes)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_ContractorContactTxn", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }
        #endregion Contractor Contact Details

        #region Contractor Address 

        public async Task<PagedDataTable<LeadAddressTxn>> GetContractorAllAddressAsync(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int ContractorId = 0)
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
                        ,new SqlParameter("@ContractorID",ContractorId)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_ContractorAddressTxn", param))
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
                    PagedDataTable<LeadAddressTxn> lst = table.ToPagedDataTableList<LeadAddressTxn>
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

        public async Task<LeadAddressTxn> GetContractorAddressTxn(int ContractorAddressTxnID, int ContractorId)
        {
            LeadAddressTxn result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@ContractorAddressTxnID", ContractorAddressTxnID),
                    new SqlParameter("@ContractorID", ContractorId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_ContractorAddressTxn", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<LeadAddressTxn>();
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

        public async Task<int> AddUpdateContractorAddress(LeadAddressTxn contractorAddressTxn)
        {
            try
            {
                SqlParameter[] param = {
                     new SqlParameter("@ContractorAddressTxnID", contractorAddressTxn.ContractorAddressTxnID)
                    ,new SqlParameter("@ContractorID", contractorAddressTxn.ContractorID)
                    ,new SqlParameter("@AddressTypeID", contractorAddressTxn.AddressTypeID)
                    ,new SqlParameter("@PlotNoName", contractorAddressTxn.PlotNoName)
                    ,new SqlParameter("@StreetNoName", contractorAddressTxn.StreetNoName)
                    ,new SqlParameter("@Landmark", contractorAddressTxn.Landmark)
                    ,new SqlParameter("@Area", contractorAddressTxn.Area)
                    ,new SqlParameter("@ZIPCode", contractorAddressTxn.ZIPCode)
                    ,new SqlParameter("@City", contractorAddressTxn.City)
                    ,new SqlParameter("@Taluka", contractorAddressTxn.Taluka)
                    ,new SqlParameter("@DistrictName", contractorAddressTxn.DistrictName)
                    ,new SqlParameter("@StateID", contractorAddressTxn.StateID)
                    ,new SqlParameter("@CountryID", contractorAddressTxn.CountryID)
                    ,new SqlParameter("@IsActive", contractorAddressTxn.IsActive)
                    ,new SqlParameter("@CreatedOrModifiedBy", contractorAddressTxn.CreatedOrModifiedBy)                    
                    /*,new SqlParameter("@AddressType", contractorAddressTxn.AddressType)
                    ,new SqlParameter("@MainAddress", contractorAddressTxn.MainAddress)
                    ,new SqlParameter("@StateName", contractorAddressTxn.StateName)
                    ,new SqlParameter("@CountryName", contractorAddressTxn.CountryName)
                    ,new SqlParameter("@Address1", contractorAddressTxn.Address1)
                    ,new SqlParameter("@Address2", contractorAddressTxn.Address2)
                    ,new SqlParameter("@Address3", contractorAddressTxn.Address3)
                    ,new SqlParameter("@District", contractorAddressTxn.District)*/               
                    };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_ContractorAddressTxn", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        #endregion Contractor Address 


        #region Contractor Contact Details 

        public async Task<LeadContactDetail> GetContractorContactDetail(int ContractorId)
        {
            LeadContactDetail result = null;
            try
            {
                SqlParameter[] param = {
                    //new SqlParameter("@EmployeeFamilyDetailID", employeeFamilyId),
                    new SqlParameter("@ContractorID", ContractorId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_ContractorContactDetail", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<LeadContactDetail>();
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

        public async Task<int> AddUpdateContractorContactDetail(LeadContactDetail contractorContactDetail)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@ContractorContactDetailID",contractorContactDetail.ContractorContactDetailID)
                    ,new SqlParameter("@ContractorID",contractorContactDetail.ContractorID)
                    ,new SqlParameter("@OfficePhone",contractorContactDetail.OfficePhone)
                    ,new SqlParameter("@MobileNo",contractorContactDetail.MobileNo)
                    ,new SqlParameter("@Website",contractorContactDetail.Website)
                };
                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_ContractorContactDetail", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        #endregion Contractor Contact Details

        #region Contractor Registration

        public async Task<LeadRegistration> GetContractorRegistration(int ContractorId)
        {
            LeadRegistration result = null;
            try
            {
                SqlParameter[] param = {
                    //new SqlParameter("@EmployeeFamilyDetailID", employeeFamilyId),
                    new SqlParameter("@ContractorID", ContractorId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_ContractorRegistration", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<LeadRegistration>();
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

        public async Task<int> AddUpdateContractorRegistration(LeadRegistration contractorRegistration)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@ContractorRegistrationID", contractorRegistration.ContractorRegistrationID)
                    ,new SqlParameter("@ContractorID", contractorRegistration.ContractorID)
                    ,new SqlParameter("@PANNo", contractorRegistration.PANNo)
                    ,new SqlParameter("@GSTINNo", contractorRegistration.GSTINNo)
                    ,new SqlParameter("@GSTINType", contractorRegistration.GSTINType)
                    ,new SqlParameter("@FactoryLicenseNo", contractorRegistration.FactoryLicenseNo)
                    ,new SqlParameter("@FactoryRegNo", contractorRegistration.FactoryRegNo)
                    ,new SqlParameter("@ARNNo", contractorRegistration.ARNNo)
                    ,new SqlParameter("@ECCNo", contractorRegistration.ECCNo)
                    ,new SqlParameter("@MSMENo", contractorRegistration.MSMENo)
                    ,new SqlParameter("@SSINo", contractorRegistration.SSINo)
                    ,new SqlParameter("@TANNo", contractorRegistration.TANNo)
                    ,new SqlParameter("@ExportNo", contractorRegistration.ExportNo)
                    ,new SqlParameter("@ImportNo", contractorRegistration.ImportNo)
                    ,new SqlParameter("@TaxRange", contractorRegistration.TaxRange)
                    ,new SqlParameter("@TaxDivisio", contractorRegistration.TaxDivisio)
                    ,new SqlParameter("@TaxCommisionerate", contractorRegistration.TaxCommisionerate)
                    ,new SqlParameter("@CreatedByORModifiedBy", contractorRegistration.CreatedOrModifiedBy)
                    ,new SqlParameter("@LabourContratorNo", contractorRegistration.LabourContratorNo)
                };
                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_ContractorRegistration", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        #endregion Contractor Registration 

        #region Contractor Bank Details

        public async Task<PagedDataTable<LeadBankDetails>> GetContractorAllBankAccount(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int ContractorId = 0)
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
                        ,new SqlParameter("@ContractorID",ContractorId)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_ContractorBankDetails", param))
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
                    PagedDataTable<LeadBankDetails> lst = table.ToPagedDataTableList<LeadBankDetails>
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

        public async Task<LeadBankDetails> GetContractorBankAccount(int ContractorBankDetailsId, int ContractorId)
        {
            LeadBankDetails result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@ContractorBankDetailsID", ContractorBankDetailsId),
                    new SqlParameter("@ContractorID", ContractorId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_ContractorBankDetails", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<LeadBankDetails>();
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

        public async Task<int> AddUpdateContractorBankDetails(LeadBankDetails ContractorBankDetails)
        {
            try
            {
                SqlParameter[] param = {
                     new SqlParameter("@ContractorBankDetailsID", ContractorBankDetails.ContractorBankDetailsID)
                    ,new SqlParameter("@ContractorID", ContractorBankDetails.ContractorID)
                    ,new SqlParameter("@BankName", ContractorBankDetails.BankName)
                    ,new SqlParameter("@AccountName", ContractorBankDetails.AccountName)
                    ,new SqlParameter("@IFSCCode", ContractorBankDetails.IFSCCode)
                    ,new SqlParameter("@AccountNO", ContractorBankDetails.AccountNO)
                    ,new SqlParameter("@BranchLocation", ContractorBankDetails.BranchLocation)
                    ,new SqlParameter("@City", ContractorBankDetails.City)
                    ,new SqlParameter("@BankCode", ContractorBankDetails.BankCode)
                    ,new SqlParameter("@BICSwiftCode", ContractorBankDetails.BICSwiftCode)
                    ,new SqlParameter("@CountryID", ContractorBankDetails.CountryID)
                    ,new SqlParameter("@IsDefaultBankAccount" , ContractorBankDetails.IsDefaultBankAccount)
                    ,new SqlParameter("@IsActive", ContractorBankDetails.IsActive)
                    ,new SqlParameter("@CreatedOrModifiedBy", ContractorBankDetails.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_ContractorBankDetails", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        #endregion Contractor Bank Details


        #region Contractor Setting

        public async Task<LeadSetting> GetContractorSetting(int ContractorId)
        {
            LeadSetting result = null;
            try
            {
                SqlParameter[] param = {
                    //new SqlParameter("@EmployeeFamilyDetailID", employeeFamilyId),
                    new SqlParameter("@ContractorID", ContractorId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_ContractorSetting", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<LeadSetting>();
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

        public async Task<int> AddUpdateContractorSetting(LeadSetting ContractorSetting)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@ContractorSettingID",ContractorSetting.ContractorSettingID)
                    ,new SqlParameter("@ContractorID",ContractorSetting.ContractorID)
                    ,new SqlParameter("@CreditLimit",ContractorSetting.CreditLimit)
                    ,new SqlParameter("@CommitementLimit",ContractorSetting.CommitementLimit)
                    ,new SqlParameter("@PaymentTerm",ContractorSetting.PaymentTerm)
                    ,new SqlParameter("@AllowDiscountPerPO",ContractorSetting.AllowDiscountPerPO)
                };
                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_ContractorSetting", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        #endregion Contractor Bank Setting

        #region Contractor Document

        public async Task<PagedDataTable<LeadDocument>> GetContractorsAllDocuments(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int ContractorId = 0)
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
                        ,new SqlParameter("@ContractorID",ContractorId)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_ContractorDocument", param))
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
                    PagedDataTable<LeadDocument> lst = table.ToPagedDataTableList<LeadDocument>
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

        public async Task<LeadDocument> GetContractorDocument(int ContractorDocumentId, int ContractorId)
        {
            LeadDocument result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@ContractorDocumentID", ContractorDocumentId),
                    new SqlParameter("@ContractorID", ContractorId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_ContractorDocument", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<LeadDocument>();
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

        public async Task<int> AddUpdateContractorDocument(LeadDocument ContractorDocument)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@ContractorDocumentID", ContractorDocument.ContractorDocumentID)
                    ,new SqlParameter("@ContractorID", ContractorDocument.ContractorID)
                    ,new SqlParameter("@DocumentName", ContractorDocument.DocumentName)
                    ,new SqlParameter("@DocumentExtension", ContractorDocument.DocumentExtension)
                    ,new SqlParameter("@DocumentTypeID", ContractorDocument.DocumentTypeID)
                    //,new SqlParameter("@IsDeleted", ContractorDocument.IsDeleted)
                    ,new SqlParameter("@DocumentDescription", ContractorDocument.DocumentDescription)
                    ,new SqlParameter("@DocumentPath", ContractorDocument.DocumentPath)
                    ,new SqlParameter("@IsActive", ContractorDocument.IsActive)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_ContractorDocument", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        #endregion Contractor Document
    }
}
