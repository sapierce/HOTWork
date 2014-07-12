using System;
using System.Collections;
using System.Web;
using System.Web.UI.WebControls;

namespace HOTTropicalTans
{
    public partial class CustomerNotes : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                switch (Request.QueryString["Act"])
                {
                    case "Edit":
                        addNote.Visible = false;
                        editNote.Visible = true;
                        HOTBAL.CustomerNote noteResponse = sqlClass.GetNoteByNoteID(Convert.ToInt32(Request.QueryString["NID"].ToString()));

                        if (noteResponse != null)
                        {
                            editNoteText.Text = noteResponse.NoteText;
                            editOwedProduct.Checked = noteResponse.OwedProduct;
                            editOwesMoney.Checked = noteResponse.OwesMoney;
                            editCheckTransactions.Checked = noteResponse.NeedsUpgrade;
                        }
                        else
                        {
                            addNote.Visible = true;
                            editNote.Visible = false;
                        }
                        break;
                    case "Delete":
                        bool response = sqlClass.DeleteCustomerNote(Convert.ToInt32(Request.QueryString["NID"].ToString()));
                        if (response)
                        {
                            Response.Redirect(HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" + Request.QueryString["ID"], false);
                        }
                        else
                        {
                            addNote.Visible = false;
                            editNote.Visible = false;
                            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                            errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
                        }
                        break;
                    default:
                        addNote.Visible = true;
                        editNote.Visible = false;
                        break;
                }
            }
        }

        protected void addNoteSubmit_OnClick(object sender, EventArgs e)
        {
            bool response = sqlClass.AddCustomerNote(Convert.ToInt32(Request.QueryString["ID"].ToString()), noteText.Text, owedProduct.Checked, owesMoney.Checked, checkTransactions.Checked);
            if (response)
            {
                Response.Redirect(HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" + Request.QueryString["ID"], false);
            }
            else
            {
                addNote.Visible = false;
                editNote.Visible = false;
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
            }
        }

        protected void editNoteSubmit_OnClick(object sender, EventArgs e)
        {
            bool response = sqlClass.EditCustomerNote(Convert.ToInt32(Request.QueryString["NID"].ToString()), editNoteText.Text, editOwedProduct.Checked, editOwesMoney.Checked, editCheckTransactions.Checked);
            if (response)
            {
                Response.Redirect(HOTBAL.TansConstants.CUSTOMER_INFO_INTERNAL_URL + "?ID=" + Request.QueryString["ID"], false);
            }
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_GENERIC_INTERNAL;
            }
        }
    }
}