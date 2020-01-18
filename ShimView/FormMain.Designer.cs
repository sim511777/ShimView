namespace ShimView {
    partial class FormMain {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.useImageFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.pasteImageToolStripMenuItem,
            this.resetZoomToolStripMenuItem,
            this.useImageFilterToolStripMenuItem});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(201, 114);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // pasteImageToolStripMenuItem
            // 
            this.pasteImageToolStripMenuItem.Name = "pasteImageToolStripMenuItem";
            this.pasteImageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteImageToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.pasteImageToolStripMenuItem.Text = "Paste Image";
            this.pasteImageToolStripMenuItem.Click += new System.EventHandler(this.pasteImageToolStripMenuItem_Click);
            // 
            // resetZoomToolStripMenuItem
            // 
            this.resetZoomToolStripMenuItem.Name = "resetZoomToolStripMenuItem";
            this.resetZoomToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.resetZoomToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.resetZoomToolStripMenuItem.Text = "Reset Zoom";
            this.resetZoomToolStripMenuItem.Click += new System.EventHandler(this.resetZoomToolStripMenuItem_Click);
            // 
            // useImageFilterToolStripMenuItem
            // 
            this.useImageFilterToolStripMenuItem.Checked = true;
            this.useImageFilterToolStripMenuItem.CheckOnClick = true;
            this.useImageFilterToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.useImageFilterToolStripMenuItem.Name = "useImageFilterToolStripMenuItem";
            this.useImageFilterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.useImageFilterToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.useImageFilterToolStripMenuItem.Text = "Use Image Filter";
            this.useImageFilterToolStripMenuItem.Click += new System.EventHandler(this.useImageFilterToolStripMenuItem_Click);
            // 
            // dlgOpen
            // 
            this.dlgOpen.FileName = "openFileDialog1";
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ContextMenuStrip = this.menu;
            this.DoubleBuffered = true;
            this.Name = "FormMain";
            this.Text = "Shim View";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormMain_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseUp);
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetZoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem useImageFilterToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
    }
}

