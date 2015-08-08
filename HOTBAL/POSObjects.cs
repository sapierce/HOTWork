using System;
using System.Collections;
using System.Runtime.Serialization;

namespace HOTBAL
{
    //public class ShoppingCart //: IEnumerable
    //{
    //    private List<CartItem> _cartItem;

    //    public List<CartItem> CartItem
    //    {
    //        get
    //        {
    //            return this._cartItem;
    //        }
    //        set
    //        {
    //            this._cartItem = value;
    //        }
    //    }

    //    //IEnumerator IEnumerable.GetEnumerator()
    //    //{
    //    //    return (IEnumerator)GetEnumerator();
    //    //}

    //    //public CartItemEnum GetEnumerator()
    //    //{
    //    //    return new CartItemEnum(_cartItem);
    //    //}
    //}

    [DataContract]
    public class CartItem
    {
        private int itemId;

        private string itemName;

        private double itemPrice;

        private int itemQuantity;

        private string itemType;

        private string itemSubType;

        private bool itemIsTaxed;

        public int ItemId
        {
            get
            {
                return this.itemId;
            }
            set
            {
                this.itemId = value;
            }
        }

        public string ItemName
        {
            get
            {
                return this.itemName;
            }
            set
            {
                this.itemName = value;
            }
        }

        public double ItemPrice
        {
            get
            {
                return this.itemPrice;
            }
            set
            {
                this.itemPrice = value;
            }
        }

        public int ItemQuantity
        {
            get
            {
                return this.itemQuantity;
            }
            set
            {
                this.itemQuantity = value;
            }
        }

        public string ItemType
        {
            get
            {
                return this.itemType;
            }
            set
            {
                this.itemType = value;
            }
        }

        public string ItemSubType
        {
            get
            {
                return this.itemSubType;
            }
            set
            {
                this.itemSubType = value;
            }
        }

        public bool ItemIsTaxed
        {
            get
            {
                return this.itemIsTaxed;
            }
            set
            {
                this.itemIsTaxed = value;
            }
        }
    }

    [DataContract]
    public class CartItemEnum : IEnumerator
    {
        public CartItem[] _cartItem;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        private int position = -1;

