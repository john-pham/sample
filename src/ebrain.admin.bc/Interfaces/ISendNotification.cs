using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Interfaces
{
    public interface ISendNotification
    {
        Task<(bool success, string errorMsg)> GenerateSendEmail(Func<string> Func_GetBodyHtml, string[] recepientEmails, string subject);
    }
}
