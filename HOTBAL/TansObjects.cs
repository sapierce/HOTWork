using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace HOTBAL
{
    public class Customer
    {
        private Int64 _ID;
        private string _firstName;
        private string _lastName;
        private string _address;
        private string _city;
        private string _state;
        private string _zipCode;
        private string _phoneNumber;
        private DateTime _dateOfBirth;
        private DateTime _joinDate;
        private DateTime _renewalDate;
        private int _planId;
        private string _plan;
        private int _fitzNumber;
        private string _email;
        private string _onlineName;
        private bool _onlineUser = false;
        private bool _newOnline = false;
        private bool _receiveSpecials = false;
        private bool _emailVerify = false;
        private bool _lotionWarning = false;
        private bool _acknowledge = false;
        private string _warningText;
        private bool _familyHistory = false;
        private bool _personalHistory = false;
        private bool _specialFlag = false;
        private int _specialNumber;
        private DateTime _specialDate;
        private string _remarks;
        private bool _onlineRestrict = false;
        private bool _isActive = false;
        private List<CustomerNote> _notes;
        private List<Tan> _tans;
        private List<Massage> _massages;
        private string _error;

        public Int64 ID
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

        public DateTime DateOfBirth
        {
            get
            {
                return this._dateOfBirth;
            }
            set
            {
                this._dateOfBirth = value;
            }
        }

        public DateTime JoinDate
        {
            get
            {
                return this._joinDate;
            }
            set
            {
                this._joinDate = value;
            }
        }

        public DateTime RenewalDate
        {
            get
            {
                return this._renewalDate;
            }
            set
            {
                this._renewalDate = value;
            }
        }

        public string Plan
        {
            get
            {
                return this._plan;
            }
            set
            {
                this._plan = value;
            }
        }

        public int PlanId
        {
            get
            {
                return this._planId;
            }
            set
            {
                this._planId = value;
            }
        }

        public Int32 FitzPatrickNumber
        {
            get
            {
                return this._fitzNumber;
            }
            set
            {
                this._fitzNumber = value;
            }
        }

        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }

        public string OnlineName
        {
            get
            {
                return this._onlineName;
            }
            set
            {
                this._onlineName = value;
            }
        }

        public bool OnlineUser
        {
            get
            {
                return this._onlineUser;
            }
            set
            {
                this._onlineUser = value;
            }
        }

        public bool NewOnlineCustomer
        {
            get
            {
                return this._newOnline;
            }
            set
            {
                this._newOnline = value;
            }
        }

        public bool ReceiveSpecials
        {
            get
            {
                return this._receiveSpecials;
            }
            set
            {
                this._receiveSpecials = value;
            }
        }

        public bool VerifiedEmail
        {
            get
            {
                return this._emailVerify;
            }
            set
            {
                this._emailVerify = value;
            }
        }

        public bool LotionWarning
        {
            get
            {
                return this._lotionWarning;
            }
            set
            {
                this._lotionWarning = value;
            }
        }

        public bool AcknowledgeWarning
        {
            get
            {
                return this._acknowledge;
            }
            set
            {
                this._acknowledge = value;
            }
        }

        public string AcknowledgeWarningText
        {
            get
            {
                return this._warningText;
            }
            set
            {
                this._warningText = value;
            }
        }

        public bool FamilyHistory
        {
            get
            {
                return this._familyHistory;
            }
            set
            {
                this._familyHistory = value;
            }
        }

        public bool PersonalHistory
        {
            get
            {
                return this._personalHistory;
            }
            set
            {
                this._personalHistory = value;
            }
        }

        public bool SpecialFlag
        {
            get
            {
                return this._specialFlag;
            }
            set
            {
                this._specialFlag = value;
            }
        }

        public int SpecialID
        {
            get
            {
                return this._specialNumber;
            }
            set
            {
                this._specialNumber = value;
            }
        }

        public DateTime SpecialDate
        {
            get
            {
                return this._specialDate;
            }
            set
            {
                this._specialDate = value;
            }
        }

        public string Remarks
        {
            get
            {
                return this._remarks;
            }
            set
            {
                this._remarks = value;
            }
        }

        public bool OnlineRestriction
        {
            get
            {
                return this._onlineRestrict;
            }
            set
            {
                this._onlineRestrict = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                this._isActive = value;
            }
        }

        public List<CustomerNote> Notes
        {
            get
            {
                return this._notes;
            }
            set
            {
                this._notes = value;
            }
        }

        public List<Tan> Tans
        {
            get
            {
                return this._tans;
            }
            set
            {
                this._tans = value;
            }
        }

        public List<Massage> Massages
        {
            get
            {
                return this._massages;
            }
            set
            {
                this._massages = value;
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

    public class CustomerNote
    {
        private Int32 _noteID;
        private string _noteText;
        private bool _noteOwes;
        private bool _noteOwed;
        private bool _noteLotion;
        private bool _noteUpgrade;

        public Int32 NoteID
        {
            get
            {
                return this._noteID;
            }
            set
            {
                this._noteID = value;
            }
        }

        public string NoteText
        {
            get
            {
                return this._noteText;
            }
            set
            {
                this._noteText = value;
            }
        }

        public bool OwesMoney
        {
            get
            {
                return this._noteOwes;
            }
            set
            {
                this._noteOwes = value;
            }
        }

        public bool OwedProduct
        {
            get
            {
                return this._noteOwed;
            }
            set
            {
                this._noteOwed = value;
            }
        }

        public bool LotionWarning
        {
            get
            {
                return this._noteLotion;
            }
            set
            {
                this._noteLotion = value;
            }
        }

        public bool NeedsUpgrade
        {
            get
            {
                return this._noteUpgrade;
            }
            set
            {
                this._noteUpgrade = value;
            }
        }
    }

    public class Tan
    {
        private Int32 _ID;
        private Int32 _customerID;
        private string _date;
        private string _bed;
        private string _location;
        private string _time;
        private Int32 _length;
        private bool _online;
        private bool _deleteInd;

        public Int32 TanID
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

        public Int32 CustomerID
        {
            get
            {
                return this._customerID;
            }
            set
            {
                this._customerID = value;
            }
        }

        public string Date
        {
            get
            {
                return this._date;
            }
            set
            {
                this._date = value;
            }
        }

        public string Bed
        {
            get
            {
                return this._bed;
            }
            set
            {
                this._bed = value;
            }
        }

        public string Location
        {
            get
            {
                return this._location;
            }
            set
            {
                this._location = value;
            }
        }

        public string Time
        {
            get
            {
                return this._time;
            }
            set
            {
                this._time = value;
            }
        }

        public Int32 Length
        {
            get
            {
                return this._length;
            }
            set
            {
                this._length = value;
            }
        }

        public bool OnlineIndicator
        {
            get
            {
                return this._online;
            }
            set
            {
                this._online = value;
            }
        }

        public bool DeletedIndicator
        {
            get
            {
                return this._deleteInd;
            }
            set
            {
                this._deleteInd = value;
            }
        }
    }

    public class CustomerBilling
    {
        private DateTime _purchaseDate;
        private DateTime _renewalDate;
        private string _package;

        public DateTime PurchaseDate
        {
            get
            {
                return this._purchaseDate;
            }
            set
            {
                this._purchaseDate = value;
            }
        }

        public DateTime RenewalDate
        {
            get
            {
                return this._renewalDate;
            }
            set
            {
                this._renewalDate = value;
            }
        }

        public string Package
        {
            get
            {
                return this._package;
            }
            set
            {
                this._package = value;
            }
        }
    }

    public class Time
    {
        private string _beginTime;

        private string _endTime;

        private string _timeDay;

        private string _webtimeDay;


        public string BeginTime
        {
            get
            {
                return this._beginTime;
            }
            set
            {
                this._beginTime = value;
            }
        }

        public string EndTime
        {
            get
            {
                return this._endTime;
            }
            set
            {
                this._endTime = value;
            }
        }

        public string TimeDay
        {
            get
            {
                return this._timeDay;
            }
            set
            {
                this._timeDay = value;
            }
        }

        public string WebTimeDay
        {
            get
            {
                return this._webtimeDay;
            }
            set
            {
                this._webtimeDay = value;
            }
        }
    }

    public class Bed
    {
        private int _bedID = 0;

        private string _bedLong = "";

        private string _bedShort = "0";

        private string _bedType = "";

        private string _bedLocation = "W";

        private bool _bedDisplayInternal = false;

        private bool _bedDisplayExternal = false;

        private bool _bedActive = false;


        public int BedID
        {
            get
            {
                return this._bedID;
            }
            set
            {
                this._bedID = value;
            }
        }

        public string BedLong
        {
            get
            {
                return this._bedLong;
            }
            set
            {
                this._bedLong = value;
            }
        }

        public string BedShort
        {
            get
            {
                return this._bedShort;
            }
            set
            {
                this._bedShort = value;
            }
        }

        public string BedType
        {
            get
            {
                return this._bedType;
            }
            set
            {
                this._bedType = value;
            }
        }

        public string BedLocation
        {
            get
            {
                return this._bedLocation;
            }
            set
            {
                this._bedLocation = value;
            }
        }

        public bool BedDisplayInternal
        {
            get
            {
                return this._bedDisplayInternal;
            }
            set
            {
                this._bedDisplayInternal = value;
            }
        }

        public bool BedDisplayExternal
        {
            get
            {
                return this._bedDisplayExternal;
            }
            set
            {
                this._bedDisplayExternal = value;
            }
        }

        public bool BedActive
        {
            get
            {
                return this._bedActive;
            }
            set
            {
                this._bedActive = value;
            }
        }
    }

    public class Package
    {
        private Int32 _packageID = 0;

        private Int32 _productID = 0;

        private string _packageName  = "No packages available";

        private string _packageNameShort = "";

        private string _packageBarCode = "";

        private string _packageBed = "";

        private int _packageLength = 0;

        private int _packageTanCount = 0;

        private string _packagePrice = "$0.00";

        private string _packageSalePrice = "$0.00";

        private bool _packageSaleOnline = false;

        private bool _packageSaleStore = false;

        private bool _packageAvailableOnline = false;

        private bool _packageAvailableInStore = false;


        public Int32 PackageID
        {
            get
            {
                return this._packageID;
            }
            set
            {
                this._packageID = value;
            }
        }

        public Int32 ProductID
        {
            get
            {
                return this._productID;
            }
            set
            {
                this._productID = value;
            }
        }

        public string PackageName
        {
            get
            {
                return this._packageName;
            }
            set
            {
                this._packageName = value;
            }
        }

        public string PackageNameShort
        {
            get
            {
                return this._packageNameShort;
            }
            set
            {
                this._packageNameShort = value;
            }
        }

        public string PackageBarCode
        {
            get
            {
                return this._packageBarCode;
            }
            set
            {
                this._packageBarCode = value;
            }
        }

        public string PackageBed
        {
            get
            {
                return this._packageBed;
            }
            set
            {
                this._packageBed = value;
            }
        }

        public int PackageLength
        {
            get
            {
                return this._packageLength;
            }
            set
            {
                this._packageLength = value;
            }
        }

        public int PackageTanCount
        {
            get
            {
                return this._packageTanCount;
            }
            set
            {
                this._packageTanCount = value;
            }
        }

        public string PackagePrice
        {
            get
            {
                return this._packagePrice;
            }
            set
            {
                this._packagePrice = value;
            }
        }

        public string PackageSalePrice
        {
            get
            {
                return this._packageSalePrice;
            }
            set
            {
                this._packageSalePrice = value;
            }
        }

        public bool PackageSaleOnline
        {
            get
            {
                return this._packageSaleOnline;
            }
            set
            {
                this._packageSaleOnline = value;
            }
        }

        public bool PackageSaleStore
        {
            get
            {
                return this._packageSaleStore;
            }
            set
            {
                this._packageSaleStore = value;
            }
        }

        public bool PackageAvailableOnline
        {
            get
            {
                return this._packageAvailableOnline;
            }
            set
            {
                this._packageAvailableOnline = value;
            }
        }

        public bool PackageAvailableInStore
        {
            get
            {
                return this._packageAvailableInStore;
            }
            set
            {
                this._packageAvailableInStore = value;
            }
        }
    }

    public class Special
    {
        private int _specialID;

        private string _specialName;

        private string _specialShortName;

        private int _specialLength;

        private int _specialProductID;

        private bool _specialActive;


        public int SpecialID
        {
            get
            {
                return this._specialID;
            }
            set
            {
                this._specialID = value;
            }
        }

        public string SpecialName
        {
            get
            {
                return this._specialName;
            }
            set
            {
                this._specialName = value;
            }
        }

        public string SpecialShortName
        {
            get
            {
                return this._specialShortName;
            }
            set
            {
                this._specialShortName = value;
            }
        }

        public int SpecialLength
        {
            get
            {
                return this._specialLength;
            }
            set
            {
                this._specialLength = value;
            }
        }

        public int SpecialProductID
        {
            get
            {
                return this._specialProductID;
            }
            set
            {
                this._specialProductID = value;
            }
        }

        public bool SpecialActive
        {
            get
            {
                return this._specialActive;
            }
            set
            {
                this._specialActive = value;
            }
        }
    }

    public class SpecialLevel
    {
        private int _specialLevelID;

        private int _specialID;

        private string _specialLevelBed;

        private int _specialLevelLength;

        private int _specialLevelOrder;


        public int SpecialLevelID
        {
            get
            {
                return this._specialLevelID;
            }
            set
            {
                this._specialLevelID = value;
            }
        }

        public int SpecialID
        {
            get
            {
                return this._specialID;
            }
            set
            {
                this._specialID = value;
            }
        }

        public string SpecialLevelBed
        {
            get
            {
                return this._specialLevelBed;
            }
            set
            {
                this._specialLevelBed = value;
            }
        }

        public int SpecialLevelLength
        {
            get
            {
                return this._specialLevelLength;
            }
            set
            {
                this._specialLevelLength = value;
            }
        }

        public int SpecialLevelOrder
        {
            get
            {
                return this._specialLevelOrder;
            }
            set
            {
                this._specialLevelOrder = value;
            }
        }
    }

    public class Employee
    {
        private Int32 _employeeID;
        private string _firstName;
        private string _lastName;

        public Int32 EmployeeID
        {
            get
            {
                return this._employeeID;
            }
            set
            {
                this._employeeID = value;
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
    }

    public class EmailVerificationInfo
    {
        public Guid guid { get; set; }
        public string memberNumber { get; set; }
        public string userName { get; set; }
        public string emailAddress { get; set; }
        public DateTime requestSent { get; set; }
    }

    public class SiteNotification
    {
        private Int32 _noticeID;
        private string _noticeText;
        private DateTime _noticeStart;
        private DateTime _noticeEnd;

        public Int32 NoticeID
        {
            get
            {
                return this._noticeID;
            }
            set
            {
                this._noticeID = value;
            }
        }

        public string NoticeText
        {
            get
            {
                return this._noticeText;
            }
            set
            {
                this._noticeText = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return this._noticeStart;
            }
            set
            {
                this._noticeStart = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return this._noticeEnd;
            }
            set
            {
                this._noticeEnd = value;
            }
        }
    }

    public class Comment
    {
        private int _commentID;

        private string _customerName;

        private string _customerEmail;

        private string _commentAbout;

        private string _commentText;

        private DateTime _commentTime;


        public int CommentID
        {
            get
            {
                return this._commentID;
            }
            set
            {
                this._commentID = value;
            }
        }

        public string CustomerName
        {
            get
            {
                return this._customerName;
            }
            set
            {
                this._customerName = value;
            }
        }

        public string CustomerEmail
        {
            get
            {
                return this._customerEmail;
            }
            set
            {
                this._customerEmail = value;
            }
        }

        public string CommentAbout
        {
            get
            {
                return this._commentAbout;
            }
            set
            {
                this._commentAbout = value;
            }
        }

        public string CommentText
        {
            get
            {
                return this._commentText;
            }
            set
            {
                this._commentText = value;
            }
        }

        public DateTime CommentTime
        {
            get
            {
                return this._commentTime;
            }
            set
            {
                this._commentTime = value;
            }
        }
    }

    public class EmployeeNote
    {
        private int _noteID;

        private int _toID;

        private int _fromID;

        private string _noteText;

        private DateTime _noteTime;


        public int NoteID
        {
            get
            {
                return this._noteID;
            }
            set
            {
                this._noteID = value;
            }
        }

        public int NoteTo
        {
            get
            {
                return this._toID;
            }
            set
            {
                this._toID = value;
            }
        }

        public int NoteFrom
        {
            get
            {
                return this._fromID;
            }
            set
            {
                this._fromID = value;
            }
        }

        public string NoteText
        {
            get
            {
                return this._noteText;
            }
            set
            {
                this._noteText = value;
            }
        }

        public DateTime NoteTime
        {
            get
            {
                return this._noteTime;
            }
            set
            {
                this._noteTime = value;
            }
        }
    }

    public class Massage
    {
        private int _id;

        private DateTime _date;

        private string _time;

        private int _length;

        private long _userID;


        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return this._date;
            }
            set
            {
                this._date = value;
            }
        }

        public string Time
        {
            get
            {
                return this._time;
            }
            set
            {
                this._time = value;
            }
        }

        public int Length
        {
            get
            {
                return this._length;
            }
            set
            {
                this._length = value;
            }
        }

        public long UserID
        {
            get
            {
                return this._userID;
            }
            set
            {
                this._userID = value;
            }
        }
    }
}