using System;
using System.Collections.Generic;
using System.Text;

namespace HOTBAL
{
    public class SDAMessages
    {
        public const string SUCCESS_MESSAGE = "Success";

        public const string ERROR_GENERIC = "The system you are trying to access is currently unavailable. Please try again later. " +
            "If you continue to receive this error, please contact us at <a href='mailto:problems@hotselfdefense.net' class='center'>problems@hotselfdefense.net</a>.";

        public const string ERROR_ADD_STUDENT = "Error adding student.";

        public const string ERROR_DELETE_STUDENT = "Error deleting student.";

        public const string ERROR_ADD_CLASS = "Error adding class or lesson.";

        public const string ERROR_DELETE_CLASS = "Error deleting class or lesson.";

        public const string ERROR_ADD_STUDENT_CLASS = "Error adding student to the class or lesson.";

        public const string ERROR_ADD_STUDENT_ART = "Error adding art to student.";

        public const string ERROR_STUDENT_CHECK = "Error checkin in the student.";

        public const string NO_STUDENTS = "Unable to locate student information.";

        public const string NO_STUDENTS_CLASS = "No students currently registered for this class or lesson.";

        public const string NO_STUDENT_FOUND = "Unable to locate information for this student.";

        public const string NO_STUDENT_ATTEND = "Unable to locate attendance information for this student.";

        public const string NO_ARTS = "Unable to locate art information.";

        public const string NO_BELTS = "Unable to locate belt information.";

        public const string NO_TIP_CLASS = "Unable to locate tip/class count information.";

        public const string NO_TIPS = "Unable to locate tip information.";

        public const string NO_INSTRUCTORS = "Unable to locate instructor information.";

        public const string NO_TIMES = "Unable to locate available time information.";

        public const string NO_CLASS = "Unable to locate class or lesson information.";

        public const string NO_ITEMS = "Unable to locate transaction items.";

        public const string NO_TRANSACTION = "Unable to locate transaction information.";

        public const string NO_TRANSACTIONS = "No transactions found.";
    }
}
