using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCAudioSpectrumAnalyzer
{
    public partial class Form1 : Form
    {
        public static Form1 Instance;
        public Form1()
        {
            InitializeComponent();
            Instance = this;
            Analyzer = new Analyzer(new ProgressBar(), new ProgressBar(), new ComboBox());
        }

        public void Set(List<byte> data)
        {
            if (data.Count < 16) return;
            client.Send(data.ToArray(), data.Count, new IPEndPoint(IPAddress.Loopback, 5896)); //TODO: Make visualiser source selectable
        }
        
        private UdpClient client = new UdpClient(AddressFamily.InterNetwork);
        private Analyzer Analyzer;
        private void button1_Click(object sender, EventArgs e)
        {
            Analyzer.Enable = !Analyzer.Enable;
            button1.Text = (Analyzer.Enable ? "Disable" : "Enable");
        }
    }
}
