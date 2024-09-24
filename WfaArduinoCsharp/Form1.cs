using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WfaArduinoCsharp
{
    public partial class Form1 : Form
    {
        private SerialPort srpArduino;
        public Form1()
        {
            InitializeComponent();
            srpArduino = new SerialPort();
            srpArduino.DataReceived += SrpArduino_DataReceived;
        }
        private void SrpArduino_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var dados = srpArduino.ReadLine();
            textBox1.Invoke(new Action(() => { textBox1.Text += dados + "\n"; }));
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (!srpArduino.IsOpen)
            {
                try
                {
                    srpArduino.PortName = cmbPortas.Text;
                    srpArduino.Open();
                    btnConectar.Text = "Desconectar";
                    cmbPortas.Enabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    srpArduino.Close();
                    btnConectar.Text = "Conectar";
                    cmbPortas.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            if (srpArduino.IsOpen)
                srpArduino.Write(comboBox1.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            tmrPortas.Tick += tmrPortas_Tick;
            tmrPortas.Enabled = true;
            
        }

        private void tmrPortas_Tick(object sender, EventArgs e)
        {
            var i = 0;
            var qtdeDiferente = false;

            if (cmbPortas.Items.Count == SerialPort.GetPortNames().Length)
            {
                foreach (var porta in SerialPort.GetPortNames())
                    if (!cmbPortas.Items[i++].Equals(porta))
                    {
                        qtdeDiferente = true;
                        break;
                    }
            }
            else
                qtdeDiferente = true;

            if (!qtdeDiferente)
                return;

            cmbPortas.Items.Clear();

            foreach (var porta in SerialPort.GetPortNames())
                cmbPortas.Items.Add(porta);

            cmbPortas.SelectedIndex = 0;
        }

        private void cmbEscolha_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
