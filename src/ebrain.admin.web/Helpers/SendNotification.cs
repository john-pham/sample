using ebrain.admin.bc.Interfaces;
using Ebrain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebrain.admin.web.Helpers
{
    public class SendNotification : ISendNotification
    {


        public Task<(bool success, string errorMsg)> GenerateSendEmail(Func<string> Func_GetBodyHtml, string[] recepientEmails, string subject)
        {
            return EmailTemplates.GenerateSendEmail(Func_GetBodyHtml, recepientEmails, subject);
        }
    }
}
