using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HOTTropicalTans.admin
{
    public partial class HoursEdit : System.Web.UI.Page
    {
        HOTBAL.TansFunctionsClass functionsClass = new HOTBAL.TansFunctionsClass();
        HOTBAL.TansMethods sqlClass = new HOTBAL.TansMethods();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Page.Header.Title = HOTBAL.TansConstants.INTERNAL_NAME + " - Edit Store Hours";

                if (!functionsClass.isAdmin())
                    Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL);
                else
                {
                    LoadWacoTanningHours();
                    //LoadWacoMassageHours();
                }
            }
        }

        protected void LoadWacoTanningHours()
        {
            List<HOTBAL.Time> wacoTanningHours = sqlClass.GetLocationTimes("W", "T");

            foreach (HOTBAL.Time time in wacoTanningHours)
            {
                switch(time.TimeDay)
                {
                    case "MON":
                        wacoTanningMondayBeginTime.Text = (time.BeginTime == "CLOSED" ? "12:00 AM" : time.BeginTime);
                        wacoTanningMondayEndTime.Text = (time.EndTime == "CLOSED" ? "12:00 AM" : time.EndTime);
                        break;
                    case "TUE":
                        wacoTanningTuesdayBeginTime.Text = (time.BeginTime == "CLOSED" ? "12:00 AM" : time.BeginTime);
                        wacoTanningTuesdayEndTime.Text = (time.EndTime == "CLOSED" ? "12:00 AM" : time.EndTime);
                        break;
                    case "WED":
                        wacoTanningWednesdayBeginTime.Text = (time.BeginTime == "CLOSED" ? "12:00 AM" : time.BeginTime);
                        wacoTanningWednesdayEndTime.Text = (time.EndTime == "CLOSED" ? "12:00 AM" : time.EndTime);
                        break;
                    case "THU":
                        wacoTanningThursdayBeginTime.Text = (time.BeginTime == "CLOSED" ? "12:00 AM" : time.BeginTime);
                        wacoTanningThursdayEndTime.Text = (time.EndTime == "CLOSED" ? "12:00 AM" : time.EndTime);
                        break;
                    case "FRI":
                        wacoTanningFridayBeginTime.Text = (time.BeginTime == "CLOSED" ? "12:00 AM" : time.BeginTime);
                        wacoTanningFridayEndTime.Text = (time.EndTime == "CLOSED" ? "12:00 AM" : time.EndTime);
                        break;
                    case "SAT":
                        wacoTanningSaturdayBeginTime.Text = (time.BeginTime == "CLOSED" ? "12:00 AM" : time.BeginTime);
                        wacoTanningSaturdayEndTime.Text = (time.EndTime == "CLOSED" ? "12:00 AM" : time.EndTime);
                        break;
                    case "SUN":
                        wacoTanningSundayBeginTime.Text = (time.BeginTime == "CLOSED" ? "12:00 AM" : time.BeginTime);
                        wacoTanningSundayEndTime.Text = (time.EndTime == "CLOSED" ? "12:00 AM" : time.EndTime);
                        break;
                }
            }
        }

        //protected void LoadWacoMassageHours()
        //{
        //    List<HOTBAL.Time> wacoMassageHours = sqlClass.GetLocationTimes("W", "M");

        //    foreach (HOTBAL.Time time in wacoMassageHours)
        //    {
        //        switch (time.TimeDay)
        //        {
        //            case "MON":
        //                wacoMassageMondayBeginTime.Text = (time.BeginTime == "CLOSED" ? "12:00 AM" : time.BeginTime);
        //                wacoMassageMondayEndTime.Text = (time.EndTime == "CLOSED" ? "12:00 AM" : time.EndTime);
        //                break;
        //            case "TUE":
        //                wacoMassageTuesdayBeginTime.Text = (time.BeginTime == "CLOSED" ? "12:00 AM" : time.BeginTime);
        //                wacoMassageTuesdayEndTime.Text = (time.EndTime == "CLOSED" ? "12:00 AM" : time.EndTime);
        //                break;
        //            case "WED":
        //                wacoMassageWednesdayBeginTime.Text = (time.BeginTime == "CLOSED" ? "12:00 AM" : time.BeginTime);
        //                wacoMassageWednesdayEndTime.Text = (time.EndTime == "CLOSED" ? "12:00 AM" : time.EndTime);
        //                break;
        //            case "THU":
        //                wacoMassageThursdayBeginTime.Text = (time.BeginTime == "CLOSED" ? "12:00 AM" : time.BeginTime);
        //                wacoMassageThursdayEndTime.Text = (time.EndTime == "CLOSED" ? "12:00 AM" : time.EndTime);
        //                break;
        //            case "FRI":
        //                wacoMassageFridayBeginTime.Text = (time.BeginTime == "CLOSED" ? "12:00 AM" : time.BeginTime);
        //                wacoMassageFridayEndTime.Text = (time.EndTime == "CLOSED" ? "12:00 AM" : time.EndTime);
        //                break;
        //            case "SAT":
        //                wacoMassageSaturdayBeginTime.Text = (time.BeginTime == "CLOSED" ? "12:00 AM" : time.BeginTime);
        //                wacoMassageSaturdayEndTime.Text = (time.EndTime == "CLOSED" ? "12:00 AM" : time.EndTime);
        //                break;
        //            case "SUN":
        //                wacoMassageSundayBeginTime.Text = (time.BeginTime == "CLOSED" ? "12:00 AM" : time.BeginTime);
        //                wacoMassageSundayEndTime.Text = (time.EndTime == "CLOSED" ? "12:00 AM" : time.EndTime);
        //                break;
        //        }
        //    }
        //}

        protected void updateHours_OnClick(object sender, EventArgs e)
        {
            Page.Validate();
            if (Page.IsValid)
            {
                bool mondayWacoTanningUpdate = sqlClass.UpdateLocationTimes("W", wacoTanningMondayBeginTime.Text, wacoTanningMondayEndTime.Text, "MON", "T");
                bool tuesdayWacoTanningUpdate = sqlClass.UpdateLocationTimes("W", wacoTanningTuesdayBeginTime.Text, wacoTanningTuesdayEndTime.Text, "TUE", "T");
                bool wednesdayWacoTanningUpdate = sqlClass.UpdateLocationTimes("W", wacoTanningWednesdayBeginTime.Text, wacoTanningWednesdayEndTime.Text, "WED", "T");
                bool thursdayWacoTanningUpdate = sqlClass.UpdateLocationTimes("W", wacoTanningThursdayBeginTime.Text, wacoTanningThursdayEndTime.Text, "THU", "T");
                bool fridayWacoTanningUpdate = sqlClass.UpdateLocationTimes("W", wacoTanningFridayBeginTime.Text, wacoTanningFridayEndTime.Text, "FRI", "T");
                bool saturdayWacoTanningUpdate = sqlClass.UpdateLocationTimes("W", wacoTanningSaturdayBeginTime.Text, wacoTanningSaturdayEndTime.Text, "SAT", "T");
                bool sundayWacoTanningUpdate = sqlClass.UpdateLocationTimes("W", wacoTanningSundayBeginTime.Text, wacoTanningSundayEndTime.Text, "SUN", "T");
                
                //bool mondayWacoMassageUpdate = sqlClass.UpdateLocationTimes("W", wacoMassageMondayBeginTime.Text, wacoMassageMondayEndTime.Text, "MON", "M");
                //bool tuesdayWacoMassageUpdate = sqlClass.UpdateLocationTimes("W", wacoMassageTuesdayBeginTime.Text, wacoMassageTuesdayEndTime.Text, "TUE", "M");
                //bool wednesdayWacoMassageUpdate = sqlClass.UpdateLocationTimes("W", wacoMassageWednesdayBeginTime.Text, wacoMassageWednesdayEndTime.Text, "WED", "M");
                //bool thursdayWacoMassageUpdate = sqlClass.UpdateLocationTimes("W", wacoMassageThursdayBeginTime.Text, wacoMassageThursdayEndTime.Text, "THU", "M");
                //bool fridayWacoMassageUpdate = sqlClass.UpdateLocationTimes("W", wacoMassageFridayBeginTime.Text, wacoMassageFridayEndTime.Text, "FRI", "M");
                //bool saturdayWacoMassageUpdate = sqlClass.UpdateLocationTimes("W", wacoMassageSaturdayBeginTime.Text, wacoMassageSaturdayEndTime.Text, "SAT", "M");
                //bool sundayWacoMassageUpdate = sqlClass.UpdateLocationTimes("W", wacoMassageSundayBeginTime.Text, wacoMassageSundayEndTime.Text, "SUN", "M");

                if ((!mondayWacoTanningUpdate) || (!tuesdayWacoTanningUpdate) || (!wednesdayWacoTanningUpdate) ||
                    (!thursdayWacoTanningUpdate) || (!fridayWacoTanningUpdate) || (!saturdayWacoTanningUpdate) ||
                    (!sundayWacoTanningUpdate))
                    //|| (!mondayWacoMassageUpdate) || (!tuesdayWacoMassageUpdate) ||
                    //(!wednesdayWacoMassageUpdate) || (!thursdayWacoMassageUpdate) || (!fridayWacoMassageUpdate) ||
                    //(!saturdayWacoMassageUpdate) || (!sundayWacoMassageUpdate))
                    errorMessage.Text = "Error updating hours";
                else
                    Response.Redirect(HOTBAL.TansConstants.ADMIN_INTERNAL_URL);
            }
        }
    }
}