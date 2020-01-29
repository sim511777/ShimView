using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShimView {
    public partial class FormMain : Form {
        public FormMain() {
            InitializeComponent();
            MouseWheel += this.FormMain_MouseWheel;
        }

        Image img = null;
        PointF ptPanning = PointF.Empty;
        int zoomLevel = 0;

        bool mouseDown = false;
        Point ptOld = Point.Empty;

        static float GetZoomFactor(int level) {
            return (float)Math.Pow(Math.Sqrt(2), level);
        }

        private static string GetDragDataOneFile(IDataObject dragData) {
            if (dragData.GetDataPresent(DataFormats.FileDrop) == false)
                return null;
            var files = (string[])dragData.GetData(DataFormats.FileDrop);
            if (files.Length != 1)
                return null;
            var file = files[0];
            var fileAttr = File.GetAttributes(file);
            if ((fileAttr | FileAttributes.Directory) == FileAttributes.Directory)
                return null;
            return file;
        }

        private void FormMain_Paint(object sender, PaintEventArgs e) {
            if (img == null)
                return;

            Graphics g = e.Graphics;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.InterpolationMode = this.useImageFilterToolStripMenuItem.Checked ? InterpolationMode.Bilinear : InterpolationMode.NearestNeighbor;
            var zoomFactor = GetZoomFactor(zoomLevel);
            g.ScaleTransform(zoomFactor, zoomFactor, MatrixOrder.Append);
            g.TranslateTransform(ptPanning.X, ptPanning.Y, MatrixOrder.Append);
            g.DrawImage(img, 0, 0);
            var pen = new Pen(Color.Lime, 1f / zoomFactor);
            g.DrawLine(pen, 0.5f, 0.5f, 1.5f, 1.5f);
            pen.Dispose();
        }

        private void FormMain_MouseWheel(object sender, MouseEventArgs e) {
            var zoomFactorOld = GetZoomFactor(zoomLevel);
            var delta = (e.Delta > 0) ? 1 : -1;
            zoomLevel += delta;
            var zoomFactorNew = GetZoomFactor(zoomLevel);
            var zoomFactorDelta = zoomFactorNew / zoomFactorOld;
            var ptX = (ptPanning.X - e.Location.X) * zoomFactorDelta + e.Location.X;
            var ptY = (ptPanning.Y - e.Location.Y) * zoomFactorDelta + e.Location.Y;
            ptPanning.X = ptX;
            ptPanning.Y = ptY;
            Invalidate();
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left)
                mouseDown = true;
        }

        private void FormMain_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left)
                mouseDown = false;
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e) {
            var ptNow = e.Location;
            if (mouseDown) {
                ptPanning += ((Size)ptNow - (Size)ptOld);
                Invalidate();
            }
            ptOld = ptNow;
        }

        string dragFile = null;
        private void FormMain_DragEnter(object sender, DragEventArgs e) {
            dragFile = GetDragDataOneFile(e.Data);
            if (dragFile != null)
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void ChangeImage(Image imgNew) {
            if (img != null)
                img.Dispose();
            img = imgNew;
        }

        private void LoadImageFile(string file) {
            try {
                var imgNew = Image.FromFile(file);
                ChangeImage(imgNew);
                Invalidate();
            } catch {}
        }

        private void FormMain_DragDrop(object sender, DragEventArgs e) {
            LoadImageFile(dragFile);
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e) {
            if (dlgOpen.ShowDialog(this) != DialogResult.OK)
                return;
            LoadImageFile(dlgOpen.FileName);
        }

        private void pasteImageToolStripMenuItem_Click(object sender, EventArgs e) {
            var imgNew = Clipboard.GetImage();
            if (imgNew == null)
                return;

            ChangeImage(imgNew);
            Invalidate();
        }

        private void resetZoomToolStripMenuItem_Click(object sender, EventArgs e) {
            zoomLevel = 0;
            ptPanning.X = 0;
            ptPanning.Y = 0;
            Invalidate();
        }

        private void useImageFilterToolStripMenuItem_Click(object sender, EventArgs e) {
            Invalidate();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show(this, Program.AboutInfo, Program.Name);
        }

        private void lennaToolStripMenuItem_Click(object sender, EventArgs e) {
            ChangeImage(Properties.Resources.Lenna);
            Invalidate();
        }

        private void chessToolStripMenuItem_Click(object sender, EventArgs e) {
            ChangeImage(Properties.Resources.Chess);
            Invalidate();
        }
    }
}
