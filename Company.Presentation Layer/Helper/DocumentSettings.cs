using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Company.Presentation_Layer.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            // 1. Get located fold path 

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", folderName);

            // 2.Get File Name
            var fileName = $"{Guid.NewGuid()}{Path.GetFileName( file.FileName) }"; 
            
            // 3. Get file path 
            var filePath = Path.Combine(folderPath, fileName);
            //4. save file  
            
            using var fileStream = new FileStream(filePath, FileMode.Create); 
            file.CopyTo(fileStream);

            return fileName;
        }

        public static void DeleteFile(string fileName , string FolderName) // to Del file itself in fold Imgs 
        {
            var filePath = Path.Combine( Directory.GetCurrentDirectory() ,"wwwroot/files" , FolderName, fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
