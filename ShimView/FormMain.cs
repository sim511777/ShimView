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

        private void FormMain_Paint(object sender, PaintEventArgs e) {
            if (img == null)
                return;

            e.Graphics.DrawImage(img, 0, 0);
        }
    }
}
