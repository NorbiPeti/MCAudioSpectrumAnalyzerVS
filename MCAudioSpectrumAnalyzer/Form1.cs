﻿using System;
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
            for (int i = 0; i < data.Count / 2; i++)
                if (data[i] == 0)
                    SetBar((byte)i, 1);
                else
                    SetBar((byte)i, (byte)(data[i] * 2));
        }

        private UdpClient client = new UdpClient(AddressFamily.InterNetwork);
        private void SetBar(byte index, byte data)
        {
            byte value = (byte)((((double)data) / 255) * 80);
            client.Send(new byte[2] { (byte)(index + 1), data }, 2, new IPEndPoint(IPAddress.Loopback, 5896));
        }

        private Analyzer Analyzer;
        private void button1_Click(object sender, EventArgs e)
        {
            Analyzer.Enable = !Analyzer.Enable;
            button1.Text = (Analyzer.Enable ? "Disable" : "Enable");
        }
    }
}
