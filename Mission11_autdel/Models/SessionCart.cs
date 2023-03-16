using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Mission11_autdel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mission11_autdel.Models
{
    // Inherit from the Cart class
    public class SessionCart : Cart
    {
        public static Cart GetCart (IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            // Gets and uses session if available, otherwise create new session cart
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();

            // Update the session information and return the cart to whatever called the method
            cart.Session = session;

            return cart;
        }

        [JsonIgnore] // Prevents it from being serialized or deserialized
        public ISession Session { get; set; }

        // Method overrides
        public override void AddItem(Book book, int qty)
        {
            base.AddItem(book, qty);
            Session.SetJson("Cart", this);
        }

        public override void RemoveItem(Book book)
        {
            base.RemoveItem(book);
            Session.SetJson("Cart", this);
        }

        public override void ClearCart()
        {
            base.ClearCart();
            Session.Remove("Cart");
        }

    }
}
