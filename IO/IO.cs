using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoCSaver
{
    public static class IO
    {

        public static void TrySaveImage(string rootDirectory, string fileName, string extension, Bitmap image) 
        {
            string parsedText = fileName;
            try
            {                
                string safeFileName = string.Concat(parsedText.Split(System.IO.Path.GetInvalidFileNameChars()));
                string savePath = System.IO.Path.Combine(rootDirectory, safeFileName + "." + extension);

                System.Drawing.Imaging.ImageFormat format;
                switch (extension.ToLower())
                {
                    case "jpg":
                    case "jpeg":
                        format = System.Drawing.Imaging.ImageFormat.Jpeg;
                        break;
                    case "png":
                        format = System.Drawing.Imaging.ImageFormat.Png;
                        break;
                    case "bmp":
                        format = System.Drawing.Imaging.ImageFormat.Bmp;
                        break;
                    default:
                        format = System.Drawing.Imaging.ImageFormat.Jpeg;
                        break;
                }

                image.Save(savePath, format);
                MessageBox.Show($"Image saved to: {savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving image: " + ex.Message);
            }
        }
    }
}
