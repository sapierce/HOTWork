using System;
using System.Collections.Generic;
using System.Text;

namespace HOTBAL
{
    public class School
        {
            private Int32 schoolID;
            private string schoolName;

            public Int32 SchoolID
            {
                get
                {
                    return this.schoolID;
                }
                set
                {
                    this.schoolID = value;
                }
            }

            public string SchoolName
            {
                get
                {
                    return this.schoolName;
                }
                set
                {
                    this.schoolName = value;
                }
            }
        }
}
