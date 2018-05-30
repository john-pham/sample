using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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

        protected string CSV<T>(IEnumerable<T> value)
        {
            var myType = typeof(T);
            var props = new List<PropertyInfo>(myType.GetProperties());
            var m_Ret = new StringBuilder();

            //
            m_Ret.AppendLine(string.Join(",\t", props.Select(x => x.Name)));

            //
            foreach (var item in value)
            {
                var list = new List<string>();

                foreach (PropertyInfo prop in props)
                {
                    var obj = prop.GetValue(item);
                    if (obj != null)
                        list.Add(obj.ToString());
                    else
                        list.Add(string.Empty);
                }
                //
                m_Ret.AppendLine(string.Join(",\t", list));
            }

            return m_Ret.ToString();
            //return System.Text.Encoding.UTF8.GetBytes(content);
        }

        readonly ILogger _logger;
        private IList<ebrain.admin.bc.Report.AccessRight> _accessRights;

        public BaseController(IUnitOfWork unitOfWork, ILogger<BaseController> logger)
        {
            this._logger = logger;
            this.loadPermission(unitOfWork);
            this.loadBehavior(this.GetType());

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

    public class BaseController1 : Controller
    {
        public bool CanView { get; private set; }
        public bool CanEdit { get; private set; }
        public bool CanDelete { get; private set; }
        public bool CanCreate { get; private set; }

        readonly ILogger _logger;
        private IList<ebrain.admin.bc.Report.AccessRight> _accessRights;

        public BaseController1(IUnitOfWork unitOfWork, ILogger<BaseController> logger)
        {
            this._logger = logger;
            this.loadPermission(unitOfWork);
            this.loadBehavior(this.GetType());

            if (!this.CanView)
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

                        foreach (var value in list)
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