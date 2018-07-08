// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

using System;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Ebrain.Helpers
{
    public static class EmailTemplates
    {
        static IHostingEnvironment _hostingEnvironment;
        static string testEmailTemplate;
        static string plainTextTestEmailTemplate;
        static IOptions<SmtpConfig> _serviceSmtpConfig;

        public static void Initialize(IHostingEnvironment hostingEnvironment, IOptions<SmtpConfig> serviceSmtpConfig)
        {
            _hostingEnvironment = hostingEnvironment;
            _serviceSmtpConfig = serviceSmtpConfig;
        }


        public static string GetTestEmail(string recepientName, DateTime testDate)
        {
            if (testEmailTemplate == null)
                testEmailTemplate = ReadPhysicalFile("Helpers/Templates/TestEmail.template");


            string emailMessage = testEmailTemplate
                .Replace("{user}", recepientName)
                .Replace("{testDate}", testDate.ToString());

            return emailMessage;
        }



        public static string GetPlainTextTestEmail(DateTime date)
        {
            if (plainTextTestEmailTemplate == null)
                plainTextTestEmailTemplate = ReadPhysicalFile("Helpers/Templates/PlainTextTestEmail.template");


            string emailMessage = plainTextTestEmailTemplate
                .Replace("{date}", date.ToString());

            return emailMessage;
        }




        private static string ReadPhysicalFile(string path)
        {
            if (_hostingEnvironment == null)
                throw new InvalidOperationException($"{nameof(EmailTemplates)} is not initialized");

            IFileInfo fileInfo = _hostingEnvironment.ContentRootFileProvider.GetFileInfo(path);

            if (!fileInfo.Exists)
                throw new FileNotFoundException($"Template file located at \"{path}\" was not found");

            using (var fs = fileInfo.CreateReadStream())
            {
                using (var sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public static async Task<(bool success, string errorMsg)> GenerateSendEmail(Func<string> Func_GetBodyHtml, string[] recepientEmails, 
            string subject)
        {
            // get body html
            var bodyHtml = Func_GetBodyHtml?.Invoke();
            var configStmp = _serviceSmtpConfig.Value;
            var result = await EmailSender.SendEmailAsync(
                EmailSender.GetMailboxAddress(recepientEmails).ToArray(), //"qmvnn2000@gmail.com",
                subject,
                bodyHtml,
                configStmp
                );
            return result;
        }
    }
}
