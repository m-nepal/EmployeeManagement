using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagement.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            var viewModel = new ErrorViewModel
            {
                ErrorPath = statusCodeResult.OriginalPath,
                ErrorQueryString = statusCodeResult.OriginalQueryString

            };
            switch (statusCode)
            {
                case 400:
                    viewModel.ErrorMessage = "Sorry, The request could not be processed by the server due to invalid syntax.";

                    break;
                case 401:
                    viewModel.ErrorMessage = "Sorry, The requested resource requires user authentication.";
                    break;
                case 404:
                    viewModel.ErrorMessage = "Sorry, The server has not found anything that matches the requested URI.";
                    _logger.LogWarning($"404 Error Occured. Path = {statusCodeResult.OriginalPath}" + $" and Query String =  {statusCodeResult.OriginalQueryString}");
                    break;
                case 500:
                    viewModel.ErrorMessage = "Sorry, The server encountered an unexpected condition that prevented it from fulfilling the request.";
                    break;

            }
            return View("NotFound", viewModel);
        }


        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetail = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var viewModel = new ExceptionViewModel
            {
                ExceptionMessage = exceptionDetail.Error.Message,
                ExceptionPath = exceptionDetail.Path,
                StackTrace = exceptionDetail.Error.StackTrace

            };
            _logger.LogError($"The path {exceptionDetail.Path} threw exception {exceptionDetail.Error}");
            return View("Error", viewModel);
        }
    }
}
