// ======================================
// Author: Ebrain Team
// Email:  info@ebrain.com.vn
// Copyright (c) 2017 www.ebrain.com.vn
// 
// ==> Contact Us: contact@ebrain.com.vn
// ======================================

using ebrain.admin.bc.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ebrain.admin.bc
{
    public interface IUnitOfWork
    {
        IAccessRightsRepository AccessRights { get; }
        IUserGroupRepository UserGroups { get; }
        IUserRoleRepository UserRoles { get; }
        IFeatureRepository Features { get; }
        IFeatureGroupRepository FeatureGroups { get; }
        ISMSRepository SMSs { get; }
        IBranchRepository Branches { get; }
        IGroupDocumentRepository GroupDocuments { get; }
        IDocumentRepository Documents { get; }
        IIOStockRepository IOStocks { get; }
        IPaymentRepository Payments { get; }
        IInventoriesRepository Inventories { get; }
        IProfitRepository Profits { get; }
        IDeptRepository Depts { get; }
        IStockRepository Stocks { get; }
        IPaymentTypeRepository PaymentTypes { get; }
        ITypeMaterialRepository TypeMaterials { get; }
        IGrpMaterialRepository GrpMaterials { get; }
        IGenderRepository Genders { get; }
        IMaterialRepository Materials { get; }
        IStudentRepository Students { get; }
        IGrpSupplierRepository GrpSuppliers { get; }
        IUnitRepository Units { get; }
        IExamineRepository Examines { get; }
        IStudentStatusRepository StudentStatus { get; }
        IConfigNumberOfCodeRepository ConfigNumberOfCodes { get; }
        ISupplierRepository Suppliers { get; }
        IClassRepository Classes { get; }
        IClassStudentRepository ClassStudents { get; }
        IClassTimeRepository ClassTimes { get; }
        ITodayRepository Today { get; }
        IClassStatusRepository ClassStatus { get; }
        IRoomRepository Rooms { get; }
        IAttendanceRepository Attendances { get; }
        ILevelClassRepository LevelClasses { get; }
        IShiftClassRepository ShiftClasses { get; }
        IConsultantRepository Consultants { get; }

        ICustomerRepository Customers { get; }
        IProductRepository Products { get; }
        IOrdersRepository Orders { get; }


        int SaveChanges();
    }
}
