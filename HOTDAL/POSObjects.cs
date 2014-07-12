using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Collections;

namespace HOTDAL
{
     public class ShoppingCart : IEnumerable
        {
            private CartItem[] _cartItem;

            public CartItem[] CartItem
            {
                get
                {
                    return this._cartItem;
                }
                set
                {
                    this._cartItem = value;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)GetEnumerator();
            }

            public CartItemEnum GetEnumerator()
            {
                return new CartItemEnum(_cartItem);
            }
        }

        public class CartItem
        {
            private Int32 _itemID;

            private string _itemName;

            private Double _itemPrice;

            private Int32 _itemQuantity;

            private string _itemType;

            private bool _itemTaxed;


            public Int32 ItemID
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

            public Int32 ItemQuantity
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
            private Int32 _ID;
            private Int32 _custID;
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

            public Int32 ID
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
            private Int32 _ID;
            private Int32 _transactionID;
            private Int32 _productID;
            private Int32 _quantity;
            private string _productName;
            private Double _price;
            private bool _tax;
            private string _error;

            public Int32 ID
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

            public Int32 TransactionID
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

            public Int32 Quantity
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
            private Int32 _productID = 0;

            private string _productName = TansMessages.ERROR_NO_PRODUCT_TYPE;

            private string _productDescription = TansMessages.ERROR_NO_PRODUCT_TYPE;

            private Double _productPrice = 0.00;

            private bool _productTaxable = false;

            private Double _productSalePrice = 0.00;

            private bool _productSaleOnline = false;

            private bool _productSaleInStore = false;

            private bool _productOnlineOnly = false;

            private bool _productInStoreOnly = false;

            private Int32 _productCount = 0;

            private string _productCode = "";

            private string _productFileName = "";

            private string _productType = "";

            private string _productCategory = "";

            private bool _productActive = false;


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

            public string ProductDescription
            {
                get
                {
                    return this._productDescription;
                }
                set
                {
                    this._productDescription = value;
                }
            }

            public Double ProductPrice
            {
                get
                {
                    return this._productPrice;
                }
                set
                {
                    this._productPrice = value;
                }
            }

            public bool ProductTaxable
            {
                get
                {
                    return this._productTaxable;
                }
                set
                {
                    this._productTaxable = value;
                }
            }

            public Double ProductSalePrice
            {
                get
                {
                    return this._productSalePrice;
                }
                set
                {
                    this._productSalePrice = value;
                }
            }

            public bool ProductSaleOnline
            {
                get
                {
                    return this._productSaleOnline;
                }
                set
                {
                    this._productSaleOnline = value;
                }
            }

            public bool ProductSaleInStore
            {
                get
                {
                    return this._productSaleInStore;
                }
                set
                {
                    this._productSaleInStore = value;
                }
            }

            public bool ProductOnlineOnly
            {
                get
                {
                    return this._productOnlineOnly;
                }
                set
                {
                    this._productOnlineOnly = value;
                }
            }

            public bool ProductInStoreOnly
            {
                get
                {
                    return this._productInStoreOnly;
                }
                set
                {
                    this._productInStoreOnly = value;
                }
            }

            public Int32 ProductCount
            {
                get
                {
                    return this._productCount;
                }
                set
                {
                    this._productCount = value;
                }
            }

            public string ProductCode
            {
                get
                {
                    return this._productCode;
                }
                set
                {
                    this._productCode = value;
                }
            }

            public string ProductFileName
            {
                get
                {
                    return this._productFileName;
                }
                set
                {
                    this._productFileName = value;
                }
            }

            public string ProductType
            {
                get
                {
                    return this._productType;
                }
                set
                {
                    this._productType = value;
                }
            }

            public string ProductCategory
            {
                get
                {
                    return this._productCategory;
                }
                set
                {
                    this._productCategory = value;
                }
            }

            public bool Active
            {
                get
                {
                    return this._productActive;
                }
                set
                {
                    this._productActive = value;
                }
            }
        }
}