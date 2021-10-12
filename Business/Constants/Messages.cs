using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarAdded = "Araba eklendi";
        public static string CarsListed = "Arabalar listelendi";
        public static string CarUpdated = "Araba güncellendi";
        public static string CarDeleted = "Araba silindi";
        public static string InvalidDailyPrice = "Geçersiz günlük fiyat";
        public static string CarImageAdded = "Araba resim eklendi";
        public static string CarImagesListed = "Araba resimleri listelendi";
        public static string CarImageUpdated = "Araba resim güncellendi";
        public static string CarImageDeleted = "Araba resim silindi";
        public static string CarImageLimitExceeded = "Bir arabanın maxismum 5 resmi olabilir";
        public static string CarImageUploadFailed = "Araba resmi yüklenirken bir hata oluştu";
        public static string UserRegistered = "Kullanıcı kayıt oldu";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Şifre hatalı";
        public static string SuccessfulLogin = "Başarıyla giriş yapıldı";
        public static string UserAlreadyExists = "Kullanıcı zaten kayıtlı";
        public static string AccessTokenCreated = "Bağlantı Jetonu oluşturuldu";
        public static string AuthorizationDenied = "Yetkniz yok";
        public static string BrandsListed = "Markalar listelendi";
        public static string BrandUpdated = "Marka güncellendi";
        public static string BrandDeleted = "Marka silindi";
        public static string BrandAdded = "Marka eklendi";
    }
}
