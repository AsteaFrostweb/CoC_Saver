using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIA;
using Tesseract;


namespace CoCSaver
{
    public partial class Form1 : Form
    {
        private ScalableImage scalableImage;
        private string rootDirectory;
        private string parsedText;
        private byte ppBrightnessThreshold = 132;
        private Bitmap croppedImage = null;
        private OCR ocrProcessor;

        public Form1()
        {
            InitializeComponent();

            //Initialize OCR isntance
            ocrProcessor = new OCR("./tessdata", "eng");          

            // Create scalable image wrapper around panel
            scalableImage = new ScalableImage(ImagePanel);
            scalableImage.CropRect.onEndSelection += (s, e) => TryUpdateCroppedImage();

            // Initialize combo box
            SaveFormatComboBox.Items.AddRange(new string[] { "jpg", "png", "bmp" });
            SaveFormatComboBox.SelectedIndex = 0;

            
        }

        //-------------------------------------- OCR + Cropping --------------------------------------
        private void TryUpdateCroppedImage()
        {
            if (scalableImage.CropRect.HasSelection)
            {
                UpdateCroppedImage();
                ParseText();
            }
        }

        private void UpdateCroppedImage()
        {
            croppedImage = GetCroppedPreProcessedArea();
            CroppedImagePictureBox.Invalidate();
        }

        private Bitmap GetCroppedPreProcessedArea()
        {
            if (scalableImage.LoadedImage == null || !scalableImage.CropRect.HasSelection)
                return null;

            Rectangle imgRect = scalableImage.CropRect.Rect;
            Bitmap cropped = scalableImage.LoadedImage.Clone(imgRect, scalableImage.LoadedImage.PixelFormat);

            return ImageProcessing.PreprocessForOCR(cropped, ppBrightnessThreshold);
        }

        private void CroppedImagePictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (croppedImage == null) return;

            e.Graphics.DrawImage(croppedImage, 0, 0, CroppedImagePictureBox.Width, CroppedImagePictureBox.Height);
        }

        //-------------------------------------- OCR Parsing --------------------------------------
        private async void ParseTextButton_Click(object sender, EventArgs e)
        {
            if (scalableImage.LoadedImage == null || !scalableImage.CropRect.HasSelection)
            {
                MessageBox.Show("No image loaded or selection empty.");
                return;
            }

            Rectangle imgRect = scalableImage.CropRect.Rect;
            Bitmap cropped = scalableImage.LoadedImage.Clone(imgRect, scalableImage.LoadedImage.PixelFormat);

            try
            {
                string text = await ocrProcessor.ProcessBitmapAsync(cropped, ppBrightnessThreshold);
                parsedText = text;
                ParsedTextTextBox.Text = parsedText;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            TryUpdateCroppedImage();
        }
        private async void ParseText()
        {
            if (scalableImage.LoadedImage == null || !scalableImage.CropRect.HasSelection)
            {
                MessageBox.Show("No image loaded or selection empty.");
                return;
            }
            if (ocrProcessor == null) 
            {
                MessageBox.Show("OCR processor not initialized.");
                return;
            }

            // Get the selected rectangle
            Rectangle imgRect = scalableImage.CropRect.Rect;

            // Clone the cropped area
            Bitmap cropped = scalableImage.LoadedImage.Clone(imgRect, scalableImage.LoadedImage.PixelFormat);
                      

            try
            {
                // Process the image asynchronously
                string text = await ocrProcessor.ProcessBitmapAsync(cropped, ppBrightnessThreshold);

                // Update UI
                parsedText = text.Trim();
                ParsedTextTextBox.Text = parsedText;
            }
            catch (Exception ex)
            {
                MessageBox.Show("OCR Error: " + ex.Message);
            }
        }

        //-------------------------------------- File Import/Save --------------------------------------
        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog()
            {
                Title = "Select an Image",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.tif;*.tiff"
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                    scalableImage.DisplayBitmap(new Bitmap(ofd.FileName));
            }
        }

        private void ImportImageButton_Click(object sender, EventArgs e)
        {
            WIA.CommonDialog dialog = new WIA.CommonDialog();
            Device scanner;
            try { scanner = dialog.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, true, false); }
            catch { MessageBox.Show("Error selecting scanner."); return; }

            if (scanner != null)
            {
                Item item = scanner.Items[1];
                ImageFile image = (ImageFile)dialog.ShowTransfer(item, WIA.FormatID.wiaFormatJPEG, false);
                if (image != null)
                    scalableImage.DisplayBitmap(ImageProcessing.WiaImageFileToBitmap(image));
            }
        }

        private void SaveImageButton_Click(object sender, EventArgs e)
        {
            if (parsedText != ParsedTextTextBox.Text) parsedText = ParsedTextTextBox.Text;
            if (scalableImage.LoadedImage == null) { MessageBox.Show("No image loaded."); return; }
            if (string.IsNullOrWhiteSpace(parsedText)) { MessageBox.Show("No filename specified."); return; }
            if (string.IsNullOrWhiteSpace(rootDirectory)) { MessageBox.Show("No root directory specified."); return; }
            if (SaveFormatComboBox.SelectedItem == null) { MessageBox.Show("No file format selected."); return; }

            try
            {
                string extension = SaveFormatComboBox.SelectedItem.ToString().ToLower();
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

                scalableImage.LoadedImage.Save(savePath, format);
                MessageBox.Show($"Image saved to: {savePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving image: " + ex.Message);
            }
        }

        private void RotateImageButton_Click(object sender, EventArgs e)
        {
            scalableImage.RotateImage(1);            
        }

        private void SetRootDirButton_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog()
            {
                Description = "Select a root directory",
                ShowNewFolderButton = true
            })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    rootDirectory = fbd.SelectedPath;
                    RootDirLabel.Text = rootDirectory;
                }
            }
        }

        //-------------------------------------- Threshold Trackbar --------------------------------------
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // BASE OF 100 + 20, range 100–140
            ppBrightnessThreshold = (byte)(100 + ppBrightnessThresholdTrackBar.Value);
            TryUpdateCroppedImage();
        }

     
    }
}
