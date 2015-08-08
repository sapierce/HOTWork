using System.Runtime.Serialization;

namespace HOTBAL
{
    [DataContract]
    public class School
    {
        private int schoolID;
        private string schoolName;

        [DataMember]
        public int SchoolID
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

        [DataMember]
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