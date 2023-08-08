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
            Application.Run(new WeekNumberAppContext());
        }

    }

    //System.Windows.Forms
    public class WeekNumberAppContext : ApplicationContext
    {
        private readonly NotifyIcon trayIcon;
        private readonly ContextMenu contextMenu1;
        private readonly MenuItem menuItem1;
        private readonly System.ComponentModel.IContainer components;

        public WeekNumberAppContext()
        {
            components = new System.ComponentModel.Container();
            contextMenu1 = new ContextMenu();
            menuItem1 = new MenuItem();
            // Initialize contextMenu
            contextMenu1.MenuItems.AddRange(
                        new MenuItem[] { this.menuItem1 });
            // Initialize context menuItem
            menuItem1.Index = 0;
            menuItem1.Text = "Exit";
            menuItem1.Click += new EventHandler(this.Exit);


            // Initialize Tray Icon
            trayIcon = new NotifyIcon(this.components);
            int weekNumber = GetWeekNumber();
            trayIcon.Icon = GetIcon(""+weekNumber);
            trayIcon.ContextMenu = contextMenu1;
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