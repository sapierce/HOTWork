﻿using System;
using System.Collections.Generic;
using System.Web;

namespace HOTBAL
{
    public class TansConstants
    {
        public const string PUBLIC_NAME = "HOT Tropical Tans";

        public const string INTERNAL_NAME = "HOT Tropical Tans Scheduling";

        public const string ROOT_URL = "http://www.hottropicaltans.net";

        #region Internal URLs
        public const string MAIN_INTERNAL_URL = "/Schedule/Default.aspx";

        public const string CUSTOMER_ADD_INTERNAL_URL = "/Schedule/CustomerAdd.aspx";

        public const string SEARCH_INTERNAL_URL = "/Schedule/Search.aspx";

        public const string PROBLEMS_INTERNAL_URL = "/Schedule/Problems.aspx";

        public const string ADMIN_INTERNAL_URL = "/Schedule/Admin/Default.aspx";

        public const string ADMIN_SITE_NOTICE_INTERNAL_URL = "/Schedule/Admin/SiteNotice.aspx";

        public const string ADMIN_SITE_PWD_INTERNAL_URL = "/Schedule/Admin/SitePasswords.aspx";

        public const string ADMIN_CUST_REPLY_INTERNAL_URL = "/Schedule/Admin/CustomerReply.aspx";

        public const string ADMIN_CUST_COMB_INTERNAL_URL = "/Schedule/Admin/CustomerCombine.aspx";

        public const string ADMIN_EMP_NOTES_URL = "/Schedule/Admin/EmployeeNotes.aspx";

        public const string ADMIN_ADD_BED_URL = "/Schedule/Admin/BedsAdd.aspx";

        public const string ADMIN_ADD_PACKAGE_URL = "/Schedule/Admin/PackageAdd.aspx";

        public const string ADMIN_ADD_PRODUCT_URL = "/Schedule/Admin/ProductAdd.aspx";

        public const string ADMIN_ADD_SPECIAL_URL = "/Schedule/Admin/SpecialsAdd.aspx";

        public const string ADMIN_ADD_EMP_URL = "/Schedule/Admin/EmployeeAdd.aspx";

        public const string ADMIN_ADD_EMP_NOTE_URL = "/Schedule/Admin/EmployeeNotesAdd.aspx";

        public const string ADMIN_EDIT_BED_URL = "/Schedule/Admin/BedsEdit.aspx";

        public const string ADMIN_EDIT_PACKAGE_URL = "/Schedule/Admin/PackageEdit.aspx";

        public const string ADMIN_EDIT_SPECIAL_URL = "/Schedule/Admin/SpecialsEdit.aspx";

        public const string ADMIN_EDIT_PRODUCT_URL = "/Schedule/Admin/ProductEdit.aspx";

        public const string ADMIN_EDIT_HOURS_URL = "/Schedule/Admin/HoursEdit.aspx";

        public const string ADMIN_EDIT_EMP_URL = "/Schedule/Admin/EmployeeEdit.aspx";

        public const string ADMIN_EDIT_EMP_NOTE_URL = "/Schedule/Admin/EmployeeNotesEdit.aspx";

        public const string ADMIN_DEL_PROD_URL = "/Schedule/Admin/ProductsDeleted.aspx";

        public const string ADMIN_DEL_EMP_NOTE_URL = "/Schedule/Admin/EmployeeNotesDelete.aspx";

        public const string ADMIN_RPT_EMP_WORKED_URL = "/Schedule/Admin/Reports/EmployeeClockedHours.aspx";

        public const string ADMIN_RPT_EMP_SCHED_URL = "/Schedule/Admin/Reports/EmployeeScheduledHours.aspx";

        public const string ADMIN_RPT_BED_URL = "/Schedule/Admin/Reports/BedInformation.aspx";

        public const string ADMIN_RPT_PROD_INV_URL = "/Schedule/Admin/Reports/ProductInventory.aspx";

        public const string EMP_INTERNAL_URL = "/Schedule/Employees/Default.aspx";

        public const string EMP_INFO_INTERNAL_URL = "/Schedule/Employees/EmployeeInformation.aspx";

        public const string EMP_NOTES_INTERNAL_URL = "/Schedule/Employees/EmployeeNotes.aspx";

