using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdventureWorksApp.Models;
using System.Data.Entity;

namespace AdventureWorksApp.Controllers
{
    [ValueReporter]
    public class PhotoController : Controller
    {
        private AdventureWorksDB _adventureWorksDB = new AdventureWorksDB();

        // GET: Photo
        public ActionResult Index()
        {
            return View("Index", _adventureWorksDB.Photos.ToList());
        }

        [ChildActionOnly]
        public ActionResult _PhotoGallery(int number = 0)
        {
            //We want to display only the latest photos when a positive integer is supplied to the view.
            //Otherwise we'll display them all
            List<Photo> photos;

            if (number == 0)
            {
                photos = _adventureWorksDB.Photos.ToList();
            }
            else
            {
                photos = (from photo in _adventureWorksDB.Photos
                          orderby photo.CreatedDate descending
                          select photo).Take(number).ToList();
            }

            return PartialView("_PhotoGallery", photos);
        }

        public ActionResult Details(int id)
        {
            Photo photo = _adventureWorksDB.Photos.Find(id);
            if(photo != null)
            {
                return View("Details", photo);
            }
            return HttpNotFound();
        }

        public ActionResult Create()
        {
            Photo photo = new Photo();

            return View("Create", photo);
        }

        //
        // POST: /Photo/Create
        [HttpPost]
        public ActionResult Create(Photo photo, HttpPostedFileBase image)
        {
            photo.Owner = "test";
            photo.CreatedDate = DateTime.Today;
            if (ModelState.IsValid)
            {
                //Is there a photo? If so save it
                if (image != null)
                {
                    photo.ImageMimeType = image.ContentType;
                    photo.PhotoFile = new byte[image.ContentLength];
                    image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
                }

                //Add the photo to the database and save it
                _adventureWorksDB.Photos.Add(photo);
                _adventureWorksDB.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(photo);
        }


        public ActionResult Delete(int id)
        {
            Photo photo = _adventureWorksDB.Photos.Find(id);
            if (photo != null)
            {
                return View("Delete", photo);
            }
            return HttpNotFound();
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = _adventureWorksDB.Photos.Find(id);
            _adventureWorksDB.Photos.Remove(photo);
            _adventureWorksDB.SaveChanges();
            return RedirectToAction("Index");
        }

        public FileContentResult GetImage(int PhotoId)
        {
            //Get the right photo
            Photo photo = _adventureWorksDB.Photos.FirstOrDefault(p => p.PhotoID == PhotoId);
            if (photo != null)
            {
                return File(photo.PhotoFile, photo.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public FileContentResult GetThumpnail(int PhotoId)
        {
            //Get the right photo
            Photo photo = _adventureWorksDB.Photos.FirstOrDefault(p => p.PhotoID == PhotoId);
            if (photo != null)
            {
                return File(photo.ThumpnailFile, photo.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}