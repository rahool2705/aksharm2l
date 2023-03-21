using Business.Entities.Employee;
using Business.Interface.ICustomer;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Business.Entities.Customer;

namespace Business.Service.Customer
{
    public class CustomerService : ICustomerService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public CustomerService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<PagedDataTable<CustomerMaster>> GetAllCustomerAsync(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "CustomerID", string sortBy = "ASC")
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

                //    using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeMaster", param))     Change SP to customers

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CustomerMaster", param))
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
                    PagedDataTable<CustomerMaster> lst = table.ToPagedDataTableList<CustomerMaster>
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

        public async Task<CustomerMaster> GetCustomerAsync(int customerId)
        {
            CustomerMaster result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@CustomerID", customerId) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CustomerMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CustomerMaster>();
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

        public async Task<int> AddUpdateCustomer(CustomerMaster customerMaster)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CustomerID", customerMaster.CustomerID),
                    new SqlParameter("@CustomerCode", customerMaster.CustomerCode),
                    new SqlParameter("@CustomerName", customerMaster.CustomerName),
                    new SqlParameter("@GroupName", customerMaster.GroupName),
                    new SqlParameter("@OwnerName", customerMaster.OwnerName),
                    new SqlParameter("@UnitNoName", customerMaster.UnitNoName),
                    new SqlParameter("@ContactPhone1", customerMaster.ContactPhone1),
                    new SqlParameter("@Mobile1", customerMaster.Mobile1),
                    new SqlParameter("@FaxNo", customerMaster.FaxNo),
                    new SqlParameter("@IndustryTypeID", customerMaster.IndustryTypeID),
                    new SqlParameter("@BusinessTypeID", customerMaster.BusinessTypeID),
                    new SqlParameter("@IsActive", customerMaster.IsActive),
                    new SqlParameter("@CreatedOrModifiedBy", customerMaster.CreatedOrModifiedBy),
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CustomerMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> UpdateCustomerLogoImage(CustomerLogoImage customerLogoImage)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@CustomerID", customerLogoImage.CustomerID),
                new SqlParameter("@LogoImageName", customerLogoImage.LogoImageName),
                new SqlParameter("@LogoImagePath", customerLogoImage.LogoImagePath),
                new SqlParameter("@IsActive", customerLogoImage.IsActive),
                new SqlParameter("@CreatedOrModifiedBy", customerLogoImage.CreatedOrModifiedBy ),

                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CustomerLogoImage", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Contact Person Detail
        public async Task<PagedDataTable<CustomerContactTxn>> GetCustomerAllContactPerson(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int customerId = 0)
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
                        ,new SqlParameter("@CustomerID",customerId)
                        };

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CustomerContactTxn", param))
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
                    PagedDataTable<CustomerContactTxn> lst = table.ToPagedDataTableList<CustomerContactTxn>
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

