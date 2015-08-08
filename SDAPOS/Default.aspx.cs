using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HOTBAL;

namespace SDAPOS
{
    public partial class _Default : System.Web.UI.Page
    {
        SDAFunctionsClass FunctionsClass = new SDAFunctionsClass();
        SDAMethods sqlClass = new SDAMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = HOTBAL.SDAPOSConstants.INTERNAL_NAME;

            if (!Page.IsPostBack)
            {
                txtDate.Text = DateTime.Now.ToShortDateString();
            }
            else
            {
                if (!String.IsNullOrEmpty(txtLName.Text))
                {
                    if (notACustomer.Checked)
                    {
                        Response.Redirect(HOTBAL.SDAPOSConstants.CART_URL + "?ID=0&Name=" + txtFName.Text + txtLName.Text + "&Action=");
                    }
                    else
                    {
                        List<HOTBAL.Student> studentName = sqlClass.GetStudentsByName(txtFName.Text, txtLName.Text, 1, false);

                        if (studentName != null)
                        {
                            if (studentName.Count > 0)
                            {
                                if (String.IsNullOrEmpty(studentName[0].ErrorMessage))
                                {
                                    if (studentName.Count > 1)
                                    {
                                        lblResults.Text = "Customer Results";
                                        lblCustomers.Text = "<table>";
                                        foreach (Student s in studentName)
                                        {
                                            lblCustomers.Text += "<tr><td class='reg'><a href='" + HOTBAL.SDAPOSConstants.CART_URL + "?ID=" + s.StudentId.ToString() + "&Action='>" + s.LastName + ", " + s.FirstName + "</a></td></tr>";
                                        }
                                        lblCustomers.Text += "</table>";
                                    }
                                    else
                                    {
                                        Response.Redirect(HOTBAL.SDAPOSConstants.CART_URL + "?ID=" + studentName[0].StudentId.ToString() + "&Action=");
                                    }
                                }
                                else
                                {
                                    lblError.Text = studentName[0].ErrorMessage;
                                }
                            }
                            else
                            {
                                lblError.Text = HOTBAL.SDAMessages.NO_STUDENT_FOUND;
                            }
                        }
                        else
                        {
                            lblError.Text = HOTBAL.SDAMessages.NO_STUDENT_FOUND;
                        }
                    }
                }
                else if (!String.IsNullOrEmpty(txtEmpNum.Text))
                {
                    Response.Redirect(HOTBAL.SDAPOSConstants.TRANSACTION_LOG_URL + "?ID=" + txtEmpNum.Text);
                }
            }
        }
    }
}
