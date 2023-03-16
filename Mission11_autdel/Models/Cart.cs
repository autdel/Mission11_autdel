using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mission11_autdel.Models
{
    public class Cart
    {
        // Creates an instance of a cart
        public List<CartLineItem> Items { get; set; } = new List<CartLineItem>();

        public virtual void AddItem (Book book, int qty)
        {
            CartLineItem line = Items.Where(b => b.Book.BookId == book.BookId).FirstOrDefault();

            if (line == null)
            {
                Items.Add(new CartLineItem
                {
                    Book = book,
                    Quantity = qty
                });
            }
            else
            {
                line.Quantity += qty;
            }
        }

        public virtual void RemoveItem(Book book)
        {
            Items.RemoveAll(x => x.Book.BookId == book.BookId);
        }

        public virtual void ClearCart()
        {
            Items.Clear();
        }

        public double CalculateTotal()
        {
            double sum = Items.Sum(x => x.Quantity * x.Book.Price);

            return sum;
        }
    }

    // Used for cart line items
    public class CartLineItem
    {
        [Key]
        public int LineID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
