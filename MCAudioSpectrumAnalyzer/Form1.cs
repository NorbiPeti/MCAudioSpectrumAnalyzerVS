using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCAudioSpectrumAnalyzer
{
    public partial class Form1 : Form
    {
        public static Form1 Instance;
        private static StreamWriter sw = new StreamWriter(new FileStream("output.txt", FileMode.Create));
        private static int X = 0;
        public Form1()
        {
            InitializeComponent();
            Instance = this; //TODO: Send to a Java plugin instead
        }

        public void Set(List<byte> data)
        {
            if (data.Count < 16) return;
            data.ForEach(entry => SetBar(entry, entry));
        }
        
        private void SetBar(byte index, byte data)
        {
            byte value = (byte)((((double)data) / 255) * 80);
            textBox1.AppendText("SetBar: " + value + "\n");
            sw.WriteLine(string.Concat("#", X++, ",64,0" + ":@"));
            sw.WriteLine(string.Concat("scoreboard players set t", (X + 1).ToString(), " tracks ", value));
            sw.WriteLine("");
        }

        ~Form1()
        {
            sw.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Analyzer(new ProgressBar(), new ProgressBar(), new ComboBox()).Enable = true;
            button1.Enabled = false;
            button1.Text = "Close to stop";
        }
    }
}
