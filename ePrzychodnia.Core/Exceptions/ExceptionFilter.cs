using System.Linq;
using System.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace ePrzychodnia.Core.Exceptions
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

        public ExceptionFilter(ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ClinicException clinicException)
            {
                var tempData = _tempDataDictionaryFactory.GetTempData(context.HttpContext);


                var controller = context.ActionDescriptor.RouteValues.FirstOrDefault(x => x.Key == "controller");
                var errorMessages = new ResourceManager(typeof(ErrorCodes));
                var errorMessage = errorMessages.GetString(clinicException.ErrorCode.ToString());

                if (!tempData.ContainsKey(NotificationMessageType.error.ToString()))
                    tempData.Add(NotificationMessageType.error.ToString(), errorMessage);
                else
                    tempData[NotificationMessageType.error.ToString()] = errorMessage;

                var routeDictionary = new RouteValueDictionary {{"action", "Index"}, {"controller", controller.Value}};


                context.Result = new RedirectToRouteResult(routeDictionary);
            }
        }
    }
}