        public const string EMP_PROD_CNTS_INTERNAL_URL = "/Schedule/Employees/ProductEmployeeCounts.aspx";

        public const string CUSTOMER_INFO_INTERNAL_URL = "/Schedule/CustomerInfo.aspx";

        public const string CUSTOMER_INFO_EDIT_INTERNAL_URL = "/Schedule/CustomerEdit.aspx";

        public const string CUSTOMER_INFO_DELETE_INTERNAL_URL = "/Schedule/CustomerDelete.aspx";

        public const string CUSTOMER_ONLINE_INFO_INTERNAL_URL = "/Schedule/CustomerOnlineInfo.aspx";

        public const string CUSTOMER_TAN_HISTORY_INTERNAL_URL = "/Schedule/CustomerTanHistory.aspx";

        public const string CUSTOMER_MASSAGE_HISTORY_INTERNAL_URL = "/Schedule/CustomerMassageHistory.aspx";

        public const string CUSTOMER_BILL_HISTORY_INTERNAL_URL = "/Schedule/CustomerBillingHistory.aspx";

        public const string CUSTOMER_NOTES_INTERNAL_URL = "/Schedule/CustomerNotes.aspx";

        public const string ADD_APPT_INTERNAL_URL = "/Schedule/AppointmentAdd.aspx";

        public const string EDIT_APPT_INTERNAL_URL = "/Schedule/AppointmentEdit.aspx";

        public const string DELETE_APPT_INTERNAL_URL = "/Schedule/AppointmentDelete.aspx";

        public const string ADD_APPT_MASSAGE_INTERNAL_URL = "/Schedule/AppointmentMassageAdd.aspx";

        public const string EDIT_APPT_MASSAGE_INTERNAL_URL = "/Schedule/AppointmentMassageEdit.aspx";

        public const string DELETE_APPT_MASSAGE_INTERNAL_URL = "/Schedule/AppointmentMassageDelete.aspx";

        public const string CUSTOMER_TRANS_INTERNAL_URL = "/Schedule/CustomerTransactions.aspx";

        public const string PRODUCT_INFO_INTERNAL_URL = "/Schedule/ProductInformation.aspx";

        #endregion

        #region Mobile URLs
        //public const string CUSTOMER_LOGON_MOBILE_URL = "/m/Logon.aspx";

        public const string CUSTOMER_LOGON_MOBILE_URL = "/m/Default.aspx";

        //public const string CUSTOMER_INFO_MOBILE_URL = "/m/MemberInfo.aspx";

        public const string CUSTOMER_INFO_MOBILE_URL = "/m/Default.aspx";

        //public const string ABOUT_MOBILE_URL = "/m/About.aspx";

        public const string ABOUT_MOBILE_URL = "/m/Default.aspx";

        //public const string BEDS_MOBILE_URL = "/m/Beds.aspx";

        public const string BEDS_MOBILE_URL = "/m/Default.aspx";

        //public const string PRODUCTS_MOBILE_URL = "/m/Products.aspx";

        public const string PRODUCTS_MOBILE_URL = "/m/Default.aspx";

        //public const string PRODUCTS_DETAILS_MOBILE_URL = "/m/ProductDetails.aspx";

        public const string PRODUCTS_DETAILS_MOBILE_URL = "/m/Default.aspx";

        //public const string ADD_APPT_MOBILE_URL = "/m/AddAppointment.aspx";

        public const string ADD_APPT_MOBILE_URL = "/m/Default.aspx";

        //public const string DELETE_APPT_MOBILE_URL = "/m/DeleteAppointment.aspx";

        public const string DELETE_APPT_MOBILE_URL = "/m/Default.aspx";
        #endregion

        #region Public URLs
        public const string HOME_PUBLIC_URL = "/Default.aspx";

        //public const string PRIVACY_PUBLIC_URL = "/PrivacyPolicy.aspx";

        public const string PRIVACY_PUBLIC_URL = "/Default.aspx";

        //public const string TOS_PUBLIC_URL = "/TermsOfService.aspx";

        public const string TOS_PUBLIC_URL = "/Default.aspx";

        //public const string ABOUT_PUBLIC_URL = "/About.aspx";

