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
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        public bool CanView { get; private set; }
        public bool CanEdit { get; private set; }
        public bool CanDelete { get; private set; }
        public bool CanCreate { get; private set; }

        readonly ILogger _logger;
        private IList<ebrain.admin.bc.Report.AccessRight> _accessRights;

        public BaseController(IUnitOfWork unitOfWork, ILogger<BaseController> logger)
        {
            this._logger = logger;
            this.loadPermission(unitOfWork);
            //this.loadBehavior(this.GetType());

            if(!this.CanView)
            {
                Redirect("/account/login");
            }
        }

        [HttpGet("accessrights")]
        public IActionResult GetAccessRights()
        {
            return Ok(this._accessRights);
        }

        private void loadPermission(IUnitOfWork context)
        {
            var userId = Utilities.GetUserId(this.User);
            var list = context.UserRoles.GetAll(userId);

            //
            _accessRights = list.Result;
        }

        public void loadBehavior(System.Type type)
        {
            var items = _accessRights;
            var view = false;
            var edit = false;
            var delete = false;
            var create = false;

            if (items != null)
            {
                // Using reflection.
                System.Attribute[] attrs = Attribute.GetCustomAttributes(type);  // Reflection. 

                // Displaying output. 
                foreach (System.Attribute attr in attrs)
                {
                    if (attr is ViewModels.Security)
                    {
                        var att = (ViewModels.Security)attr;
                        var list = from c in items
                                   join s in att.IDs on c.FeatureId equals s
                                   select c;

                        foreach(var value in list)
                        {
                            view = view || value.View;
                            edit = edit || value.Edit;
                            delete = delete || value.Delete;
                            create = create || value.Create;
                        }

                        break;
                    }
                }
            }

            this.CanView = view;
            this.CanEdit = edit;
            this.CanDelete = delete;
            this.CanCreate = create;
        }
    }
}