        public CartItemEnum(CartItem[] list)
        {
            _cartItem = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _cartItem.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public CartItem Current
        {
            get
            {
                try
                {
                    return _cartItem[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }

    [DataContract]
    public class Transaction
    {
        private int transactionId;
        private int customerId;
        private string sellerId;
        private double transactionTotal;
        private double taxTotal;
        private string paymentMethod;
        private string transactionLocation;
        private DateTime transactionDate;
        private bool isTransactionVoid;
        private bool isTransactionPaid;
        private string otherInformation;
        private string errorMessage;

        [DataMember]
        public int TransactionId
        {
            get
            {
                return this.transactionId;
            }
            set
            {
                this.transactionId = value;
            }
        }

        [DataMember]
        public int CustomerId
        {
            get
            {
                return this.customerId;
            }
            set
            {
                this.customerId = value;
            }
        }

        [DataMember]
        public string SellerId
        {
            get
            {
                return this.sellerId;
            }
            set
            {
                this.sellerId = value;
            }
        }

        [DataMember]
        public double TransactionTotal
        {
            get
            {
                return this.transactionTotal;
            }
            set
            {
                this.transactionTotal = value;
            }
        }

        [DataMember]
        public double TaxTotal
        {
            get
            {
                return this.taxTotal;
            }
            set
            {
                this.taxTotal = value;
            }
        }

        [DataMember]
        public string PaymentMethod
        {
            get
            {
                return this.paymentMethod;
            }
            set
            {
                this.paymentMethod = value;
            }
        }

        [DataMember]
        public string TransactionLocation
        {
            get
            {
                return this.transactionLocation;
            }
            set
            {
                this.transactionLocation = value;
            }
        }

        [DataMember]
        public DateTime TransactionDate
        {
            get
            {
                return this.transactionDate;
            }
            set
            {
                this.transactionDate = value;
            }
        }

        [DataMember]
        public bool IsTransactionVoid
        {
            get
            {
                return this.isTransactionVoid;
            }
            set
            {
                this.isTransactionVoid = value;
            }
        }

        [DataMember]
        public bool IsTransactionPaid
        {
            get
            {
                return this.isTransactionPaid;
            }
            set
            {
                this.isTransactionPaid = value;
            }
        }

        [DataMember]
        public string OtherInformation
        {
            get
            {
                return this.otherInformation;
            }
            set
            {
                this.otherInformation = value;
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
    public class TransactionItem
    {
        private int transactionItemId;
        private int transactionId;
        private int productId;
        private int itemQuantity;
        private string productName;
        private double productPrice;
        private bool isTaxed;
        private string errorMessage;

        [DataMember]
        public int TransactionItemId
        {
            get
            {
                return this.transactionItemId;
            }
            set
            {
                this.transactionItemId = value;
            }
        }

        [DataMember]
        public int TransactionId
        {
            get
            {
                return this.transactionId;
            }
            set
            {
                this.transactionId = value;
            }
        }

        [DataMember]
        public int ProductId
        {
            get
            {
                return this.productId;
            }
            set
            {
                this.productId = value;
            }
        }

        [DataMember]
        public int ItemQuantity
        {
            get
            {
                return this.itemQuantity;
            }
            set
            {
                this.itemQuantity = value;
            }
        }

        [DataMember]
        public string ProductName
        {
            get
            {
                return this.productName;
            }
            set
            {
                this.productName = value;
            }
        }

        [DataMember]
        public double ProductPrice
        {
            get
            {
                return this.productPrice;
            }
            set
            {
                this.productPrice = value;
            }
        }

        [DataMember]
        public bool IsTaxed
        {
            get
            {
                return this.isTaxed;
            }
            set
            {
                this.isTaxed = value;
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
    public class Product
    {
        private int productId = 0;
        private string productName = string.Empty;
        private string productDescription = string.Empty;
        private double productPrice = 0.00;
        private bool isTaxable = false;
        private double productSalePrice = 0.00;
        private bool isOnSaleOnline = false;
        private bool isOnSaleInStore = false;
        private bool isAvailableOnline = false;
        private bool isAvailableInStore = false;
        private int productCount = 0;
        private string productBarCode = string.Empty;
        private string productFileName = string.Empty;
        private string productType = string.Empty;
        private string productSubType = string.Empty;
        private bool isActive = false;
        private string errorMessage = string.Empty;

        [DataMember]
        public int ProductId
        {
            get
            {
                return this.productId;
            }
            set
            {
                this.productId = value;
            }
        }

        [DataMember]
        public string ProductName
        {
            get
            {
                return this.productName;
            }
            set
            {
                this.productName = value;
            }
        }

        [DataMember]
        public string ProductDescription
        {
            get
            {
                return this.productDescription;
            }
            set
            {
                this.productDescription = value;
            }
        }

        [DataMember]
        public double ProductPrice
        {
            get
            {
                return this.productPrice;
            }
            set
            {
                this.productPrice = value;
            }
        }

        [DataMember]
        public bool IsTaxable
        {
            get
            {
                return this.isTaxable;
            }
            set
            {
                this.isTaxable = value;
            }
        }

        [DataMember]
        public double ProductSalePrice
        {
            get
            {
                return this.productSalePrice;
            }
            set
            {
                this.productSalePrice = value;
            }
        }

        [DataMember]
        public bool IsOnSaleOnline
        {
            get
            {
                return this.isOnSaleOnline;
            }
            set
            {
                this.isOnSaleOnline = value;
            }
        }

        [DataMember]
        public bool IsOnSaleInStore
        {
            get
            {
                return this.isOnSaleInStore;
            }
            set
            {
                this.isOnSaleInStore = value;
            }
        }

        [DataMember]
        public bool IsAvailableOnline
        {
            get
            {
                return this.isAvailableOnline;
            }
            set
            {
                this.isAvailableOnline = value;
            }
        }

        [DataMember]
        public bool IsAvailableInStore
        {
            get
            {
                return this.isAvailableInStore;
            }
            set
            {
                this.isAvailableInStore = value;
            }
        }

        [DataMember]
        public int ProductCount
        {
            get
            {
                return this.productCount;
            }
            set
            {
                this.productCount = value;
            }
        }

        [DataMember]
        public string ProductCode
        {
            get
            {
                return this.productBarCode;
            }
            set
            {
                this.productBarCode = value;
            }
        }

        [DataMember]
        public string ProductFileName
        {
            get
            {
                return this.productFileName;
            }
            set
            {
                this.productFileName = value;
            }
        }

        [DataMember]
        public string ProductType
        {
            get
            {
                return this.productType;
            }
            set
            {
                this.productType = value;
            }
        }

        [DataMember]
        public string ProductSubType
        {
            get
            {
                return this.productSubType;
            }
            set
            {
                this.productSubType = value;
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