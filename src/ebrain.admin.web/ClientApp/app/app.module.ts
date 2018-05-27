// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { NgModule, ErrorHandler } from "@angular/core";
import { RouterModule } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpModule } from '@angular/http';

import 'bootstrap';
import { TranslateModule, TranslateLoader } from "@ngx-translate/core";
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { ToastyModule } from 'ng2-toasty';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TooltipModule } from "ngx-bootstrap/tooltip";
import { PopoverModule } from "ngx-bootstrap/popover";
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ChartsModule } from 'ng2-charts';

import { AppRoutingModule } from './app-routing.module';
import { AppErrorHandler } from './app-error.handler';
import { AppTitleService } from './services/app-title.service';
import { AppTranslationService, TranslateLanguageLoader } from './services/app-translation.service';
import { ConfigurationService } from './services/configuration.service';
import { AlertService } from './services/alert.service';
import { LocalStoreManager } from './services/local-store-manager.service';
import { EndpointFactory } from './services/endpoint-factory.service';
import { NotificationService } from './services/notification.service';
import { NotificationEndpoint } from './services/notification-endpoint.service';
import { AccountService } from './services/account.service';
import { AccountEndpoint } from './services/account-endpoint.service';

import { EqualValidator } from './directives/equal-validator.directive';
import { LastElementDirective } from './directives/last-element.directive';
import { AutofocusDirective } from './directives/autofocus.directive';
import { BootstrapTabDirective } from './directives/bootstrap-tab.directive';
import { BootstrapToggleDirective } from './directives/bootstrap-toggle.directive';
import { BootstrapSelectDirective } from './directives/bootstrap-select.directive';
import { BootstrapDatepickerDirective } from './directives/bootstrap-datepicker.directive';
import { GroupByPipe } from './pipes/group-by.pipe';
import { CurrencyPipe } from "./pipes/currency.pipe";

import { MessengerService } from './services/messengers.service';
import { MessengerEndpoint } from './services/messengers-endpoint.service';

import { SupportService } from './services/support.service';
import { SupportEndpoint } from './services/support-endpoint.service';

import { AccessRightsService } from './services/access-rights.service';
import { AccessRightsEndpoint } from './services/access-rights.endpoint';

import { UserRolesService } from './services/userroles.service';
import { UserRolesEndpoint } from './services/userroles-endpoint.service';

import { FeatureGroupsService } from './services/featuregroup.service';
import { FeatureGroupsEndpoint } from './services/featuregroup-endpoint.service';

import { UserGroupsService } from './services/usergroup.service';
import { UserGroupsEndpoint } from './services/usergroup-endpoint.service';

import { BranchesService } from './services/branches.service';
import { BranchesEndpoint } from './services/branches-endpoint.service';

import { DocumentsService } from './services/documents.service';
import { DocumentsEndpoint } from './services/documents-endpoint.service';

import { GrpDocumentsService } from './services/grpdocuments.service';
import { GrpDocumentsEndpoint } from './services/grpdocuments-endpoint.service';

import { GenderStudentService } from './services/genderstudent.service';
import { GenderStudentEndpoint } from './services/genderstudent-endpoint.service';

import { StocksService } from './services/stocks.service';
import { StocksEndpoint } from './services/stocks-endpoint.service';

import { TypeMaterialLearnsService } from './services/typeMaterialLearns.service';
import { TypeMaterialLearnsEndpoint } from './services/typeMaterialLearns-endpoint.service';

import { TypeMaterialsService } from './services/typeMaterials.service';
import { TypeMaterialsEndpoint } from './services/typeMaterials-endpoint.service';

import { GrpMaterialsService } from './services/grpMaterials.service';
import { GrpMaterialsEndpoint } from './services/grpMaterials-endpoint.service';

import { GrpMaterialLearnsService } from './services/grpMaterialLearns.service';
import { GrpMaterialLearnsEndpoint } from './services/grpMaterialLearns-endpoint.service';

import { UnitsService } from './services/units.service';
import { UnitsEndpoint } from './services/units-endpoint.service';

import { ExaminesService } from './services/examines.service';
import { ExaminesEndpoint } from './services/examines-endpoint.service';

