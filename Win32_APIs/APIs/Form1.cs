using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;



namespace WallPaperManager
{
    public partial class Form1 : Form
    {

        // Define the SYSTEM_POWER_STATUS structure with fields as per the Windows API documentation
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_POWER_STATUS
        {
            public byte ACLineStatus;
            public byte BatteryFlag;
            public byte BatteryLifePercent;
            public byte Reserved1;
            public int BatteryLifeTime;
            public int BatteryFullLifeTime;
        }
        // Import the GetSystemPowerStatus API from kernel32.dll
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetSystemPowerStatus(out SYSTEM_POWER_STATUS sps);

        public Form1()
        {
            InitializeComponent();
        }


        [DllImport("user32.dll")]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, string pvParam, uint fWinIni);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool MessageBox(IntPtr ptr, string text, string caption,int type);



        private void UploadFileBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Select an Image";
            openFileDialog1.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            DialogResult result = openFileDialog1.ShowDialog();


            if (result == DialogResult.OK)
            {
                string FilePath = openFileDialog1.FileName;
                openFileDialog1.Tag = FilePath;
            }



        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.Tag is null)
            {
                
               System.Windows.Forms.MessageBox.Show("Please choose an img");
                return;
            }
            string? Path = openFileDialog1.Tag.ToString();
            ChangeWallPaper(Path);
        }
        private void ChangeWallPaper(string FilePath)
        {
            if (string.IsNullOrEmpty(FilePath))
                return;

            const uint SPI_SETDESKWALLPAPER = 0x0014;
            const uint SPIF_UPDATEINIFILE = 0x01;
            const uint SPIF_SENDWININICHANGE = 0x02;
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, FilePath, SPIF_SENDWININICHANGE | SPIF_UPDATEINIFILE);

        }

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int Dimention);


        private string GetScreenResolution()
        {
            int Width = GetSystemMetrics(0);
            int Height = GetSystemMetrics(1);
            return $"{Width}X{Height}";
        }
        private void GetResBtn_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Your Screen Resolution :" + GetScreenResolution());
        }

        private void BatteryBtn_Click(object sender, EventArgs e)
        {
            if (GetSystemPowerStatus(out SYSTEM_POWER_STATUS status))
            {
                System.Windows.Forms.MessageBox.Show(
                 "AC Line Status: " + (status.ACLineStatus == 0 ? "Offline" : "Online \n"
                 + "Battery Charge Status: " + GetBatteryStatus(status.BatteryFlag))
                 + "\nBattery Life Percent: " + (status.BatteryLifePercent == 255 ? "Unknown" : status.BatteryLifePercent + "%")
                 + "\nBattery Life Remaining: " + (status.BatteryLifeTime == -1 ? "Unknown" : status.BatteryLifeTime + " seconds")
                 + "\nFull Battery Lifetime: " + (status.BatteryFullLifeTime == -1 ? "Unknown" : status.BatteryFullLifeTime + " seconds")
                 , "Battery Information: ");
            }
            else
            {
                Console.WriteLine("Unable to get battery status.");
            }
        }
        static string GetBatteryStatus(byte flag)
        {
            switch (flag)
            {
                case 1:
                    return "High, more than 66% charged";
                case 2:
                    return "Low, less than 33% charged";
                case 4:
                    return "Critical, less than 5% charged";
                case 8:
                    return "Charging";
                case 128:
                    return "No battery";
                case 255:
                    return "Unknown status";
                default:
                    return "Battery status not detected";
            }
        }

        private void BoxBtn_Click(object sender, EventArgs e)
        {
            MessageBox(IntPtr.Zero, "Hello every one", "message box", 1);
        }
    }
}
