using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndoGhana
{
    public class ElmahHandleErrorAttribute : System.Web.Mvc.HandleErrorAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            var e = context.Exception;
            var httpcontext = HttpContext.Current;
            var signal = ErrorSignal.FromContext(httpcontext);
            if (signal != null)
            {
                signal.Raise(e, httpcontext);
            }
        }
    }
}