import { StudentstatusService } from './services/studentstatus.service';
import { StudentstatusEndpoint } from './services/studentstatus-endpoint.service';

import { StudentsService } from './services/students.service';
import { StudentsEndpoint } from './services/students-endpoint.service';

import { IOStudentsService } from './services/iostudents.service';
import { IOStudentsEndpoint } from './services/iostudents-endpoint.service';

import { PurchaseOrderService } from './services/purchaseorders.service';
import { PurchaseOrdersEndpoint } from './services/purchaseorders-endpoint.service';

import { PaymentsService } from './services/payments.service';
import { PaymentsEndpoint } from './services/payments-endpoint.service';

import { InventoriesService } from './services/inventories.service';
import { InventoriesEndpoint } from './services/inventories-endpoint.service';

import { ProfitsService } from './services/profits.service';
import { ProfitsEndpoint } from './services/profits-endpoint.service';


import { DeptService } from './services/depts.service';
import { DeptsEndpoint } from './services/depts-endpoint.service';

import { IOStudentListService } from './services/iostudentlists.service';
import { IOStudentListEndpoint } from './services/iostudentlists-endpoint.service';

import { GrpsuppliersService } from './services/grpsuppliers.service';
import { GrpsuppliersEndpoint } from './services/grpsuppliers-endpoint.service';

import { MaterialsService } from './services/materials.service';
import { MaterialsEndpoint } from './services/materials-endpoint.service';

import { MaterialLearnsService } from './services/materialLearns.service';
import { MaterialLearnsEndpoint } from './services/materialLearns-endpoint.service';

import { SuppliersService } from './services/suppliers.service';
import { SuppliersEndpoint } from './services/suppliers-endpoint.service';

import { mn_stocksService } from './services/mn-stocks.service';
import { mn_stocksEndpoint } from './services/mn-stocks-endpoint.service';

import { mn_studentsService } from './services/mn-students.service';
import { mn_studentsEndpoint } from './services/mn-students-endpoint.service';

import { mn_financesService } from './services/mn-finances.service';
import { mn_financesEndpoint } from './services/mn-finances-endpoint.service';

import { mn_humansService } from './services/mn-humans.service';
import { mn_humansEndpoint } from './services/mn-humans-endpoint.service';

import { mn_classesService } from './services/mn-classes.service';
import { mn_classesEndpoint } from './services/mn-classes-endpoint.service';

import { mn_learningService } from './services/mn-learning.service';
import { mn_learningEndpoint } from './services/mn-learning-endpoint.service';

import { ClassesService } from './services/classes.service';
import { ClassesEndpoint } from './services/classes-endpoint.service';

import { TodayService } from './services/today.service';
import { TodayEndpoint } from './services/today-endpoint.service';

import { ClassStatusService } from './services/classstatus.service';
import { ClassStatusEndpoint } from './services/classstatus-endpoint.service';

import { AttendancesService } from './services/attendances.service';
import { AttendancesEndpoint } from './services/attendances-endpoint.service';

import { LevelclassesService } from './services/levelclasses.service';
import { LevelclassesEndpoint } from './services/levelclasses-endpoint.service';

import { RoomsService } from './services/rooms.service';
import { RoomsEndpoint } from './services/rooms-endpoint.service';

import { ShiftclassesService } from './services/shiftclasses.service';
import { ShiftclassesEndpoint } from './services/shiftclasses-endpoint.service';

import { ConsultantsService } from './services/consultants.service';
import { ConsultantsEndpoint } from './services/consultants-endpoint.service';

import { SMSService } from './services/sms.service';
import { SMSEndpoint } from './services/sms-endpoint.service';

import { AlteComponent } from "./components/alte.component";
import { LoginComponent } from "./components/login/login.component";
import { HomeComponent } from "./components/home/home.component";
import { BannerTopsComponent } from "./components/bannertops/bannertops.component";
import { AccessRightsComponent } from "./components/access-rights/access-rights";
import { UserRolesComponent } from "./components/userroles/userroles.component";
import { FeatureGroupsComponent } from "./components/featuregroups/featuregroups.component";
import { UserGroupsComponent } from "./components/usergroups/usergroups.component";
import { StatisticsChartComponent } from "./components/controls/statistics-chart.component";

