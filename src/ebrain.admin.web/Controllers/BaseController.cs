using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ebrain.admin.bc;
using Ebrain.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ebrain.Controllers
{
    public class BaseController : Controller
    {
        readonly ILogger _logger;

        public BaseController(IUnitOfWork unitOfWork, ILogger<BaseController> logger)
        {
            this._logger = logger;
            this.loadPermission(unitOfWork);
        }

        private void loadPermission(IUnitOfWork context)
        {
            var userId = Utilities.GetUserId(this.User);
            var list = context.UserRoles.GetAll(userId);
        }
    }
}