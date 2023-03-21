using Business.Entities.OurProduct;
using Business.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interface.IOurProduct
{
    public interface IOurProductService
    {
        Task<string> AddOurProduct(OurProduct ourProduct);
        Task<PagedDataTable<GoForRFQCardTable>> GetSessionGoForRFQCardTable(string CollectionID);
        Task<int> GoForRFQItemInactive(GoForRFQCardTable goForRFQCard);
        Task<int> IGoForRFQCardItem(GoForRFQCard goForRFQCard);
        dynamic IGoForRFQCardItemList(DataTable dataTable);
    }
}