import { MessengerComponent } from "./components/messengers/messengers.component";
import { SupportComponent } from "./components/supports/supports.component";
import { MessengerListComponent } from "./components/messengerlists/messengerlists.component";

import { BranchesComponent } from "./components/branches/branches.component";

import { GrpDocumentsComponent } from "./components/grpdocuments/grpdocuments.component";
import { DocumentsComponent } from "./components/documents/documents.component";

import { StocksComponent } from "./components/stocks/stocks.component";
import { GrpMaterialsComponent } from "./components/grpMaterials/grpMaterials.component";
import { GrpMaterialLearnsComponent } from "./components/grpMaterialLearns/grpMaterialLearns.component";
import { UnitsComponent } from "./components/units/units.component";
import { ExamineComponent } from "./components/examines/examines.component";

import { StudentstatusComponent } from "./components/studentstatus/studentstatus.component";

import { StudentDatesComponent } from "./components/studentdates/studentdates.component";
import { StudentBirthdaysComponent } from "./components/studentbirthdays/studentbirthdays.component";
import { StudentEndClassComponent } from "./components/studentendclass/studentendclass.component";

import { StudentsComponent } from "./components/students/students.component";

import { IOStudentsComponent } from "./components/iostudents/iostudents.component";
import { PurchaseOrdersComponent } from "./components/purchaseorders/purchaseorders.component";
import { PurHistoriesComponent } from "./components/purhistories/purhistories.component";
import { PurSummarizesComponent } from "./components/pursummarizes/pursummarizes.component";
import { PurDetailsComponent } from "./components/purdetails/purdetails.component";

import { IOStudenListComponent } from "./components/iostudentlists/iostudentlists.component";
import { IOInputsComponent } from "./components/ioinputs/ioinputs.component";
import { IOSummarizesComponent } from "./components/iosummarizes/iosummarizes.component";
import { IODetailsComponent } from "./components/iodetails/iodetails.component";
import { WarehouseCardsComponent } from "./components/warehousecards/warehousecards.component";

import { PaymentsComponent } from "./components/payments/payments.component";
import { PaymentVouchersComponent } from "./components/paymentvouchers/paymentvouchers.component";
import { PaymentListsComponent } from "./components/paymentlists/paymentlists.component";
import { PaymentDetailListsComponent } from "./components/paymentdetaillists/paymentdetaillists.component";

import { InventoriesListsComponent } from "./components/inventorieslists/inventorieslists.component";
import { InventoriesComponent } from "./components/inventories/inventories.component";

import { ProfitsListsComponent } from "./components/profitslist/profitslist.component";
import { ProfitsComponent } from "./components/profits/profits.component";

import { DeptsListsComponent } from "./components/deptslists/deptslists.component";
import { DeptsComponent } from "./components/depts/depts.component";

import { MaterialLearnsComponent } from "./components/materialLearns/materialLearns.component";
import { MaterialsComponent } from "./components/materials/materials.component";

import { SuppliersComponent } from "./components/suppliers/suppliers.component";
import { SupplierCusComponent } from "./components/suppliercus/suppliercus.component";
import { SupplierEmpsComponent } from "./components/supplieremps/supplieremps.component";
import { SupplierTeacherComponent } from "./components/supplierteachs/supplierteachs.component";

import { GrpsuppliersComponent } from "./components/grpsuppliers/grpsuppliers.component";
import { TypeMaterialLearnsComponent } from "./components/typeMaterialLearns/typeMaterialLearns.component";
import { TypeMaterialsComponent } from "./components/typeMaterials/typeMaterials.component";

import { ClassListsComponent } from "./components/classlists/classlists.component";
import { ClassesComponent } from "./components/classes/classes.component";
import { AttendancesComponent } from "./components/attendances/attendances.component";

import { RoomsComponent } from "./components/rooms/rooms.component";
import { LevelclassesComponent } from "./components/levelclasses/levelclasses.component";
import { ShiftclassesComponent } from "./components/shiftclasses/shiftclasses.component";
import { ConsultantsComponent } from "./components/consultants/consultants.component";

