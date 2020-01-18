using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

            var zoomFactor = GetZoomFactor(zoomLevel);
            var rect = new RectangleF(ptPanninng.X, ptPanninng.Y, (float)(img.Width * zoomFactor), (float)(img.Height * zoomFactor));
            e.Graphics.DrawImage(img, rect);
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

        private void FormMain_KeyDown(object sender, KeyEventArgs e) {
            if (e.Modifiers != Keys.Control | e.KeyCode != Keys.V)
                return;

            var imgOld = Clipboard.GetImage();
            if (imgOld == null)
                return;

            img = imgOld;
            Invalidate();
        }

        string dragFile = null;
        private void FormMain_DragEnter(object sender, DragEventArgs e) {
            dragFile = GetDragDataOneFile(e.Data);
            if (dragFile != null)
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void LoadImageFile(string file) {
            try {
                img = Image.FromFile(file);
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

        }

        private void resetZoomToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void useImageFilterToolStripMenuItem_Click(object sender, EventArgs e) {

        }
    }
}
