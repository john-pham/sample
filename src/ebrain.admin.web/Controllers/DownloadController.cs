using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ebrain.admin.bc;
using Ebrain.Helpers;
using Ebrain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Ebrain.Controllers
{
    [Authorize]
    public class DownloadController : BaseController1
    {
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;

        public DownloadController(IUnitOfWork unitOfWork, ILogger<UnitsController> logger) : base(unitOfWork, logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        [Produces("text/csv")]
        public async Task<IActionResult> OutputBranchesCSV(string filter, string value, int page, int size)
        {
            var bus = this._unitOfWork.Branches;
            var ret = from c in await bus.Search(filter, value, page, size)
                      select new BranchViewModel
                      {
                          ID = c.BranchId,
                          Code = c.BranchCode,
                          Name = c.BranchName,
                          Email = c.Email,
                          Address = c.Address,
                          PhoneNumber = c.PhoneNumber,
                          Fax = c.FAX
                      };

            var contents = this.Convert<BranchViewModel>(ret);

            Response.Headers.Add("Content-Disposition", "inline; filename=Branches.csv");

            return File(contents, "text/csv");
        }

        [AllowAnonymous]
        [HttpGet("OutputUnitsCSV")]
        [Produces("text/csv")]
        public async Task<FileResult> OutputUnitsCSV(string filter, string value, int page, int size)
        {
            var test = Utilities.GetUserId(this.User);

            var unit = this._unitOfWork.Units;
            var ret = from c in await unit.Search(filter, value, this._unitOfWork.Branches.GetAllBranchOfUserString(test), page, size)
                      select new UnitViewModel
                      {
                          ID = c.UnitId,
                          Code = c.UnitCode,
                          Name = c.UnitName,
                          Note = c.Note
                      };

            var contents = this.Convert<UnitViewModel>(ret);
            Response.Headers.Add("Content-Disposition", "inline; filename=Units.csv");

            return File(contents, "text/csv");
        }


        #region internal process

        private byte[] Convert<T>(IEnumerable<T> value)
        {
            var content = string.Join('\t', value);

            return System.Text.Encoding.UTF8.GetBytes(content);
        }

        #endregion
    }

}