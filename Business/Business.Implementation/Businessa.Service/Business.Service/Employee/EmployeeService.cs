using Business.Entities.Employee;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Threading.Tasks;
using Business.Interface.IEmployee;
using Business.Entities.Employee.EmployeeMedicalHistory;
using Business.Entities.Employee.EmployeeMedicalInsurance;
using Business.Entities.SalaryPaidHr;

namespace Business.Service
{
    public class EmployeeService : IEmployeeService
    {
        private IConfiguration _config { get; set; }
        private string connection = string.Empty;
        public EmployeeService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }
        public async Task<PagedDataTable<EmployeeMaster>> GetAllEmployeesAsync(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "EmployeeID", string sortBy = "ASC")
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

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeMaster", param))
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
                    PagedDataTable<EmployeeMaster> lst = table.ToPagedDataTableList<EmployeeMaster>
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
        #region Basic detail
        public async Task<int> AddUpdateEmployee(AddUpdateEmployee addUpdateEmployee)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeID",addUpdateEmployee.EmployeeID),
                //new SqlParameter("@EmployeeCode", addUpdateEmployee.EmployeeCode ),
                new SqlParameter("@PrefixName", addUpdateEmployee.PrefixName ),
                new SqlParameter("@FirstName", addUpdateEmployee.FirstName ),
                new SqlParameter("@MiddleName", addUpdateEmployee.MiddleName ),
                new SqlParameter("@LastName", addUpdateEmployee.LastName ),
                new SqlParameter("@GenderID", addUpdateEmployee.GenderID ),
                new SqlParameter("@EmployeeBloodGroupID", addUpdateEmployee.EmployeeBloodGroupID),
                //new SqlParameter("@IsActive", addUpdateEmployee.IsActive ),
                new SqlParameter("@CreatedOrModifiedBy", addUpdateEmployee.CreatedOrModifiedBy ),
                new SqlParameter("@CompanyID", addUpdateEmployee.CompanyID ),
                new SqlParameter("@JobTitle", addUpdateEmployee.JobTitle ),
                new SqlParameter("@DepartmentID", addUpdateEmployee.DepartmentID ),
                new SqlParameter("@DesignationID", addUpdateEmployee.DesignationID ),
                new SqlParameter("@ReportingTo", addUpdateEmployee.ReportingTo),
                new SqlParameter("@PersonalMobileNo", addUpdateEmployee.PersonalMobileNo ),
                new SqlParameter("@OfficeMobileNo", addUpdateEmployee.OfficeMobileNo ),
                new SqlParameter("@AlternativeMobileNo", addUpdateEmployee.AlternativeMobileNo ),
                new SqlParameter("@IsResigned", addUpdateEmployee.IsResigned ),
                new SqlParameter("@Note", addUpdateEmployee.Note ),
                new SqlParameter("@EmailGroupID", addUpdateEmployee.EmailGroupID ),
                new SqlParameter("@PersonalEmail", addUpdateEmployee.PersonalEmail ),
                new SqlParameter("@OfficeEmail", addUpdateEmployee.OfficeEmail ),
                new SqlParameter("@BirthDate", addUpdateEmployee.BirthDate ),
                new SqlParameter("@Religion", addUpdateEmployee.Religion ),
                new SqlParameter("@ReferenceBy", addUpdateEmployee.ReferenceBy ),
                new SqlParameter("@ReferenceContact", addUpdateEmployee.ReferenceContact ),
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> UpdateEmployeeProfilePhoto(EmployeeProfileImage employeeProfileImage)
        {
            try
            {
                SqlParameter[] param = {
                new SqlParameter("@EmployeeID", employeeProfileImage.EmployeeID),
                new SqlParameter("@ImageName", employeeProfileImage.ImageName),
                new SqlParameter("@ImagePath", employeeProfileImage.ImagePath),
                new SqlParameter("@IsActive", employeeProfileImage.IsActive),
                new SqlParameter("@CreatedOrModifiedBy", employeeProfileImage.CreatedOrModifiedBy ),

                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeProfileImage", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<AddUpdateEmployee> GetEmployeeAsync(int employeeId)
        {
            AddUpdateEmployee result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@EmployeeID", employeeId) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeMaster", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<AddUpdateEmployee>();
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
        #endregion Basic detail

        #region Address detail
        public async Task<PagedDataTable<EmployeeAddressTxn>> GetEmployeesAllAddressAsync(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0)
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
                        ,new SqlParameter("@EmployeeID",employeeId)
                        };

                var list = SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeAddressTxn", param);

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeAddressTxn", param))
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
                    PagedDataTable<EmployeeAddressTxn> lst = table.ToPagedDataTableList<EmployeeAddressTxn>
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
        public async Task<int> CreateOrUpdateEmployeeAddressAsync(EmployeeAddressTxn addressMaster)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeAddressTxnID", addressMaster.EmployeeAddressTxnID)
                    ,new SqlParameter("@EmployeeID", addressMaster.EmployeeID)
                    ,new SqlParameter("@Address1", addressMaster.PlotNoName)
                    ,new SqlParameter("@Address2", addressMaster.StreetNoName)
                    //,new SqlParameter("@Address3", addressMaster.Address3)
                    ,new SqlParameter("@Landmark", addressMaster.Landmark)
                    ,new SqlParameter("@Area", addressMaster.Area)
                    ,new SqlParameter("@ZIPCode", addressMaster.ZIPCode)
                    ,new SqlParameter("@IsActive", addressMaster.IsActive)
                    ,new SqlParameter("@City", addressMaster.City)
                    ,new SqlParameter("@District", addressMaster.DistrictName)
                    ,new SqlParameter("@Taluka", addressMaster.Taluka)
                    ,new SqlParameter("@AddressTypeID", addressMaster.AddressTypeID)
                    ,new SqlParameter("@CountryID", addressMaster.CountryID)
                    ,new SqlParameter("@StateID", addressMaster.StateID)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeAddressTxn", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        public async Task<EmployeeAddressTxn> GetEmployeeAddressTxn(int employeeAddressTxnId, int employeeId)
        {
            EmployeeAddressTxn result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeAddressTxnID", employeeAddressTxnId),
                    new SqlParameter("@EmployeeID", employeeId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeAddressTxn", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<EmployeeAddressTxn>();
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

        #region Family Background detail
        public async Task<EmployeeFamilyDetail> GetEmployeeFamily(int employeeId)
        {
            EmployeeFamilyDetail result = null;
            try
            {
                SqlParameter[] param = {
                    //new SqlParameter("@EmployeeFamilyDetailID", employeeFamilyId),
                    new SqlParameter("@EmployeeID", employeeId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeFamilyDetail", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<EmployeeFamilyDetail>();
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

        public async Task<int> CreateOrUpdateEmployeeFamilyBackgroundAsync(EmployeeFamilyDetail employeeFamilyDetail)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeFamilyDetailID",employeeFamilyDetail.EmployeeFamilyDetailID)
                    ,new SqlParameter("@EmployeeID",employeeFamilyDetail.EmployeeID)
                    //,new SqlParameter("@EmployeeBloodGroupID",employeeFamilyDetail.EmployeeBloodGroupID)
                    ,new SqlParameter("@MaritalStatusID",employeeFamilyDetail.MaritalStatusID)
                    ,new SqlParameter("@FatherName",employeeFamilyDetail.FatherName)
                    ,new SqlParameter("@MotherName",employeeFamilyDetail.MotherName)
                    ,new SqlParameter("@WifeName",employeeFamilyDetail.WifeName)
                    ,new SqlParameter("@MotherMobileNumber",employeeFamilyDetail.MotherContact)
                    ,new SqlParameter("@MotherBloodGroupID",employeeFamilyDetail.MotherBloodGroupID)
                    ,new SqlParameter("@FatherMobileNumber",employeeFamilyDetail.FatherContact)
                    ,new SqlParameter("@FatherBloodGroupID",employeeFamilyDetail.FatherBloodGroupID)
                    ,new SqlParameter("@BrotherName",employeeFamilyDetail.BrotherName)
                    ,new SqlParameter("@BrotherMobileNumber",employeeFamilyDetail.BrotherContact)
                    ,new SqlParameter("@BrotherBloodGroupID",employeeFamilyDetail.BrotherBloodGroupID)
                    ,new SqlParameter("@SisterName",employeeFamilyDetail.SisterName)
                    ,new SqlParameter("@SisterMobileNumber",employeeFamilyDetail.SisterContact)
                    ,new SqlParameter("@SisterBloodGroupID",employeeFamilyDetail.SisterBloodGroupID)
                    ,new SqlParameter("@WifeMobileNumber",employeeFamilyDetail.WifeContact)
                    ,new SqlParameter("@WifeBloodGroupID",employeeFamilyDetail.WifeBloodGroupID)
                    ,new SqlParameter("@NoofChild",employeeFamilyDetail.NoofChild)
                    ,new SqlParameter("@NoofBikeScooty",employeeFamilyDetail.NoofBikeScooty)
                    ,new SqlParameter("@NoofCar",employeeFamilyDetail.NoofCar)
                    ,new SqlParameter("@EmergencyMobileNumber",employeeFamilyDetail.EmergencyMobileNumber)
                    ,new SqlParameter("@WhatsAppNo",employeeFamilyDetail.WhatsAppNo)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeFamilyDetail", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }
        #endregion Family Background detail

        #region Employee banking detail
        public async Task<PagedDataTable<EmployeeBankDetails>> GetEmployeesAllBankAccount(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0)
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
                        ,new SqlParameter("@EmployeeID",employeeId)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeBankDetails", param))
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
                    PagedDataTable<EmployeeBankDetails> lst = table.ToPagedDataTableList<EmployeeBankDetails>
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

        public async Task<EmployeeBankDetails> GetEmployeeBankAccount(int employeeBankDetailId, int employeeId)
        {
            EmployeeBankDetails result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeBankDetailsID", employeeBankDetailId),
                    new SqlParameter("@EmployeeID", employeeId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeBankDetails", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<EmployeeBankDetails>();
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

        public async Task<int> CreateOrUpdateEmployeeBankDetail(EmployeeBankDetails employeeBankDetails)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeBankDetailsID", employeeBankDetails.EmployeeBankDetailsID)
                    ,new SqlParameter("@EmployeeID", employeeBankDetails.EmployeeID)
                    ,new SqlParameter("@BankName", employeeBankDetails.BankName)
                    ,new SqlParameter("@EmpNameAsperBank", employeeBankDetails.EmpNameAsperBank)
                    ,new SqlParameter("@IFSCCode", employeeBankDetails.IFSCCode)
                    ,new SqlParameter("@AccountNO", employeeBankDetails.AccountNO)
                    ,new SqlParameter("@BranchLocation", employeeBankDetails.BranchLocation)
                    ,new SqlParameter("@City", employeeBankDetails.City)
                    ,new SqlParameter("@BankCode", employeeBankDetails.BankCode)
                    ,new SqlParameter("@BICSwiftCode", employeeBankDetails.BICSwiftCode)
                    ,new SqlParameter("@CountryID", employeeBankDetails.CountryID)
                    ,new SqlParameter("@IsDefaultBankAccount" , employeeBankDetails.IsDefaultBankAccount)
                    ,new SqlParameter("@IsActive", employeeBankDetails.IsActive)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeBankDetails", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }
        #endregion Employee banking detail

        #region Employee document
        public async Task<PagedDataTable<EmployeeDocument>> GetEmployeesAllDocuments(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0)
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
                        ,new SqlParameter("@EmployeeID",employeeId)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeDocument", param))
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
                    PagedDataTable<EmployeeDocument> lst = table.ToPagedDataTableList<EmployeeDocument>
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

        public async Task<EmployeeDocument> GetEmployeeDocument(int employeeDocumentId, int employeeId)
        {
            EmployeeDocument result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeDocumentID", employeeDocumentId),
                    new SqlParameter("@EmployeeID", employeeId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeDocument", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<EmployeeDocument>();
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

        public async Task<int> CreateOrUpdateEmployeeDocument(EmployeeDocument employeeDocument)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeDocumentID", employeeDocument.EmployeeDocumentID)
                    ,new SqlParameter("@EmployeeID", employeeDocument.EmployeeID)
                    ,new SqlParameter("@DocumentName", employeeDocument.DocumentName)
                    ,new SqlParameter("@DocumentExtension", employeeDocument.DocumentExtension)
                    ,new SqlParameter("@DocumentTypeID", employeeDocument.DocumentTypeID)
                    //,new SqlParameter("@IsDeleted", employeeDocument.IsDeleted)
                    ,new SqlParameter("@Description", employeeDocument.Description)
                    ,new SqlParameter("@DocumentPath", employeeDocument.DocumentPath)
                    ,new SqlParameter("@IsActive", employeeDocument.IsActive)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeDocument", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> ActiveInActiveEmployeeDocument(EmployeeDocument employeeDocument)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeDocumentID", employeeDocument.EmployeeDocumentID)
                    ,new SqlParameter("@EmployeeID", employeeDocument.EmployeeID)
                    ,new SqlParameter("@CreatedOrModifiedBy", employeeDocument.CreatedOrModifiedBy)
                    ,new SqlParameter("@IsActive", employeeDocument.IsActive == true ? 1 : 0)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_U_EmployeeDocumentIsActive", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }
        #endregion Employee document

        #region Employee Education

        public async Task<PagedDataTable<EmployeeEducation>> GetEmployeesAllEducation(int pageNo = 1, int pageSize = 5, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0)
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
                        ,new SqlParameter("@EmployeeID",employeeId)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeEducation", param))
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
                    PagedDataTable<EmployeeEducation> lst = table.ToPagedDataTableList<EmployeeEducation>
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

        public async Task<EmployeeEducation> AddUpdateEmployeeEducation(int employeeEducationId, int employeeId)
        {
            EmployeeEducation result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeEducationID", employeeEducationId),
                    new SqlParameter("@EmployeeID", employeeId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeEducation", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<EmployeeEducation>();
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

        public async Task<int> AddUpdateEmployeeEducation(EmployeeEducation employeeEducation)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeEducationID", employeeEducation.EmployeeEducationID)
                    ,new SqlParameter("@EmployeeID", employeeEducation.EmployeeID)
                    ,new SqlParameter("@SchoolOrUniversity", employeeEducation.SchoolOrUniversity)
                    ,new SqlParameter("@Degree", employeeEducation.Degree)
                    ,new SqlParameter("@FieldOfStudy", employeeEducation.FieldOfStudy)
                    ,new SqlParameter("@StartDate", employeeEducation.StartDate)
                    ,new SqlParameter("@EndDate", employeeEducation.EndDate)
                    ,new SqlParameter("@Grade", employeeEducation.Grade)
                    ,new SqlParameter("@ActivitiesAndSocialities", employeeEducation.ActivitiesAndSocialities)
                    ,new SqlParameter("@Description", employeeEducation.Description)
                    ,new SqlParameter("@IsCurrentEducation", employeeEducation.IsCurrentEducation)
                    ,new SqlParameter("@CreatedOrModifiedBy", employeeEducation.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeEducation", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        #endregion Employee Education

        #region Employee Experience

        public async Task<PagedDataTable<EmployeeExperience>> GetEmployeesAllExperience(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0)
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
                        ,new SqlParameter("@EmployeeID",employeeId)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeExperience", param))
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
                    PagedDataTable<EmployeeExperience> lst = table.ToPagedDataTableList<EmployeeExperience>
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

        public async Task<EmployeeExperience> AddUpdateEmployeeExperience(int employeeExperienceId, int employeeId)
        {
            EmployeeExperience result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeExperienceID", employeeExperienceId),
                    new SqlParameter("@EmployeeID", employeeId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeExperience", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<EmployeeExperience>();
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

        public async Task<int> AddUpdateEmployeeExperience(EmployeeExperience employeeExperience)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeExperienceID", employeeExperience.EmployeeExperienceID)
                    ,new SqlParameter("@EmployeeID", employeeExperience.EmployeeID)
                    ,new SqlParameter("@JobTitle", employeeExperience.JobTitle)
                    ,new SqlParameter("@EmploymentTypeID", employeeExperience.EmploymentTypeID)
                    ,new SqlParameter("@CompanyName", employeeExperience.CompanyName)
                    ,new SqlParameter("@Location", employeeExperience.Location)
                    ,new SqlParameter("@LocationTypeID", employeeExperience.LocationTypeID)
                    ,new SqlParameter("@IsCurrentlyWorking", employeeExperience.IsCurrentlyWorking)
                    ,new SqlParameter("@StartMonth", employeeExperience.StartMonth)
                    ,new SqlParameter("@StartYear", employeeExperience.StartYear)
                    ,new SqlParameter("@EndMonth", employeeExperience.EndMonth)
                    ,new SqlParameter("@EndYear", employeeExperience.EndYear)
                    ,new SqlParameter("@IndustryTypeID", employeeExperience.IndustryTypeID)
                    ,new SqlParameter("@CompanyDescription", employeeExperience.CompanyDescription)
                    ,new SqlParameter("@ProfileHeadLine", employeeExperience.ProfileHeadLine)
                    ,new SqlParameter("@Skills", employeeExperience.Skills)
                    ,new SqlParameter("@CreatedOrModifiedBy", employeeExperience.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeExperience", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        #endregion Employee Experience

        #region Employee HR Administration

        public async Task<EmployeeHRAdministration> GetEmployeeHRAdministration(int employeeId)
        {
            EmployeeHRAdministration result = null;
            try
            {
                SqlParameter[] param = {
                    //new SqlParameter("@EmployeeFamilyDetailID", employeeFamilyId),
                    new SqlParameter("@EmployeeID", employeeId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeHRAdministration", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<EmployeeHRAdministration>();
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

        public PagedDataTable<EmploymentStatusMaster> GetAllEmploymentStatus()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<EmploymentStatusMaster> lst = new PagedDataTable<EmploymentStatusMaster>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_EmploymentStatusMaster"))

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

                    lst = table.ToPagedDataTableList<EmploymentStatusMaster>
                       (1, 20, totalItemCount, null, "EmploymentStatusText", "ASC");

                    lst = table.ToPagedDataTableList<EmploymentStatusMaster>();

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
            return lst;
        }

        public async Task<int> AddUpdateEmployeeHRAdministration(EmployeeHRAdministration employeeHRAdministration)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeAdminID", employeeHRAdministration.EmployeeAdminID)
                    ,new SqlParameter("@EmployeeID", employeeHRAdministration.EmployeeID)
                    ,new SqlParameter("@CompanyID", employeeHRAdministration.CompanyID)
                    ,new SqlParameter("@EmployeeCategoryID", employeeHRAdministration.EmployeeCategoryID)
                    ,new SqlParameter("@JoiningDate", employeeHRAdministration.JoiningDate)
                    ,new SqlParameter("@EmploymentStatusID", employeeHRAdministration.EmploymentStatusID)
                    ,new SqlParameter("@ConfirmationDate", employeeHRAdministration.ConfirmationDate)
                    ,new SqlParameter("@EmploymentTypeID", employeeHRAdministration.EmploymentTypeID)
                    ,new SqlParameter("@YearlyCTC", employeeHRAdministration.YearlyCTC)
                    ,new SqlParameter("@MonthlyCTC", employeeHRAdministration.MonthlyCTC)
                    ,new SqlParameter("@ContractorID", employeeHRAdministration.ContractorID)
                    ,new SqlParameter("@CreatedOrModifiedBy", employeeHRAdministration.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeHRAdministration", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        #endregion Employee HR Administration

        #region Employee Salary Breakup

        public async Task<EmployeeSalaryBreakup> GetEmployeeSalaryBreakup(int employeeId)
        {
            EmployeeSalaryBreakup result = null;
            try
            {
                SqlParameter[] param = {
                    //new SqlParameter("@EmployeeFamilyDetailID", employeeFamilyId),
                    new SqlParameter("@EmployeeID", employeeId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeSalaryBreakup", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<EmployeeSalaryBreakup>();
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

        public async Task<int> AddUpdateEmployeeSalaryBreakup(EmployeeSalaryBreakup employeeSalaryBreakup)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeSalaryBreakupID", employeeSalaryBreakup.EmployeeSalaryBreakupID)
                    ,new SqlParameter("@EmployeeID", employeeSalaryBreakup.EmployeeID)
                    ,new SqlParameter("@Basic", employeeSalaryBreakup.Basic)
                    ,new SqlParameter("@DearnessAllowence", employeeSalaryBreakup.DearnessAllowence)
                    ,new SqlParameter("@HouseRentAllowence", employeeSalaryBreakup.HouseRentAllowence)
                    ,new SqlParameter("@OtherAllowence", employeeSalaryBreakup.OtherAllowence)
                    ,new SqlParameter("@AllCashReembersment", employeeSalaryBreakup.AllCashReembersment)
                    ,new SqlParameter("@LTA", employeeSalaryBreakup.LTA)
                    ,new SqlParameter("@Medical", employeeSalaryBreakup.Medical)
                    ,new SqlParameter("@Arrears", employeeSalaryBreakup.Arrears)
                    ,new SqlParameter("@ProvidentFund", employeeSalaryBreakup.ProvidentFund)
                    ,new SqlParameter("@EmployeeStateInsurance", employeeSalaryBreakup.EmployeeStateInsurance)
                    ,new SqlParameter("@IncomeTax", employeeSalaryBreakup.IncomeTax)
                    ,new SqlParameter("@ProfessionalTax", employeeSalaryBreakup.ProfessionalTax)
                    ,new SqlParameter("@LoanAndAdvance", employeeSalaryBreakup.LoanAndAdvance)
                    ,new SqlParameter("@Prerequisites", employeeSalaryBreakup.Prerequisites)
                    ,new SqlParameter("@GrossEarnings", employeeSalaryBreakup.GrossEarnings)
                    ,new SqlParameter("@GrossDeduction", employeeSalaryBreakup.GrossDeduction)
                    ,new SqlParameter("@NetSalaryPayable", employeeSalaryBreakup.NetSalaryPayable)
                    ,new SqlParameter("@CostToCompany", employeeSalaryBreakup.CostToCompany)
                    ,new SqlParameter("@SalaryCalculateWithFormula", employeeSalaryBreakup.SalaryCalculateWithFormula)
                    ,new SqlParameter("@CreatedOrModifiedBy", employeeSalaryBreakup.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeSalaryBreakup", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        #endregion Employee Salary Breakup

        #region Employee Identity

        public async Task<PagedDataTable<EmployeeIdentity>> GetEmployeeAllIdentity(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0)
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
                        ,new SqlParameter("@EmployeeID",employeeId)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeIdentity", param))
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
                    PagedDataTable<EmployeeIdentity> lst = table.ToPagedDataTableList<EmployeeIdentity>
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


        public async Task<EmployeeIdentity> AddUpdateEmployeeIdentity(int employeeIdentityId, int employeeId)
        {
            EmployeeIdentity result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeIdentityID", employeeIdentityId),
                    new SqlParameter("@EmployeeID", employeeId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeIdentity", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<EmployeeIdentity>();
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

        public async Task<int> AddUpdateEmployeeIdentity(EmployeeIdentity employeeIdentity)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeIdentityID", employeeIdentity.EmployeeIdentityID)
                    ,new SqlParameter("@EmployeeID", employeeIdentity.EmployeeID)
                    ,new SqlParameter("@IdentityProofTypeID", employeeIdentity.IdentityProofTypeID)
                    ,new SqlParameter("@IdentityProofCode", employeeIdentity.IdentityProofCode)
                    ,new SqlParameter("@IsActive", employeeIdentity.IsActive)
                    ,new SqlParameter("@CreatedOrModifiedBy", employeeIdentity.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeIdentity", param);


                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        #endregion Employee Identity

        #region Employee Medical History
        //MedicalHistory
        public async Task<PagedDataTable<EmployeeMedicalHistory>> GetEmployeeAllMedicalHistory(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0)
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
                        ,new SqlParameter("@EmployeeID",employeeId)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeMedicalDetails", param))
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
                    PagedDataTable<EmployeeMedicalHistory> lst = table.ToPagedDataTableList<EmployeeMedicalHistory>
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

        public async Task<EmployeeMedicalHistory> AddUpdateEmployeeMedicalHistory(int employeeMedicalDetailsID, int employeeId)
        {
            EmployeeMedicalHistory result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeMedicalDetailsID", employeeMedicalDetailsID),
                    new SqlParameter("@EmployeeID", employeeId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeMedicalDetails", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<EmployeeMedicalHistory>();
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

        public async Task<int> AddUpdateEmployeeMedicalHistory(EmployeeMedicalHistory employeeMedicalHistory)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeMedicalDetailsID", employeeMedicalHistory.EmployeeMedicalDetailsID)
                    ,new SqlParameter("@EmployeeID", employeeMedicalHistory.EmployeeID)
                    ,new SqlParameter("@HospitalName", employeeMedicalHistory.HospitalName)
                    ,new SqlParameter("@DoctorName", employeeMedicalHistory.DoctorName)
                    ,new SqlParameter("@MedicalReason", employeeMedicalHistory.MedicalReason)
                    ,new SqlParameter("@TreatmentMonth", employeeMedicalHistory.TreatmentMonth)
                    ,new SqlParameter("@TreatmentYear", employeeMedicalHistory.TreatmentYear)
                    ,new SqlParameter("@FacingAnyProblem", employeeMedicalHistory.FacingAnyProblem)
                    ,new SqlParameter("@TakingTreatmentNow", employeeMedicalHistory.TakingTreatmentNow)
                    ,new SqlParameter("@TakingMedicinesNow", employeeMedicalHistory.TakingMedicinesNow)
                    ,new SqlParameter("@PlaceofTreatment", employeeMedicalHistory.PlaceofTreatment)
                    //,new SqlParameter("@IsActive", employeeMedicalHistory.IsActive)
                    ,new SqlParameter("@CreatedOrModifiedBy", employeeMedicalHistory.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeMedicalDetails", param);


                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        #endregion Employee Medical History

        #region Employee Medical Insurance
        //MedicalInsurance
        public async Task<PagedDataTable<EmployeeMedicalInsurance>> GetEmployeeAllMedicalInsurance(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "", string sortBy = "ASC", int employeeId = 0)
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
                        ,new SqlParameter("@EmployeeID",employeeId)
                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeMedicalInsurance", param))
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
                    PagedDataTable<EmployeeMedicalInsurance> lst = table.ToPagedDataTableList<EmployeeMedicalInsurance>
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

        public async Task<EmployeeMedicalInsurance> AddUpdateEmployeeMedicalInsurance(int employeeMedicalInsuranceId, int employeeId)
        {
            EmployeeMedicalInsurance result = null;
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeMedicalInsuranceID", employeeMedicalInsuranceId),
                    new SqlParameter("@EmployeeID", employeeId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_EmployeeMedicalInsurance", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<EmployeeMedicalInsurance>();
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

        public async Task<int> AddUpdateEmployeeMedicalInsurance(EmployeeMedicalInsurance employeeMedicalInsurance)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@EmployeeMedicalInsuranceID", employeeMedicalInsurance.EmployeeMedicalInsuranceID)
                    ,new SqlParameter("@EmployeeID", employeeMedicalInsurance.EmployeeID)
                    ,new SqlParameter("@InsuranceCompany", employeeMedicalInsurance.InsuranceCompany)
                    ,new SqlParameter("@PolicyName", employeeMedicalInsurance.PolicyName)
                    ,new SqlParameter("@PolicyNumber", employeeMedicalInsurance.PolicyNumber)
                    ,new SqlParameter("@PolicyStartDate", employeeMedicalInsurance.PolicyStartDate)
                    ,new SqlParameter("@PolicyExpiryDate", employeeMedicalInsurance.PolicyExpiryDate)
                    ,new SqlParameter("@PolicyPremiumAmt", employeeMedicalInsurance.PolicyPremiumAmt)
                    ,new SqlParameter("@AgentName", employeeMedicalInsurance.AgentName)
                    //,new SqlParameter("@IsActive", employeeMedicalInsurance.IsActive)
                    ,new SqlParameter("@CreatedOrModifiedBy", employeeMedicalInsurance.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeMedicalInsurance", param);


                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        #endregion Employee Medical Insurance

        #region Employee Present

        public async Task<int> AddUpdateEmployeePresent(EmployeePresent employeePresent, DataTable dataTable)
        {
            try
            {

                SqlParameter[] param = {
                    new SqlParameter("@EmployeeTimeSheetMasterID", employeePresent.EmployeeTimeSheetMasterID),
                    new SqlParameter("@CompanyID", employeePresent.CompanyID),
                    new SqlParameter("@PresenceDate", employeePresent.PresenceDateTime),
                    new SqlParameter("@CreatedOrModifyBy",employeePresent.CreatedOrModifiedBy),
                    new SqlParameter("@EmployeeTimeSheetdtl",dataTable)
                };
                param[4].Direction = ParameterDirection.Input;
                param[4].SqlDbType = SqlDbType.Structured;
                param[4].ParameterName = "@EmployeeTimeSheetdtl";
                param[4].TypeName = "UDTT_EmployeeTimeSheetDetail";
                param[4].Value = dataTable;

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_EmployeeTimeSheet", param);
                return obj != null ? Convert.ToInt32(obj) : 0;
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Employee Present

        #region Employee Salary Paid Hours
        public async Task<SalaryPaidHr> GetSalaryPaidHr(int employeeId)
        {
            SalaryPaidHr result = null;
            try
            {
                SqlParameter[] param = {
                    //new SqlParameter("@SalaryPaidHrID", SalaryPaidHrId),
                    new SqlParameter("@EmployeeID", employeeId)
                };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_Get_SalaryPaidHr", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<SalaryPaidHr>();
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

        public async Task<int> AddUpdateSalaryPaidHr(SalaryPaidHr salaryPaidHr)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@SalaryPaidHrID", salaryPaidHr.SalaryPaidHrID)
                    ,new SqlParameter("@EmployeeID", salaryPaidHr.EmployeeID)
                    ,new SqlParameter("@ActualHours", salaryPaidHr.ActualHours)
                    ,new SqlParameter("@AdjustmentHour", salaryPaidHr.AdjustmentHour)
                    ,new SqlParameter("@AttendanceShownWithActualHours", salaryPaidHr.UserID)
                    ,new SqlParameter("@CreatedOrModifiedBy", salaryPaidHr.CreatedOrModifiedBy)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(connection, CommandType.StoredProcedure, "Usp_IU_SalaryPaidHr", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }

        #endregion Employee Salary Paid Hours
    }
}
