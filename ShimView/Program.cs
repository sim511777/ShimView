using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShimView {
    static class Program {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
        
        public const string Name = @"Shim View";
        public const string AboutInfo = 
            @"Shim's simple Image Viewer
(https://github.com/sim511777/ShimView)

==== Hot keys ====
- Ctrl + O : Open File
- File Drag & Drop : Open File
- Ctrl + V : Paste Clipboard
- Ctrl + R : Reset Zoom
- Ctrl + F : Image Filter On/Off
- Mouse Wheel : Zoom-In/Zoom-Out
- Mouse LButton + Move : Panning
- Mouse RButton : Context Menu

==== Version History ====
v1.0.0.0 - 20200118
- 기본 기능 완성
";
    }
}
