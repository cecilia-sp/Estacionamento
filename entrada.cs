using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Estacionamento
{
    public partial class entrada : Form
    {
        MySqlConnection conexao;
        string data_source = "datasource=localhost; username=root;password=admin;database=db_cliente";
        string pasta_images = "";
        Image img_fundo;
        private string ?placaVeiculoSelecionado = null;

        public entrada()
        {
            InitializeComponent();
            pasta_images = Application.StartupPath + @"images\";

            listVeiculos.View = View.Details;
            listVeiculos.LabelEdit = true;
            listVeiculos.AllowColumnReorder = true;
            listVeiculos.FullRowSelect = true;
            listVeiculos.GridLines = true;

            //CRIA AS COLUAS DO LIST VIEW
            listVeiculos.Columns.Add("PLACA", 100, HorizontalAlignment.Left);
            listVeiculos.Columns.Add("VEICULO", 100, HorizontalAlignment.Left);
            listVeiculos.Columns.Add("COR", 100, HorizontalAlignment.Left);
            listVeiculos.Columns.Add("ENTRADA", 100, HorizontalAlignment.Left);
            listVeiculos.Columns.Add("VAGA", 100, HorizontalAlignment.Left);

            //carrega a lista
            carregarLista();
        }

        private void entrada_Load(object sender, EventArgs e)
        {
            pasta_images = Application.StartupPath + @"images\";

            //carregamento da imagem
            img_fundo = Image.FromFile(pasta_images + "logo.png");
            logoEntrada.BackgroundImage = img_fundo;
            entradaVeiculo.Text = DateTime.Now.ToLongTimeString();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {//Fecha o form atual e abre outro
            this.Hide();
            Form1 frm = new Form1();
            frm.Closed += (s, args) => this.Close();
            frm.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection(data_source);
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexao;               

                if(placaVeiculoSelecionado == null){
                    //insert
                    cmd.CommandText = "INSERT INTO db_cliente.cliente (placa, veiculo, cor, entrada, box) " +
                    "VALUES (@placa, @veiculo, @corVeiculo, @entradaVeiculo, @vagaVeiculo)";
                    cmd.Parameters.AddWithValue("@placa", placa.Text);
                    cmd.Parameters.AddWithValue("@veiculo", veiculo.Text);
                    cmd.Parameters.AddWithValue("@corVeiculo", corVeiculo.Text);
                    cmd.Parameters.AddWithValue("@entradaVeiculo", entradaVeiculo.Text);
                    cmd.Parameters.AddWithValue("@vagaVeiculo", vagaVeiculo.Text);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro inserido com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                }
                else
                {
                    //update
                    cmd.CommandText = "UPDATE db_cliente.cliente SET placa = @placa, veiculo = @veiculo, cor = @corVeiculo, entrada = @entradaVeiculo, box=@vagaVeiculo WHERE placa = @placaSelecionada";
             
                    cmd.Parameters.AddWithValue("@placaSelecionada", placaVeiculoSelecionado);
                    cmd.Parameters.AddWithValue("@placa", placa.Text);
                    cmd.Parameters.AddWithValue("@veiculo", veiculo.Text);
                    cmd.Parameters.AddWithValue("@corVeiculo", corVeiculo.Text);
                    cmd.Parameters.AddWithValue("@entradaVeiculo", entradaVeiculo.Text);
                    cmd.Parameters.AddWithValue("@vagaVeiculo", vagaVeiculo.Text);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registro atualizado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                placa.Text = "";
                veiculo.Text = "";
                corVeiculo.Text = "";
                entradaVeiculo.Text = DateTime.Now.ToLongTimeString();
                vagaVeiculo.Text = "";                
                carregarLista();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ocorreu: " + ex.Message,"Erro", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }
        }

        private void carregarLista()
        {
            try
            {
                conexao = new MySqlConnection(data_source);
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexao;
                cmd.CommandText = "SELECT placa, veiculo, cor, entrada,box FROM db_cliente.cliente order by entrada asc";
                cmd.Prepare();
                MySqlDataReader reader = cmd.ExecuteReader();
                listVeiculos.Items.Clear();

                //pega todos os campo do banco
                while (reader.Read())
                {
                    string[] row =
                    {
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                    };
                    //coloca dentro do list a busca do banco
                    listVeiculos.Items.Add(new ListViewItem(row));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ocorreu: " + ex.Message, "Erro",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }

        }

        private void listVeiculos_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListView.SelectedListViewItemCollection itensSelecionados = listVeiculos.SelectedItems;
            //percorre cada item da lista
            foreach (ListViewItem itens in itensSelecionados)
            {
                placaVeiculoSelecionado = itens.SubItems[0].Text;

                placa.Text = itens.SubItems[0].Text;
                veiculo.Text = itens.SubItems[1].Text;
                corVeiculo.Text = itens.SubItems[2].Text;
                entradaVeiculo.Text = itens.SubItems[3].Text;
                vagaVeiculo.Text = itens.SubItems[4].Text;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            placaVeiculoSelecionado = null;
            placa.Text = "";
            veiculo.Text = "";
            corVeiculo.Text = "";
            entradaVeiculo.Text = DateTime.Now.ToLongTimeString();
            vagaVeiculo.Text = "";
            placa.Focus();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {            
            try
            {
                conexao = new MySqlConnection(data_source);
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexao;
                cmd.CommandText = "SELECT  placa, veiculo, cor, entrada, box FROM db_cliente.cliente where placa = @buscarPlaca";
                cmd.Parameters.AddWithValue("@buscarPlaca", buscarPlaca.Text);
                cmd.Prepare();
                MySqlDataReader reader = cmd.ExecuteReader();
                listVeiculos.Items.Clear();

                //pega todos os campo do banco
                while (reader.Read())
                {
                    string[] row =
                    {
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                    };
                    //coloca dentro do list a busca do banco
                    listVeiculos.Items.Add(new ListViewItem(row));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ocorreu: " + ex.Message, "Erro",
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            excluirRegistro();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            excluirRegistro();
        }

        private void excluirRegistro()
        {
            try
            {
                placa.Text = placaVeiculoSelecionado;
                if (placaVeiculoSelecionado != null)
                {

                    DialogResult confirmacao = MessageBox.Show("Tem certeza que deseja excluir?", "Certeza de exclusão",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (confirmacao == DialogResult.Yes && placaVeiculoSelecionado != null)
                    {
                        //Abre conexão
                        conexao = new MySqlConnection(data_source);
                        conexao.Open();
                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = conexao;

                        //Excluir
                        cmd.CommandText = "DELETE FROM db_cliente.cliente  WHERE placa = @placa ";
                        cmd.Parameters.AddWithValue("@placa", placaVeiculoSelecionado);
                        cmd.Prepare();
                        cmd.ExecuteNonQuery();


                        MessageBox.Show("Registro excluido com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        carregarLista();

                    }
                }
                else{
                    MessageBox.Show("Por favor selecionar o registro a ser excluido", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ocorreu: " + ex.Message, "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }
        }
    }


}