import { SMSComponent } from "./components/sms/sms.component";
import { SMSSendComponent } from "./components/smssend/smssend.component";

import { StudentMaterialsComponent } from "./components/studentmaterials/studentmaterials.component";

import { mn_main_categoriesComponent } from "./components/menu/mn-main/mn-main-categories.component";
import { mn_main_educateComponent } from "./components/menu/mn-main/mn-main-educate.component";
import { mn_main_accountantComponent } from "./components/menu/mn-main/mn-main-accountant.component";
import { mn_main_stockComponent } from "./components/menu/mn-main/mn-main-stock.component";
import { mn_main_systemComponent } from "./components/menu/mn-main/mn-main-system.component";
import { mn_main_smsComponent } from "./components/menu/mn-main/mn-main-sms.component";
import { mn_main_functionComponent } from "./components/menu/mn-main/mn-main-function.component";
import { mn_main_reportComponent } from "./components/menu/mn-main/mn-main-report.component";
import { mn_main_documentComponent } from "./components/menu/mn-main/mn-main-document.component";

import { mn_stocksComponent } from "./components/menu/mn-stocks.component";
import { mn_studentsComponent } from "./components/menu/mn-students.component";
import { mn_financesComponent } from "./components/menu/mn-finances.component";
import { mn_humansComponent } from "./components/menu/mn-humans.component";
import { mn_classesComponent } from "./components/menu/mn-classes.component";
import { mn_learningComponent } from "./components/menu/mn-learning.component";

import { mn_student_main_studentComponent } from "./components/menu/mn-student/mn-student-main-student.component";
import { mn_student_categoriesComponent } from "./components/menu/mn-student/mn-student-categories.component";
import { mn_student_categories_studentComponent } from "./components/menu/mn-student/mn-student-categories-student.component";
import { mn_student_categories_classesComponent } from "./components/menu/mn-student/mn-student-categories-classes.component";
import { mn_student_reports_studentComponent } from "./components/menu/mn-student/mn-student-reports-student.component";
import { mn_student_sms_studentComponent } from "./components/menu/mn-student/mn-student-sms-student.component";

import { mn_stock_categories_stockComponent } from "./components/menu/mn-stock/mn-stock-categories-stock.component";
import { mn_stock_main_stockComponent } from "./components/menu/mn-stock/mn-stock-main-stock.component";
import { mn_stock_manages_stockComponent } from "./components/menu/mn-stock/mn-stock-manages-stock.component";
import { mn_stock_reports_stockComponent } from "./components/menu/mn-stock/mn-stock-reports-stock.component";
import { mn_stock_inputs_stockComponent } from "./components/menu/mn-stock/mn-stock-inputs-stock.component";

import { mn_accountant_main_accountantComponent } from "./components/menu/mn-accountant/mn-accountant-main-accountant.component";
import { mn_accountant_payment_accountantComponent } from "./components/menu/mn-accountant/mn-accountant-payment-accountant.component";
import { mn_accountant_promotion_accountantComponent } from "./components/menu/mn-accountant/mn-accountant-promotion-accountant.component";

import { CategoriesComponent } from "./components/categories/categories.component";
import { CustomersComponent } from "./components/customers/customers.component";
import { ProductsComponent } from "./components/products/products.component";
import { OrdersComponent } from "./components/orders/orders.component";
import { SettingsComponent } from "./components/settings/settings.component";
import { AboutComponent } from "./components/about/about.component";
import { NotFoundComponent } from "./components/not-found/not-found.component";

import { PageControllerComponent } from "./components/controls/page-controller.component";
import { BannerDemoComponent } from "./components/controls/banner-demo.component";
import { TodoDemoComponent } from "./components/controls/todo-demo.component";
import { StatisticsDemoComponent } from "./components/controls/statistics-demo.component";
import { NotificationsViewerComponent } from "./components/controls/notifications-viewer.component";
import { SearchBoxComponent } from "./components/controls/search-box.component";
import { SearChartComponent } from "./components/controls/search-chart.component";
import { UserInfoComponent } from "./components/controls/user-info.component";
import { UserPreferencesComponent } from "./components/controls/user-preferences.component";
import { UsersManagementComponent } from "./components/controls/users-management.component";
import { RolesManagementComponent } from "./components/controls/roles-management.component";
import { ClassStudentComponent } from "./components/classstudents/classstudents.component";
import { ClassExamineComponent } from "./components/classexamines/classexamines.component";
import { RoleEditorComponent } from "./components/controls/role-editor.component";
import { CurrencyMaskModule } from "ng2-currency-mask";
import { CURRENCY_MASK_CONFIG } from "ng2-currency-mask/src/currency-mask.config";
import { CustomCurrencyMaskConfig } from "./currency-mask/currency-mask.config";

