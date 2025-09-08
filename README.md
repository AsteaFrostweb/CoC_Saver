# CoCSaver

CoCSaver is a Windows desktop tool for processing text from images.  
It allows you to load, crop, and run OCR (Optical Character Recognition) on scanned documents.  
The extracted text (such as lot numbers) can be used to automatically save files with the correct filename.

---

## Features
- Image Viewer  
  - Load images in multiple formats (JPG, PNG, BMP, TIFF)
  - Import images from scanner using WIA 
  - Zoom with the mouse wheel  
  - Pan with right-click drag  

- Crop Tool  
  - Draw and adjust selection rectangles  
  - Preview cropped image  

- OCR (Text Recognition)  
  - Powered by [Tesseract OCR](https://github.com/tesseract-ocr/tesseract)  
  - Preprocessing images to improve recognition accuracy  
  - Extracts characters (A-Z, 0-9, -, _)  

- File Saving  
  - Easily save cropped images using the parsed lot number as filename  
  - Supports multiple output formats (JPG, PNG, BMP)  
  - Save location can be set once and reused  

---

## Requirements
- Windows 7, 8, 10, or 11  
- [.NET Framework 4.7.2 or higher](https://dotnet.microsoft.com/download/dotnet-framework)  
- Tesseract OCR DLLs (included in release)  
- `tessdata` folder with language files (included in release)

> Note: This application does **not** support Windows XP or Vista.


---

## Getting Started
1. Download the release directory as .zip.  
2. Extract the zip file.  
3. Run `CoCSaver.exe`.  
4. Load an image from your pc or a scanner.  
5. Select the line on text to scan.  aprox. (1-16 characters)  
6. Text should automatically parse into "Parsed Text" field (If not press the "Parse Text" button).
7. Check the parsed text for errors, usual suspects:(0, D, I, 1)
8. Save the image — the file will be named using the parsed text.  

---

## Development
1. Clone the repository and open it in Visual Studio 2019:
2. Right click solution and "Restone NuGet Packages"

```bash
git clone https://github.com/yourusername/CoCSaver.git
