using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace RedCoreApi_Test.Util
{
    public class ImageUtil
    {
        public static  Bitmap Base64StringToBitmap(string base64String)
        {
            Bitmap bmpReturn = null;

            byte[] byteBuffer = Convert.FromBase64String(base64String);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);

            memoryStream.Position = 0;

            bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);

            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;

            return bmpReturn;
        }

        public static string SaveImageFile(string filename, string base64Image)
        {
            string mainPath =  "./host_file"; mainPath = Path.GetFullPath(mainPath);
            string destinationImgPath = mainPath + "\\" + filename+".png";
            try
            {
                Directory.CreateDirectory(mainPath );
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message + " " + iox.Data);
            }
            var bytes = Convert.FromBase64String(base64Image);
            using (var imageFile = new FileStream(destinationImgPath, FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                imageFile.Flush();
            }
            return destinationImgPath; 
        }
        public static string exportbase64toimg(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            string filepath = Guid.NewGuid() + ".jpg";
            using (Image image = Image.FromStream(new MemoryStream(bytes)))
            {
                image.Save(filepath, ImageFormat.Jpeg);  // Or Png
            }
            return filepath;
        }

        public static string  BlobUrl()
        { 
            var account = new CloudStorageAccount(new StorageCredentials("sphv2", "PVIKvZEUBne+2R46SX042EvYp200tU2hZ5Ht3zOf4IdNqljiySgue2HmpNn61GZiMOcBHIyYGlszFCwP7JhYbA=="), true);
            var cloudBlobClient = account.CreateCloudBlobClient();
            var container = cloudBlobClient.GetContainerReference("randomproject");
            var blob = container.GetBlockBlobReference("image.png");
            blob.UploadFromFile(@"C:\Users\Michael\Documents\host_file\1.png");//Upload file....
            string blobUrl = blob.Uri.AbsoluteUri;
            return blobUrl;
        }
        public static void ExportToImage(string base64)
        {
            ////string base64 = Request.Form[hfImageData.UniqueID].Split(',')[1];
            byte[] bytes = Convert.FromBase64String(base64);
            using (Image image = Image.FromStream(new MemoryStream(bytes)))
            {
                image.Save("output.png", ImageFormat.Png);  // Or Png
            }
            //File.Copy(bytes.ToString()+".jpg", "\\\\192.168.2.9\\Web");
            
            // Create a string array with the lines of text
            //string[] lines = { base64, "Second line", "Third line" };

            //// Set a variable to the Documents path.
            //string docPath =
            //  Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //// Write the string array to a new file named "WriteLines.txt".
            //using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "WriteLines.txt")))
            //{
            //    foreach (string line in lines)
            //        outputFile.WriteLine(line);
            //}
        }
        public  static bool SaveImage(string ImgStr, string ImgName)
        {
            String path = HttpContext.Current.Server.MapPath("~/ImageStorage"); //Path

            //Check if directory exist
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }

            string imageName = ImgName + ".jpg";

            //set the image path
            string imgPath = Path.Combine(path, imageName);

            byte[] imageBytes = Convert.FromBase64String(ImgStr);

            File.WriteAllBytes(imgPath, imageBytes);

            return true;
        }
        public static  Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }
        public static string saveBitmaptoimage(Bitmap bitmap)
        {
            string filepath = Guid.NewGuid()+".png";
            Bitmap bmp = bitmap;
            bmp.Save(filepath, ImageFormat.Png);
            return filepath;
        }
    }
}