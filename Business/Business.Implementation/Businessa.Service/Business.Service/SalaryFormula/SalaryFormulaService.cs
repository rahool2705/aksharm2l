using Business.SQL;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using System;
using Business.Entities.SalaryFormula;
using Microsoft.Extensions.Configuration;

namespace Business.Interface.ISalaryFormula
{
    public class SalaryFormulaService : ISalaryFormula
    {
        private IConfiguration _configuration { get; set; }
        private string _connection = string.Empty;
        public SalaryFormulaService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<SalaryFormulaMaster> GetSalaryFormula()
        {
            SalaryFormulaMaster result = null;
            try
            {
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(_connection, CommandType.StoredProcedure, "Usp_Get_SalaryFormulaMaster");
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<SalaryFormulaMaster>();
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

        public async Task<int> AddUpdateSalaryCalculationFormula(SalaryFormulaMaster salaryFormulaMaster)
        {
            try
            {
                SqlParameter[] param = {
                     new SqlParameter("@SalaryFormulaID", salaryFormulaMaster.SalaryFormulaID)
                    ,new SqlParameter("@X_CTC", salaryFormulaMaster.X_CTC)
                    ,new SqlParameter("@A_BasicOf", salaryFormulaMaster.A_BasicOf)
                    ,new SqlParameter("@Basic", salaryFormulaMaster.Basic)
                    ,new SqlParameter("@B_DearnessAllowenceOf", salaryFormulaMaster.B_DearnessAllowenceOf)
                    ,new SqlParameter("@DearnessAllowence", salaryFormulaMaster.DearnessAllowence)
                    ,new SqlParameter("@DearnessAllowenceVl", salaryFormulaMaster.DearnessAllowenceVl)
                    ,new SqlParameter("@C_HouseRentAllowenceOf", salaryFormulaMaster.C_HouseRentAllowenceOf)
                    ,new SqlParameter("@HouseRentAllowence", salaryFormulaMaster.HouseRentAllowence)
                    ,new SqlParameter("@HouseRentAllowenceVl", salaryFormulaMaster.HouseRentAllowenceVl)
                    ,new SqlParameter("@D_OtherAllowenceOf", salaryFormulaMaster.D_OtherAllowenceOf)
                    ,new SqlParameter("@OtherAllowence", salaryFormulaMaster.OtherAllowence)
                    ,new SqlParameter("@OtherAllowenceVl", salaryFormulaMaster.OtherAllowenceVl)
                    ,new SqlParameter("@E_AllCashReembersmentOf", salaryFormulaMaster.E_AllCashReembersmentOf)
                    ,new SqlParameter("@AllCashReembersment", salaryFormulaMaster.AllCashReembersment)
                    ,new SqlParameter("@AllCashReembersmentVl", salaryFormulaMaster.AllCashReembersmentVl)
                    ,new SqlParameter("@F_LeaveTravelAllowenceOf", salaryFormulaMaster.F_LeaveTravelAllowenceOf)
                    ,new SqlParameter("@LeaveTravelAllowence", salaryFormulaMaster.LeaveTravelAllowence)
                    ,new SqlParameter("@LeaveTravelAllowenceVl", salaryFormulaMaster.LeaveTravelAllowenceVl)
                    ,new SqlParameter("@G_MedicalOf", salaryFormulaMaster.G_MedicalOf)
                    ,new SqlParameter("@Medical", salaryFormulaMaster.Medical)
                    ,new SqlParameter("@MedicalVl", salaryFormulaMaster.MedicalVl)
                    ,new SqlParameter("@H_ArrearsOf", salaryFormulaMaster.H_ArrearsOf)
                    ,new SqlParameter("@Arrears", salaryFormulaMaster.Arrears)
                    ,new SqlParameter("@ArrearsVl", salaryFormulaMaster.ArrearsVl)
                    ,new SqlParameter("@P_ProvidentFundOf", salaryFormulaMaster.P_ProvidentFundOf)
                    ,new SqlParameter("@ProvidentFund", salaryFormulaMaster.ProvidentFund)
                    ,new SqlParameter("@ProvidentFundVl", salaryFormulaMaster.ProvidentFundVl)
                    ,new SqlParameter("@Q_EmployeeStateInsuranceOf", salaryFormulaMaster.Q_EmployeeStateInsuranceOf)
                    ,new SqlParameter("@EmployeeStateInsurance", salaryFormulaMaster.EmployeeStateInsurance)
                    ,new SqlParameter("@EmployeeStateInsuranceVl", salaryFormulaMaster.EmployeeStateInsuranceVl)
                    ,new SqlParameter("@R_IncomeTaxOf", salaryFormulaMaster.R_IncomeTaxOf)
                    ,new SqlParameter("@IncomeTax", salaryFormulaMaster.IncomeTax)
                    ,new SqlParameter("@IncomeTaxVl", salaryFormulaMaster.IncomeTaxVl)
                    ,new SqlParameter("@S_ProfessionalTaxOf", salaryFormulaMaster.S_ProfessionalTaxOf)
                    ,new SqlParameter("@ProfessionalTax", salaryFormulaMaster.ProfessionalTax)
                    ,new SqlParameter("@ProfessionalTaxVl", salaryFormulaMaster.ProfessionalTaxVl)
                    ,new SqlParameter("@T_LoanAndAdvanceOf", salaryFormulaMaster.T_LoanAndAdvanceOf)
                    ,new SqlParameter("@LoanAndAdvance", salaryFormulaMaster.LoanAndAdvance)
                    ,new SqlParameter("@LoanAndAdvanceVl", salaryFormulaMaster.LoanAndAdvanceVl)
                    ,new SqlParameter("@U_PrerequisitesOf", salaryFormulaMaster.U_PrerequisitesOf)
                    ,new SqlParameter("@Prerequisites", salaryFormulaMaster.Prerequisites)
                    ,new SqlParameter("@PrerequisiteVl", salaryFormulaMaster.PrerequisiteVl)
                    ,new SqlParameter("@A1_GrossEarnings", salaryFormulaMaster.A1_GrossEarnings)
                    ,new SqlParameter("@B1_GrossDeduction", salaryFormulaMaster.B1_GrossDeduction)
                    ,new SqlParameter("@A1B1_NetSalaryPayable", salaryFormulaMaster.A1B1_NetSalaryPayable)
                };

                var obj = await SqlHelper.ExecuteScalarAsync(_connection, CommandType.StoredProcedure, "Usp_IU_SalaryFormulaMaster", param);

                return obj != null ? Convert.ToInt32(obj) : 0;
            }
            catch
            {
                throw;
            }
        }


        public async Task<PagedDataTable<SalaryHead>> GetAllSalaryHeads(int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "SrNo", string sortBy = "ASC")
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

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(_connection, CommandType.StoredProcedure, "Usp_GetAll_SalaryHead", param))
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
                    PagedDataTable<SalaryHead> lst = table.ToPagedDataTableList<SalaryHead>
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

        public async Task<SalaryHead> GetSalaryHeadAsync(int salaryHeadId)
        {
            SalaryHead result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@SalaryHeadID", salaryHeadId) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(_connection, CommandType.StoredProcedure, "Usp_Get_SalaryHead", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<SalaryHead>();
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

        public async Task<int> AddUpdateSalaryHead(SalaryHead salaryHead)
        {
            try
            {
                SqlParameter[] param = {
                    new SqlParameter("@SalaryHeadID",salaryHead.SalaryHeadID),
                new SqlParameter("@SalaryHeadName", salaryHead.SalaryHeadName ),
                new SqlParameter("@CreatedOrModifiedBy", salaryHead.CreatedOrModifiedBy),
                new SqlParameter("@IsActive", salaryHead.IsActive )
                };

                var obj = await SqlHelper.ExecuteScalarAsync(_connection, CommandType.StoredProcedure, "Usp_IU_SalaryHead", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PagedDataTable<SalaryFormula>> GetAllSalaryFormula(int employeeCategoryId, int pageNo = 1, int pageSize = 10, string searchString = "", string orderBy = "SrNo", string sortBy = "ASC")
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
                        ,new SqlParameter("@EmployeeCategoryId",employeeCategoryId)
                        ,new SqlParameter("@SortBy",sortBy=="ASC"?0:1)
                        };

                using (DataSet ds = await SqlHelper.ExecuteDatasetAsync(_connection, CommandType.StoredProcedure, "Usp_GetAll_SalaryFormula", param))
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
                    PagedDataTable<SalaryFormula> lst = table.ToPagedDataTableList<SalaryFormula>
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

        public async Task<SalaryFormula> GetSalaryFormulaAsync(int salaryFormulaId)
        {
            SalaryFormula result = null;
            try
            {
                SqlParameter[] param = { new SqlParameter("@SalaryFormulaID", salaryFormulaId) };
                DataSet ds = await SqlHelper.ExecuteDatasetAsync(_connection, CommandType.StoredProcedure, "Usp_Get_SalaryFormula", param);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            result = dr.ToPagedDataTableList<SalaryFormula>();
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

        public async Task<int> AddUpdateSalaryFormula(SalaryFormula salaryFormula)
        {
            try
            {
                SqlParameter[] param = {
                     new SqlParameter("@SalaryFormulaID", salaryFormula.SalaryFormulaID )
                    ,new SqlParameter("@EmployeeCategoryId", salaryFormula.EmployeeCategoryId )
                    ,new SqlParameter("@EmploymentTypeID", salaryFormula.EmploymentTypeID )
                    ,new SqlParameter("@SalaryHeadID", salaryFormula.SalaryHeadID )
                    ,new SqlParameter("@SalaryHeadLabel", salaryFormula.SalaryHeadLabel )
                    ,new SqlParameter("@SalaryFormula", salaryFormula.SalaryFormulaText )
                    ,new SqlParameter("@SalaryTypeID", salaryFormula.SalaryTypeID )
                    ,new SqlParameter("@LabelPercentage", salaryFormula.LabelPercentage )
                    ,new SqlParameter("@LabelValue", salaryFormula.LabelValue )
                    ,new SqlParameter("@IsActive", salaryFormula.IsActive )
                    ,new SqlParameter("@CreatedOrModifiedBy", salaryFormula.CreatedOrModifiedBy )
                };

                var obj = await SqlHelper.ExecuteScalarAsync(_connection, CommandType.StoredProcedure, "Usp_IU_SalaryFormula", param);

                return obj != null ? Convert.ToInt32(obj) : 0;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
