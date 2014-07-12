using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

namespace PublicWebsite
{
    public partial class ShoppingCartPayPal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Prepopulate EC Token value if coming from another page
            if (Request.Params["token"] != null)
            {
               // Create request object
                GetExpressCheckoutDetailsRequestType request = new GetExpressCheckoutDetailsRequestType();
                request.Token = Request.Params["token"];

                // Invoke the API
                GetExpressCheckoutDetailsReq wrapper = new GetExpressCheckoutDetailsReq();
                wrapper.GetExpressCheckoutDetailsRequest = request;
                PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();
                GetExpressCheckoutDetailsResponseType ecResponse = service.GetExpressCheckoutDetails(wrapper);

                // Check for API return status
                setKeyResponseObjects(service, ecResponse);

                GetExpressCheckoutDetailsReq getECWrapper = new GetExpressCheckoutDetailsReq();
                getECWrapper.GetExpressCheckoutDetailsRequest = new GetExpressCheckoutDetailsRequestType(Request.Params["token"]);
                GetExpressCheckoutDetailsResponseType getECResponse = service.GetExpressCheckoutDetails(getECWrapper);

                // Create request object
                DoExpressCheckoutPaymentRequestType requestDo = new DoExpressCheckoutPaymentRequestType();
                DoExpressCheckoutPaymentRequestDetailsType requestDetails = new DoExpressCheckoutPaymentRequestDetailsType();
                requestDo.DoExpressCheckoutPaymentRequestDetails = requestDetails;

                requestDetails.PaymentDetails = getECResponse.GetExpressCheckoutDetailsResponseDetails.PaymentDetails;
                requestDetails.Token = Request.Params["token"];
                requestDetails.PayerID = Request.Params["PayerID"];
                requestDetails.PaymentAction = (PaymentActionCodeType)
                    Enum.Parse(typeof(PaymentActionCodeType), "SALE");

                // Invoke the API
                DoExpressCheckoutPaymentReq wrapperDo = new DoExpressCheckoutPaymentReq();
                wrapperDo.DoExpressCheckoutPaymentRequest = requestDo;
                DoExpressCheckoutPaymentResponseType doECResponse = service.DoExpressCheckoutPayment(wrapperDo);

                // Check for API return status
                setKeyResponseObjects(service, doECResponse);
            }
        }

        // A helper method used by APIResponse.aspx that returns select response parameters 
        // of interest. 
        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, GetExpressCheckoutDetailsResponseType response)
        {
            HttpContext CurrContext = HttpContext.Current;
            CurrContext.Items.Add("Response_apiName", "GetExpressChecoutDetails");
            CurrContext.Items.Add("Response_redirectURL", null);
            CurrContext.Items.Add("Response_requestPayload", service.getLastRequest());
            CurrContext.Items.Add("Response_responsePayload", service.getLastResponse());

            Dictionary<string, string> keyResponseParameters = new Dictionary<string, string>();

            //Selenium Test Case
            keyResponseParameters.Add("PayerID", Request.QueryString["PayerID"]);
            keyResponseParameters.Add("EC token", Request.QueryString["token"]);

            keyResponseParameters.Add("Correlation Id", response.CorrelationID);
            keyResponseParameters.Add("API Result", response.Ack.ToString());
            
            if (response.Ack.Equals(AckCodeType.FAILURE) ||
                (response.Errors != null && response.Errors.Count > 0))
            {
                CurrContext.Items.Add("Response_error", response.Errors);
            }
            else
            {
                CurrContext.Items.Add("Response_error", null);
            }
            CurrContext.Items.Add("Response_keyResponseObject", keyResponseParameters);
            Server.Transfer("../APIResponse.aspx");
        }

        // A helper method used by APIResponse.aspx that returns select response parameters 
        // of interest. You must process API response objects as applicable to your application
        private void setKeyResponseObjects(PayPalAPIInterfaceServiceService service, DoExpressCheckoutPaymentResponseType doECResponse)
        {
            Dictionary<string, string> responseParams = new Dictionary<string, string>();
            responseParams.Add("Correlation Id", doECResponse.CorrelationID);
            responseParams.Add("API Result", doECResponse.Ack.ToString());
            HttpContext CurrContext = HttpContext.Current;
            if (doECResponse.Ack.Equals(AckCodeType.FAILURE) ||
                (doECResponse.Errors != null && doECResponse.Errors.Count > 0))
            {
                CurrContext.Items.Add("Response_error", doECResponse.Errors);
            }
            else
            {
                CurrContext.Items.Add("Response_error", null);
                responseParams.Add("EC Token", doECResponse.DoExpressCheckoutPaymentResponseDetails.Token);
                responseParams.Add("Transaction Id", doECResponse.DoExpressCheckoutPaymentResponseDetails.PaymentInfo[0].TransactionID);
                responseParams.Add("Payment status", doECResponse.DoExpressCheckoutPaymentResponseDetails.PaymentInfo[0].PaymentStatus.ToString());
                if (doECResponse.DoExpressCheckoutPaymentResponseDetails.PaymentInfo[0].PendingReason != null)
                {
                    responseParams.Add("Pending reason", doECResponse.DoExpressCheckoutPaymentResponseDetails.PaymentInfo[0].PendingReason.ToString());
                }
                if (doECResponse.DoExpressCheckoutPaymentResponseDetails.BillingAgreementID != null)
                    responseParams.Add("Billing Agreement Id", doECResponse.DoExpressCheckoutPaymentResponseDetails.BillingAgreementID);
            }

            CurrContext.Items.Add("Response_keyResponseObject", responseParams);
            CurrContext.Items.Add("Response_apiName", "DoExpressChecoutPayment");
            CurrContext.Items.Add("Response_redirectURL", null);
            CurrContext.Items.Add("Response_requestPayload", service.getLastRequest());
            CurrContext.Items.Add("Response_responsePayload", service.getLastResponse());
            Server.Transfer("../APIResponse.aspx");
        }
    }
}