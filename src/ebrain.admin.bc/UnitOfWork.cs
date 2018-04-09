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
using ebrain.admin.bc.Repositories;
using ebrain.admin.bc.Repositories.Interfaces;

namespace ebrain.admin.bc
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationDbContext _context;

        IAccessRightsRepository _accessRights;
        IUserGroupRepository _userGroups;
        IFeatureRepository _features;
        IFeatureGroupRepository _featureGroups;

        ISMSRepository _SMSs;
        IGroupDocumentRepository _GroupDocuments;
        IDocumentRepository _Documents;
        IGenderRepository _Genders;
        IBranchRepository _branches;
        IStockRepository _stocks;
        IPaymentTypeRepository _paymentTypes;
        IUnitRepository _units;
        IExamineRepository _Examines;
        IClassRepository _classes;
        IClassStatusRepository _ClassStatus;
        ITypeMaterialRepository _typeMaterial;
        IGrpMaterialRepository _grpMaterial;
        IMaterialRepository _materials;
        IGrpSupplierRepository _grpSupplier;
        ISupplierRepository _supplier;
        IRoomRepository _rooms;
        IAttendanceRepository _Attendances;
        IStudentRepository _students;
        IStudentStatusRepository _StudentStatus;
        ILevelClassRepository _levelclass;
        IShiftClassRepository _shiftClass;
        IConsultantRepository _consultants;
        IClassStudentRepository _ClassStudents;
        IClassTimeRepository _ClassTimes;

        ICustomerRepository _customers;
        IProductRepository _products;
        IOrdersRepository _orders;
        IIOStockRepository _IOStocks;
        IConfigNumberOfCodeRepository _ConfigNumberOfCodes;
        IPaymentRepository _Payments;
        IInventoriesRepository _Inventories;
        IProfitRepository _Profits;
        IDeptRepository _Depts;
        ITodayRepository _Today;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IAccessRightsRepository AccessRights
        {
            get
            {
                if (_accessRights == null)
                    _accessRights = new AccessRightsRepository(_context);

                return _accessRights;
            }
        }

        public IFeatureRepository Features
        {
            get
            {
                if (_features == null)
                    _features = new FeatureRepository(_context);

                return _features;
            }
        }

        public IFeatureGroupRepository FeatureGroups
        {
            get
            {
                if (_featureGroups == null)
                    _featureGroups = new FeatureGroupRepository(_context);

                return _featureGroups;
            }
        }

        public IUserGroupRepository UserGroups
        {
            get
            {
                if (_userGroups == null)
                    _userGroups = new UserGroupRepository(_context);

                return _userGroups;
            }
        }

        public IStudentStatusRepository StudentStatus
        {
            get
            {
                if (_StudentStatus == null)
                    _StudentStatus = new StudentStatusRepository(_context);

                return _StudentStatus;
            }
        }

        public IPaymentTypeRepository PaymentTypes
        {
            get
            {
                if (_paymentTypes == null)
                    _paymentTypes = new PaymentTypeRepository(_context);

                return _paymentTypes;
            }
        }

        public IPaymentRepository Payments
        {
            get
            {
                if (_Payments == null)
                    _Payments = new PaymentRepository(_context);

                return _Payments;
            }
        }

        public IProfitRepository Profits
        {
            get
            {
                if (_Profits == null)
                    _Profits = new ProfitRepository(_context);

                return _Profits;
            }
        }

        public IInventoriesRepository Inventories
        {
            get
            {
                if (_Inventories == null)
                    _Inventories = new InventoriesRepository(_context);

                return _Inventories;
            }
        }

        public IDeptRepository Depts
        {
            get
            {
                if (_Depts == null)
                    _Depts = new DeptRepository(_context);

                return _Depts;
            }
        }

        public ITodayRepository Today
        {
            get
            {
                if (_Today == null)
                    _Today = new TodayRepository(_context);

                return _Today;
            }
        }

        public IConfigNumberOfCodeRepository ConfigNumberOfCodes
        {
            get
            {
                if (_ConfigNumberOfCodes == null)
                    _ConfigNumberOfCodes = new ConfigNumberOfCodeRepository(_context);

                return _ConfigNumberOfCodes;
            }
        }

        public IIOStockRepository IOStocks
        {
            get
            {
                if (_IOStocks == null)
                    _IOStocks = new IOStockRepository(_context);

                return _IOStocks;
            }
        }

        public IStudentRepository Students
        {
            get
            {
                if (_students == null)
                    _students = new StudentRepository(_context);

                return _students;
            }
        }
        public IGroupDocumentRepository GroupDocuments
        {
            get
            {
                if (_GroupDocuments == null)
                    _GroupDocuments = new GroupDocumentRepository(_context);

                return _GroupDocuments;
            }
        }

        public IDocumentRepository Documents
        {
            get
            {
                if (_Documents == null)
                    _Documents = new DocumentRepository(_context);

                return _Documents;
            }
        }


        public ISMSRepository SMSs
        {
            get
            {
                if (_SMSs == null)
                    _SMSs = new SMSRepository(_context);

                return _SMSs;
            }
        }

        public IGenderRepository Genders
        {
            get
            {
                if (_Genders == null)
                    _Genders = new GenderRepository(_context);

                return _Genders;
            }
        }

        public IBranchRepository Branches
        {
            get
            {
                if (_branches == null)
                    _branches = new BranchRepository(_context);

                return _branches;
            }
        }

        public IConsultantRepository Consultants
        {
            get
            {
                if (_consultants == null)
                    _consultants = new ConsultantRepository(_context);

                return _consultants;
            }
        }

        public ILevelClassRepository LevelClasses
        {
            get
            {
                if (_levelclass == null)
                    _levelclass = new LevelClassRepository(_context);

                return _levelclass;
            }
        }

        public IShiftClassRepository ShiftClasses
        {
            get
            {
                if (_shiftClass == null)
                    _shiftClass = new ShiftClassRepository(_context);

                return _shiftClass;
            }
        }

        public IStockRepository Stocks
        {
            get
            {
                if (_stocks == null)
                    _stocks = new StockRepository(_context);

                return _stocks;
            }
        }

        public IMaterialRepository Materials
        {
            get
            {
                if (_materials == null)
                    _materials = new MaterialRepository(_context);

                return _materials;
            }
        }

        public IAttendanceRepository Attendances
        {
            get
            {
                if (_Attendances == null)
                    _Attendances = new AttendanceRepository(_context);

                return _Attendances;
            }
        }

        public IRoomRepository Rooms
        {
            get
            {
                if (_rooms == null)
                    _rooms = new RoomRepository(_context);

                return _rooms;
            }
        }

        public IGrpMaterialRepository GrpMaterials
        {
            get
            {
                if (_grpMaterial == null)
                    _grpMaterial = new GrpMaterialRepository(_context);

                return _grpMaterial;
            }
        }

        public ITypeMaterialRepository TypeMaterials
        {
            get
            {
                if (_typeMaterial == null)
                    _typeMaterial = new TypeMaterialRepository(_context);

                return _typeMaterial;
            }
        }

        public IGrpSupplierRepository GrpSuppliers
        {
            get
            {
                if (_grpSupplier == null)
                    _grpSupplier = new GrpSupplierRepository(_context);

                return _grpSupplier;
            }
        }
        public ISupplierRepository Suppliers
        {
            get
            {
                if (_supplier == null)
                    _supplier = new SupplierRepository(_context);

                return _supplier;
            }
        }

        public IExamineRepository Examines
        {
            get
            {
                if (_Examines == null)
                    _Examines = new ExamineRepository(_context);
                return _Examines;
            }
        }

        public IUnitRepository Units
        {
            get
            {
                if (_units == null)
                    _units = new UnitRepository(_context);

                return _units;
            }
        }

        public IClassStudentRepository ClassStudents
        {
            get
            {
                if (_ClassStudents == null)
                    _ClassStudents = new ClassStudentRepository(_context);

                return _ClassStudents;
            }
        }

        public IClassTimeRepository ClassTimes
        {
            get
            {
                if (_ClassTimes == null)
                    _ClassTimes = new ClassTimeRepository(_context);

                return _ClassTimes;
            }
        }

        public IClassRepository Classes
        {
            get
            {
                if (_classes == null)
                    _classes = new ClassRepository(_context);

                return _classes;
            }
        }

        public IClassStatusRepository ClassStatus
        {
            get
            {
                if (_ClassStatus == null)
                    _ClassStatus = new ClassStatusRepository(_context);

                return _ClassStatus;
            }
        }

        public ICustomerRepository Customers
        {
            get
            {
                if (_customers == null)
                    _customers = new CustomerRepository(_context);

                return _customers;
            }
        }

        public IProductRepository Products
        {
            get
            {
                if (_products == null)
                    _products = new ProductRepository(_context);

                return _products;
            }
        }

        public IOrdersRepository Orders
        {
            get
            {
                if (_orders == null)
                    _orders = new OrdersRepository(_context);

                return _orders;
            }
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
