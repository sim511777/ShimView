using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShimView {
    public partial class FormMain : Form {
        public FormMain() {
            InitializeComponent();
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

            e.Graphics.DrawImage(img, ptPanninng);
        }

        bool mouseDown = false;
        private void FormMain_MouseDown(object sender, MouseEventArgs e) {
            mouseDown = true;
        }

        private void FormMain_MouseUp(object sender, MouseEventArgs e) {
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
    }
}
