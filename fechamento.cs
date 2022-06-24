using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Estacionamento
{
    public partial class fechamento : Form
    {
        public fechamento()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 frm = new Form1();
            frm.Closed += (s, args) => this.Close();
            frm.Show();
        }

        private void fechamento_Load(object sender, EventArgs e)
        {
            saida.Text = DateTime.Now.ToLongTimeString();
            fechamentoValorPagar();
        }

        private void fechamentoValorPagar()
        {
            double duracao = 2.00;
            double valorHora = 2.00;
            double valorTotal = valorHora * duracao;
            String teste = Convert.ToString(valorTotal);
            textBox7.Text = "R$ " + teste;

        }
    }
}
