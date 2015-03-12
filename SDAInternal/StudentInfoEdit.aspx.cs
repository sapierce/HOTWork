using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTSelfDefense
{
    public partial class StudentInfoEdit : System.Web.UI.Page
    {
        HOTBAL.SDAFunctionsClass functionsClass = new HOTBAL.SDAFunctionsClass();
        HOTBAL.SDAMethods sqlClass = new HOTBAL.SDAMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.SDAConstants.INTERNAL_NAME + " - Edit Student Details";

            if (!Page.IsPostBack)
            {
                HOTBAL.Student studentInfo = new HOTBAL.Student();
                
                studentInfo = sqlClass.GetStudentInformation(Convert.ToInt32(Request.QueryString["ID"].ToString()));

                if (String.IsNullOrEmpty(studentInfo.Error))
                {
                    studentId.Text = studentInfo.ID.ToString();
                    firstName.Text = studentInfo.FirstName;
                    lastName.Text = studentInfo.LastName;

                    if (!String.IsNullOrEmpty(studentInfo.Suffix))
                        suffixName.Items.FindByValue(studentInfo.Suffix).Selected = true;

                    address.Text = studentInfo.Address;
                    city.Text = studentInfo.City;
                    state.Text = studentInfo.State;
                    zipCode.Text = studentInfo.ZipCode;
                    birthdayDate.Text = studentInfo.BirthDate.ToShortDateString();
                    emergencyContact.Text = studentInfo.EmergencyContact;
                    schoolList.Items.FindByValue(studentInfo.School.ToString()).Selected = true;
                    isPassing.Checked = studentInfo.Pass;
                    isPaid.Checked = studentInfo.Paid;
                    paymentPlan.Text = studentInfo.PaymentPlan;
                    paymentAmount.Text = studentInfo.PaymentAmount.ToString();
                    paymentDate.Text = studentInfo.PaymentDate.ToShortDateString();
                    studentNote.Text = studentInfo.Note;
                    isActive.Checked = studentInfo.Active;
                }
                else
                {
                    Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                    errorLabel.Text = studentInfo.Error;
                }
            }
        }

        protected void deleteCustomer_Click(object sender, EventArgs e)
        {
            Label errorLabel = (Label)this.Master.FindControl("errorMessage");
            if (Request.QueryString["ID"] != null)
            {
                int studentId = Convert.ToInt32(Request.QueryString["ID"].ToString());
                bool success = sqlClass.DeleteStudent(studentId);

                if (success)
                    Response.Redirect(HOTBAL.TansConstants.MAIN_INTERNAL_URL, false);
                else
                {
                    sqlClass.LogErrorMessage(new Exception("CannotDeleteStudent"), Request.QueryString["ID"].ToString(), "Internal: Delete Student");
                    errorLabel.Text = HOTBAL.SDAMessages.ERROR_DELETE_STUDENT + "<br />";
                }
            }
            else
            {
                sqlClass.LogErrorMessage(new Exception("NoStudentId"), "", "Internal: Delete Student");
                errorLabel.Text = HOTBAL.TansMessages.ERROR_CANNOT_FIND_CUSTOMER_INTERNAL + "<br />";
            }
        }

        protected void editCustomer_Click(object sender, EventArgs e)
        {
            bool updateResponse = sqlClass.UpdateStudent(Convert.ToInt32(Request.QueryString["ID"].ToString()), functionsClass.CleanUp(firstName.Text),
                    functionsClass.CleanUp(lastName.Text), suffixName.SelectedValue, functionsClass.CleanUp(address.Text),
                    functionsClass.CleanUp(city.Text), functionsClass.CleanUp(state.Text),
                    functionsClass.CleanUp(zipCode.Text), functionsClass.CleanUp(emergencyContact.Text), schoolList.SelectedValue,
                    Convert.ToDateTime(birthdayDate.Text), Convert.ToDateTime(paymentDate.Text),
                    paymentPlan.Text, Convert.ToDouble(paymentAmount.Text), functionsClass.CleanUp(studentNote.Text),
                    isActive.Checked, isPaid.Checked, isPassing.Checked);

            if (updateResponse)
                Response.Redirect(HOTBAL.SDAConstants.STUDENT_INFO_INTERNAL_URL + "?ID=" + Request.QueryString["ID"].ToString());
            else
            {
                Label errorLabel = (Label)this.Master.FindControl("errorMessage");
                errorLabel.Text = HOTBAL.SDAMessages.ERROR_GENERIC;
            }
        }
    }
}