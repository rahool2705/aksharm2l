using System;
using System.Data;

namespace Business.Entities.Customer
{
    public class CustomerAddressTxn
    {
        public int SrNo { get; set; }
        public string AddressType { get; set; }
        public string PlotNoName { get; set; }
        public string StreetNoName { get; set; }
        public string MainAddress { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public int CustomerAddressTxnID { get; set; }
        public int CustomerID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Area { get; set; }
        public string ZIPCode { get; set; }
        public bool IsActive { get; set; } = true;
        public string City { get; set; }
        public string District { get; set; }
        public string Taluka { get; set; }
        public string Landmark { get; set; }
        public int StateID { get; set; }
        public int CountryID { get; set; }
        public int AddressTypeID { get; set; }
        public int CreatedOrModifiedBy { get; set; }
        public string DistrictName { get; set; }
    }
}
