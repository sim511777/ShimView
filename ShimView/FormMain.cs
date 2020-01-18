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

        double GetZoomFactor(int level) {
            return Math.Pow(Math.Sqrt(2), level);
        }

        int zoomLevel = 0;
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

        Image img = null;
        private void FormMain_KeyDown(object sender, KeyEventArgs e) {
            if (e.Modifiers != Keys.Control | e.KeyCode != Keys.V)
                return;

            img = Clipboard.GetImage();
            if (img == null)
                return;

            Invalidate();
        }

        Point ptPanninng = Point.Empty;
        private void FormMain_Paint(object sender, PaintEventArgs e) {
            if (img == null)
                return;

            var zoomFactor = GetZoomFactor(zoomLevel);
            var rect = new RectangleF(ptPanninng.X, ptPanninng.Y, (float)(img.Width * zoomFactor), (float)(img.Height * zoomFactor));
            e.Graphics.DrawImage(img, rect);
        }

        bool mouseDown = false;
        private void FormMain_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left)
                mouseDown = true;
        }

        private void FormMain_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left)
                mouseDown = false;
        }

        Point ptOld = Point.Empty;
        private void FormMain_MouseMove(object sender, MouseEventArgs e) {
            var ptNow = e.Location;
            if (mouseDown) {
                ptPanninng += ((Size)ptNow - (Size)ptOld);
                Invalidate();
            }
            ptOld = ptNow;
        }

        private string GetDragDataOneFile(IDataObject dragData) {
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

        string dragFile = null;
        private void FormMain_DragEnter(object sender, DragEventArgs e) {
            dragFile = GetDragDataOneFile(e.Data);
            if (dragFile != null)
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void FormMain_DragDrop(object sender, DragEventArgs e) {
            try {
                img = Image.FromFile(dragFile);
                Invalidate();
            } catch {}
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e) {
            if (dlgOpen.ShowDialog(this) != DialogResult.OK)
                return;
            try {
                img = Image.FromFile(dlgOpen.FileName);
                Invalidate();
            } catch {}
        }

        private void pasteImageToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void resetZoomToolStripMenuItem_Click(object sender, EventArgs e) {

        }

        private void useImageFilterToolStripMenuItem_Click(object sender, EventArgs e) {

        }
    }
}
