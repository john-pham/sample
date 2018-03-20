// ======================================
// Author: Ebrain Team
// Email:  info@ebrain.com.vn
// Copyright (c) 2017 www.ebrain.com.vn
// 
// ==> Contact Us: contact@ebrain.com.vn
// ======================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ebrain.admin.bc.Models;
using ebrain.admin.bc.Repositories.Interfaces;
using ebrain.admin.bc.Utilities;
using System.Globalization;

namespace ebrain.admin.bc.Repositories
{
    public class ConfigNumberOfCodeRepository : Repository<ConfigNumberOfCode>, IConfigNumberOfCodeRepository
    {
        public ConfigNumberOfCodeRepository(ApplicationDbContext context) : base(context)
        { }

        public string GenerateCode(int iotypeId, string empployId)
        {
            return GenerateCode(iotypeId, empployId, "IOStock");
        }
        public string GenerateCode(int iotypeId, string empployId, string key)
        {
            var iotype = this.appContext.IOType.FirstOrDefault(p => p.IOTypeId == iotypeId);
            if (iotype != null)
            {
                return GenerateCodeMain(key, iotype.IOTypeCode, empployId, 1);
            }
            return string.Empty;
        }

        public string GenerateCodePayment(int paymentTypeId, string empployId)
        {
            var itemType = this.appContext.PaymentType.FirstOrDefault(p => p.PaymentTypeId == paymentTypeId);
            if (itemType != null)
            {
                return GenerateCodeMain("Payment", itemType.PaymentTypeCode, empployId, 2);
            }
            return string.Empty;
        }
        
        public string GenerateCodeMain(string key, string code, string empId, int numberTable)
        {
            var itemConfig = this.appContext.ConfigNumberOfCode.FirstOrDefault(p => p.Key == key);
            if (itemConfig == null) return string.Empty;
            string sCurrentDate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            string sPattern = string.Format("{0}{1}{2}{3}_"
                , itemConfig.Value, code, empId.Substring(0, 4).ToUpper(), sCurrentDate);

            int len = sPattern.Length + itemConfig.LenOfNoOfNumber;

            var maxSNumber = string.Empty;
            switch (numberTable)
            {
                case 1: //IOStock
                    maxSNumber = this.appContext.IOStock
                        .Where(p => p.IONumber.StartsWith(sPattern)
                        && p.IONumber.Length == len)
                        .Max(p => p.IONumber);
                    break;
                case 2://payment
                    maxSNumber = this.appContext.Payment
                        .Where(p => p.PaymentCode.StartsWith(sPattern)
                        && p.PaymentCode.Length == len)
                        .Max(p => p.PaymentCode);
                    break;
                case 3://class
                    maxSNumber = this.appContext.Class
                        .Where(p => p.ClassCode.StartsWith(sPattern)
                        && p.ClassCode.Length == len)
                        .Max(p => p.ClassCode);
                    break;
            }

            int nRemainNumber = 0;
            if (!string.IsNullOrEmpty(maxSNumber))
            {
                string sRemainNumberOfCode = maxSNumber.Substring(sPattern.Length);
                nRemainNumber = StringHelper.IntParseFast(sRemainNumberOfCode);
            }
            nRemainNumber++;
            string sNewNumberCode = nRemainNumber.ToString(new string('0', itemConfig.LenOfNoOfNumber));
            string sReturnCode = string.Format("{0}{1}", sPattern, sNewNumberCode);
            return sReturnCode;
        }
        public Task<bool> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ConfigNumberOfCode> GetTopActive(int count)
        {
            throw new NotImplementedException();
        }

        public Task<ConfigNumberOfCode> Save(ConfigNumberOfCode value)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ConfigNumberOfCode>> Search(string filter, string value)
        {
            throw new NotImplementedException();
        }


        private ApplicationDbContext appContext
        {
            get { return (ApplicationDbContext)_context; }
        }
    }
}
