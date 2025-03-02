
using MD.PersianDateTime;
using System.Runtime.InteropServices;
using System.Text;


namespace WinFormNafisNakh
{
    public partial class Form1 : Form
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetDefaultPrinter(StringBuilder pszBuffer, ref int pcchBuffer);

        static string GetDefaultPrinterName()
        {
            int bufferSize = 256;
            StringBuilder buffer = new StringBuilder(bufferSize);

            if (GetDefaultPrinter(buffer, ref bufferSize))
            {
                return buffer.ToString();
            }
            else
            {
                return "هیچ پرینتری تنظیم نشده است!";
            }
        }

        public Form1()
        {
            InitializeComponent();
            label2.Text = GetDefaultPrinterName();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }



        private void SelectFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "یک فایل انتخاب کنید";
                openFileDialog.Filter = "همه فایل‌ها (*.*)|*.*|فایل‌های PRN (*.prn)|*.prn";
                openFileDialog.Multiselect = false; // فقط یک فایل انتخاب شود

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    label3.Text = openFileDialog.FileName;
                    MessageBox.Show("فایل انتخاب شده:\n" + openFileDialog.FileName, "انتخاب موفق!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

            if (string.IsNullOrEmpty(label3.Text) || !File.Exists(label3.Text))
            {
                MessageBox.Show("لطفاً یک فایل معتبر انتخاب کنید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string defaultPrinter = GetDefaultPrinterName();
            if (string.IsNullOrEmpty(defaultPrinter))
            {
                MessageBox.Show("هیچ پرینتری به عنوان پیش‌فرض تنظیم نشده است!", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var persianDate = new PersianDateTime(DateTime.Now);
                string fileContent = File.ReadAllText(label3.Text);
                int StartCount = int.Parse(numericUpDown1.Text);
                int EndCount = int.Parse(numericUpDown2.Text);
                while (StartCount<=EndCount)
                {
                    string FinalPrint = fileContent;
                    FinalPrint=FinalPrint.Replace("HAMBAFT", textBox1.Text);
                    FinalPrint=FinalPrint.Replace("DENIER", textBox2.Text);
                    FinalPrint=FinalPrint.Replace("FILAMENT", textBox3.Text);
                    FinalPrint=FinalPrint.Replace("lA", textBox4.Text);
                    FinalPrint=FinalPrint.Replace("MINGEL", textBox5.Text);
                    FinalPrint=FinalPrint.Replace("SALON", textBox6.Text);
                    FinalPrint=FinalPrint.Replace("1938", textBox7.Text);
                    FinalPrint=FinalPrint.Replace("2B", textBox8.Text);
                    FinalPrint=FinalPrint.Replace("COUNT1", StartCount++.ToString());
                   
                    FinalPrint=FinalPrint.Replace("COUNT2", StartCount++.ToString());
                    
                    FinalPrint=FinalPrint.Replace("COUNT3", StartCount++.ToString());
              
                    FinalPrint=FinalPrint.Replace("138/01/24", persianDate.ToString("yy/MM/dd"));
                    RawDataPrint.RawPrinterHelper.SendStringToPrinter(label2.Text, FinalPrint);
                    Thread.Sleep(500);
                    
                   
                }
                
                MessageBox.Show("فایل به پرینتر ارسال شد!", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا در چاپ فایل: {ex.Message}", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}

    
