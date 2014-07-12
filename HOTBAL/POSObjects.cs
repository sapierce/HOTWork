using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Collections;

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

        public class CartItem
        {
            private int _itemID;

            private string _itemName;

            private Double _itemPrice;

            private int _itemQuantity;

            private string _itemType;

            private string _itemSubType;

            private bool _itemTaxed;


            public int ItemID
            {
                get
                {
                    return this._itemID;
                }
                set
                {
                    this._itemID = value;
                }
            }

            public string ItemName
            {
                get
                {
                    return this._itemName;
                }
                set
                {
                    this._itemName = value;
                }
            }

            public Double ItemPrice
            {
                get
                {
                    return this._itemPrice;
                }
                set
                {
                    this._itemPrice = value;
                }
            }

            public int ItemQuantity
            {
                get
                {
                    return this._itemQuantity;
                }
                set
                {
                    this._itemQuantity = value;
                }
            }

            public string ItemType
            {
                get
                {
                    return this._itemType;
                }
                set
                {
                    this._itemType = value;
                }
            }

            public string ItemSubType
            {
                get
                {
                    return this._itemSubType;
                }
                set
                {
                    this._itemSubType = value;
                }
            }

            public bool ItemTaxed
            {
                get
                {
                    return this._itemTaxed;
                }
                set
                {
                    this._itemTaxed = value;
                }
            }
        }

        public class CartItemEnum : IEnumerator
        {
            public CartItem[] _cartItem;

            // Enumerators are positioned before the first element
            // until the first MoveNext() call.
            int position = -1;

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

        public class Transaction
        {
            private int _ID;
            private int _custID;
            private string _seller;
            private Double _total;
            private Double _tax;
            private string _payment;
            private string _location;
            private DateTime _date;
            private bool _void;
            private bool _paid;
            private string _other;
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

            public int CustomerID
            {
                get
                {
                    return this._custID;
                }
                set
                {
                    this._custID = value;
                }
            }

            public string Seller
            {
                get
                {
                    return this._seller;
                }
                set
                {
                    this._seller = value;
                }
            }

            public Double Total
            {
                get
                {
                    return this._total;
                }
                set
                {
                    this._total = value;
                }
            }

            public Double Tax
            {
                get
                {
                    return this._tax;
                }
                set
                {
                    this._tax = value;
                }
            }

            public string Payment
            {
                get
                {
                    return this._payment;
                }
                set
                {
                    this._payment = value;
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

            public bool Void
            {
                get
                {
                    return this._void;
                }
                set
                {
                    this._void = value;
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

            public string Other
            {
                get
                {
                    return this._other;
                }
                set
                {
                    this._other = value;
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

        public class TransactionItem
        {
            private int _ID;
            private int _transactionID;
            private int _productID;
            private int _quantity;
            private string _productName;
            private Double _price;
            private bool _tax;
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

            public int TransactionID
            {
                get
                {
                    return this._transactionID;
                }
                set
                {
                    this._transactionID = value;
                }
            }

            public int ProductID
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

            public int Quantity
            {
                get
                {
                    return this._quantity;
                }
                set
                {
                    this._quantity = value;
                }
            }

            public string ProductName
            {
                get
                {
                    return this._productName;
                }
                set
                {
                    this._productName = value;
                }
            }

            public Double Price
            {
                get
                {
                    return this._price;
                }
                set
                {
                    this._price = value;
                }
            }

            public bool Tax
            {
                get
                {
                    return this._tax;
                }
                set
                {
                    this._tax = value;
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

        public class Product
        {
            private int productID = 0;

            private string productName = string.Empty;

            private string productDescription = string.Empty;

            private double productPrice = 0.00;

            private bool productTaxable = false;

            private double productSalePrice = 0.00;

            private bool productSaleOnline = false;

            private bool productSaleInStore = false;

            private bool productAvailableOnline = false;

            private bool productAvailableInStore = false;

            private int productCount = 0;

            private string productBarCode = string.Empty;

            private string productFileName = string.Empty;

            private string productType = string.Empty;

            private string productSubType = string.Empty;

            private bool productActive = false;

            private string errorMessage = string.Empty;


            public int ProductID
            {
                get
                {
                    return this.productID;
                }
                set
                {
                    this.productID = value;
                }
            }

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

            public Double ProductPrice
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

            public bool ProductTaxable
            {
                get
                {
                    return this.productTaxable;
                }
                set
                {
                    this.productTaxable = value;
                }
            }

            public Double ProductSalePrice
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

            public bool ProductSaleOnline
            {
                get
                {
                    return this.productSaleOnline;
                }
                set
                {
                    this.productSaleOnline = value;
                }
            }

            public bool ProductSaleInStore
            {
                get
                {
                    return this.productSaleInStore;
                }
                set
                {
                    this.productSaleInStore = value;
                }
            }

            public bool ProductAvailableOnline
            {
                get
                {
                    return this.productAvailableOnline;
                }
                set
                {
                    this.productAvailableOnline = value;
                }
            }

            public bool ProductAvailableInStore
            {
                get
                {
                    return this.productAvailableInStore;
                }
                set
                {
                    this.productAvailableInStore = value;
                }
            }

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

            public bool Active
            {
                get
                {
                    return this.productActive;
                }
                set
                {
                    this.productActive = value;
                }
            }

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