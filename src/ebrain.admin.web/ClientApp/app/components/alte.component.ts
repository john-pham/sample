// ======================================
// Author: Ebrain Team
// Email:  johnpham@ymail.com
// Copyright (c) 2017 supperbrain.visualstudio.com
// 
// ==> Contact Us: supperbrain@outlook.com
// ======================================

import { Component, ViewEncapsulation, OnInit, OnDestroy, ViewChildren, AfterViewInit, QueryList, ElementRef } from "@angular/core";
import { Router, NavigationStart } from '@angular/router';
import { ToastyService, ToastyConfig, ToastOptions, ToastData } from 'ng2-toasty';
import { ModalDirective } from 'ngx-bootstrap/modal';

import { AlertService, AlertDialog, DialogType, AlertMessage, MessageSeverity } from '../services/alert.service';
import { NotificationService } from "../services/notification.service";
import { AppTranslationService } from "../services/app-translation.service";
import { AccountService } from '../services/account.service';
import { LocalStoreManager } from '../services/local-store-manager.service';
import { AppTitleService } from '../services/app-title.service';
import { AuthService } from '../services/auth.service';
import { ConfigurationService } from '../services/configuration.service';
import { Permission } from '../models/permission.model';
import { LoginComponent } from "../components/login/login.component";
import { MessengerService } from "../services/messengers.service";
import { Results } from "../models/results.model";
import { Messenger } from "../models/messenger.model";
import { Utilities } from "../services/utilities";
import { AccessRight } from "../models/accessright.model";
import { AccessRightsService } from "../share/services/access-rights.service";
import { SupportService } from "../users/services/support.service";
import { Support } from "../models/support.model";
import { DBkeys } from "../services/db-Keys";

require('jquery');
var alertify: any = require('../assets/scripts/alertify.js');
require('../assets/adminLTE-2.4.3/bower_components/jquery/dist/jquery.min.js');
require('../assets/adminLTE-2.4.3/bower_components/fastclick/lib/fastclick.js');
require('../assets/adminLTE-2.4.3/dist/js/adminlte.js');
require('../assets/adminLTE-2.4.3/bower_components/jquery-sparkline/dist/jquery.sparkline.min.js');
require('../assets/adminLTE-2.4.3/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js');
require('../assets/adminLTE-2.4.3/plugins/jvectormap/jquery-jvectormap-world-mill-en.js');
require('../assets/adminLTE-2.4.3/bower_components/jquery-slimscroll/jquery.slimscroll.js');
require('../assets/adminLTE-2.4.3/bower_components/chart.js/Chart.js');
require('../assets/adminLTE-2.4.3/dist/js/pages/dashboard2.js');
require('../assets/adminLTE-2.4.3/dist/js/demo.js');

@Component({
    selector: "superbrain-app",
    templateUrl: './alte.component.html',
    styleUrls: ['./alte.component.css',
        '../assets/styles/ngx-datatable.css',
        '../assets/styles/alertify.core.css',
        '../assets/styles/alertify.bootstrap.css',
        '../assets/styles/icon.css',
        '../assets/styles/ebrain.css',
        '../themes.css',
        '../assets/adminLTE-2.4.3/bower_components/font-awesome/css/font-awesome.min.css',
        '../assets/adminLTE-2.4.3/bower_components/Ionicons/css/ionicons.min.css',
        '../assets/adminLTE-2.4.3/bower_components/jvectormap/jquery-jvectormap.css',
        '../assets/adminLTE-2.4.3/dist/css/AdminLTE.min.css',
        '../assets/adminLTE-2.4.3/dist/css/skins/_all-skins.min.css'],
    encapsulation: ViewEncapsulation.None
})
export class AlteComponent implements OnInit, AfterViewInit {

    isAppLoaded: boolean;
    isUserLoggedIn: boolean;
    shouldShowLoginModal: boolean;
    removePrebootScreen: boolean;
    newNotificationCount = 0;
    appTitle = "Superbrain";
    appLogo = require("../assets/images/logo.png");

    stickyToasties: number[] = [];

    dataLoadingConsecutiveFailurs = 0;
    notificationsLoadingSubscription: any;

    /*
    @ViewChildren('loginModal,loginControl')
    modalLoginControls: QueryList<any>;

    loginModal: ModalDirective;
    loginControl: LoginComponent;
    */

    //messenger
    countMessenger: any = 0;
    countSupport: any = 0;
    messengers = [];
    accessRights = [];
    supports = [];

    get notificationsTitle() {

        let gT = (key: string) => this.translationService.getTranslation(key);

        if (this.newNotificationCount)
            return `${gT("app.Notifications")} (${this.newNotificationCount} ${gT("app.New")})`;
        else
            return gT("app.Notifications");
    }