@NgModule({
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        HttpModule,
        FormsModule,
        AppRoutingModule,
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useClass: TranslateLanguageLoader
            }
        }),
        NgxDatatableModule,
        ToastyModule.forRoot(),
        TooltipModule.forRoot(),
        PopoverModule.forRoot(),
        BsDropdownModule.forRoot(),
        CarouselModule.forRoot(),
        BsDatepickerModule.forRoot(),
        ModalModule.forRoot(),
        ChartsModule,
        CurrencyMaskModule
    ],
    declarations: [
        AlteComponent,
        LoginComponent,
        HomeComponent,
        BannerTopsComponent,
        AccessRightsComponent,
        UserRolesComponent,
        FeatureGroupsComponent,
        UserGroupsComponent,
        StatisticsChartComponent,

        MessengerComponent,
        MessengerListComponent,
        SupportComponent,

        BranchesComponent,
        StocksComponent,
        GrpDocumentsComponent,
        DocumentsComponent,
        UnitsComponent,
        ExamineComponent,
        StudentstatusComponent,
        StudentsComponent,
        StudentDatesComponent,
        StudentBirthdaysComponent,
        StudentEndClassComponent,
        IOStudentsComponent,
        PurchaseOrdersComponent,
        PurSummarizesComponent,
        PurHistoriesComponent,
        PurDetailsComponent,

        IOInputsComponent,

        PaymentsComponent,
        PaymentVouchersComponent,
        PaymentListsComponent,
        PaymentDetailListsComponent,
        InventoriesListsComponent,
        InventoriesComponent,

        ProfitsListsComponent,
        ProfitsComponent,

        DeptsComponent,
        DeptsListsComponent,

        IOStudenListComponent,
        IOSummarizesComponent,
        IODetailsComponent,
        WarehouseCardsComponent,

        MaterialsComponent,
        MaterialLearnsComponent,

        SuppliersComponent,
        SupplierCusComponent,
        SupplierEmpsComponent,
        SupplierTeacherComponent,

        TypeMaterialLearnsComponent,
        TypeMaterialsComponent,
        GrpMaterialsComponent,
        GrpMaterialLearnsComponent,
        GrpsuppliersComponent,

        ClassListsComponent,
        ClassesComponent,
        ClassStudentComponent,
        ClassExamineComponent,

        RoomsComponent,
        LevelclassesComponent,
        ShiftclassesComponent,
        ConsultantsComponent,
        SMSComponent,
        SMSSendComponent,
        AttendancesComponent,

        StudentMaterialsComponent,

        mn_main_accountantComponent,
        mn_main_categoriesComponent,
        mn_main_educateComponent,
        mn_main_stockComponent,
        mn_main_systemComponent,
        mn_main_smsComponent,
        mn_main_functionComponent,
        mn_main_reportComponent,
        mn_main_documentComponent,

        mn_stocksComponent,
        mn_studentsComponent,
        mn_financesComponent,
        mn_humansComponent,
        mn_classesComponent,
        mn_learningComponent,

        mn_student_categoriesComponent,
        mn_student_categories_studentComponent,
        mn_student_categories_classesComponent,
        mn_student_reports_studentComponent,
        mn_student_main_studentComponent,
        mn_student_sms_studentComponent,

        mn_stock_categories_stockComponent,
        mn_stock_main_stockComponent,
        mn_stock_manages_stockComponent,
        mn_stock_reports_stockComponent,
        mn_stock_inputs_stockComponent,

        mn_accountant_main_accountantComponent,
        mn_accountant_payment_accountantComponent,
        mn_accountant_promotion_accountantComponent,

        CategoriesComponent,
        CustomersComponent,
        ProductsComponent,
        OrdersComponent,
        PageControllerComponent,
        SettingsComponent,
        UsersManagementComponent, UserInfoComponent, UserPreferencesComponent,
        RolesManagementComponent, RoleEditorComponent,
        AboutComponent,
        NotFoundComponent,
        NotificationsViewerComponent,
        SearchBoxComponent,
        SearChartComponent,
        StatisticsDemoComponent, TodoDemoComponent, BannerDemoComponent,
        EqualValidator,
        LastElementDirective,
        AutofocusDirective,
        BootstrapTabDirective,
        BootstrapToggleDirective,
        BootstrapSelectDirective,
        BootstrapDatepickerDirective,
        GroupByPipe,
        CurrencyPipe
    ],
    providers: [
        { provide: 'BASE_URL', useFactory: getBaseUrl },
        { provide: ErrorHandler, useClass: AppErrorHandler },
        { provide: CURRENCY_MASK_CONFIG, useValue: CustomCurrencyMaskConfig },
        AlertService,
        ConfigurationService,
        AppTitleService,
        AppTranslationService,
        NotificationService,
        NotificationEndpoint,
        AccountService,
        AccountEndpoint,
        LocalStoreManager,
        EndpointFactory,
        AccessRightsService,
        AccessRightsEndpoint,
        MessengerService,
        MessengerEndpoint,
        SupportService,
        SupportEndpoint,
        UserRolesService,
        UserRolesEndpoint,
        UserGroupsEndpoint,
        UserGroupsService,
        FeatureGroupsEndpoint,
        FeatureGroupsService,

        BranchesService,
        BranchesEndpoint,
        DocumentsService,
        DocumentsEndpoint,
        GrpDocumentsService,
        GrpDocumentsEndpoint,

        GenderStudentService,
        GenderStudentEndpoint,

        StocksService,
        StocksEndpoint,

        TypeMaterialLearnsService,
        TypeMaterialLearnsEndpoint,

        TypeMaterialsService,
        TypeMaterialsEndpoint,

        GrpMaterialsService,
        GrpMaterialsEndpoint,

        GrpMaterialLearnsService,
        GrpMaterialLearnsEndpoint,

        UnitsService,
        UnitsEndpoint,
        ExaminesService,
        ExaminesEndpoint,

        StudentstatusService,
        StudentstatusEndpoint,

        StudentsService,
        StudentsEndpoint,

        IOStudentsService,
        IOStudentsEndpoint,

        PurchaseOrderService,
        PurchaseOrdersEndpoint,

        PaymentsService,
        PaymentsEndpoint,

        InventoriesService,
        InventoriesEndpoint,

        ProfitsService,
        ProfitsEndpoint,

        DeptService,
        DeptsEndpoint,

        IOStudentListService,
        IOStudentListEndpoint,

        GrpsuppliersService,
        GrpsuppliersEndpoint,

        MaterialsService,
        MaterialsEndpoint,

        MaterialLearnsService,
        MaterialLearnsEndpoint,

        SuppliersService,
        SuppliersEndpoint,

        ClassesService,
        ClassesEndpoint,

        ClassStatusService,
        ClassStatusEndpoint,

        AttendancesService,
        AttendancesEndpoint,

        TodayService,
        TodayEndpoint,
        RoomsService,
        RoomsEndpoint,

        LevelclassesService,
        LevelclassesEndpoint,

        ShiftclassesService,
        ShiftclassesEndpoint,

        ConsultantsService,
        ConsultantsEndpoint,

        SMSService,
        SMSEndpoint,

        mn_main_categoriesComponent,

        mn_stocksService,
        mn_stocksEndpoint,
        mn_studentsService,
        mn_studentsEndpoint,
        mn_financesService,
        mn_financesEndpoint,
        mn_humansService,
        mn_humansEndpoint,
        mn_classesService,
        mn_classesEndpoint,
        mn_learningService,
        mn_learningEndpoint,

    ],
    bootstrap: [AlteComponent]
})
export class AppModule {
}

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
