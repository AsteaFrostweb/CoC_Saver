using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIA;

namespace CoCSaver
{
    public static class IO
    {
        public static Bitmap TryScanImage()
        {
            WIA.CommonDialog dialog = new WIA.CommonDialog();
            Device scanner;
            try { scanner = dialog.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, true, false); }
            catch { MessageBox.Show("Error selecting scanner."); return null; }

            ImageFile image = null;
            if (scanner != null)
            {
                Item item = scanner.Items[1];
                image = (ImageFile)dialog.ShowTransfer(item, WIA.FormatID.wiaFormatJPEG, false);
            }

            return ImageProcessing.WiaImageFileToBitmap(image);
        }
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
