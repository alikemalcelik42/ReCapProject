using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.Helpers.FileHelpers
{
    public static class CarImageFileHelper
    {
        private static string carImageFolderName = "Images\\CarImages";
        private static string[] roots = {
            Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Business", carImageFolderName),
            Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "WebAPI", "wwwroot", carImageFolderName)};
        private static string defaultCarImage = "defaultCar.png";
        private static string baseUrl = "https://localhost:44352/";

        public static object Request { get; private set; }

        public static IResult Upload(IFormFile formFile, ref CarImage carImage)
        {

            if (formFile.Length > 0)
            {
                string fileName = Path.GetFileName(formFile.FileName);
                string guid = Guid.NewGuid().ToString();
                string fileExtension = Path.GetExtension(fileName);
                string newFileName = guid + fileExtension;
                var urlPath = baseUrl + carImageFolderName.Replace("\\", "/") + "/" + newFileName;

                foreach (string root in roots)
                {
                    string filePath = root + $@"\{newFileName}";

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }
                }

                carImage.ImagePath = urlPath;
                carImage.Date = DateTime.Now;
                return new SuccessResult();
            }
            return new ErrorResult(Messages.CarImageUploadFailed);
        }

        public static IResult Delete(string fileName)
        {
            try
            {
                foreach (string root in roots)
                {
                    string filePath = root + $@"\{fileName}";
                    File.Delete(filePath);
                }
                return new SuccessResult();
            }
            catch (Exception exception)
            {
                return new ErrorResult(exception.Message);
            }
        }


        public static string GetDefaultCarImagePath()
        {
            return baseUrl + carImageFolderName.Replace("\\", "/") + "/" + defaultCarImage;
        }
    }
}
