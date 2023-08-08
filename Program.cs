using System;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;

namespace TaskbarWeekNumber
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new MyCustomApplicationContext());
        }

    }

    //System.Windows.Forms
    public class MyCustomApplicationContext : ApplicationContext
    {
        private readonly NotifyIcon trayIcon;
        private readonly ContextMenu contextMenu1;
        private readonly MenuItem menuItem1;
        private readonly System.ComponentModel.IContainer components;

        public MyCustomApplicationContext()
        {
            components = new System.ComponentModel.Container();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            // Initialize contextMenu1
            this.contextMenu1.MenuItems.AddRange(
                        new System.Windows.Forms.MenuItem[] { this.menuItem1 });
            // Initialize menuItem1
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "Exit";
            this.menuItem1.Click += new System.EventHandler(this.Exit);


            // Initialize Tray Icon
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            int weekNumber = this.GetWeekNumber();
            trayIcon.Icon = GetIcon(""+weekNumber);
            trayIcon.ContextMenu = this.contextMenu1;
            trayIcon.Text = "Week Number";
            trayIcon.Visible = true;
        }
        private int GetWeekNumber()
        {
            CultureInfo myCI = new CultureInfo("de-DE");
            Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
            return myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW);
        }

        //System.Drawing package
        private Icon GetIcon(string text)
        {
            Bitmap bitmap = new Bitmap(32,32);
            Font drawFont = new Font("3ds", 16, FontStyle.Bold);
            SolidBrush drawBrush = new SolidBrush(Color.LightGreen);

            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            graphics.DrawString(text, drawFont, drawBrush, 1, 2);
            Icon createdIcon = Icon.FromHandle(bitmap.GetHicon());

            drawFont.Dispose();
            drawBrush.Dispose();
            graphics.Dispose();
            bitmap.Dispose();

            return createdIcon;
        }

        //Exit the application
        void Exit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }
    }
}