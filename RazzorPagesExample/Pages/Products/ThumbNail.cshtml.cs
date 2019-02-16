using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazzorPagesExample.Pages.Products
{
    public class ThumbNailModel : PageModel
    {
        private readonly IHostingEnvironment _host;

        public ThumbNailModel(IHostingEnvironment host)
        {
            _host = host;

        }

        private Image CropImage(Image sourceImage, int sourceX, int sourceY, int sourceWidth, int sourceHeight, int destinationWidth, int destinationHeight)
        {  
         Image destinationImage = new Bitmap(destinationWidth, destinationHeight); 
        
        
         using (Graphics g = Graphics.FromImage(destinationImage)) 
           g.DrawImage(
             sourceImage, 
             new Rectangle(0, 0, destinationWidth, destinationHeight), 
             new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), 
              GraphicsUnit.Pixel 
            ); 
        
        
          return destinationImage; 
        }


        public IActionResult OnGet()
        {

            //string fileName = Request.Query["FileName"].ToString();
            //string dir = _host.WebRootPath + "\\uploads\\";
            //string file = Path.Combine(dir, fileName);

            //using (Image sourceImage = Image.FromFile(file) )
            //{
            //    if (sourceImage != null)
            //    {
            //        try
            //        {
            //            using (Image destinationImage = this.CropImage(sourceImage, 1274, 829, sourceWidth, sourceHeight, destinationWidth, destinationHeight))
            //            {
            //                Stream outputStream = new MemoryStream();
            //                destinationImage.Save(outputStream, ImageFormat.Jpeg);
            //                outputStream.Seek(0, SeekOrigin.Begin);
            //                return this.File(outputStream, "image/png");
            //            }
            //        }
            //        catch (Exception)
            //        {

            //            throw;
            //        }
            //    }

            //    return NotFound();

            //}


            //참고 1
            //string fileName = Request.Query["FileName"].ToString();
            //string dir = _host.WebRootPath + "\\uploads\\";
            //string file = Path.Combine(dir, fileName);


            //Image image = Image.FromFile(file);
            //Image thumb = image.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
            //thumb.Save(System.IO.File.OpenRead(file), image.RawFormat);

            //참고2
            //Bitmap b = new Bitmap(file);
            //Bitmap bmp1 = new Bitmap(b, 100, 100);
            //bmp1.Save(System.IO.File.OpenRead(file), b.RawFormat);


            int boxWidth = 100;
            int boxHeight = 100;
            double scale = 0;


            string fileName = string.Empty;
            string selectedFile = string.Empty;
            string dir = _host.WebRootPath + "\\uploads\\";

            if (!string.IsNullOrEmpty((Request.Query["FileName"])))
            {

                selectedFile = Request.Query["FileName"];
                fileName = Path.Combine(dir, selectedFile);
            }
            else
            {
                selectedFile = "/images/Library/dnn/img.jpg";
                fileName = Path.Combine(dir, selectedFile);

            }

            string ext = Path.GetExtension(fileName);
            string contentType = string.Empty;

            if (ext == ".gif" || ext == ".jpg" || ext == ".jpeg" || ext == ".png")
            {
                switch (ext)
                {
                    case ".gif":
                        contentType = "image/gif"; break;
                    case ".jpg":
                        contentType = "image/jpeg"; break;
                    case ".jpeg":
                        contentType = "image/jpeg"; break;
                    case ".png":
                        contentType = "image/png"; break;
                }

            }
            else
            {
                return Content("이미지 파일이 아닙니다.");

            }

            //[C] 넘어온 Width와 Height 쿼리스트링 저장  
            int tmpW = 0;
            int tmpH = 0;

            if (!String.IsNullOrEmpty(Request.Query["Width"])
                && (!String.IsNullOrEmpty(Request.Query["Height"])))
            {
                tmpW = Convert.ToInt32(Request.Query["Width"]);
                tmpH = Convert.ToInt32(Request.Query["Height"]);
            }

            if (tmpW > 0 && tmpH > 0)
            {
                boxWidth = tmpW;
                boxHeight = tmpH;
            }

            //[D] 새 이미지 생성
            Bitmap b = new Bitmap(fileName);

            // 이미지 비율에 맞게 비율 조절: 크기 비율을 계산한다.
            if (b.Height < b.Width)
            {
                scale = ((double)boxHeight) / b.Width;
            }
            else
            {
                scale = ((double)boxWidth) / b.Height;
            }

            // 새 너비와 높이를 설정한다.
            int newWidth = (int)(scale * b.Width);
            int newHeight = (int)(scale * b.Height);

            //[E] 출력 비트맵을 생성, 출력한다.
            Stream outputStream = new MemoryStream();

            Bitmap bOut = new Bitmap(b, newWidth, newHeight);
            bOut.Save(outputStream, b.RawFormat);
            outputStream.Seek(0, SeekOrigin.Begin);

            // 마무리
            b.Dispose();
            bOut.Dispose();

           

            return File(outputStream, contentType);

            

        }
    }
}