        public const string ABOUT_PUBLIC_URL = "/Default.aspx";

        public const string CONTACT_PUBLIC_URL = "/ContactUs.aspx";

        //public const string PACKAGES_PUBLIC_URL = "/Packages.aspx";

        public const string PACKAGES_PUBLIC_URL = "/Default.aspx";

        //public const string ACCESSORIES_PUBLIC_URL = "/Accessories.aspx";

        public const string ACCESSORIES_PUBLIC_URL = "/Default.aspx";

        //public const string SPECIALS_PUBLIC_URL = "/Specials.aspx";

        public const string SPECIALS_PUBLIC_URL = "/Default.aspx";

        //public const string TIPS_PUBLIC_URL = "/Tips.aspx";

        public const string TIPS_PUBLIC_URL = "/Default.aspx";

        //public const string MEMBERS_PUBLIC_URL = "/Members.aspx";

        public const string MEMBERS_PUBLIC_URL = "/Default.aspx";

        //public const string PRODUCT_PUBLIC_URL = "/Product.aspx";

        public const string PRODUCT_PUBLIC_URL = "/Default.aspx";

        //public const string LOTION_PUBLIC_URL = "/Lotions.aspx";

        public const string LOTION_PUBLIC_URL = "/Default.aspx";

        //public const string LOTIONS_PUBLIC_URL = "/LotionsList.aspx";

        public const string LOTIONS_PUBLIC_URL = "/Default.aspx";

        //public const string LIP_BALM_PUBLIC_URL = "/LipBalms.aspx";

        public const string LIP_BALM_PUBLIC_URL = "/Default.aspx";

        //public const string GIFT_BAG_PUBLIC_URL = "/GiftBags.aspx";

        public const string GIFT_BAG_PUBLIC_URL = "/Default.aspx";

        //public const string MISC_PUBLIC_URL = "/Misc.aspx";

        public const string MISC_PUBLIC_URL = "/Default.aspx";

        //public const string CUSTOMER_LOGON_PUBLIC_URL = "/Members/Logon.aspx";

        public const string CUSTOMER_LOGON_PUBLIC_URL = "/Default.aspx";

        //public const string CUSTOMER_INFO_PUBLIC_URL = "/Members/MemberInfo.aspx";

        public const string CUSTOMER_INFO_PUBLIC_URL = "/Default.aspx";

        //public const string ADD_APPT_PUBLIC_URL = "/Members/AddAppointment.aspx";

        public const string ADD_APPT_PUBLIC_URL = "/Default.aspx";

        //public const string DELETE_APPT_PUBLIC_URL = "/Members/DeleteAppointment.aspx";

        public const string DELETE_APPT_PUBLIC_URL = "/Default.aspx";

        //public const string REGISTER_AGREE_PUBLIC_URL = "/Members/RegisterAgree.aspx";

        public const string REGISTER_AGREE_PUBLIC_URL = "/Default.aspx";

        //public const string REGISTER_NEW_PUBLIC_URL = "/Members/NewRegister.aspx";

        public const string REGISTER_NEW_PUBLIC_URL = "/Default.aspx";

        //public const string REGISTER_EXIST_PUBLIC_URL = "/Members/CurrentRegister.aspx";

        public const string REGISTER_EXIST_PUBLIC_URL = "/Default.aspx";

        //public const string SHOPPING_PUBLIC_URL = "/ShoppingCart.aspx";

        public const string SHOPPING_PUBLIC_URL = "/Default.aspx";

        //public const string SHOPPING_DONE_PUBLIC_URL = "/ShoppingCartDone.aspx";

        public const string SHOPPING_DONE_PUBLIC_URL = "/Default.aspx";

        //public const string SHOPPING_REDIRECT_PUBLIC_URL = "/ShoppingCartRedirect.aspx";

        public const string SHOPPING_REDIRECT_PUBLIC_URL = "/Default.aspx";

        public const string PAYPAL_RETURN_URL = "http://www.hottropicaltans.net/ShoppingCartPayPal.aspx";

        public const string PAYPAL_CANCEL_URL = "http://www.hottropicaltans.net/ShoppingCart.aspx";

        #endregion
    }
}