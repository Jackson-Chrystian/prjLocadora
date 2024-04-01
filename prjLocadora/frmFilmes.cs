using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjLocadora
{
    public partial class frmFilmes : Form
    {
        int registroAtual = 0;
        int totalRegistro = 0;
        DataTable dtProdutora = new DataTable();
        String connectionString = @"Server=darnassus\motorhead; Database=db_230717; User Id=230717; Password=a12345678A";
        bool novo;
        DataTable dtFilme = new DataTable();

        private void navegar()
        {

            carregaComboProdutoras();
            txtCodFilme.Text = dtFilme.Rows[registroAtual][0].ToString();
            txtTituloFilme.Text = dtFilme.Rows[registroAtual][1].ToString();
            txtAnoFilme.Text = dtFilme.Rows[registroAtual][2].ToString();
            //cbbProdutora.Text = dtFilmes.Rows[registroAtual][3].ToString();
            cbbGenero.Text = dtFilme.Rows[registroAtual][4].ToString();
        }

        private void carregaComboProdutoras()
        {
            dtProdutora = new DataTable();
            string sql = "Select * from tbl_" +
                "Produtora WHERE CodProd="+
                dtFilme.Rows[registroAtual][3].ToString();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtFilme.Load(reader);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
            cbbProdutora.DataSource = dtProdutora;
            cbbProdutora.DisplayMember = "nomeProd";
            cbbProdutora.ValueMember = "codProd";
        }

        public frmFilmes()
        {
            InitializeComponent();
        }

        private void frmFilmes_Load(object sender, EventArgs e)
        {
            btnSalvar.Enabled = false;
            txtCodFilme.Enabled = false;
            txtTituloFilme.Enabled = false;
            txtAnoFilme.Enabled = false;
            cbbProdutora.Enabled = false;
            cbbGenero.Enabled = false;
            string sql = "SELECT * FROM tblFilme";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtFilme.Load(reader);
                    totalRegistro = dtFilme.Rows.Count;
                    registroAtual = 0;
                    navegar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if(registroAtual<totalRegistro - 1)
            {
                registroAtual++;
                navegar();
            }
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (registroAtual > 0)
            {
                registroAtual = 0;
                navegar();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (registroAtual > 0)
            {
                registroAtual--;
                navegar();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (registroAtual < totalRegistro - 1)
            {
                registroAtual = totalRegistro - 1;
                navegar();
            }
        }
    }
}
