using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace HOTBAL
{
	public class Student
	{
		private int _ID;
        private string _regID;
		private string _firstName;
        private string _lastName;
        private string _suffix;
		private string _address;
		private string _city;
		private string _state;
		private string _zipCode;
		private string _emergencyContact;
        private int _school;
		private DateTime _birthDate;
		private DateTime _paymentDate;
		private string _paymentPlan;
		private Double _paymentAmount;
		private string _note;
		private bool _paid;
		private bool _pass;
		private bool _active;
		private string _error;

		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

        public string RegistrationID
        {
            get
            {
                return this._regID;
            }
            set
            {
                this._regID = value;
            }
        }

		public string FirstName
		{
			get
			{
				return this._firstName;
			}
			set
			{
				this._firstName = value;
			}
		}

		public string LastName
		{
			get
			{
				return this._lastName;
			}
			set
			{
				this._lastName = value;
			}
		}

        public string Suffix
        {
            get
            {
                return this._suffix;
            }
            set
            {
                this._suffix = value;
            }
        }

		public string Address
		{
			get
			{
				return this._address;
			}
			set
			{
				this._address = value;
			}
		}

		public string City
		{
			get
			{
				return this._city;
			}
			set
			{
				this._city = value;
			}
		}

		public string State
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		public string ZipCode
		{
			get
			{
				return this._zipCode;
			}
			set
			{
				this._zipCode = value;
			}
		}

		public string EmergencyContact
		{
			get
			{
				return this._emergencyContact;
			}
			set
			{
				this._emergencyContact = value;
			}
		}

        public int School
        {
            get
            {
                return this._school;
            }
            set
            {
                this._school = value;
            }
        }

		public DateTime BirthDate
		{
			get
			{
				return this._birthDate;
			}
			set
			{
				this._birthDate = value;
			}
		}

		public DateTime PaymentDate
		{
			get
			{
				return this._paymentDate;
			}
			set
			{
				this._paymentDate = value;
			}
		}

		public string PaymentPlan
		{
			get
			{
				return this._paymentPlan;
			}
			set
			{
				this._paymentPlan = value;
			}
		}

		public Double PaymentAmount
		{
			get
			{
				return this._paymentAmount;
			}
			set
			{
				this._paymentAmount = value;
			}
		}

		public string Note
		{
			get
			{
				return this._note;
			}
			set
			{
				this._note = value;
			}
		}

		public bool Paid
		{
			get
			{
				return this._paid;
			}
			set
			{
				this._paid = value;
			}
		}

		public bool Pass
		{
			get
			{
				return this._pass;
			}
			set
			{
				this._pass = value;
			}
		}

		public bool Active
		{
			get
			{
				return this._active;
			}
			set
			{
				this._active = value;
			}
		}

		public string Error
		{
			get
			{
				return this._error;
			}
			set
			{
				this._error = value;
			}
		}
	}

	public class Course
	{
		private int _ID;
		private string _Title;
        private string _Day;
		private string _Time;
		private string _Date;
		private int _InstructorID;
		private bool _Repeating;
		private string _ClassOrLesson;
		private int _FirstArtID;
		private int _SecondArtID;
		private string _error;

		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				this._Title = value;
			}
		}

		public string Day
		{
			get
			{
				return this._Day;
			}
			set
			{
				this._Day = value;
			}
		}

		public string Time
		{
			get
			{
				return this._Time;
			}
			set
			{
				this._Time = value;
			}
		}

		public string Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				this._Date = value;
			}
		}

		public int InstructorID
		{
			get
			{
				return this._InstructorID;
			}
			set
			{
				this._InstructorID = value;
			}
		}

		public bool Repeating
		{
			get
			{
				return this._Repeating;
			}
			set
			{
				this._Repeating = value;
			}
		}

		public string ClassOrLesson
		{
			get
			{
				return this._ClassOrLesson;
			}
			set
			{
				this._ClassOrLesson = value;
			}
		}

		public int FirstArtID
		{
			get
			{
				return this._FirstArtID;
			}
			set
			{
				this._FirstArtID = value;
			}
		}

		public int SecondArtID
		{
			get
			{
				return this._SecondArtID;
			}
			set
			{
				this._SecondArtID = value;
			}
		}

		public string Error
		{
			get
			{
				return this._error;
			}
			set
			{
				this._error = value;
			}
		}
	}

	public class ClassAttendance
	{
		private int _StudentID;
		private bool _Attendance;
		private string _Date;

		public int StudentID
		{
			get
			{
				return this._StudentID;
			}
			set
			{
				this._StudentID = value;
			}
		}

		public bool Attendance
		{
			get
			{
				return this._Attendance;
			}
			set
			{
				this._Attendance = value;
			}
		}

		public string Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				this._Date = value;
			}
		}
	}

	public class Art
	{
        private int _ID;
        private int _schoolID;
		private string _title;
		private bool _active;
		private string _error;

		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

        public int SchoolID
        {
            get
            {
                return this._schoolID;
            }
            set
            {
                this._schoolID = value;
            }
        }

		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		public bool Active
		{
			get
			{
				return this._active;
			}
			set
			{
				this._active = value;
			}
		}

		public string Error
		{
			get
			{
				return this._error;
			}
			set
			{
				this._error = value;
			}
		}
	}

	public class Belt
	{
		private int _ID;
		private int _artID;
		private string _title;
		private string _level;
		private string _classTip;
		private int _classCount;
		private bool _active;
		private string _error;

		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

		public int ArtID
		{
			get
			{
				return this._artID;
			}
			set
			{
				this._artID = value;
			}
		}

		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		public string Level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		public string ClassOrTip
		{
			get
			{
				return this._classTip;
			}
			set
			{
				this._classTip = value;
			}
		}

		public int ClassCount
		{
			get
			{
				return this._classCount;
			}
			set
			{
				this._classCount = value;
			}
		}

		public bool Active
		{
			get
			{
				return this._active;
			}
			set
			{
				this._active = value;
			}
		}

		public string Error
		{
			get
			{
				return this._error;
			}
			set
			{
				this._error = value;
			}
		}
	}

	public class Tip
	{
		private int _ID;
		private int _beltID;
		private string _title;
		private string _level;
		private bool _lastTip;
		private bool _active;
		private string _error;

		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

		public int BeltID
		{
			get
			{
				return this._beltID;
			}
			set
			{
				this._beltID = value;
			}
		}

		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		public string Level
		{
			get
			{
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		public bool LastTipIndicator
		{
			get
			{
				return this._lastTip;
			}
			set
			{
				this._lastTip = value;
			}
		}

		public bool Active
		{
			get
			{
				return this._active;
			}
			set
			{
				this._active = value;
			}
		}

		public string Error
		{
			get
			{
				return this._error;
			}
			set
			{
				this._error = value;
			}
		}
	}

	public class Term
	{
		private int _ID;
		private int _beltID;
		private string _english;
		private string _chinese;
		private bool _active;
		private string _error;

		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

		public int BeltID
		{
			get
			{
				return this._beltID;
			}
			set
			{
				this._beltID = value;
			}
		}

		public string English
		{
			get
			{
				return this._english;
			}
			set
			{
				this._english = value;
			}
		}

		public string Chinese
		{
			get
			{
				return this._chinese;
			}
			set
			{
				this._chinese = value;
			}
		}

		public bool Active
		{
			get
			{
				return this._active;
			}
			set
			{
				this._active = value;
			}
		}

		public string Error
		{
			get
			{
				return this._error;
			}
			set
			{
				this._error = value;
			}
		}
	}

	public class Instructor
	{
		private int _ID;
		private string _firstName;
		private string _lastName;
		private string _bio;
		private string _type;
		private bool _active;
		private string _error;

		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

		public string FirstName
		{
			get
			{
				return this._firstName;
			}
			set
			{
				this._firstName = value;
			}
		}

		public string LastName
		{
			get
			{
				return this._lastName;
			}
			set
			{
				this._lastName = value;
			}
		}

		public string Bio
		{
			get
			{
				return this._bio;
			}
			set
			{
				this._bio = value;
			}
		}

		public string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		public bool Active
		{
			get
			{
				return this._active;
			}
			set
			{
				this._active = value;
			}
		}

		public string Error
		{
			get
			{
				return this._error;
			}
			set
			{
				this._error = value;
			}
		}
	}

	public class StudentArt
	{
		private int _ID;
		private string _artTitle;
		private int _artID;
		private string _beltTitle;
		private int _beltID;
		private string _tipTitle;
		private int _tipID;
		private string _classOrTip;
		private int _classCount;
		private string _completeDate;
		private bool _active;
		private string _error;

		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

		public string ArtTitle
		{
			get
			{
				return this._artTitle;
			}
			set
			{
				this._artTitle = value;
			}
		}

		public int ArtID
		{
			get
			{
				return this._artID;
			}
			set
			{
				this._artID = value;
			}
		}

		public string BeltTitle
		{
			get
			{
				return this._beltTitle;
			}
			set
			{
				this._beltTitle = value;
			}
		}

		public int BeltID
		{
			get
			{
				return this._beltID;
			}
			set
			{
				this._beltID = value;
			}
		}

		public string TipTitle
		{
			get
			{
				return this._tipTitle;
			}
			set
			{
				this._tipTitle = value;
			}
		}

		public int TipID
		{
			get
			{
				return this._tipID;
			}
			set
			{
				this._tipID = value;
			}
		}

		public string ClassOrTip
		{
			get
			{
				return this._classOrTip;
			}
			set
			{
				this._classOrTip = value;
			}
		}

		public int ClassCount
		{
			get
			{
				return this._classCount;
			}
			set
			{
				this._classCount = value;
			}
		}

		public string CompleteDate
		{
			get
			{
				return this._completeDate;
			}
			set
			{
				this._completeDate = value;
			}
		}

		public bool Active
		{
			get
			{
				return this._active;
			}
			set
			{
				this._active = value;
			}
		}

		public string Error
		{
			get
			{
				return this._error;
			}
			set
			{
				this._error = value;
			}
		}
	}

	public class StudentPhone
	{
		private int _ID;
		private string _stdtID;
		private string _phoneNumber;
		private string _relationship;
		private bool _active;
		private string _error;

		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

		public string StudentID
		{
			get
			{
				return this._stdtID;
			}
			set
			{
				this._stdtID = value;
			}
		}

		public string PhoneNumber
		{
			get
			{
				return this._phoneNumber;
			}
			set
			{
				this._phoneNumber = value;
			}
		}

		public string Relationship
		{
			get
			{
				return this._relationship;
			}
			set
			{
				this._relationship = value;
			}
		}

		public bool Active
		{
			get
			{
				return this._active;
			}
			set
			{
				this._active = value;
			}
		}

		public string Error
		{
			get
			{
				return this._error;
			}
			set
			{
				this._error = value;
			}
		}
	}
}
