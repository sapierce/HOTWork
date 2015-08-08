using System;
using System.Runtime.Serialization;

namespace HOTBAL
{
    [DataContract]
    public class Student
    {
        private int studentId;
        private string registrationId;
        private string firstName;
        private string lastName;
        private string suffix;
        private string streetAddress;
        private string city;
        private string state;
        private string zipCode;
        private string emergencyContact;
        private int schoolId;
        private DateTime birthDate;
        private DateTime paymentDate;
        private string paymentPlan;
        private double paymentAmount;
        private string studentNote;
        private bool isPaying;
        private bool isPassing;
        private bool isActive;
        private string errorMessage;

        [DataMember]
        public int StudentId
        {
            get
            {
                return this.studentId;
            }
            set
            {
                this.studentId = value;
            }
        }

        [DataMember]
        public string RegistrationId
        {
            get
            {
                return this.registrationId;
            }
            set
            {
                this.registrationId = value;
            }
        }

        [DataMember]
        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            set
            {
                this.firstName = value;
            }
        }

        [DataMember]
        public string LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                this.lastName = value;
            }
        }

        [DataMember]
        public string Suffix
        {
            get
            {
                return this.suffix;
            }
            set
            {
                this.suffix = value;
            }
        }

        [DataMember]
        public string StreetAddress
        {
            get
            {
                return this.streetAddress;
            }
            set
            {
                this.streetAddress = value;
            }
        }

        [DataMember]
        public string City
        {
            get
            {
                return this.city;
            }
            set
            {
                this.city = value;
            }
        }

        [DataMember]
        public string State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }

        [DataMember]
        public string ZipCode
        {
            get
            {
                return this.zipCode;
            }
            set
            {
                this.zipCode = value;
            }
        }

        [DataMember]
        public string EmergencyContact
        {
            get
            {
                return this.emergencyContact;
            }
            set
            {
                this.emergencyContact = value;
            }
        }

        [DataMember]
        public int SchoolId
        {
            get
            {
                return this.schoolId;
            }
            set
            {
                this.schoolId = value;
            }
        }

        [DataMember]
        public DateTime BirthDate
        {
            get
            {
                return this.birthDate;
            }
            set
            {
                this.birthDate = value;
            }
        }

        [DataMember]
        public DateTime PaymentDate
        {
            get
            {
                return this.paymentDate;
            }
            set
            {
                this.paymentDate = value;
            }
        }

        [DataMember]
        public string PaymentPlan
        {
            get
            {
                return this.paymentPlan;
            }
            set
            {
                this.paymentPlan = value;
            }
        }

        [DataMember]
        public double PaymentAmount
        {
            get
            {
                return this.paymentAmount;
            }
            set
            {
                this.paymentAmount = value;
            }
        }

        [DataMember]
        public string Note
        {
            get
            {
                return this.studentNote;
            }
            set
            {
                this.studentNote = value;
            }
        }

        [DataMember]
        public bool IsPaying
        {
            get
            {
                return this.isPaying;
            }
            set
            {
                this.isPaying = value;
            }
        }

        [DataMember]
        public bool IsPassing
        {
            get
            {
                return this.isPassing;
            }
            set
            {
                this.isPassing = value;
            }
        }

        [DataMember]
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                this.isActive = value;
            }
        }

        [DataMember]
        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                this.errorMessage = value;
            }
        }
    }

    [DataContract]
    public class Course
    {
        private int courseId;
        private string courseTitle;
        private string courseDay;
        private string courseTime;
        private string courseDate;
        private int instructorId;
        private bool isRepeating;
        private string classOrLesson;
        private int firstArtId;
        private int secondArtId;
        private string errorMessage;

        [DataMember]
        public int CourseId
        {
            get
            {
                return this.courseId;
            }
            set
            {
                this.courseId = value;
            }
        }

        [DataMember]
        public string CourseTitle
        {
            get
            {
                return this.courseTitle;
            }
            set
            {
                this.courseTitle = value;
            }
        }

        [DataMember]
        public string Day
        {
            get
            {
                return this.courseDay;
            }
            set
            {
                this.courseDay = value;
            }
        }

        [DataMember]
        public string Time
        {
            get
            {
                return this.courseTime;
            }
            set
            {
                this.courseTime = value;
            }
        }

        [DataMember]
        public string Date
        {
            get
            {
                return this.courseDate;
            }
            set
            {
                this.courseDate = value;
            }
        }

        [DataMember]
        public int InstructorId
        {
            get
            {
                return this.instructorId;
            }
            set
            {
                this.instructorId = value;
            }
        }

        [DataMember]
        public bool IsRepeating
        {
            get
            {
                return this.isRepeating;
            }
            set
            {
                this.isRepeating = value;
            }
        }

        [DataMember]
        public string ClassOrLesson
        {
            get
            {
                return this.classOrLesson;
            }
            set
            {
                this.classOrLesson = value;
            }
        }

        [DataMember]
        public int FirstArtId
        {
            get
            {
                return this.firstArtId;
            }
            set
            {
                this.firstArtId = value;
            }
        }

        [DataMember]
        public int SecondArtId
        {
            get
            {
                return this.secondArtId;
            }
            set
            {
                this.secondArtId = value;
            }
        }

        [DataMember]
        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                this.errorMessage = value;
            }
        }
    }

    [DataContract]
    public class ClassAttendance
    {
        private int studentId;
        private bool didAttend;
        private string classDate;

        [DataMember]
        public int StudentId
        {
            get
            {
                return this.studentId;
            }
            set
            {
                this.studentId = value;
            }
        }

        [DataMember]
        public bool DidAttend
        {
            get
            {
                return this.didAttend;
            }
            set
            {
                this.didAttend = value;
            }
        }

        [DataMember]
        public string ClassDate
        {
            get
            {
                return this.classDate;
            }
            set
            {
                this.classDate = value;
            }
        }
    }

    [DataContract]
    public class Art
    {
        private int artId;
        private int schoolId;
        private string artTitle;
        private bool isActive;
        private string errorMessage;

        [DataMember]
        public int ArtId
        {
            get
            {
                return this.artId;
            }
            set
            {
                this.artId = value;
            }
        }

        [DataMember]
        public int SchoolId
        {
            get
            {
                return this.schoolId;
            }
            set
            {
                this.schoolId = value;
            }
        }

        [DataMember]
        public string ArtTitle
        {
            get
            {
                return this.artTitle;
            }
            set
            {
                this.artTitle = value;
            }
        }

        [DataMember]
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                this.isActive = value;
            }
        }

        [DataMember]
        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                this.errorMessage = value;
            }
        }
    }

    [DataContract]
    public class Belt
    {
        private int beltId;
        private int artId;
        private string beltTitle;
        private string beltLevel;
        private string classOrTip;
        private int classCount;
        private bool isActive;
        private string errorMessage;

        [DataMember]
        public int BeltId
        {
            get
            {
                return this.beltId;
            }
            set
            {
                this.beltId = value;
            }
        }

        [DataMember]
        public int ArtId
        {
            get
            {
                return this.artId;
            }
            set
            {
                this.artId = value;
            }
        }

        [DataMember]
        public string BeltTitle
        {
            get
            {
                return this.beltTitle;
            }
            set
            {
                this.beltTitle = value;
            }
        }

        [DataMember]
        public string BeltLevel
        {
            get
            {
                return this.beltLevel;
            }
            set
            {
                this.beltLevel = value;
            }
        }

        [DataMember]
        public string ClassOrTip
        {
            get
            {
                return this.classOrTip;
            }
            set
            {
                this.classOrTip = value;
            }
        }

        [DataMember]
        public int ClassCount
        {
            get
            {
                return this.classCount;
            }
            set
            {
                this.classCount = value;
            }
        }

        [DataMember]
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                this.isActive = value;
            }
        }

        [DataMember]
        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                this.errorMessage = value;
            }
        }
    }

    [DataContract]
    public class Tip
    {
        private int tipId;
        private int beltId;
        private string tipTitle;
        private string tipLevel;
        private bool isLastTip;
        private bool isActive;
        private string errorMessage;

        [DataMember]
        public int TipId
        {
            get
            {
                return this.tipId;
            }
            set
            {
                this.tipId = value;
            }
        }

        [DataMember]
        public int BeltId
        {
            get
            {
                return this.beltId;
            }
            set
            {
                this.beltId = value;
            }
        }

        [DataMember]
        public string TipTitle
        {
            get
            {
                return this.tipTitle;
            }
            set
            {
                this.tipTitle = value;
            }
        }

        [DataMember]
        public string TipLevel
        {
            get
            {
                return this.tipLevel;
            }
            set
            {
                this.tipLevel = value;
            }
        }

        [DataMember]
        public bool IsLastTip
        {
            get
            {
                return this.isLastTip;
            }
            set
            {
                this.isLastTip = value;
            }
        }

        [DataMember]
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                this.isActive = value;
            }
        }

        [DataMember]
        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                this.errorMessage = value;
            }
        }
    }

    [DataContract]
    public class Term
    {
        private int termId;
        private int beltId;
        private string englishTerm;
        private string chineseTerm;
        private bool isActive;
        private string errorMessage;

        [DataMember]
        public int TermId
        {
            get
            {
                return this.termId;
            }
            set
            {
                this.termId = value;
            }
        }

        [DataMember]
        public int BeltId
        {
            get
            {
                return this.beltId;
            }
            set
            {
                this.beltId = value;
            }
        }

        [DataMember]
        public string EnglishTerm
        {
            get
            {
                return this.englishTerm;
            }
            set
            {
                this.englishTerm = value;
            }
        }

        [DataMember]
        public string ChineseTerm
        {
            get
            {
                return this.chineseTerm;
            }
            set
            {
                this.chineseTerm = value;
            }
        }

        [DataMember]
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                this.isActive = value;
            }
        }

        [DataMember]
        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                this.errorMessage = value;
            }
        }
    }

    [DataContract]
    public class Instructor
    {
        private int instructorId;
        private string firstName;
        private string lastName;
        private string instructorBiography;
        private string instructorType;
        private bool isActive;
        private string errorMessage;

        [DataMember]
        public int InstructorId
        {
            get
            {
                return this.instructorId;
            }
            set
            {
                this.instructorId = value;
            }
        }

        [DataMember]
        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            set
            {
                this.firstName = value;
            }
        }

        [DataMember]
        public string LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                this.lastName = value;
            }
        }

        [DataMember]
        public string InstructorBiography
        {
            get
            {
                return this.instructorBiography;
            }
            set
            {
                this.instructorBiography = value;
            }
        }

        [DataMember]
        public string InstructorType
        {
            get
            {
                return this.instructorType;
            }
            set
            {
                this.instructorType = value;
            }
        }

        [DataMember]
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                this.isActive = value;
            }
        }

        [DataMember]
        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                this.errorMessage = value;
            }
        }
    }

    [DataContract]
    public class StudentArt
    {
        private int studentArtId;
        private string artTitle;
        private int artId;
        private string beltTitle;
        private int beltId;
        private string tipTitle;
        private int tipId;
        private string classOrTip;
        private int classCount;
        private string completionDate;
        private bool isActive;
        private string errorMessage;

        [DataMember]
        public int StudentArtId
        {
            get
            {
                return this.studentArtId;
            }
            set
            {
                this.studentArtId = value;
            }
        }

        [DataMember]
        public string ArtTitle
        {
            get
            {
                return this.artTitle;
            }
            set
            {
                this.artTitle = value;
            }
        }

        [DataMember]
        public int ArtId
        {
            get
            {
                return this.artId;
            }
            set
            {
                this.artId = value;
            }
        }

        [DataMember]
        public string BeltTitle
        {
            get
            {
                return this.beltTitle;
            }
            set
            {
                this.beltTitle = value;
            }
        }

        [DataMember]
        public int BeltId
        {
            get
            {
                return this.beltId;
            }
            set
            {
                this.beltId = value;
            }
        }

        [DataMember]
        public string TipTitle
        {
            get
            {
                return this.tipTitle;
            }
            set
            {
                this.tipTitle = value;
            }
        }

        [DataMember]
        public int TipId
        {
            get
            {
                return this.tipId;
            }
            set
            {
                this.tipId = value;
            }
        }

        [DataMember]
        public string ClassOrTip
        {
            get
            {
                return this.classOrTip;
            }
            set
            {
                this.classOrTip = value;
            }
        }

        [DataMember]
        public int ClassCount
        {
            get
            {
                return this.classCount;
            }
            set
            {
                this.classCount = value;
            }
        }

        [DataMember]
        public string CompletionDate
        {
            get
            {
                return this.completionDate;
            }
            set
            {
                this.completionDate = value;
            }
        }

        [DataMember]
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                this.isActive = value;
            }
        }

        [DataMember]
        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                this.errorMessage = value;
            }
        }
    }

    [DataContract]
    public class StudentPhone
    {
        private int phoneId;
        private string studentId;
        private string phoneNumber;
        private string relationshipToStudent;
        private bool isActive;
        private string errorMessage;

        [DataMember]
        public int PhoneId
        {
            get
            {
                return this.phoneId;
            }
            set
            {
                this.phoneId = value;
            }
        }

        [DataMember]
        public string StudentId
        {
            get
            {
                return this.studentId;
            }
            set
            {
                this.studentId = value;
            }
        }

        [DataMember]
        public string PhoneNumber
        {
            get
            {
                return this.phoneNumber;
            }
            set
            {
                this.phoneNumber = value;
            }
        }

        [DataMember]
        public string RelationshipToStudent
        {
            get
            {
                return this.relationshipToStudent;
            }
            set
            {
                this.relationshipToStudent = value;
            }
        }

        [DataMember]
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                this.isActive = value;
            }
        }

        [DataMember]
        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                this.errorMessage = value;
            }
        }
    }
}