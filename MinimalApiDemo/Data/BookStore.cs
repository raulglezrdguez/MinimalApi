using System;
using MinimalApiDemo.Models;

namespace MinimalApiDemo.Data
{
    public static class BookStore
    {
        public static List<Book> books = new List<Book>
        {
            new Book{Id=1, Title="Book one", IsActive=true},
            new Book{Id=2, Title="Book two", IsActive=false}
        };

    }
}

