using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Filters;
using WebApiFundamental.Core.Data.Entities;
using WebApiFundamental.Core.Repositories;

namespace WebApiFundamental.Core.Filters
{
    public class ExceptionHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        private readonly IUnitOfWork unitOfWork;
        public ExceptionHandlerAttribute(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task OnException(ExceptionContext filterContext)
        {
            await Log(filterContext.Exception, filterContext);
        }

        private async Task Log(Exception ex, ExceptionContext filterContext)
        {
            string action = filterContext.ActionContext.ActionDescriptor.ActionName.ToString();
            string controller = filterContext.ControllerContext.ControllerDescriptor.ControllerName.ToString();
            ErrorLogger logger = new ErrorLogger
            {
                Error_message = ex.Message,
                Error_message_detail = ex.StackTrace,
                Controller_Name=controller+"/"+action,
                Error_Logged_date=DateTime.Now 
            };
            unitOfWork.log.Add(logger);
            await unitOfWork.SaveChangesAsync();
        }

    }
}