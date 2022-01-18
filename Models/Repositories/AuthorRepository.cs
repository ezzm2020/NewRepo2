using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repositories
{
    class AuthorRepository : IBooKRepository<Author>
    {
        IList<Author> authors;
        public AuthorRepository()
        {
            authors = new List<Author>()
            {
                new Author{Id=1,FullName="moo ezz"},
                new Author{Id=2,FullName="Ahmed tawfik"},
                new Author{Id=3,FullName="NethaR Abbani"}
            };
        }
        public void Add(Author entity)
        {
            entity.Id = authors.Max(b => b.Id) + 1;
            authors.Add(entity);
        }

        public void Delete(int id)
        {
            var auth = Find(id);
            authors.Remove(auth); 
        }

        public Author Find(int id)
        {
            var auth = authors.SingleOrDefault(a=>a.Id==id);
            return auth;
        }

        public IList<Author> List()
        {
            return authors;
        }

        public List<Author> Search(string term)
        {
            return  authors.Where(a=>a.FullName.Contains(term)).ToList(); 
        }

        public void Update(int id, Author author)
        {
            var auth = Find(id);

            auth.FullName = author.FullName;
        }
    }
}
