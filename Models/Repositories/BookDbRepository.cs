﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    public class BookDbRepository : IBooKRepository<Book>
    {

        BookStroeDbContext db;

        public BookDbRepository(BookStroeDbContext _db)
        {
            db = _db;
        }
        public void Add(Book entity)
        {
            db.Books.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = Find(id);

            db.Books.Remove(book);
            db.SaveChanges();
        }

        public Book Find(int id)
        {
            var book = db.Books.Include(a => a.author).SingleOrDefault(b => b.Id == id);

            return book;
        }

        public IList<Book> List()
        {
            return db.Books.Include(a => a.author).ToList();
        }

        public void Update(int id, Book newBook)
        {
            db.Update(newBook);
            db.SaveChanges();
        }

        public List<Book> Search(string term)
        {
            var result = db.Books.Include(a => a.author)
                .Where(b => b.Title.Contains(term)
                        || b.Descroption.Contains(term)
                        || b.author.FullName.Contains(term)).ToList();

            return result;
        }
    }
}
