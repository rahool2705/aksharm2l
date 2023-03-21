using System;

namespace Business.Entities.Machinery.MachineryMaster
{
    public class MachineryMaster
    {
        public int MachineryMasterID { get; set; }
        public string MachineryMastertext { get; set; }
        public int MachineryID { get; set; }
        public string MachineryText { get; set; }
        public string MachineryCode { get; set; }
        public string Manufacturer { get; set; }
        public string Location { get; set; }
        public string MCSerialNo {get; set;}
        public string MCModelNo { get; set;}
        public string CapacitySpecification { get; set; }
        public string ConditionOfWorking { get; set; }
        public string Remarks { get; set; }
        public string SupplierName { get; set; }
        public string PurchaseDate { get; set;}
        public string PurchaseInvioceNo { get; set; }
        public int PurchaseAmount { get; set; }
        public string PartyName { get; set; }
        public DateTime SalesDate { get; set; }
        public string SalesInvioceNo { get; set; }
        public int SalesAmount { get; set; }
        public string PartyLocation { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public object SrNo { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
