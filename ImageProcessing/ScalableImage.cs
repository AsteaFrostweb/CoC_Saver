using System;
using System.Drawing;
using System.Windows.Forms;

namespace CoCSaver
{
    public class ScalableImage
    {
        private readonly Panel panel;
        private Bitmap loadedImage;
        private Point panStart;
        private float zoom = 1.0f;
        private PointF offset = new PointF(0, 0);
        public readonly ScalableImageCropRect CropRect = new ScalableImageCropRect();

        public Bitmap LoadedImage => loadedImage;
        public float Zoom => zoom;
        public PointF Offset => offset;


        public ScalableImage(Panel panel)
        {
            this.panel = panel;

            // enable double buffering
            var aProp = typeof(Control).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            aProp?.SetValue(panel, true, null);

            // hook events
            panel.Paint += Panel_Paint;
            panel.MouseWheel += Panel_MouseWheel;
            panel.MouseDown += Panel_MouseDown;
            panel.MouseMove += Panel_MouseMove;
            panel.MouseUp += Panel_MouseUp;
        }


        // Load bitmap
        public void DisplayBitmap(Bitmap bmp)
        {
            loadedImage?.Dispose();
            loadedImage = new Bitmap(bmp);
            UpdateScrollSize();
            panel.Invalidate();
        }


        // --------  -------- Event Handlers --------  -------- 
        private void Panel_MouseWheel(object sender, MouseEventArgs e) //ScrollWheel
        {
            if (loadedImage == null) return;
            float oldZoom = zoom;
            UpdateZoomLevel(e.Delta);
            AdjustOffsetForMouse(e.Location, oldZoom);
            panel.Invalidate();
        }


        private void Panel_MouseDown(object sender, MouseEventArgs e) //MouseDown
        {
            if (loadedImage == null) return;

            if (e.Button == MouseButtons.Left)
            {
                CropRect.BeginDrag(PanelToImage(e.Location)); //LEFT CLICK: BEGIT DRAGGING CROPPED RECT
            }
            else if (e.Button == MouseButtons.Right)
            {
                panStart = e.Location; //RIGHT CLICK: BEGIN PANNING THE IMAGE
            }
        }


        private void Panel_MouseMove(object sender, MouseEventArgs e) //MOUSE MOVE
        {
            if (loadedImage == null) return;

            if (e.Button == MouseButtons.Left)
            {
                CropRect.UpdateDrag(PanelToImage(e.Location));//LEFT CLICK: UPDATE CROPPED RECT
                panel.Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                // pan
                int dx = e.X - panStart.X;
                int dy = e.Y - panStart.Y;
                offset.X += dx;
                offset.Y += dy;
                panStart = e.Location; //RIGHT CLICK: UPDATE IMAGE PANNING OFFSET
                panel.Invalidate();
            }
        }


        private void Panel_MouseUp(object sender, MouseEventArgs e)//MOUSE UP
        {
            if (e.Button == MouseButtons.Left) CropRect.EndDrag();//LEFT CLICK: FINISH CROPPED RECT
        }


        private void Panel_Paint(object sender, PaintEventArgs e) //PAINT IMAGE
        {
            if (loadedImage == null) return;

            e.Graphics.Clear(Color.Black);
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            //Calculate relative draw size using "zoom"
            int drawWidth = (int)(loadedImage.Width * zoom);
            int drawHeight = (int)(loadedImage.Height * zoom);

            e.Graphics.DrawImage(loadedImage, offset.X, offset.Y, drawWidth, drawHeight);

            CropRect.Draw(e.Graphics, ImageToPanel);
        }


        // -------- Helpers --------
        public void UpdateZoomLevel(int delta)
        {
            zoom *= delta > 0 ? 1.1f : 0.9f;                 // ZOOM IN (delta > 0) OR ZOOM OUT (delta < 0)
            zoom = Math.Max(0.1f, Math.Min(zoom, 10f));      // LIMIT ZOOM RANGE BETWEEN 0.1x AND 10x
        }


        public void RotateImage(int count)
        {
            if (loadedImage == null) return;                 

            int normalizedCount = ((count % 4) + 4) % 4;     

            if (normalizedCount == 0) return;                

            for (int i = 0; i < normalizedCount; i++)        // ROTATE CLOCKWISE 90 DEGREES "normalizedCount" TIMES
            {
                loadedImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            }

            offset = new PointF(0, 0);                      
            UpdateScrollSize();                              
            panel.Invalidate();                              
        }


        public void AdjustOffsetForMouse(Point mousePosition, float oldZoom)
        {
            float scale = zoom / oldZoom;                    // SCALE CHANGE FACTOR
            offset.X = mousePosition.X - scale * (mousePosition.X - offset.X);
            offset.Y = mousePosition.Y - scale * (mousePosition.Y - offset.Y);
            // ADJUST OFFSET SO IMAGE ZOOMS RELATIVE TO MOUSE POSITION (NOT TOP-LEFT CORNER)
        }


        private void UpdateScrollSize()
        {
            if (loadedImage != null)
                panel.AutoScrollMinSize = new Size(
                    (int)(loadedImage.Width * zoom),         
                    (int)(loadedImage.Height * zoom)       
                );
        }


        public Point PanelToImage(Point panelPoint)
        {
            if (loadedImage == null) return Point.Empty;     

            float imgX = (panelPoint.X - offset.X) / zoom;   //CONVERT COORDS.
            float imgY = (panelPoint.Y - offset.Y) / zoom;   

            int x = (int)Math.Max(0, Math.Min(imgX, loadedImage.Width - 1));  // CLAMP X WITHIN IMAGE BOUNDS
            int y = (int)Math.Max(0, Math.Min(imgY, loadedImage.Height - 1)); // CLAMP Y WITHIN IMAGE BOUNDS

            return new Point(x, y);                          // RETURN IMAGE-SPACE POINT
        }


        public Rectangle ImageToPanel(Rectangle imgRect)
        {
            return new Rectangle(
                (int)(imgRect.X * zoom + offset.X),          // CONVERT IMAGE X TO PANEL X
                (int)(imgRect.Y * zoom + offset.Y),          // CONVERT IMAGE Y TO PANEL Y
                (int)(imgRect.Width * zoom),                 // SCALE IMAGE WIDTH
                (int)(imgRect.Height * zoom)                 // SCALE IMAGE HEIGHT
            );
        }
    }
}
   
