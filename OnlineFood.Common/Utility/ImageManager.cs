using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OnlineFood.Common.Utility
{
    public static class ImageManager
    {
        public static string DoumentFolder = "document";
        public static string RestaurantFolder = "restaurant";

        // Basic validation on mime types and file extension
        private static string[] imageMimetypes = { "image/jpeg", "image/pjpeg", "image/png", };
        private static string[] imageExt = { ".jpeg", ".jpg", ".png" };

        public static bool IsValidExtension(string extension)
        {
            return Array.IndexOf(imageExt, extension) >= 0;
        }

        public static bool IsValidMimeType(string mimeType)
        {
            return Array.IndexOf(imageMimetypes, mimeType) >= 0;
        }

        public static bool ValidateImage(IFormFile image)
        {
            bool isValid = false;
            string extension = Path.GetExtension(image.FileName);
            var mim = image.ContentType;
            if (IsValidMimeType(mim) || IsValidExtension(extension))
                isValid = true;
            return isValid;
        }

        public static string GetImageLink(string webRootPath, string imageFolderName ,string userFolderName, string extension, ref string fileName)
        {
            // Building the path to the uploads directory
            var fileRoute = Path.Combine(webRootPath, string.Format("upload\\{0}\\{1}\\", imageFolderName, userFolderName));
            // Generate Random name.
            fileName = Guid.NewGuid().ToString().Substring(0, 8) + extension;
            // Build the full path inclunding the file name
            string link = Path.Combine(fileRoute, fileName);
            // Create directory if it does not exist.
            FileInfo dir = new FileInfo(fileRoute);
            /*if (!dir.Directory.Exists)*/
            dir.Directory.Create();
            return link;
        }

        public static string GetImageUrl(string imageFolderName, string userFolderName, string fileName)
        {
            // Return the file path as json
            Hashtable imageUrl = new Hashtable();
            imageUrl.Add("link", string.Format("/upload/{0}/{1}/{2}",imageFolderName ,userFolderName, fileName));
            return imageUrl.Values.Cast<string>().FirstOrDefault();
        }


        static readonly string[] SizeSuffixesENG = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        static readonly string[] SizeSuffixesFA = { "بایت", "کیلوبایت", "مگابایت", "گیگابایت", "ترابایت", "PB", "EB", "ZB", "YB" };

        /// <summary>
        /// convert size of image to string label
        /// </summary>
        /// <param name="value">size</param>
        /// <returns></returns>
        public static string SizeSuffix(Int64 value)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0.0 bytes"; }

            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixesENG[mag]);
        }
    }
}
