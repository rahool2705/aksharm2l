using Business.Interface;
using Business.Interface.Marketing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace ERP.Extensions
{
    public class MarketingExtension
    {
        private static HttpContext Current => new HttpContextAccessor().HttpContext;
        public static IMasterService _masterService => (IMasterService)Current.RequestServices.GetService(typeof(IMasterService));
        public static IMarketingFeedbackService _iMarketingFeedbackService => (IMarketingFeedbackService)Current.RequestServices.GetService(typeof(IMarketingFeedbackService));

        public static SelectList GetAllMarketingClientFeedbackNote()
        {
            try
            {
                var FeedbackNote = _iMarketingFeedbackService.GetAllFeedbackNote();
                return new SelectList(FeedbackNote, "PositiveNoteID", "PositiveNoteText");
            }
            catch
            {
                return new SelectList(Enumerable.Empty<SelectListItem>());
            }
        }
    }
}
