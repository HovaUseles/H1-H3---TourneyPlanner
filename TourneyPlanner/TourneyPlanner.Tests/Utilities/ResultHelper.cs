using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourneyPlanner.Tests.Utilities
{
    internal static class ResultHelper
    {

        public static int? GetStatusCode(ActionResult result)
        {
            if (result is StatusCodeResult statusCodeResult)
            {
                return statusCodeResult.StatusCode;
            }
            else if (result is ObjectResult objectResult)
            {
                return objectResult.StatusCode;
            }

            throw new NotImplementedException();
        }

        public static int? GetStatusCode<T>(ActionResult<T?> result)
        {
            IConvertToActionResult convertToActionResult = result; // ActionResult implicit implements IConvertToActionResult
            var actionResultWithStatusCode = convertToActionResult.Convert() as IStatusCodeActionResult;
            return actionResultWithStatusCode?.StatusCode;
        }
    }
}
