// ======================================
// Author: Ebrain Team
// Email:  info@ebrain.com.vn
// Copyright (c) 2017 www.ebrain.com.vn
// 
// ==> Contact Us: contact@ebrain.com.vn
// ======================================

using ebrain.admin.bc.Models;
using ebrain.admin.bc.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ebrain.admin.bc.Repositories.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        int Total { get; }
        IEnumerable<Payment> GetTopActive(int count, string branchIds);
        Task<Payment> FindById(Guid? id);
        Task<IEnumerable<Payment>> Search(string filter, string value, string branchIds);
        Task<IEnumerable<Payment>> GetAlls(string branchIds);
        Task<Payment> Save(Payment value, PaymentDetail[] iosd, Guid? id);
        Task<Boolean> Delete(string id);
        Task<IEnumerable<PaymentDetail>> GetDetailByIOId(Guid? id);
        Task<Payment> DeleteMaster(Guid? id);
        Task<Payment> CancelMaster(Guid? id);
        IEnumerable<PaymentList> GetPaymentList(DateTime fromDate, DateTime toDate, string ioNumber, 
            int paymentTypeId, bool? isPayment, Guid? userAccessRightPerson, string branchIds, int page, int size);
        IEnumerable<PaymentDetailList> GetPaymentDetailList(DateTime fromDate, DateTime toDate, string ioNumber, 
            int paymentTypeId, bool isPayment, Guid? userAccessRightPerson, string branchIds, int page, int size);
        Task<IEnumerable<PaymentType>> GetAllPaymentTypes(bool isPayment, string branchIds);
    }
}
