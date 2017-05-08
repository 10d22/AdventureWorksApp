using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace AdventureWorksApp.Models
{
    public class AdventureWorksInitializer : DropCreateDatabaseAlways<AdventureWorksDB>
    {
        protected override void Seed(AdventureWorksDB context)
        {

            var photos = new List<Photo>
            {
                new Photo
                {
                    Title = "Test",
                    Description = "Test Description",
                    PhotoFile = GetFileBytes("\\Images\\flower.jpg"),
                    ThumpnailFile = GetFileBytes("\\Images\\flower.jpg"),
                    CreatedDate = DateTime.Now,
                    ImageMimeType = "image/jpeg",
                    Owner = "Sebastian"
                },
                new Photo
                {
                    Title = "Test2",
                    Description = "Test2 Description",
                    PhotoFile = GetFileBytes("\\Images\\flower.jpg"),
                    ThumpnailFile = GetFileBytes("\\Images\\flower.jpg"),
                    CreatedDate = DateTime.Now,
                    ImageMimeType = "image/jpeg",
                    Owner = "Lars"
                },
                new Photo
                {
                    Title = "Test3",
                    Description = "Test3 Description",
                    PhotoFile = GetFileBytes("\\Images\\flower.jpg"),
                    ThumpnailFile = GetFileBytes("\\Images\\flower.jpg"),
                    CreatedDate = DateTime.Now,
                    ImageMimeType = "image/jpeg",
                    Owner = "Bo"
                }
             };

            photos.ForEach(s => context.Photos.Add(s));
            context.SaveChanges();

            var comments = new List<Comment>
                {
                new Comment
                {
                    PhotoID = 1,
                    Username = "TESTNAME",
                    Text = "this is a comment for photo 1"
                }
             };

            
            comments.ForEach(s => context.Comments.Add(s));
            context.SaveChanges();
        }
        //This gets a byte array for a file at the path specified
        //The path is relative to the route of the web site
        //It is used to seed images
        private byte[] GetFileBytes(string path)
        {
            FileStream fileOnDisk = new FileStream(HttpRuntime.AppDomainAppPath + path, FileMode.Open);
            byte[] fileBytes;
            using (BinaryReader br = new BinaryReader(fileOnDisk))
            {
                fileBytes = br.ReadBytes((int)fileOnDisk.Length);
            }
            return fileBytes;
        }
    }

}