    constructor(storageManager: LocalStoreManager, private toastyService: ToastyService, private toastyConfig: ToastyConfig,
        private accountService: AccountService, private alertService: AlertService, private notificationService: NotificationService,
        private appTitleService: AppTitleService, private messengerService: MessengerService, private supportService: SupportService,
        private authService: AuthService, private translationService: AppTranslationService, public configurations: ConfigurationService,
        public accessRightService: AccessRightsService, public router: Router) {

        storageManager.initialiseStorageSyncListener();

        translationService.addLanguages(["vn", "en", "fr", "de", "ar", "ko"]);
        translationService.setDefaultLanguage('vn');


        this.toastyConfig.theme = 'bootstrap';
        this.toastyConfig.position = 'top-right';
        this.toastyConfig.limit = 100;
        this.toastyConfig.showClose = true;

        this.appTitleService.appName = this.appTitle;
        this.shouldShowLoginModal = false;
    }


    ngAfterViewInit() {
        /*
        this.modalLoginControls.changes.subscribe((controls: QueryList<any>) => {
            controls.forEach(control => {
                if (control) {
                    if (control instanceof LoginComponent) {
                        this.loginControl = control;
                        this.loginControl.modalClosedCallback = () => this.loginModal.hide();
                    }
                    else {
                        this.loginModal = control;
                        this.loginModal.show();
                    }
                }
            });
        });
        */
    }

    /*
    onLoginModalShown() {
        this.alertService.showStickyMessage("Session Expired", "Your Session has expired. Please log in again", MessageSeverity.info);
    }

    onLoginModalHidden() {
        this.alertService.resetStickyMessage();
        this.loginControl.reset();
        this.shouldShowLoginModal = false;

        if (this.authService.isSessionExpired)
            this.alertService.showStickyMessage("Session Expired", "Your Session has expired. Please log in again to renew your session", MessageSeverity.warn);
    }

    onLoginModalHide() {
        this.alertService.resetStickyMessage();
    }
    */


    ngOnInit() {
        this.isUserLoggedIn = this.authService.isLoggedIn;
        /*
         * this.shouldShowLoginModal = !this.isUserLoggedIn;
         */

        // 1 sec to ensure all the effort to get the css animation working is appreciated :|, Preboot screen is removed .5 sec later
        setTimeout(() => this.isAppLoaded = true, 1000);
        setTimeout(() => this.removePrebootScreen = true, 1500);

        setTimeout(() => {
            if (this.isUserLoggedIn) {
                this.alertService.resetStickyMessage();

                //if (!this.authService.isSessionExpired)
                this.alertService.showMessage("Login", `Welcome back ${this.userName}!`, MessageSeverity.default);
                //else
                //    this.alertService.showStickyMessage("Session Expired", "Your Session has expired. Please log in again", MessageSeverity.warn);
                //
            }
        }, 2000);


        this.alertService.getDialogEvent().subscribe(alert => this.showDialog(alert));
        this.alertService.getMessageEvent().subscribe(message => this.showToast(message, false));
        this.alertService.getStickyMessageEvent().subscribe(message => this.showToast(message, true));

        this.authService.reLoginDelegate = () => this.shouldShowLoginModal = true;

        this.authService.getLoginStatusEvent().subscribe(isLoggedIn => {
            this.isUserLoggedIn = isLoggedIn;
            this.shouldShowLoginModal = false;

            if (this.isUserLoggedIn) {
                this.initNotificationsLoading();
            }
            else {
                this.unsubscribeNotifications();
            }

            setTimeout(() => {
                if (!this.isUserLoggedIn) {
                    this.alertService.showMessage("Session Ended!", "", MessageSeverity.default);
                }
            }, 500);
        });

        this.router.events.subscribe(event => {
            if (event instanceof NavigationStart) {
                let url = (<NavigationStart>event).url;

                if (url !== url.toLowerCase()) {
                    this.router.navigateByUrl((<NavigationStart>event).url.toLowerCase());
                }
            }
        });
    }

    ngOnDestroy() {
        this.unsubscribeNotifications();
    }


    private unsubscribeNotifications() {
        if (this.notificationsLoadingSubscription)
            this.notificationsLoadingSubscription.unsubscribe();
    }

    initNotificationsLoading() {

        this.notificationsLoadingSubscription = this.notificationService.getNewNotificationsPeriodically()
            .subscribe(notifications => {
                this.dataLoadingConsecutiveFailurs = 0;
                this.newNotificationCount = notifications.filter(n => !n.isRead).length;
            },
            error => {
                this.alertService.logError(error);

                if (this.dataLoadingConsecutiveFailurs++ < 20)
                    setTimeout(() => this.initNotificationsLoading(), 5000);
                else
                    this.alertService.showStickyMessage("Load Error", "Loading new notifications from the server failed!", MessageSeverity.error);
            });

        //load messenger
        this.getData();
    }
    
