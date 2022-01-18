using BookStore.Models;
using BookStore.Models.Repositories;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BooKController : Controller
    {
        public IBooKRepository<Book> bookrepository;
        private readonly IBooKRepository<Author> authorRepository;

        public IHostingEnvironment hosting { get; }

        public BooKController(IBooKRepository<Book> bookrepository, IBooKRepository<Author> authorRepository , 
            IHostingEnvironment hosting)
        {
            this.bookrepository = bookrepository;
            this.authorRepository = authorRepository;
           this.hosting = hosting;
        }
        // GET: BooKController
        public ActionResult Index()
        {
            var books = bookrepository.List();
            return View(books);
        }

        // GET: BooKController/Details/5
        public ActionResult Details(int id)
        {
            var bb = bookrepository.Find(id);
            return View(bb);


        }

        // GET: BooKController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                //Authors = authorRepository.List().ToList()
                Authors = FillSelectList() 

            };
            return View(model );
        }

        // POST: BooKController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
               
                    string fileName = UploadFile(model.File) ?? string.Empty;

                    if (model.AuthorId == -1)
                    {
                        ViewBag.Mess = "pls select author";
                        //var vmodel = new BookAuthorViewModel
                        //{
                        //    //Authors = authorRepository.List().ToList()
                        //    Authors = FillSelectList()

                        //};
                        return View(GetAllAuth());
                    }
                    var auths = authorRepository.Find(model.AuthorId);
                    Book book = new Book
                    {
                        Id = model.BooKID,
                        Title = model.Title,
                        Descroption = model.Descroption,
                        author = auths,
                        ImageUrl= fileName
                    };
                    bookrepository.Add(book);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            
            ModelState.AddModelError("", "You Must Enter all Fields");
            return View(GetAllAuth());

        }

        // GET: BooKController/Edit/5
        public ActionResult Edit(int id)
        {

            //نبحث علي اللكتاب اللي عاوز يتعدل 
            var book = bookrepository.Find(id);
            var authid = book.author == null ? book.author.Id = 0 : book.author.Id;
            var bookmodel = new BookAuthorViewModel
            {
                BooKID = book.Id,
                Title = book.Title,
                Descroption = book.Descroption,
                AuthorId = authid,
                Authors = authorRepository.List().ToList(),
                Imgurl = book.ImageUrl

            };
            return View(bookmodel);
            ////نبحث علي اللكتاب اللي عاوز يتعدل 
            //var book = bookrepository.Find(id);
            //var authid = book.author == null ? book.author.Id = 0 :book.author.Id;
            //var bookmodel = new BookAuthorViewModel
            //{
            //    BooKID=book.Id,
            //    Title=book.Title,
            //    Descroption=book.Descroption,
            //    AuthorId= authid,
            //    Authors=authorRepository.List().ToList() ,
            //    Imgurl=book.ImageUrl

            //};
            //return View(bookmodel);
        }

        // POST: BooKController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAuthorViewModel model)
        {

            try
            {
            
                string fileName = UploadFile(model.File, model.Imgurl);


                var auths = authorRepository.Find(model.AuthorId);
                Book book = new Book
                {
                    Id = model.BooKID,
                    Title = model.Title,
                    Descroption = model.Descroption,
                    author = auths,
                    ImageUrl = fileName
                };
                bookrepository.Update(model.BooKID, book);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BooKController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookrepository.Find(id);
            return View(book);
        }
        public ActionResult Search(string term)
        {
            var result = bookrepository.Search(term);
            return View("Index",result);
        }


        // POST: BooKController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id )
        {
            try
            {
                bookrepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<Author>FillSelectList()
        {
            var authors = authorRepository.List().ToList();
            authors.Insert(0,new Author { Id=-1,FullName="---pls select author"});
            return authors;
        }
        BookAuthorViewModel GetAllAuth()
        {
            var vmodel = new BookAuthorViewModel
            {
                //Authors = authorRepository.List().ToList()
                Authors = FillSelectList()

            };
            return vmodel;
        }
        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                string fullPath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));

                return file.FileName;
            }

            return null;
        }

        string UploadFile(IFormFile file, string imageUrl)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");

                string newPath = Path.Combine(uploads, file.FileName);
                string oldPath = Path.Combine(uploads, imageUrl);

                if (oldPath != newPath)
                {
                    System.IO.File.Delete(oldPath);
                    file.CopyTo(new FileStream(newPath, FileMode.Create));
                }

                return file.FileName;
            }

            return imageUrl;
        }

        //public ActionResult Search(string term)
        //{
        //    var result = bookrepository.Search(term);

        //    return View("Index", result);
        //}

        //string UploadFile(IFormFile file)
        //{
        //    if (file != null)
        //    {
        //        string upload = Path.Combine(hosting.WebRootPath, "uploads");//ملف اللي موجود في root
        //                                                                     //filename = model.File.FileName;//اسم الصوره
        //        string fullpath = Path.Combine(upload, file.FileName);
        //        file.CopyTo(new FileStream(fullpath, FileMode.Create));//حفظ الملف
        //        return file.FileName;
        //    }
        //    return null;
        //}

        //string UploadFile(IFormFile file, string imgurl)
        //{

        //    if (file != null)
        //    {
        //        string upload = Path.Combine(hosting.WebRootPath, "uploads");//ملف اللي موجود في root
        //        string newpath = Path.Combine(upload, file.FileName);
        //        //حذف الصوره القديمه 
        //        string oldpath = Path.Combine(upload, imgurl);
        //        //الصوره الجديده لا تساوي الصوره القديمه
        //        if (oldpath != newpath)
        //        {
        //            System.IO.File.Delete(oldpath);
        //            //حفظ مسار الصوره الجديده
        //            file.CopyTo(new FileStream(newpath, FileMode.Create));//حفظ الملف
        //        }
        //        return file.FileName;

        //    }
        //    return imgurl;
        //}

    }


}