        public async Task<CustomerContactTxn> GetCustomerContactPerson(int customerContactID, int customerId)
        {
            CustomerContactTxn result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CustomerContactID", customerContactID),
                    new SqlParameter("@CustomerID", customerId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CustomerContactTxn", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CustomerContactTxn>();
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

        public async Task<int> AddUpdateCustomerContactPerson(CustomerContactTxn customerContactTxn)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CustomerContactID", customerContactTxn.CustomerContactID)
                    ,new SqlParameter("@CustomerID", customerContactTxn.CustomerID)
                    ,new SqlParameter("@Prefix", customerContactTxn.Prefix)
                    ,new SqlParameter("@ContactPerson", customerContactTxn.ContactPersonName)
                    ,new SqlParameter("@DesignationID", customerContactTxn.DesignationID)
                    ,new SqlParameter("@DepartmentID", customerContactTxn.DepartmentID)
                    ,new SqlParameter("@PersonalMobile", customerContactTxn.PersonalMobile)
                    ,new SqlParameter("@OfficeMobile", customerContactTxn.OfficeMobile)
                    ,new SqlParameter("@PersonalEmailID", customerContactTxn.PersonalEmailID)
                    ,new SqlParameter("@OfficeEmailID", customerContactTxn.OfficeEmailID)
                    ,new SqlParameter("@AlternativeMobile", customerContactTxn.AlternativeMobile)
                    ,new SqlParameter("@EmailGroupName", customerContactTxn.EmailGroupName)
                    ,new SqlParameter("@BirthDate", customerContactTxn.BirthDate)
                    ,new SqlParameter("@Religion", customerContactTxn.Religion)
                    ,new SqlParameter("@IsActive", customerContactTxn.IsActive)
                    ,new SqlParameter("@Notes", customerContactTxn.Notes)/*
                    ,new SqlParameter("@CreatedOrModifiedBy", customerContactTxn.CreatedOrModifiedBy)*/
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CustomerContactTxn", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }
        #endregion Contact Person Detail

        #region Address detail
        public async Task<PagedDataTable<CustomerAddressTxn>> GetCustomerAllAddressAsync(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int customerId = 0)
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
                        ,new SqlParameter("@CustomerID",customerId)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CustomerAddressTxn", param))
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
                    PagedDataTable<CustomerAddressTxn> lst = table.ToPagedDataTableList<CustomerAddressTxn>
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
        public async Task<int> AddUpdateCustomeAddress(CustomerAddressTxn customerAddressTxn)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CustomerAddressTxnID", customerAddressTxn.CustomerAddressTxnID)
                    ,new SqlParameter("@CustomerID", customerAddressTxn.CustomerID)
                    ,new SqlParameter("@Address1", customerAddressTxn.PlotNoName)
                    ,new SqlParameter("@Address2", customerAddressTxn.StreetNoName)
                    //,new SqlParameter("@Address3", customerAddressTxn.Address3)
                    ,new SqlParameter("@Landmark", customerAddressTxn.Landmark)
                    ,new SqlParameter("@Area", customerAddressTxn.Area)
                    ,new SqlParameter("@ZIPCode", customerAddressTxn.ZIPCode)
                    ,new SqlParameter("@IsActive", customerAddressTxn.IsActive)
                    ,new SqlParameter("@City", customerAddressTxn.City)
                    ,new SqlParameter("@District", customerAddressTxn.District)
                    ,new SqlParameter("@Taluka", customerAddressTxn.Taluka)
                    ,new SqlParameter("@AddressTypeID", customerAddressTxn.AddressTypeID)
                    ,new SqlParameter("@CountryID", customerAddressTxn.CountryID)
                    ,new SqlParameter("@StateID", customerAddressTxn.StateID)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CustomerAddressTxn", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CustomerAddressTxn> GetCustomerAddressTxn(int customeAddressTxnId, int customerId)
        {
            CustomerAddressTxn result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CustomerAddressTxnID", customeAddressTxnId),
                    new SqlParameter("@CustomerID", customerId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CustomerAddressTxn", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CustomerAddressTxn>();
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
        #endregion Address detail

        #region Customer Contact Detail
        public async Task<CustomerContactDetail> GetCustomerContactDetail(int customerId)
        {
            CustomerContactDetail result = null;
            try
            {
                SqlParameter[] param = {
                    //new SqlParameter("@EmployeeFamilyDetailID", employeeFamilyId),
                    new SqlParameter("@CustomerID", customerId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CustomerContactDetail", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CustomerContactDetail>();
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

        public async Task<int> AddUpdateCustomerContactDetail(CustomerContactDetail customerContactDetail)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CustomerContactDetailID",customerContactDetail.CustomerContactDetailID)
                    ,new SqlParameter("@CustomerID",customerContactDetail.CustomerID)
                    ,new SqlParameter("@OfficePhone",customerContactDetail.OfficePhone)
                    ,new SqlParameter("@MobileNo",customerContactDetail.MobileNo)
                    ,new SqlParameter("@Website",customerContactDetail.Website)
                };
                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CustomerContactDetail", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }
        #endregion Customer Contact Detail

        #region Customer Registration

        public async Task<CustomerRegistration> GetCustomerRegistration(int customerId)
        {
            CustomerRegistration result = null;
            try
            {
                SqlParameter[] param = {
                    //new SqlParameter("@EmployeeFamilyDetailID", employeeFamilyId),
                    new SqlParameter("@CustomerID", customerId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CustomerRegistration", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CustomerRegistration>();
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

        public async Task<int> AddUpdateCustomerRegistration(CustomerRegistration customerRegistration)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CustomerRegistrationID", customerRegistration.CustomerRegistrationID)
                    ,new SqlParameter("@CustomerID", customerRegistration.CustomerID)
                    ,new SqlParameter("@PANNo", customerRegistration.PANNo)
                    ,new SqlParameter("@GSTINNo", customerRegistration.GSTINNo)
                    ,new SqlParameter("@GSTINType", customerRegistration.GSTINType)
                    ,new SqlParameter("@FactoryLicenseNo", customerRegistration.FactoryLicenseNo)
                    ,new SqlParameter("@FactoryRegNo", customerRegistration.FactoryRegNo)
                    ,new SqlParameter("@ARNNo", customerRegistration.ARNNo)
                    ,new SqlParameter("@ECCNo", customerRegistration.ECCNo)
                    ,new SqlParameter("@MSMENo", customerRegistration.MSMENo)
                    ,new SqlParameter("@SSINo", customerRegistration.SSINo)
                    ,new SqlParameter("@TANNo", customerRegistration.TANNo)
                    ,new SqlParameter("@ExportNo", customerRegistration.ExportNo)
                    ,new SqlParameter("@ImportNo", customerRegistration.ImportNo)
                    ,new SqlParameter("@TaxRange", customerRegistration.TaxRange)
                    ,new SqlParameter("@TaxDivisio", customerRegistration.TaxDivisio)
                    ,new SqlParameter("@TaxCommisionerate", customerRegistration.TaxCommisionerate)
                    ,new SqlParameter("@CreatedByORModifiedBy", customerRegistration.CreatedOrModifiedBy)
                };
                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CustomerRegistration", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        #endregion Customer Registration

        #region Customer banking detail
        public async Task<PagedDataTable<CustomerBankDetails>> GetCustomerAllBankAccount(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int customerId = 0)
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
                        ,new SqlParameter("@CustomerID",customerId)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CustomerBankDetails", param))
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
                    PagedDataTable<CustomerBankDetails> lst = table.ToPagedDataTableList<CustomerBankDetails>
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

        public async Task<CustomerBankDetails> GetCustomerBankAccount(int customerBankDetailsId, int customerId)
        {
            CustomerBankDetails result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CustomerBankDetailsID", customerBankDetailsId),
                    new SqlParameter("@CustomerID", customerId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CustomerBankDetails", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CustomerBankDetails>();
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

        public async Task<int> AddUpdateCustomerBankDetails(CustomerBankDetails customerBankDetails)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CustomerBankDetailsID", customerBankDetails.CustomerBankDetailsID)
                    ,new SqlParameter("@CustomerID", customerBankDetails.CustomerID)
                    ,new SqlParameter("@BankName", customerBankDetails.BankName)
                    ,new SqlParameter("@AccountName", customerBankDetails.AccountName)
                    ,new SqlParameter("@IFSCCode", customerBankDetails.IFSCCode)
                    ,new SqlParameter("@AccountNO", customerBankDetails.AccountNO)
                    ,new SqlParameter("@BranchLocation", customerBankDetails.BranchLocation)
                    ,new SqlParameter("@City", customerBankDetails.City)
                    ,new SqlParameter("@BankCode", customerBankDetails.BankCode)
                    ,new SqlParameter("@BICSwiftCode", customerBankDetails.BICSwiftCode)
                    ,new SqlParameter("@CountryID", customerBankDetails.CountryID)
                    ,new SqlParameter("@IsDefaultBankAccount" , customerBankDetails.IsDefaultBankAccount)
                    ,new SqlParameter("@IsActive", customerBankDetails.IsActive)
                    ,new SqlParameter("@CreatedOrModifiedBy", customerBankDetails.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CustomerBankDetails", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }
        #endregion Customer banking detail

        #region Customer document
        public async Task<PagedDataTable<CustomerDocument>> GetCustomersAllDocuments(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int CustomerId = 0)
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
                        ,new SqlParameter("@CustomerID",CustomerId)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_CustomerDocument", param))
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
                    PagedDataTable<CustomerDocument> lst = table.ToPagedDataTableList<CustomerDocument>
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

        public async Task<CustomerDocument> GetCustomerDocument(int CustomerDocumentId, int CustomerId)
        {
            CustomerDocument result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CustomerDocumentID", CustomerDocumentId),
                    new SqlParameter("@CustomerID", CustomerId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CustomerDocument", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CustomerDocument>();
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

        public async Task<int> AddUpdateCustomerDocument(CustomerDocument CustomerDocument)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CustomerDocumentID", CustomerDocument.CustomerDocumentID)
                    ,new SqlParameter("@CustomerID", CustomerDocument.CustomerID)
                    ,new SqlParameter("@DocumentName", CustomerDocument.DocumentName)
                    ,new SqlParameter("@DocumentExtension", CustomerDocument.DocumentExtension)
                    ,new SqlParameter("@DocumentTypeID", CustomerDocument.DocumentTypeID)
                    //,new SqlParameter("@IsDeleted", CustomerDocument.IsDeleted)
                    ,new SqlParameter("@DocumentDescription", CustomerDocument.DocumentDescription)
                    ,new SqlParameter("@DocumentPath", CustomerDocument.DocumentPath)
                    ,new SqlParameter("@IsActive", CustomerDocument.IsActive)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CustomerDocument", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> ActiveInActiveCustomerDocument(CustomerDocument CustomerDocument)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CustomerDocumentID", CustomerDocument.CustomerDocumentID)
                    ,new SqlParameter("@CustomerID", CustomerDocument.CustomerID)
                    ,new SqlParameter("@CreatedOrModifiedBy", CustomerDocument.CreatedOrModifiedBy)
                    ,new SqlParameter("@IsActive", CustomerDocument.IsActive == true ? 1 : 0)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_U_CustomerDocumentIsActive", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }
        #endregion Customer document

        #region Customer Setting
        public async Task<CustomerSetting> GetCustomerSetting(int customerId)
        {
            CustomerSetting result = null;
            try
            {
                SqlParameter[] param = {
                    //new SqlParameter("@EmployeeFamilyDetailID", employeeFamilyId),
                    new SqlParameter("@CustomerID", customerId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_CustomerSetting", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<CustomerSetting>();
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

        public async Task<int> AddUpdateCustomerSetting(CustomerSetting customerSetting)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@CustomerSettingID",customerSetting.CustomerSettingID)
                    ,new SqlParameter("@CustomerID",customerSetting.CustomerID)
                    ,new SqlParameter("@CreditLimit",customerSetting.CreditLimit)
                    ,new SqlParameter("@CommitementLimit",customerSetting.CommitementLimit)
                    ,new SqlParameter("@PaymentTerm",customerSetting.PaymentTerm)
                    ,new SqlParameter("@AllowDiscountPerPO",customerSetting.AllowDiscountPerPO)
                };
                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_CustomerSetting", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }
        #endregion Customer Setting
    }
}
