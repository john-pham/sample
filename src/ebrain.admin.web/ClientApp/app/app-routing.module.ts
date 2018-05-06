// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { LoginComponent } from "./components/login/login.component";
import { HomeComponent } from "./components/home/home.component";
import { AccessRightsComponent } from "./components/access-rights/access-rights";
import { UserRolesComponent } from "./components/userroles/userroles.component";
import { FeatureGroupsComponent } from "./components/featuregroups/featuregroups.component";
import { UserGroupsComponent } from "./components/usergroups/usergroups.component";
import { UserInfoComponent } from "./components/controls/user-info.component";
import { UsersManagementComponent } from "./components/controls/users-management.component";

import { MessengerComponent } from "./components/messengers/messengers.component";
import { SupportComponent } from "./components/supports/supports.component";
import { BranchesComponent } from "./components/branches/branches.component";
import { StocksComponent } from "./components/stocks/stocks.component";

import { TypeMaterialLearnsComponent } from "./components/typeMaterialLearns/typeMaterialLearns.component";

import { TypeMaterialsComponent } from "./components/typeMaterials/typeMaterials.component";
import { GrpMaterialsComponent } from "./components/grpMaterials/grpMaterials.component";
import { GrpMaterialLearnsComponent } from "./components/grpMaterialLearns/grpMaterialLearns.component";
import { UnitsComponent } from "./components/units/units.component";
import { ExamineComponent } from "./components/examines/examines.component";
import { GrpDocumentsComponent } from "./components/grpdocuments/grpdocuments.component";
import { DocumentsComponent } from "./components/documents/documents.component";

import { StudentstatusComponent } from "./components/studentstatus/studentstatus.component";

import { StudentsComponent } from "./components/students/students.component";
import { StudentDatesComponent } from "./components/studentdates/studentdates.component";
import { StudentBirthdaysComponent } from "./components/studentbirthdays/studentbirthdays.component";
import { StudentEndClassComponent } from "./components/studentendclass/studentendclass.component";
import { IOStudentsComponent } from "./components/iostudents/iostudents.component";
import { PaymentsComponent } from "./components/payments/payments.component";
import { PaymentVouchersComponent } from "./components/paymentvouchers/paymentvouchers.component";
import { PaymentListsComponent } from "./components/paymentlists/paymentlists.component";
import { PaymentDetailListsComponent } from "./components/paymentdetaillists/paymentdetaillists.component";
import { InventoriesListsComponent } from "./components/inventorieslists/inventorieslists.component";
import { InventoriesComponent } from "./components/inventories/inventories.component";
import { ProfitsComponent } from "./components/profits/profits.component";
import { ProfitsListsComponent } from "./components/profitslist/profitslist.component";

import { DeptsListsComponent } from "./components/deptslists/deptslists.component";
import { DeptsComponent } from "./components/depts/depts.component";

import { IODetailsComponent } from "./components/iodetails/iodetails.component";
import { WarehouseCardsComponent } from "./components/warehousecards/warehousecards.component";

import { IOStudenListComponent } from "./components/iostudentlists/iostudentlists.component";
import { IOInputsComponent } from "./components/ioinputs/ioinputs.component";
import { IOSummarizesComponent } from "./components/iosummarizes/iosummarizes.component";

import { GrpsuppliersComponent } from "./components/grpsuppliers/grpsuppliers.component";
import { SuppliersComponent } from "./components/suppliers/suppliers.component";
import { SupplierCusComponent } from "./components/suppliercus/suppliercus.component";
import { SupplierEmpsComponent } from "./components/supplieremps/supplieremps.component";
import { SupplierTeacherComponent } from "./components/supplierteachs/supplierteachs.component";

import { MaterialsComponent } from "./components/materials/materials.component";
import { MaterialLearnsComponent } from "./components/materialLearns/materialLearns.component";

import { ClassListsComponent } from "./components/classlists/classlists.component";
import { ClassesComponent } from "./components/classes/classes.component";
import { RoomsComponent } from "./components/rooms/rooms.component";
import { LevelclassesComponent } from "./components/levelclasses/levelclasses.component";
import { ShiftclassesComponent } from "./components/shiftclasses/shiftclasses.component";
import { ConsultantsComponent } from "./components/consultants/consultants.component";

