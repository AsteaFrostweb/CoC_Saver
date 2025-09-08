using System;
using System.Drawing;
using System.Windows.Forms;

namespace CoCSaver
{
    public class ScalableImageCropRect
    {
        private Rectangle rect = Rectangle.Empty;
        private Point dragStart;
        private bool isDragging = false;

        public Rectangle Rect => rect;
        public bool HasSelection => rect != Rectangle.Empty && rect.Width > 0 && rect.Height > 0;
        public event EventHandler onEndSelection;
        public void BeginDrag(Point imagePoint)
        {
            isDragging = true;
            dragStart = imagePoint;
            rect = new Rectangle(dragStart, new Size(0, 0));
        }

        public void UpdateDrag(Point currentImagePoint)
        {
            if (!isDragging) return;

            int x = Math.Min(dragStart.X, currentImagePoint.X);
            int y = Math.Min(dragStart.Y, currentImagePoint.Y);
            int w = Math.Abs(currentImagePoint.X - dragStart.X);
            int h = Math.Abs(currentImagePoint.Y - dragStart.Y);

            rect = new Rectangle(x, y, w, h);
        }

        public void EndDrag()
        {
            isDragging = false;

            if (HasSelection)
                onEndSelection.Invoke(this, new EventArgs());
        }

        public void Draw(Graphics g, Func<Rectangle, Rectangle> imageToPanelMapper)
        {
            if (!HasSelection) return;

            Rectangle panelRect = imageToPanelMapper(rect);

            using (Pen pen = new Pen(Color.Red, 2))
                g.DrawRectangle(pen, panelRect);

            using (Brush brush = new SolidBrush(Color.FromArgb(50, Color.Red)))
                g.FillRectangle(brush, panelRect);
        }
    }
}
