using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    class BookRepository : IBooKRepository<Book>
    {
        List<Book> books;
        public BookRepository()
        {
            books = new List<Book>()
            {
               new Book
               {
                    Id=1,Title="Pograming",Descroption="learn more to conding",
                   ImageUrl="images.jpg",
                   author=new Author
                    {Id=1}
               },
               new Book
               {
                    Id=2,Title="sporting",Descroption="Play and wathcinh",
                   ImageUrl="validate.png",

                   author=new Author
                    {Id=3}

               },

               new Book
               {
                    Id=3,Title="fung",Descroption="learn more to conding",
                   ImageUrl="coding.jpg",

                   author=new Author
                    {Id=2}
               }

            };
        }
        public void Add(Book entity)
        {
            entity.Id = books.Max(b=>b.Id)+1;
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var bo = Find(id);
            //var bos = books.SingleOrDefault(b => b.Id==id);
            books.Remove(bo); 
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.Id == id);

            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public List<Book> Search(string term)
        {
            return books.Where(a => a.Title.Contains(term)).ToList();

        }

        public void Update(int id,Book newbokk)
        {
            //var bo = books.SingleOrDefault(b => b.Id == id);
            var bo = Find(id);
            bo.Title = newbokk.Title;
            bo.Descroption = newbokk.Descroption;
            bo.author = newbokk.author;
            bo.ImageUrl = newbokk.ImageUrl;

        }
    }
}
