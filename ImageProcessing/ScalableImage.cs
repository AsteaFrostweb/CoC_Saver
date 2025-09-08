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

        // -------- Event Handlers --------
        private void Panel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (loadedImage == null) return;
            float oldZoom = zoom;
            UpdateZoomLevel(e.Delta);
            AdjustOffsetForMouse(e.Location, oldZoom);
            panel.Invalidate();
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (loadedImage == null) return;

            if (e.Button == MouseButtons.Left)
            {
                CropRect.BeginDrag(PanelToImage(e.Location));
            }
            else if (e.Button == MouseButtons.Right)
            {
                panStart = e.Location;
            }
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (loadedImage == null) return;

            if (e.Button == MouseButtons.Left)
            {
                CropRect.UpdateDrag(PanelToImage(e.Location));
                panel.Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                // pan
                int dx = e.X - panStart.X;
                int dy = e.Y - panStart.Y;
                offset.X += dx;
                offset.Y += dy;
                panStart = e.Location;
                panel.Invalidate();
            }
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) CropRect.EndDrag();
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            if (loadedImage == null) return;

            e.Graphics.Clear(Color.Black);
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            int drawWidth = (int)(loadedImage.Width * zoom);
            int drawHeight = (int)(loadedImage.Height * zoom);

            e.Graphics.DrawImage(loadedImage, offset.X, offset.Y, drawWidth, drawHeight);

            CropRect.Draw(e.Graphics, ImageToPanel);
        }

        // -------- Helpers --------
        public void UpdateZoomLevel(int delta)
        {
            zoom *= delta > 0 ? 1.1f : 0.9f;
            zoom = Math.Max(0.1f, Math.Min(zoom, 10f));
        }

        public void AdjustOffsetForMouse(Point mousePosition, float oldZoom)
        {
            float scale = zoom / oldZoom;
            offset.X = mousePosition.X - scale * (mousePosition.X - offset.X);
            offset.Y = mousePosition.Y - scale * (mousePosition.Y - offset.Y);
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

            float imgX = (panelPoint.X - offset.X) / zoom;
            float imgY = (panelPoint.Y - offset.Y) / zoom;

            int x = (int)Math.Max(0, Math.Min(imgX, loadedImage.Width - 1));
            int y = (int)Math.Max(0, Math.Min(imgY, loadedImage.Height - 1));

            return new Point(x, y);
        }

        public Rectangle ImageToPanel(Rectangle imgRect)
        {
            return new Rectangle(
                (int)(imgRect.X * zoom + offset.X),
                (int)(imgRect.Y * zoom + offset.Y),
                (int)(imgRect.Width * zoom),
                (int)(imgRect.Height * zoom)
            );
        }
    }
}
