using System;
using System.Collections.Generic;
using System.Text;

namespace HOTBAL
{
    public class TansMessages
    {
        public const string SUCCESS_MESSAGE = "Success";

        public const string PASSWORD_SENT = "Your password has been reset and sent to your e-mail (@Email).";

        public const string ERROR_GENERIC = "The system you are trying to access is currently unavailable. Please try again later. If you continue to receive this error, please contact us at <a href='mailto:contact@hottropicaltans.com' class='center'>contact@hottropicaltans.net</a>.";

        public const string ERROR_GENERIC_INTERNAL = "The system you are trying to access is currently unavailable. Please try again. ";

        public const string ERROR_FORM_INVALID = "Some of the information on the form is invalid or missing. Please try again.";

        public const string ERROR_404 = "We're sorry, but the page you are looking for is not available. Please go back and try again, or visit the <a href='http://www.hottropicaltans.net'>home page</a>.";

        public const string ERROR_500 = "We're sorry, but the site is having difficulty processing your request. Please try again.";

        public const string ERROR_403 = "We're sorry, but you do not have permission to access this area.";

        public const string ERROR_ABOUT_HOURS = "Unable to fetch current operating hours. Please try again later.";

        public const string ERROR_LOGIN = "Sorry, we were unable to find that username/password.  Please try again later.";

        public const string ERROR_USER_EXISTS = "Sorry, that username is already taken.  Please select another.";

        public const string ERROR_EXISTING_ACCOUNT_SITE = "Our records show that you have already signed up for an online account. Would you like to be <a href='ForgotPassword.aspx'>reminded of the information</a>?";

        public const string ERROR_CANNOT_FIND_CUSTOMER_SITE = "Sorry, we are unable to locate your member information. Please check the name and try again, or if you are a new customer (have never tanned with HOT Tropical Tans before) please sign up <a href='NewRegister.aspx'>here</a>.";

        public const string ERROR_CANNOT_FIND_CUSTOMER_INTERNAL = "Unable to locate member information.";

        public const string ERROR_CANNOT_FIND_TAN = "Unable to locate tan information. Please try again.";

        public const string ERROR_INVALID_OLD_PASSWORD = "Sorry, the entered old password does not match what is on record.  Please try again.";

        public const string ERROR_INVALID_PASSWORD_INTERNAL = "Sorry, the entered password does not match what is on record. Please try again.";

        public const string ERROR_PACKAGES = "Sorry, there are no available packages for this bed.";

        public const string ERROR_PRODUCT_NO_INFO = "Unable to locate information regarding that product.";

        public const string ERROR_NO_PRODUCT_TYPE = "Sorry, currently no products are currently available for this category.";

        public const string ERROR_NO_PRODUCT_INFO = "Unable to locate information regarding that product.";

        public const string ERROR_ADD_CUSTOMER = "There was an error adding new customer information. Please try again.";

        public const string ERROR_INVALID_VERIFY_LINK = "Sorry, that verification link appears invalid or expired. If you would like to verify your e-mail address again, please <a href='Logon.aspx'>login again</a>.";

        public const string ERROR_DELETE_CUSTOMER = "Unable to successfully delete customer. Please try again.";

        public const string ERROR_EDIT_CUSTOMER = "Unable to successfully edit customer. Please try again.";

        public const string ERROR_NO_CUSTOMER_TRNS = "No customer transactions found.";

        public const string PLAN_SINGLES = "Please note that you must either buy or have bought a single tan online or in store.";

        public const string PLAN_UPGRADE = "Please note that you must either buy or have bought an upgrade online or in store.";

        public const string PLAN_MYSTIC = "Please note that you must buy or have bought a Mystic session online or in store.";

        public const string PLAN_GENERIC = "Please note that you must either buy or have bought a corresponding package online or in store.";

        public const string TAN_ALREADY_TAKEN = "We're sorry, another customer is already scheduled for @Date, @Time, in bed @Bed.  Please select a different time or bed.";

        public const string TAN_SAME_DAY_SITE = "We're sorry, you already have an appointment for @Date.  Please select another date, or delete your previous appointment and reschedule.";

        public const string TAN_SAME_DAY_STORE = "This customer already has an appointment for @Date.  Would you like to <a href='" + TansConstants.EDIT_APPT_INTERNAL_URL + "?UserID=@UserID&TanID=@TanID'>edit that appointment</a>?";

        public const string TAN_NOT24_SITE = "We're sorry, there must be 24 hours between all your tan times.  Please select a different time or date.<br><a href='/Tips.aspx#24hours'>Why can I only tan every 24 hours?</a>";

        public const string TAN_NOT24_STORE = "Customer has less than 24 hours between appointments. Please select a different time or date.";

        public const string NOTE_NEED_MYSTIC = "Needs to buy/have bought a Mystic session.";

        public const string NOTE_NEED_UPGRADE = "Needs to buy/have bought an upgrade.";

        public const string NOTE_NEED_SINGLES = "Needs to buy/have bought single tan.";

        public const string NOTE_NEED_PACKAGE_CHECK = "Check that package corresponds with bed.";

        public const string NOTE_CHECK_TRANS = "Check customer transactions for payment.";

        public const string NOTE_OWED_PROD = "Customer owed product. See <a href='" + TansConstants.TRANS_DETAILS_INTERNAL_URL + "?ID=@TransactionID'>transaction</a>.";

        public const string NOTE_OWES = "Customer has an unpaid transaction. See <a href='" + TansConstants.TRANS_DETAILS_INTERNAL_URL + "?ID=@TransactionID'>transaction</a>.";

        public const string SESSION_EXPIRED_MOBILE = "Your session has expired.  Please <a href='" + TansConstants.CUSTOMER_LOGON_MOBILE_URL + "' class='center'>log in</a> again.";

        public const string SESSION_EXPIRED_PUBLIC = "Your session has expired.  Please <a href='" + TansConstants.CUSTOMER_LOGON_PUBLIC_URL + "' class='center'>log in</a> again.";

        public const string MASSAGE_ALREADY_TAKEN = "We're sorry, another customer is already scheduled for @Date at @Time.  Please select a different time or date.";

        public const string ERROR_CANNOT_FIND_MASSAGE = "Unable to locate massage information. Please try again.";

    }
}
