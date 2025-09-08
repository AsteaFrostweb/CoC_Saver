using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Tesseract;

namespace CoCSaver
{
    public class OCR
    {
        private readonly string tessdataPath;
        private readonly string language;
        private readonly EngineMode engineMode;

        public OCR(string tessdataPath = "./tessdata", string language = "eng", EngineMode engineMode = EngineMode.Default)
        {
            this.tessdataPath = tessdataPath;
            this.language = language;
            this.engineMode = engineMode;
        }

        public async Task<string> ProcessBitmapAsync(Bitmap bitmap, byte brightnessThreshold)
        {
            if (bitmap == null)
                throw new ArgumentNullException(nameof(bitmap));

            return await Task.Run(() =>
            {
                try
                {
                    Bitmap preprocessed = ImageProcessing.PreprocessForOCR(bitmap, brightnessThreshold);

                    using (var ms = new MemoryStream())
                    {
                        preprocessed.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        ms.Position = 0;

                        using (var engine = new TesseractEngine(tessdataPath, language, engineMode))
                        {
                            engine.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_");
                            engine.SetVariable("preserve_interword_spaces", "1");
                            engine.DefaultPageSegMode = PageSegMode.SingleWord;

                            using (var pix = Pix.LoadFromMemory(ms.ToArray()))
                            using (var page = engine.Process(pix))
                            {
                                return page.GetText().Trim();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("OCR Error: " + ex.Message, ex);
                }
            });
        }
    }
}
