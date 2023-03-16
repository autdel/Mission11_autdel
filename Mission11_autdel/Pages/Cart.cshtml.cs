using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mission11_autdel.Infrastructure;
using Mission11_autdel.Models;

namespace Mission11_autdel.Pages
{
    public class CartModel : PageModel
    {
        private IBookstoreRepository repo { get; set; }

        // Needed objects and variables for cart info and return button
        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }

        public CartModel (IBookstoreRepository temp, Cart c)
        {
            repo = temp;
            cart = c;
        }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(int bookID, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookID);

            cart.AddItem(b, 1);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

        // Handles removing an item from the cart
        public IActionResult OnPostRemove (int bookID, string returnUrl)
        {
            cart.RemoveItem(cart.Items.First(x => x.Book.BookId == bookID).Book);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}
