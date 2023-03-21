using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities.Employee.EmployeeMedicalHistory
{
    public class EmployeeMedicalHistory
    {
        public int EmployeeMedicalDetailsID { get; set; }
        public int EmployeeID { get; set; }
        public string HospitalName { get; set; }
        public string DoctorName { get; set; }
        public string MedicalReason { get; set; }
        public int TreatmentMonth { get; set; }

        [Required(ErrorMessage = "Treatment Year be in number (Eg:- 2023)")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Treatment Year be in number (Eg:- 2023)")]
        public int? TreatmentYear { get; set; }

        [Required(ErrorMessage = "Please enter employee issue")]
        public string FacingAnyProblem { get; set; }
        public string TakingTreatmentNow { get; set; }
        public string TakingMedicinesNow { get; set; }
        public string PlaceofTreatment { get; set; }
        public int SrNo { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;

    }
}
