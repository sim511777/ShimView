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
        Point ptPanninng = Point.Empty;
        int zoomLevel = 0;

        bool mouseDown = false;
        Point ptOld = Point.Empty;

        static double GetZoomFactor(int level) {
            return Math.Pow(Math.Sqrt(2), level);
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
            g.InterpolationMode = this.useImageFilterToolStripMenuItem.Checked ? InterpolationMode.Default : InterpolationMode.NearestNeighbor;
            var zoomFactor = GetZoomFactor(zoomLevel);
            var rect = new RectangleF(ptPanninng.X, ptPanninng.Y, (float)(img.Width * zoomFactor), (float)(img.Height * zoomFactor));
            g.DrawImage(img, rect);
        }

        private void FormMain_MouseWheel(object sender, MouseEventArgs e) {
            var zoomFactorOld = GetZoomFactor(zoomLevel);
            var delta = (e.Delta > 0) ? 1 : -1;
            zoomLevel += delta;
            var zoomFactorNew = GetZoomFactor(zoomLevel);
            var zoomFactorDelta = zoomFactorNew / zoomFactorOld;
            var ptX = (ptPanninng.X - e.Location.X) * zoomFactorDelta + e.Location.X;
            var ptY = (ptPanninng.Y - e.Location.Y) * zoomFactorDelta + e.Location.Y;
            ptPanninng.X = (int)ptX;
            ptPanninng.Y = (int)ptY;
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
                ptPanninng += ((Size)ptNow - (Size)ptOld);
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
            ptPanninng.X = 0;
            ptPanninng.Y = 0;
            Invalidate();
        }

        private void useImageFilterToolStripMenuItem_Click(object sender, EventArgs e) {
            Invalidate();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
            MessageBox.Show(this, Program.AboutInfo, Program.Name);
        }
    }
}
