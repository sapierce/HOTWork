using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HOTDAL;

namespace HOTBAL
{
    class MassageMethods
    {
        private TansSQL tansDataAccess;
        private ErrorSQL errorDataAccess;
        private HOTFunctions functionsClass = new HOTFunctions();

        public void LogErrorMessage(Exception errorMessage, string sqlCommand, string errorLocation)
        {
            try
            {
                errorDataAccess.ExecuteTAN_ERROR_ADD(errorLocation, functionsClass.CleanUp(errorMessage.Message), functionsClass.CleanUp(errorMessage.StackTrace), functionsClass.CleanUp(sqlCommand));
            }
            catch (Exception ex)
            {
                functionsClass.SendErrorMail("MassageMethods.LogErrorMessage", ex.StackTrace, ex.Message, errorMessage.Message);
            }
        }
    }
}
