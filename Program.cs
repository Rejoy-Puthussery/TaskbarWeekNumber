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
                        new MenuItem[] { menuItem1 });
            // Initialize context menuItem
            menuItem1.Index = 0;
            menuItem1.Text = "Exit";
            menuItem1.Click += new EventHandler(Exit);

            // Initialize Tray Icon
            trayIcon = new NotifyIcon(components)
            {
                ContextMenu = contextMenu1,
                Visible = true
            };
            SetWeekNumberIconAndTooltip();
            trayIcon.MouseMove += new MouseEventHandler(UpdateInfo);
        }
       
        private void SetWeekNumberIconAndTooltip()
        {
            CultureInfo myCI = new CultureInfo(CultureInfo.CurrentCulture.Name);
            Calendar myCal = myCI.Calendar;
            //CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            //DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
            int weekNumber = myCal.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            int weekDayNumber = (int)DateTime.Today.DayOfWeek;
            string weekDay = weekNumber + "." + weekDayNumber;
            trayIcon.Icon = GetIcon(""+weekNumber);
            trayIcon.Text = "Date: " + DateTime.Now.ToString("dd/MM/yyyy") + "\nTime: " + DateTime.Now.ToString("h:mm:ss tt") + "\nDay: " + DateTime.Now.ToString("ddd") + "\nWeek: " + weekDay;
        }

        private void UpdateInfo(object sender, MouseEventArgs e)
        {
            SetWeekNumberIconAndTooltip();
        }

        //System.Drawing package
        private Icon GetIcon(string text)
        {
            Bitmap bitmap = new Bitmap(32,32);
            Font drawFont = new Font("Microsoft Sans Serif", 26, FontStyle.Bold, GraphicsUnit.Pixel);
            SolidBrush drawBrush = new SolidBrush(Color.OrangeRed);

            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.Clear(Color.Transparent);
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            graphics.DrawString(text, drawFont, drawBrush,-4,2);
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
