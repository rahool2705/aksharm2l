using Business.Entities;
using Business.Entities.ContactChannelType;
using Business.Entities.Contractor;
using Business.Entities.Department;
using Business.Entities.Designation;
using Business.Entities.Employee;
using Business.Entities.Gender;
using Business.Entities.ItemCategory;
using Business.Entities.Master;
using Business.Entities.Master.EmployeeCategory;
using Business.Entities.Master.EmployementType;
using Business.Entities.Master.FormType;
using Business.Entities.Master.InquiryType;
using Business.Entities.Master.MarketingCompanyFinancialYearMaster;
using Business.Entities.Master.MeetingStatus;
using Business.Entities.Master.Package;
using Business.Entities.SalaryFormula;
using Business.Entities.SecurityOfficer;
using Business.Entities.UOMID;
using Business.Entities.User;
using Business.Entities.VanueType;
using Business.Interface;
using Business.SQL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Business.Service
{
    public class MasterService : IMasterService
    {
        private IConfiguration _config { get; set; }

        private string connection = string.Empty;
        public MasterService(IConfiguration config)
        {
            _config = config;
            connection = _config.GetConnectionString("DefaultConnection");
        }

        public PagedDataTable<IdentityProofTypeMetaData> GetIdentityProofTypeAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<IdentityProofTypeMetaData> lst = new PagedDataTable<IdentityProofTypeMetaData>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_IdentityProofTypeMasterData"))
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
                    lst = table.ToPagedDataTableList<IdentityProofTypeMetaData>
                       (1, 20, totalItemCount, null, "IdentityProofTypeText", "ASC");
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

        public PagedDataTable<VehicleTypeMasterMetaData> GetVehicleTypeAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<VehicleTypeMasterMetaData> lst = new PagedDataTable<VehicleTypeMasterMetaData>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_VehicleTypeMasterData"))
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
                    lst = table.ToPagedDataTableList<VehicleTypeMasterMetaData>
                       (1, 20, totalItemCount, null, "VehicleTypeText", "ASC");
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

        public PagedDataTable<ZipCodeMaster> GetZipCodeAsync(string search)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<ZipCodeMaster> lst = new PagedDataTable<ZipCodeMaster>();
            try
            {
                SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@SearchString", search) };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_ZipCodeMasterForVisitorAddress", sp))
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
                    lst = table.ToPagedDataTableList<ZipCodeMaster>
                       (1, 20, totalItemCount, null, "ZipCode", "ASC");
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

        public PagedDataTable<FeedbackQuestionMasterMetaData> GetFeedbackQuestions()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<FeedbackQuestionMasterMetaData> lst = new PagedDataTable<FeedbackQuestionMasterMetaData>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_FeedbackQuestionMaster"))
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
                    lst = table.ToPagedDataTableList<FeedbackQuestionMasterMetaData>
                       (1, 20, totalItemCount, null, "FeedbackQuestionID", "ASC");
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

        public PagedDataTable<IndustryTypeMaster> GetIndustryTypeMasterAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<IndustryTypeMaster> lst = new PagedDataTable<IndustryTypeMaster>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_IndustryTypeMaster"))
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
                    lst = table.ToPagedDataTableList<IndustryTypeMaster>
                       (1, 20, totalItemCount, null, "IndustryTypeText", "ASC");
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

        public PagedDataTable<BusinessTypeMaster> GetBusinessTypeMasterAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<BusinessTypeMaster> lst = new PagedDataTable<BusinessTypeMaster>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_BusinessTypeMaster"))
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
                    lst = table.ToPagedDataTableList<BusinessTypeMaster>
                       (1, 20, totalItemCount, null, "BusinessTypeText", "ASC");
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

        public PagedDataTable<UserRoleMaster> GetUserRoleMasterAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<UserRoleMaster> lst = new PagedDataTable<UserRoleMaster>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_RoleMaster"))
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
                    lst = table.ToPagedDataTableList<UserRoleMaster>
                       (1, 20, totalItemCount, null, "RoleName", "ASC");
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

        public PagedDataTable<DepartmentGroupMaster> GetDepartmentGroupsMasterAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<DepartmentGroupMaster> lst = new PagedDataTable<DepartmentGroupMaster>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_DepartmentGroupMaster"))
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
                    lst = table.ToPagedDataTableList<DepartmentGroupMaster>
                       (1, 20, totalItemCount, null, "DepartmentGroupText", "ASC");
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

        public PagedDataTable<DesignationGroupMaster> GetDesignationGroupMasterAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<DesignationGroupMaster> lst = new PagedDataTable<DesignationGroupMaster>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_DesignationGroupMaster"))
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
                    lst = table.ToPagedDataTableList<DesignationGroupMaster>
                       (1, 20, totalItemCount, null, "DesignationGroupText", "ASC");
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

        #region

        public async Task<PagedDataTable<StateMasterMetadata>> GetAllStateAsync(string search)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<StateMasterMetadata> lst = null;
            try
            {
                
                     SqlParameter[] sp = new SqlParameter[] { new SqlParameter("@StateName", search) };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Test_Usp_GetAll_StateMaster", sp))
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
                    lst = table.ToPagedDataTableList<StateMasterMetadata>(0, 0, totalItemCount);
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


        #region Multiple record select
        public PagedDataTable<DepartmentMaster> GetAllDepartments()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<DepartmentMaster> lst = new PagedDataTable<DepartmentMaster>();
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",1)
                        ,new SqlParameter("@PageSize","0")
                        ,new SqlParameter("@SearchString","")
                        ,new SqlParameter("@OrderBy","")
                        ,new SqlParameter("@SortBy","")
                        };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_DepartmentMaster", param))
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

                    lst = table.ToPagedDataTableList<DepartmentMaster>
                       (1, 20, totalItemCount, null, "DepartmentID", "ASC");

                    lst = table.ToPagedDataTableList<DepartmentMaster>();

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

        public PagedDataTable<DesignationMaster> GetAllDesignations()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<DesignationMaster> lst = new PagedDataTable<DesignationMaster>();
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@PageNo", 1),
                    new SqlParameter("@PageSize", "0"),
                };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_DesignationMaster", param))
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
                    lst = table.ToPagedDataTableList<DesignationMaster>();
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

        public PagedDataTable<PartyTypeMaster> GetPartyTypeMasterAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<PartyTypeMaster> lst = new PagedDataTable<PartyTypeMaster>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_PartyTypeMaster"))

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

                    lst = table.ToPagedDataTableList<PartyTypeMaster>
                       (1, 20, totalItemCount, null, "PartyTypeText", "ASC");

                    lst = table.ToPagedDataTableList<PartyTypeMaster>();

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

        public PagedDataTable<EmployeeMaster> GetAllEmployees()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<EmployeeMaster> lst = new PagedDataTable<EmployeeMaster>();
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",1)
                        ,new SqlParameter("@PageSize","0")
                        ,new SqlParameter("@SearchString","")
                        ,new SqlParameter("@OrderBy","")
                        ,new SqlParameter("@SortBy","")
                        };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeMaster", param))
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
                    lst = table.ToPagedDataTableList<EmployeeMaster>();
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
        // Duplicate by Rahul Mistry on 07-Feb-2023 Start.
        public PagedDataTable<EmployeeMaster> GetEmployeesByName(string employeeName)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<EmployeeMaster> lst = new PagedDataTable<EmployeeMaster>();
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",1)
                        ,new SqlParameter("@PageSize","0")
                        ,new SqlParameter("@SearchString",employeeName)
                        ,new SqlParameter("@OrderBy","")
                        ,new SqlParameter("@SortBy","")
                        };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeMaster", param))
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
                    lst = table.ToPagedDataTableList<EmployeeMaster>();
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

        // Duplicate by Rahul Mistry on 07-Feb-2023 End.



        public PagedDataTable<GenderMaster> GetAllGenders()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<GenderMaster> lst = new PagedDataTable<GenderMaster>();
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@PageNo", 0),
                    new SqlParameter("@PageSize", "5"),
                };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_GenderMaster", param))
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
                    lst = table.ToPagedDataTableList<GenderMaster>();
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

        public PagedDataTable<EmailGroupMaster> GetAllEmailGroupMaster()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<EmailGroupMaster> lst = new PagedDataTable<EmailGroupMaster>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_EmailGroupMaster"))
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
                    lst = table.ToPagedDataTableList<EmailGroupMaster>();
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

        public PagedDataTable<SecurityOfficerMaster> GetAllSecurityOfficers()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<SecurityOfficerMaster> lst = new PagedDataTable<SecurityOfficerMaster>();
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",1)
                        ,new SqlParameter("@PageSize","0")
                        ,new SqlParameter("@SearchString","")
                        ,new SqlParameter("@OrderBy","")
                        ,new SqlParameter("@SortBy","")
                        };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_SecurityOfficer", param))
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
                    lst = table.ToPagedDataTableList<SecurityOfficerMaster>();
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

        public PagedDataTable<UOMIDMetadata> GetAllUOMID()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<UOMIDMetadata> lst = new PagedDataTable<UOMIDMetadata>();
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",1)
                        ,new SqlParameter("@PageSize","0")
                        ,new SqlParameter("@SearchString","")
                        ,new SqlParameter("@OrderBy","")
                        ,new SqlParameter("@SortBy","")
                        };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_UOMID", param))
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

                    lst = table.ToPagedDataTableList<UOMIDMetadata>
                       (1, 20, totalItemCount, null, "UOMID", "ASC");

                    lst = table.ToPagedDataTableList<UOMIDMetadata>();

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

        PagedDataTable<ItemCategory> IMasterService.GetAllItemCategory()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<ItemCategory> lst = new PagedDataTable<ItemCategory>();
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",1)
                        ,new SqlParameter("@PageSize","0")
                        ,new SqlParameter("@SearchString","")
                        ,new SqlParameter("@OrderBy","")
                        ,new SqlParameter("@SortBy","")
                        };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_ItemCategory", param))
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

                    lst = table.ToPagedDataTableList<ItemCategory>
                       (1, 20, totalItemCount, null, "UOMID", "ASC");

                    lst = table.ToPagedDataTableList<ItemCategory>();

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

        /*02-11-2022*/

        public PagedDataTable<ContactChannelTypeMaster> GetContactChannelMasterAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<ContactChannelTypeMaster> lst = new PagedDataTable<ContactChannelTypeMaster>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_ContactChannelTypeMaster"))

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

                    lst = table.ToPagedDataTableList<ContactChannelTypeMaster>
                       (1, 20, totalItemCount, null, "ContactChannelTypeText", "ASC");

                    lst = table.ToPagedDataTableList<ContactChannelTypeMaster>();

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

        public PagedDataTable<VanueTypeMaster> GetVanueTypeMasterAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<VanueTypeMaster> lst = new PagedDataTable<VanueTypeMaster>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_VanueTypeMaster"))

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

                    lst = table.ToPagedDataTableList<VanueTypeMaster>
                       (1, 20, totalItemCount, null, "VanueTypeText", "ASC");

                    lst = table.ToPagedDataTableList<VanueTypeMaster>();

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

        /*02-11-2022*/

        public List<EmployeeFamilyDetail> GetMaritalStatusMaster()
        {
            try
            {
                return QueryHelper.GetList<EmployeeFamilyDetail>(connection, "Usp_GetAll_MaritalStatusMaster", null);
            }
            catch
            {
                throw;
            }
        }

        /*02-11-2022*/

        // 11-Nov-2022
        public async Task<PagedDataTable<DocumentTypeMaster>> GetAllDocumentTypeAsync(int pageNo = 0, int pageSize = 0, string searchString = "", string orderBy = "DocumentTypeText", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<DocumentTypeMaster> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_DocumentTypeMaster"))
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
                    lst = table.ToPagedDataTableList<DocumentTypeMaster>(pageNo, pageSize, totalItemCount);
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
        // 11-Nov-2022

        // 16-nov-2022
        public async Task<PagedDataTable<BloodGroupMaster>> GetAllBloodGroupMaster(int pageNo = 0, int pageSize = 0, string searchString = "", string orderBy = "BloodGroupText", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<BloodGroupMaster> lst = null;
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",pageNo)
                        ,new SqlParameter("@PageSize",pageSize)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@OrderBy",orderBy)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)

                        };
                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(connection, CommandType.StoredProcedure, "Usp_GetAll_BloodGroupMaster"))
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
                    lst = table.ToPagedDataTableList<BloodGroupMaster>(pageNo, pageSize, totalItemCount);
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
        // 16-nov-2022

        /*11-11-2022*/
        public PagedDataTable<StatusTypeMaster> GetAllMeetingStatusTypeMasterAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<StatusTypeMaster> lst = new PagedDataTable<StatusTypeMaster>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_MeetingStatusTypeMaster"))

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

                    lst = table.ToPagedDataTableList<StatusTypeMaster>
                       (1, 20, totalItemCount, null, "MeetingStatusTypeText", "ASC");

                    lst = table.ToPagedDataTableList<StatusTypeMaster>();

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

        public PagedDataTable<DurationTypeMaster> GetAllMeetingDurationTypeMasterAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<DurationTypeMaster> lst = new PagedDataTable<DurationTypeMaster>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_MeetingDurationTypeMaster"))

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

                    lst = table.ToPagedDataTableList<DurationTypeMaster>
                       (1, 20, totalItemCount, null, "MeetingDurationTypeText", "ASC");

                    lst = table.ToPagedDataTableList<DurationTypeMaster>();

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
        /*11-11-2022*/

        /*30-11-2022*/
        public PagedDataTable<FinancialYearMaster> GetAllFinancialYearMasterAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<FinancialYearMaster> lst = new PagedDataTable<FinancialYearMaster>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_FinancialYearMasterForDropdown"))

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

                    lst = table.ToPagedDataTableList<FinancialYearMaster>
                       (1, 20, totalItemCount, null, "FinancialYear", "ASC");

                    lst = table.ToPagedDataTableList<FinancialYearMaster>();

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
        /*30-11-2022*/

        /*01-12-2022*/
        public PagedDataTable<CompanyMasterMetadata> GetAllCompanyMasterAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<CompanyMasterMetadata> lst = new PagedDataTable<CompanyMasterMetadata>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_CompanyMaster"))

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

                    lst = table.ToPagedDataTableList<CompanyMasterMetadata>
                       (1, 20, totalItemCount, null, "CompanyName", "ASC");

                    lst = table.ToPagedDataTableList<CompanyMasterMetadata>();

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

        public PagedDataTable<CustomerMasterMetadata> GetAllCustomerMasterAsync()
        {
            DataTable table = new DataTable();
            //int totalItemCount = 0;
            PagedDataTable<CustomerMasterMetadata> lst = new PagedDataTable<CustomerMasterMetadata>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_CustomerMasterForDropdown"))

                {
                    if (ds.Tables.Count > 0)
                    {
                        table = ds.Tables[0];
                        //    if (table.Rows.Count > 0)
                        //    {
                        //        if (table.ContainColumn("TotalCount"))
                        //            totalItemCount = Convert.ToInt32(table.Rows[0]["TotalCount"]);
                        //        else
                        //            totalItemCount = table.Rows.Count;
                        //    }
                        //}

                        //lst = table.ToPagedDataTableList<CustomerMasterMetadata>
                        //   (1, 20, totalItemCount, null, "FinancialYear", "ASC");

                        lst = table.ToPagedDataTableList<CustomerMasterMetadata>();
                    }
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

        /*01-12-2022*/

        /*07-12-2022*/
        public PagedDataTable<FormTypeMaster> GetAllFormTypeMasterAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<FormTypeMaster> lst = new PagedDataTable<FormTypeMaster>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_FormTypeMaster"))

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

                    lst = table.ToPagedDataTableList<FormTypeMaster>
                       (1, 20, totalItemCount, null, "FormTypeText", "ASC");

                    lst = table.ToPagedDataTableList<FormTypeMaster>();

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
        public PagedDataTable<PackageMaster> GetAllPackageMasterAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<PackageMaster> lst = new PagedDataTable<PackageMaster>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_PackageMaster"))

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

                    lst = table.ToPagedDataTableList<PackageMaster>
                       (1, 20, totalItemCount, null, "PackageName", "ASC");

                    lst = table.ToPagedDataTableList<PackageMaster>();

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
        /*07-12-2022*/

        /*08-12-2022*/
        public PagedDataTable<PackageMaster> GetAllPackageTypeMasterAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<PackageMaster> lst = new PagedDataTable<PackageMaster>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_PackageTypeMaster"))

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

                    lst = table.ToPagedDataTableList<PackageMaster>
                       (1, 20, totalItemCount, null, "PackageName", "ASC");

                    lst = table.ToPagedDataTableList<PackageMaster>();

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
        /*08-12-2022*/

        /*20-12-2022*/

        public PagedDataTable<IndustryTypeMaster> GetAllIndustryTypeMaster()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<IndustryTypeMaster> lst = new PagedDataTable<IndustryTypeMaster>();
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",1)
                        ,new SqlParameter("@PageSize","0")
                        ,new SqlParameter("@SearchString","")
                        ,new SqlParameter("@OrderBy","")
                        ,new SqlParameter("@SortBy","")
                        };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_IndustryTypeMaster", param))
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

                    lst = table.ToPagedDataTableList<IndustryTypeMaster>
                       (1, 20, totalItemCount, null, "DepartmentID", "ASC");

                    lst = table.ToPagedDataTableList<IndustryTypeMaster>();

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

        public PagedDataTable<BusinessTypeMaster> GetAllBusinessTypeMaster()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<BusinessTypeMaster> lst = new PagedDataTable<BusinessTypeMaster>();
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",1)
                        ,new SqlParameter("@PageSize","0")
                        ,new SqlParameter("@SearchString","")
                        ,new SqlParameter("@OrderBy","")
                        ,new SqlParameter("@SortBy","")
                        };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_BusinessTypeMaster", param))
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

                    lst = table.ToPagedDataTableList<BusinessTypeMaster>
                       (1, 20, totalItemCount, null, "DepartmentID", "ASC");

                    lst = table.ToPagedDataTableList<BusinessTypeMaster>();

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

        /*20-12-2022*/

        /*30-12-2022*/
        public PagedDataTable<InquiryTypeMaster> GetAllInquiryTypeMasterAsync()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<InquiryTypeMaster> lst = new PagedDataTable<InquiryTypeMaster>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_InquiryTypeMaster"))

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

                    lst = table.ToPagedDataTableList<InquiryTypeMaster>
                       (1, 20, totalItemCount, null, "FinancialYear", "ASC");

                    lst = table.ToPagedDataTableList<InquiryTypeMaster>();

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
        /*30-12-2022*/

        /* Added by Rahul Mistry On 03-Jan-2023 --  START*/
        /*public PagedDataTable<EmploymentTypeMaster> GetAllEmploymentTypeMaster()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<EmploymentTypeMaster> lst = new PagedDataTable<EmploymentTypeMaster>();
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",1)
                        ,new SqlParameter("@PageSize","0")
                        ,new SqlParameter("@SearchString","")
                        ,new SqlParameter("@OrderBy","")
                        ,new SqlParameter("@SortBy","")
                        };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployementTypeMaster", param))
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

                    lst = table.ToPagedDataTableList<EmploymentTypeMaster>
                       (1, 20, totalItemCount, null, "EmploymentTypeID", "ASC");

                    lst = table.ToPagedDataTableList<EmploymentTypeMaster>();

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
        }*/



        public PagedDataTable<EmploymentLocationTypeMaster> GetAllEmploymentLocationTypeMaster()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<EmploymentLocationTypeMaster> lst = new PagedDataTable<EmploymentLocationTypeMaster>();
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",1)
                        ,new SqlParameter("@PageSize","0")
                        ,new SqlParameter("@SearchString","")
                        ,new SqlParameter("@OrderBy","")
                        ,new SqlParameter("@SortBy","")
                        };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_EmploymentLocationTypeMaster", param))
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

                    lst = table.ToPagedDataTableList<EmploymentLocationTypeMaster>
                       (1, 20, totalItemCount, null, "EmploymentLocationTypeID", "ASC");

                    lst = table.ToPagedDataTableList<EmploymentLocationTypeMaster>();

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
        /* Added by Rahul Mistry On 03-Jan-2023 --  END*/

        /*Added by Dravesh Lokhande on 03-02-2023 -- Start */

        public PagedDataTable<EmploymentType> GetAllIdentityTypeMaster()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<EmploymentType> lst = new PagedDataTable<EmploymentType>();
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",1)
                        ,new SqlParameter("@PageSize","0")
                        ,new SqlParameter("@SearchString","")
                        ,new SqlParameter("@OrderBy","")
                        ,new SqlParameter("@SortBy","")
                        };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployementTypeMaster", param))
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

                    lst = table.ToPagedDataTableList<EmploymentType>
                       (1, 20, totalItemCount, null, "EmploymentTypeID", "ASC");

                    lst = table.ToPagedDataTableList<EmploymentType>();

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

        /* Added by Dravesh Lokhande on 03-02-2023 -- End */

        /*Added by Dravesh Lokhande on 07-02-2023 -- End */
        public PagedDataTable<EmploymentType> GetAllEmploymentTypeMaster()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<EmploymentType> lst = new PagedDataTable<EmploymentType>();
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",1)
                        ,new SqlParameter("@PageSize","0")
                        ,new SqlParameter("@SearchString","")
                        ,new SqlParameter("@OrderBy","")
                        ,new SqlParameter("@SortBy","")
                        };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_EmploymentTypeMaster", param))
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

                    lst = table.ToPagedDataTableList<EmploymentType>
                       (1, 20, totalItemCount, null, "EmploymentTypeID", "ASC");

                    lst = table.ToPagedDataTableList<EmploymentType>();

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

        public PagedDataTable<EmployeeCategory> GetAllEmployeeCategoryMaster()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<EmployeeCategory> lst = new PagedDataTable<EmployeeCategory>();
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",1)
                        ,new SqlParameter("@PageSize","0")
                        ,new SqlParameter("@SearchString","")
                        ,new SqlParameter("@OrderBy","")
                        ,new SqlParameter("@SortBy","")
                        };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeCategoryMaster", param))
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

                    lst = table.ToPagedDataTableList<EmployeeCategory>
                       (1, 20, totalItemCount, null, "EmploymentTypeID", "ASC");

                    lst = table.ToPagedDataTableList<EmployeeCategory>();

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

        /* Added by Dravesh Lokhande on 07-02-2023 -- End */

        /* Added by Rahul Mistry On 24-Jan-2023 --  START */
        public PagedDataTable<EmployeePresentList> GetAllEmployeesTimeSheet(int Companyid, int Departmentid, DateTime? PresenceDate, string serachString)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<EmployeePresentList> lst = new PagedDataTable<EmployeePresentList>();
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",1)
                        ,new SqlParameter("@PageSize",1000)
                        ,new SqlParameter("@SearchString",serachString)
                        ,new SqlParameter("@CompanyID", Companyid)
                        ,new SqlParameter("@DepartmentID", Departmentid)
                        ,new SqlParameter("@PresenceDate",PresenceDate)
                        ,new SqlParameter("@OrderBy","")
                        ,new SqlParameter("@SortBy","")
                        };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeMasterForTimeSheet", param))
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
                    lst = table.ToPagedDataTableList<EmployeePresentList>();
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

        /* Added by Rahul Mistry On 24-Jan-2023 --  END */

        /*22-02-2023*/
        public PagedDataTable<SalaryHead> GetAllSalaryHead()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<SalaryHead> lst = new PagedDataTable<SalaryHead>();
            try
            {
                SqlParameter[] param = {
            new SqlParameter("@PageNo",1)
            ,new SqlParameter("@PageSize","0")
            ,new SqlParameter("@SearchString","")
            ,new SqlParameter("@OrderBy","")
            ,new SqlParameter("@SortBy","")
            };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_SalaryHead", param))
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

                    lst = table.ToPagedDataTableList<SalaryHead>
                     (1, 20, totalItemCount, null, "SalaryHeadID", "ASC");

                    lst = table.ToPagedDataTableList<SalaryHead>();

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

        public PagedDataTable<SalaryTypeMaster> GetAllSalaryType()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<SalaryTypeMaster> lst = new PagedDataTable<SalaryTypeMaster>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_SalaryTypeMaster"))
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

                    lst = table.ToPagedDataTableList<SalaryTypeMaster>
                     (1, 20, totalItemCount, null, "SalaryHeadID", "ASC");

                    lst = table.ToPagedDataTableList<SalaryTypeMaster>();

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
        /*22-02-2023*/

        /* Added by Rahul Mistry On 13-March-2023 --  START */
        public PagedDataTable<UserMasterMetadata> GetAllUsersByCompanyId(int Companyid, int pageNo = 0, int pageSize = 0, string searchString = "", int roleBy = 0, string orderBy = "FirstName", string sortBy = "ASC")
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<UserMasterMetadata> lst = new PagedDataTable<UserMasterMetadata>();
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@CompanyID", Companyid)
                        ,new SqlParameter("@PageNo",1)
                        ,new SqlParameter("@PageSize",1000)
                        ,new SqlParameter("@SearchString",searchString)
                        ,new SqlParameter("@RoleBy",roleBy)
                        ,new SqlParameter("@OrderBy","")
                        ,new SqlParameter("@SortBy","")
                        };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_UserMasterForSalaryPaidHr", param))
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
                    lst = table.ToPagedDataTableList<UserMasterMetadata>();
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
        /* Added by Rahul Mistry On 13-March-2023 --  END */
        #region Contractor List
        public PagedDataTable<ContractorMaster> GetAllContractorList()
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<ContractorMaster> lst = new PagedDataTable<ContractorMaster>();
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",1)
                        ,new SqlParameter("@PageSize","0")
                        ,new SqlParameter("@SearchString","")
                        ,new SqlParameter("@OrderBy","")
                        ,new SqlParameter("@SortBy","")
                        };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_ContractorMaster", param))
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

                    lst = table.ToPagedDataTableList<ContractorMaster>
                       (1, 20, totalItemCount, null, "ContractorID", "ASC");

                    lst = table.ToPagedDataTableList<ContractorMaster>();

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
        #endregion Contractor List

        #endregion Multiple record select

        #region Single record select

        public PagedDataTable<DepartmentMaster> GetDepartment(int departmentId)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<DepartmentMaster> lst = new PagedDataTable<DepartmentMaster>();
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",1)
                        ,new SqlParameter("@PageSize","0")
                        ,new SqlParameter("@SearchString","")
                        ,new SqlParameter("@OrderBy","")
                        ,new SqlParameter("@SortBy","")
                        };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_DepartmentMaster", param))
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
                    lst = table.ToPagedDataTableList<DepartmentMaster>();
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

        public PagedDataTable<DesignationMaster> GetDesignation(int designationID)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<DesignationMaster> lst = new PagedDataTable<DesignationMaster>();
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@PageNo", 1),
                    new SqlParameter("@PageSize", "0"),
                };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_DesignationMaster", param))
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
                    lst = table.ToPagedDataTableList<DesignationMaster>();
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

        public PagedDataTable<EmployeeMaster> GetEmployee(int employeeID)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<EmployeeMaster> lst = new PagedDataTable<EmployeeMaster>();
            try
            {
                SqlParameter[] param = {
                        new SqlParameter("@PageNo",1)
                        ,new SqlParameter("@PageSize","0")
                        ,new SqlParameter("@SearchString","")
                        ,new SqlParameter("@OrderBy","")
                        ,new SqlParameter("@SortBy","")
                        };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_EmployeeMaster", param))
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
                    lst = table.ToPagedDataTableList<EmployeeMaster>();
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

        public PagedDataTable<GenderMaster> GetGender(int genderID)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<GenderMaster> lst = new PagedDataTable<GenderMaster>();
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@PageNo", 0),
                    new SqlParameter("@PageSize", "5"),
                };
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_GenderMaster", param))
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
                    lst = table.ToPagedDataTableList<GenderMaster>();
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

        public PagedDataTable<EmailGroupMaster> GetEmailGroupMaster(int emailGroupID)
        {
            DataTable table = new DataTable();
            int totalItemCount = 0;
            PagedDataTable<EmailGroupMaster> lst = new PagedDataTable<EmailGroupMaster>();
            try
            {
                using (DataSet ds = SqlHelper.ExecuteDataset(connection, CommandType.StoredProcedure, "Usp_GetAll_EmailGroupMaster"))
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
                    lst = table.ToPagedDataTableList<EmailGroupMaster>();
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




        #endregion Single record select
    }
}