import { mn_stocksComponent } from "./components/menu/mn-stocks.component";
import { mn_classesComponent } from "./components/menu/mn-classes.component";
import { mn_studentsComponent } from "./components/menu/mn-students.component";
import { mn_financesComponent } from "./components/menu/mn-finances.component";
import { mn_humansComponent } from "./components/menu/mn-humans.component";
import { mn_learningComponent } from "./components/menu/mn-learning.component";

import { mn_main_categoriesComponent } from "./components/menu/mn-main/mn-main-categories.component";
import { mn_main_educateComponent } from "./components/menu/mn-main/mn-main-educate.component";
import { mn_main_accountantComponent } from "./components/menu/mn-main/mn-main-accountant.component";
import { mn_main_stockComponent } from "./components/menu/mn-main/mn-main-stock.component";
import { mn_main_systemComponent } from "./components/menu/mn-main/mn-main-system.component";
import { mn_main_smsComponent } from "./components/menu/mn-main/mn-main-sms.component";
import { mn_main_documentComponent } from "./components/menu/mn-main/mn-main-document.component";

import { mn_main_functionComponent } from "./components/menu/mn-main/mn-main-function.component";
import { mn_main_reportComponent } from "./components/menu/mn-main/mn-main-report.component";

import { mn_student_categoriesComponent } from "./components/menu/mn-student/mn-student-categories.component";
import { mn_student_categories_studentComponent } from "./components/menu/mn-student/mn-student-categories-student.component";
import { mn_student_categories_classesComponent } from "./components/menu/mn-student/mn-student-categories-classes.component";
import { mn_student_reports_studentComponent } from "./components/menu/mn-student/mn-student-reports-student.component";
import { mn_student_main_studentComponent } from "./components/menu/mn-student/mn-student-main-student.component";
import { mn_student_sms_studentComponent } from "./components/menu/mn-student/mn-student-sms-student.component";

import { mn_stock_categories_stockComponent } from "./components/menu/mn-stock/mn-stock-categories-stock.component";
import { mn_stock_main_stockComponent } from "./components/menu/mn-stock/mn-stock-main-stock.component";
import { mn_stock_manages_stockComponent } from "./components/menu/mn-stock/mn-stock-manages-stock.component";
import { mn_stock_reports_stockComponent } from "./components/menu/mn-stock/mn-stock-reports-stock.component";
import { mn_stock_inputs_stockComponent } from "./components/menu/mn-stock/mn-stock-inputs-stock.component";

import { mn_accountant_main_accountantComponent } from "./components/menu/mn-accountant/mn-accountant-main-accountant.component";
import { mn_accountant_payment_accountantComponent } from "./components/menu/mn-accountant/mn-accountant-payment-accountant.component";
import { mn_accountant_promotion_accountantComponent } from "./components/menu/mn-accountant/mn-accountant-promotion-accountant.component";

import { SMSComponent } from "./components/sms/sms.component";
import { SMSSendComponent } from "./components/smssend/smssend.component";
import { AttendancesComponent } from "./components/attendances/attendances.component";

import { CategoriesComponent } from "./components/categories/categories.component";
import { CustomersComponent } from "./components/customers/customers.component";
import { ProductsComponent } from "./components/products/products.component";
import { OrdersComponent } from "./components/orders/orders.component";
import { SettingsComponent } from "./components/settings/settings.component";
import { AboutComponent } from "./components/about/about.component";
import { NotFoundComponent } from "./components/not-found/not-found.component";
import { AuthService } from './services/auth.service';
import { AuthGuard } from './services/auth-guard.service';