    getData() {
        this.messengerService.getnewmessenger().subscribe(
            resulted => this.onMessengerLoadSuccessful(resulted),
            error => this.onLoadFailed(error));

        this.supportService.search("", "", 0, 0).subscribe(
            resulted => this.onSupportLoadSuccessful(resulted),
            error => this.onLoadFailed(error));

    }

    private onSupportLoadSuccessful(resulted: Results<Support>) {
        this.countSupport = resulted.total;
        this.supports = resulted.list;
        this.alertService.stopLoadingMessage();
    }

    private onMessengerLoadSuccessful(resulted: Results<Messenger>) {
        this.countMessenger = resulted.total;
        this.messengers = resulted.list;
        this.alertService.stopLoadingMessage();
    }

    private onLoadFailed(error: any) {
        this.alertService.stopLoadingMessage();
        this.alertService.showStickyMessage("Load Error", `Unable to retrieve user data from the server.\r\nErrors: "${Utilities.getHttpResponseMessage(error)}"`,
            MessageSeverity.error, error);

    }

    markNotificationsAsRead() {

        let recentNotifications = this.notificationService.recentNotifications;

        if (recentNotifications.length) {
            this.notificationService.readUnreadNotification(recentNotifications.map(n => n.id), true)
                .subscribe(response => {
                    for (let n of recentNotifications) {
                        n.isRead = true;
                    }

                    this.newNotificationCount = recentNotifications.filter(n => !n.isRead).length;
                },
                error => {
                    this.alertService.logError(error);
                    this.alertService.showMessage("Notification Error", "Marking read notifications failed", MessageSeverity.error);

                });
        }
    }

    showDialog(dialog: AlertDialog) {

        alertify.set({
            labels: {
                ok: dialog.okLabel || "OK",
                cancel: dialog.cancelLabel || "Cancel"
            }
        });

        switch (dialog.type) {
            case DialogType.alert:
                alertify.alert(dialog.message);

                break
            case DialogType.confirm:
                alertify
                    .confirm(dialog.message, (e) => {
                        if (e) {
                            dialog.okCallback();
                        }
                        else {
                            if (dialog.cancelCallback)
                                dialog.cancelCallback();
                        }
                    });

                break;
            case DialogType.prompt:
                alertify
                    .prompt(dialog.message, (e, val) => {
                        if (e) {
                            dialog.okCallback(val);
                        }
                        else {
                            if (dialog.cancelCallback)
                                dialog.cancelCallback();
                        }
                    }, dialog.defaultValue);

                break;
        }
    }

    showToast(message: AlertMessage, isSticky: boolean) {

        if (message == null) {
            for (let id of this.stickyToasties.slice(0)) {
                this.toastyService.clear(id);
            }

            return;
        }

        let toastOptions: ToastOptions = {
            title: message.summary,
            msg: message.detail,
            timeout: isSticky ? 0 : 4000
        };


        if (isSticky) {
            toastOptions.onAdd = (toast: ToastData) => this.stickyToasties.push(toast.id);

            toastOptions.onRemove = (toast: ToastData) => {
                let index = this.stickyToasties.indexOf(toast.id, 0);

                if (index > -1) {
                    this.stickyToasties.splice(index, 1);
                }

                toast.onAdd = null;
                toast.onRemove = null;
            };
        }


        switch (message.severity) {
            case MessageSeverity.default: this.toastyService.default(toastOptions); break
            case MessageSeverity.info: this.toastyService.info(toastOptions); break;
            case MessageSeverity.success: this.toastyService.success(toastOptions); break;
            case MessageSeverity.error: this.toastyService.error(toastOptions); break
            case MessageSeverity.warn: this.toastyService.warning(toastOptions); break;
            case MessageSeverity.wait: this.toastyService.wait(toastOptions); break;
        }
    }
    closepopup() {

    }




    logout() {
        this.authService.logout();
        this.authService.redirectLogoutUser();
    }


    getYear() {
        return new Date().getUTCFullYear();
    }


    get userName(): string {
        return this.authService.currentUser ? this.authService.currentUser.userName : "";
    }


    get fullName(): string {
        return this.authService.currentUser ? this.authService.currentUser.fullName : "";
    }

    get profilerImage(): string {
        return this.authService.currentUser ? this.authService.currentUser.profilerImage : "";
    }

    get jobTitle(): string {
        return this.authService.currentUser ? this.authService.currentUser.jobTitle : "";
    }


    get canViewCustomers() {
        return this.accountService.userHasPermission(Permission.viewUsersPermission); //eg. viewCustomersPermission
    }

    get canViewProducts() {
        return this.accountService.userHasPermission(Permission.viewUsersPermission); //eg. viewProductsPermission
    }

    get canViewOrders() {
        return true; //eg. viewOrdersPermission
    }
}