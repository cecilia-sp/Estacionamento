

namespace Estacionamento
{
    public partial class Form1 : Form
    {
        string pasta_images = "";
        Image img_fundo;
        Image btnNormal; // botão normaç
        Image btnEscuro; // mause por cima do botão

        public Form1()
        {
            InitializeComponent();
            //pasta da imagem
            pasta_images = Application.StartupPath + @"images\";

            //carregamento da imagem
            img_fundo = Image.FromFile(pasta_images + "logo.png");
            logo.BackgroundImage = img_fundo;
            btnNormal = Image.FromFile(pasta_images + "btnNormal.png");
            btnEscuro = Image.FromFile(pasta_images + "btnEscuro.png");


            //Deixa o label1 transparente
            var lb1 = this.PointToScreen(label1.Location);
            lb1 = pictureBox1.PointToClient(lb1);
            label1.Parent = pictureBox1;
            label1.Location = lb1;
            label1.BackColor = Color.Transparent;

            //Deixa o label2 transparente
            var lb2 = this.PointToScreen(label2.Location);
            lb2 = pictureBox2.PointToClient(lb2);
            label2.Parent = pictureBox2;
            label2.Location = lb2;
            label2.BackColor = Color.Transparent;

            //Deixa o label3 transparente
            var lb3 = this.PointToScreen(label3.Location);
            lb3 = pictureBox3.PointToClient(lb3);
            label3.Parent = pictureBox3;
            label3.Location = lb3;
            label3.BackColor = Color.Transparent;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = btnNormal;
            pictureBox2.BackgroundImage = btnNormal;
            pictureBox3.BackgroundImage = btnNormal;
            data.Text = "Hoje é " + DateTime.Now.ToLongDateString() + " ás " + DateTime.Now.ToLongTimeString();

        }
 
        private void label1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = btnEscuro;
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = btnEscuro;
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.BackgroundImage = btnEscuro;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {//Volta ao normal do botão
            pictureBox1.BackgroundImage = btnNormal;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {//Volta ao normal do botão
            pictureBox2.BackgroundImage = btnNormal;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {//Volta ao normal do botão
            pictureBox3.BackgroundImage = btnNormal;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            fechamento fe = new fechamento();
            fe.ShowDialog(); //chama outro form
        }

        private void label1_Click(object sender, EventArgs e)
        {       
            entrada en = new entrada();
            en.ShowDialog(); //chama outro form
        }

        private void label3_Click(object sender, EventArgs e)
        {
            vagas va = new vagas();
            va.ShowDialog(); //chama outro form
        }
    }
}