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
        private const int BRIGHTNESS_THRESHOLD_BASE = 128;
        private const int BRIGHTNESS_THRESHOLD_VARIANCE = 40;

        private ScalableImage scalableImage;
        private string rootDirectory;
        private string parsedText;
        private byte ppBrightnessThreshold = 132;
        private Bitmap croppedImage = null;
        private OCR ocrProcessor;



        public Form1()
        {
            InitializeComponent();
            //Setup trackbar range
            ppBrightnessThresholdTrackBar.Value = BRIGHTNESS_THRESHOLD_VARIANCE;
            ppBrightnessThresholdTrackBar.Minimum = 0;
            ppBrightnessThresholdTrackBar.Maximum = BRIGHTNESS_THRESHOLD_VARIANCE * 2; //Example Range (0-80)



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

            Bitmap image = IO.TryScanImage();
                if (image != null)
                    scalableImage.DisplayBitmap(image);
            
        }

        private void SaveImageButton_Click(object sender, EventArgs e)
        {
            //Check all the requirements to save image (root, File Name, File Extension, loaded_image)
            if (parsedText != ParsedTextTextBox.Text) parsedText = ParsedTextTextBox.Text;
            if (scalableImage.LoadedImage == null) { MessageBox.Show("No image loaded."); return; }
            if (string.IsNullOrWhiteSpace(parsedText)) { MessageBox.Show("No filename specified."); return; }
            if (string.IsNullOrWhiteSpace(rootDirectory)) { MessageBox.Show("No root directory specified."); return; }
            if (SaveFormatComboBox.SelectedItem == null) { MessageBox.Show("No file format selected."); return; }

            //Attempt to save the image
            string extension = SaveFormatComboBox.SelectedItem.ToString().ToLower();
            IO.TrySaveImage(rootDirectory, parsedText, extension, scalableImage.LoadedImage);
        }

        
        private void RotateImageButton_Click(object sender, EventArgs e)
        {
            scalableImage.RotateImage(1);            
        }

        private void SetRootDirButton_Click(object sender, EventArgs e)
        {
            //Use fodler broser dialog to find and set root directory (save location)
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

        private void ThresholdTrackBar_Scroll(object sender, EventArgs e)
        {
                                                    // BASE - VARIANCE eg. 120 - 40 = 80              
            ppBrightnessThreshold = (byte)((BRIGHTNESS_THRESHOLD_BASE - BRIGHTNESS_THRESHOLD_VARIANCE) + ppBrightnessThresholdTrackBar.Value);
            TryUpdateCroppedImage();
        }

        private void ResetBrightnessThresholdButton_Click(object sender, EventArgs e)
        {
            ppBrightnessThresholdTrackBar.Value = BRIGHTNESS_THRESHOLD_VARIANCE; 
        }
    }
}