@NgModule({
    imports: [
        RouterModule.forRoot([
            { path: "", component: HomeComponent, canActivate: [AuthGuard], data: { title: "Home" } },
            { path: "login", component: LoginComponent, data: { title: "Login" } },
            { path: "mn-stocks", component: mn_stocksComponent, canActivate: [AuthGuard], data: { title: "stocks" } },
            { path: "mn-students", component: mn_studentsComponent, canActivate: [AuthGuard], data: { title: "students" } },
            { path: "mn-finances", component: mn_financesComponent, canActivate: [AuthGuard], data: { title: "finances" } },
            { path: "mn-humans", component: mn_humansComponent, canActivate: [AuthGuard], data: { title: "humans" } },
            { path: "mn-classes", component: mn_classesComponent, canActivate: [AuthGuard], data: { title: "classes" } },
            { path: "mn-learning", component: mn_learningComponent, canActivate: [AuthGuard], data: { title: "learning" } },


            { path: "mn_main_categories", component: mn_main_categoriesComponent, canActivate: [AuthGuard], data: { title: "Categories" } },
            { path: "mn_main_educate", component: mn_main_educateComponent, canActivate: [AuthGuard], data: { title: "Educates" } },
            { path: "mn_main_accountant", component: mn_main_accountantComponent, canActivate: [AuthGuard], data: { title: "Accountants" } },

            { path: "mn_main_stock", component: mn_main_stockComponent, canActivate: [AuthGuard], data: { title: "Stocks" } },
            { path: "mn_main_system", component: mn_main_systemComponent, canActivate: [AuthGuard], data: { title: "Systems" } },
            { path: "mn_main_sms", component: mn_main_smsComponent, canActivate: [AuthGuard], data: { title: "SMS" } },
            { path: "mn_main_document", component: mn_main_documentComponent, canActivate: [AuthGuard], data: { title: "Document" } },

            { path: "attendances", component: AttendancesComponent, canActivate: [AuthGuard], data: { title: "Attendances" } },
            { path: "mn_main_function", component: mn_main_functionComponent, canActivate: [AuthGuard], data: { title: "Functions" } },
            { path: "mn_main_report", component: mn_main_reportComponent, canActivate: [AuthGuard], data: { title: "Reports" } },

            { path: "mn_student_categories", component: mn_student_categoriesComponent, canActivate: [AuthGuard], data: { title: "learning" } },
            { path: "mn_student_categories_student", component: mn_student_categories_studentComponent, canActivate: [AuthGuard], data: { title: "learning" } },
            { path: "mn_student_categories_classes", component: mn_student_categories_classesComponent, canActivate: [AuthGuard], data: { title: "learning" } },
            { path: "mn_student_reports_student", component: mn_student_reports_studentComponent, canActivate: [AuthGuard], data: { title: "learning" } },
            { path: "mn_student_main_student", component: mn_student_main_studentComponent, canActivate: [AuthGuard], data: { title: "learning" } },
            { path: "mn_student_sms_student", component: mn_student_sms_studentComponent, canActivate: [AuthGuard], data: { title: "learning" } },

            { path: "mn_stock_categories_stock", component: mn_stock_categories_stockComponent, canActivate: [AuthGuard], data: { title: "stock" } },
            { path: "mn_stock_main_stock", component: mn_stock_main_stockComponent, canActivate: [AuthGuard], data: { title: "stock" } },
            { path: "mn_stock_manages_stock", component: mn_stock_manages_stockComponent, canActivate: [AuthGuard], data: { title: "stock" } },
            { path: "mn_stock_reports_stock", component: mn_stock_reports_stockComponent, canActivate: [AuthGuard], data: { title: "stock" } },
            { path: "mn_stock_inputs_stock", component: mn_stock_inputs_stockComponent, canActivate: [AuthGuard], data: { title: "stock" } },

            { path: "mn_accountant_payment_accountant", component: mn_accountant_payment_accountantComponent, canActivate: [AuthGuard], data: { title: "accountant" } },
            { path: "mn_accountant_promotion_accountant", component: mn_accountant_promotion_accountantComponent, canActivate: [AuthGuard], data: { title: "accountant" } },
            { path: "mn_accountant_main_accountant", component: mn_accountant_main_accountantComponent, canActivate: [AuthGuard], data: { title: "accountant" } },

            { path: "sms", component: SMSComponent, canActivate: [AuthGuard], data: { title: "SMS" } },
            { path: "smssend", component: SMSSendComponent, canActivate: [AuthGuard], data: { title: "Send SMS" } },
            { path: "accessrights", component: AccessRightsComponent, canActivate: [AuthGuard], data: { title: "Access Rights" } },
            { path: "userroles", component: UserRolesComponent, canActivate: [AuthGuard], data: { title: "User Roles" } },
            { path: "messengers", component: MessengerComponent, canActivate: [AuthGuard], data: { title: "Messenger" } },
            { path: "supports", component: SupportComponent, canActivate: [AuthGuard], data: { title: "Supports" } },

            { path: "featuregroups", component: FeatureGroupsComponent, canActivate: [AuthGuard], data: { title: "Feature Groups" } },
            { path: "usergroups", component: UserGroupsComponent, canActivate: [AuthGuard], data: { title: "User Groups" } },
            { path: "user-info", component: UserInfoComponent, canActivate: [AuthGuard], data: { title: "User Infos" } },
            { path: "users-management", component: UsersManagementComponent, canActivate: [AuthGuard], data: { title: "User Infos" } },

            { path: "branches", component: BranchesComponent, canActivate: [AuthGuard], data: { title: "Branches" } },
            { path: "stocks", component: StocksComponent, canActivate: [AuthGuard], data: { title: "Stocks" } },
            { path: "typemateriallearns", component: TypeMaterialLearnsComponent, canActivate: [AuthGuard], data: { title: "typeMaterialLearns" } },
            { path: "typematerials", component: TypeMaterialsComponent, canActivate: [AuthGuard], data: { title: "typeMaterials" } },
            { path: "grpmaterials", component: GrpMaterialsComponent, canActivate: [AuthGuard], data: { title: "grpMaterials" } },
            { path: "grpmateriallearns", component: GrpMaterialLearnsComponent, canActivate: [AuthGuard], data: { title: "grpMaterialLearns" } },
            { path: "units", component: UnitsComponent, canActivate: [AuthGuard], data: { title: "units" } },
            { path: "examines", component: ExamineComponent, canActivate: [AuthGuard], data: { title: "examines" } },

            { path: "studentstatus", component: StudentstatusComponent, canActivate: [AuthGuard], data: { title: "studentstatus" } },

            { path: "grpdocuments", component: GrpDocumentsComponent, canActivate: [AuthGuard], data: { title: "Group Documents" } },
            { path: "documents", component: DocumentsComponent, canActivate: [AuthGuard], data: { title: "Documents" } },

            { path: "students", component: StudentsComponent, canActivate: [AuthGuard], data: { title: "students" } },
            { path: "studentbirthdays", component: StudentBirthdaysComponent, canActivate: [AuthGuard], data: { title: "studentbirthdays" } },
            { path: "studentdates", component: StudentDatesComponent, canActivate: [AuthGuard], data: { title: "studentdates" } },
            { path: "studentendclass", component: StudentEndClassComponent, canActivate: [AuthGuard], data: { title: "students end classes" } },


            { path: "payments", component: PaymentsComponent, canActivate: [AuthGuard], data: { title: "payments" } },
            { path: "payment/:id", component: PaymentsComponent, canActivate: [AuthGuard], data: { title: "Payment Details" } },
            { path: "paymentio/:ioid", component: PaymentsComponent, canActivate: [AuthGuard], data: { title: "Payment" } },
            { path: "paymentvouchers", component: PaymentVouchersComponent, canActivate: [AuthGuard], data: { title: "payments" } },
            { path: "paymentvouchers/:id", component: PaymentVouchersComponent, canActivate: [AuthGuard], data: { title: "Receipt" } },
            { path: "paymentiovouchers/:ioid", component: PaymentVouchersComponent, canActivate: [AuthGuard], data: { title: "Receipt" } },
            { path: "paymentlist", component: PaymentListsComponent, canActivate: [AuthGuard], data: { title: "payments" } },
            { path: "paymentdetaillist", component: PaymentDetailListsComponent, canActivate: [AuthGuard], data: { title: "payments" } },

            { path: "inventorieslist", component: InventoriesListsComponent, canActivate: [AuthGuard], data: { title: "Dept" } },
            { path: "inventories", component: InventoriesComponent, canActivate: [AuthGuard], data: { title: "Update Dept" } },

            { path: "profitslist", component: ProfitsListsComponent, canActivate: [AuthGuard], data: { title: "Profits" } },
            { path: "profits", component: ProfitsComponent, canActivate: [AuthGuard], data: { title: "Update profits" } },

            { path: "deptslist", component: DeptsListsComponent, canActivate: [AuthGuard], data: { title: "Inventory" } },
            { path: "depts", component: DeptsComponent, canActivate: [AuthGuard], data: { title: "Update Inventory" } },

            { path: "iostudents", component: IOStudentsComponent, canActivate: [AuthGuard], data: { title: "iostudents" } },
            { path: "iostudentlist", component: IOStudenListComponent, canActivate: [AuthGuard], data: { title: "iostudentlist" } },
            { path: "ioinputs", component: IOInputsComponent, canActivate: [AuthGuard], data: { title: "ioinputs" } },
            { path: "iosummarizes", component: IOSummarizesComponent, canActivate: [AuthGuard], data: { title: "iosummarizes" } },
            { path: "iodetails", component: IODetailsComponent, canActivate: [AuthGuard], data: { title: "iodetails" } },
            { path: "warehousecards", component: WarehouseCardsComponent, canActivate: [AuthGuard], data: { title: "Warehouse Cards" } },
            { path: "warehousecards/:id", component: WarehouseCardsComponent, canActivate: [AuthGuard], data: { title: "Warehouse Cards" } },

            { path: "iooutput/:id", component: IOStudentsComponent, canActivate: [AuthGuard], data: { title: "Output" } },
            { path: "ioinput/:id", component: IOInputsComponent, canActivate: [AuthGuard], data: { title: "Input" } },

            { path: "materials", component: MaterialsComponent, canActivate: [AuthGuard], data: { title: "materials" } },
            { path: "materiallearns", component: MaterialLearnsComponent, canActivate: [AuthGuard], data: { title: "materialLearns" } },

            { path: "grpsuppliers", component: GrpsuppliersComponent, canActivate: [AuthGuard], data: { title: "grpsuppliers" } },
            { path: "suppliers", component: SuppliersComponent, canActivate: [AuthGuard], data: { title: "supplier" } },
            { path: "suppliercus", component: SupplierCusComponent, canActivate: [AuthGuard], data: { title: "customers" } },
            { path: "supplieremps", component: SupplierEmpsComponent, canActivate: [AuthGuard], data: { title: "employes" } },
            { path: "supplierteachs", component: SupplierTeacherComponent, canActivate: [AuthGuard], data: { title: "teachers" } },

            { path: "classlists", component: ClassListsComponent, canActivate: [AuthGuard], data: { title: "Classes" } },
            { path: "classdetails/:id", component: ClassesComponent, canActivate: [AuthGuard], data: { title: "Class" } },
            { path: "classes", component: ClassesComponent, canActivate: [AuthGuard], data: { title: "Classes" } },
            { path: "rooms", component: RoomsComponent, canActivate: [AuthGuard], data: { title: "rooms" } },
            { path: "levelclasses", component: LevelclassesComponent, canActivate: [AuthGuard], data: { title: "LevelClasses" } },
            { path: "shiftclasses", component: ShiftclassesComponent, canActivate: [AuthGuard], data: { title: "ShiftClasses" } },
            { path: "consultants", component: ConsultantsComponent, canActivate: [AuthGuard], data: { title: "consultants" } },

            { path: "categories", component: CategoriesComponent, canActivate: [AuthGuard], data: { title: "Categories" } },
            { path: "customers", component: CustomersComponent, canActivate: [AuthGuard], data: { title: "Customers" } },
            { path: "products", component: HomeComponent, canActivate: [AuthGuard], data: { title: "Home" } },
            { path: "orders", component: OrdersComponent, canActivate: [AuthGuard], data: { title: "Orders" } },
            { path: "settings", component: SettingsComponent, canActivate: [AuthGuard], data: { title: "Settings" } },
            { path: "about", component: AboutComponent, data: { title: "About Us" } },
            { path: "home", redirectTo: "/", pathMatch: "full" },
            { path: "**", component: NotFoundComponent, data: { title: "Page Not Found" } },
        ])
    ],
    exports: [
        RouterModule
    ],
    providers: [
        AuthService, AuthGuard
    ]
})
export class AppRoutingModule { }