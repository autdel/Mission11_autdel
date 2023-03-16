using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission11_autdel.Models
{
    public class EFBookstoreRepository : IBookstoreRepository
    {
        private BookstoreContext context { get; set; }

        public EFBookstoreRepository(BookstoreContext temp)
        {
            context = temp;
        }
        public IQueryable<Book> Books => context.Books;
    }
}
