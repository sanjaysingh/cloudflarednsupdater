using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudflareDnsUpdater
{
    public class NotificationAreaUI
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        public ContextMenu menu;
        public MenuItem mnuExit;
        public NotifyIcon notificationIcon;

        public void Launch()
        {
            Task.Run(() => {
                menu = new ContextMenu();
                mnuExit = new MenuItem("Exit");
                menu.MenuItems.Add(0, mnuExit);

                var menuHide = new MenuItem("Hide Console");
                menu.MenuItems.Add(0, menuHide);
                menuHide.Click += MenuHide_Click;

                var menuShow = new MenuItem("Show Console");
                menu.MenuItems.Add(0, menuShow);
                menuShow.Click += MenuShow_Click;

                notificationIcon = new NotifyIcon()
                {
                    Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath),
                    ContextMenu = menu,
                    Text = "Coudflare Dns Updater"
                };
                mnuExit.Click += MnuExit_Click; ;

                notificationIcon.Visible = true;
                Application.Run();

            });
        }

        private void MnuExit_Click(object sender, EventArgs e)
        {
            notificationIcon.Dispose();
            Application.Exit();
            Process.GetCurrentProcess().Kill();
        }

        private void ShowWindow()
        {
            var handle = GetConsoleWindow();

            // Show
            ShowWindow(handle, SW_SHOW);
        }

        private void HideWindow()
        {
            var handle = GetConsoleWindow();

            // Hide
            ShowWindow(handle, SW_HIDE);
        }

        private void MenuShow_Click(object sender, EventArgs e)
        {
            ShowWindow();
        }

        private void MenuHide_Click(object sender, EventArgs e)
        {
            HideWindow();
        }
    }
}
