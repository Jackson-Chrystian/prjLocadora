﻿using System;
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
    public partial class frmProdutoras : Form
    {
        int registroAtual = 0;
        int totalRegistro = 0;
        DataTable dtProdutora = new DataTable();
        String connectionString = @"Server=darnassus\motorhead; Database=db_230717; User Id=230717; Password=a12345678A";
        bool novo;

        public frmProdutoras()
        {
            InitializeComponent();
        }

        private void navegar()
        {
            txtCodProd.Text = dtProdutora.Rows[registroAtual][0].ToString();
            txtProd.Text = dtProdutora.Rows[registroAtual][1].ToString();
            txtTelProd.Text = dtProdutora.Rows[registroAtual][2].ToString();
            txtEmailProd.Text = dtProdutora.Rows[registroAtual][3].ToString();

        }
        private void frmProdutoras_Load(object sender, EventArgs e)
        {
            btnSalvar.Enabled = false;
            txtCodProd.Enabled = false;
            txtEmailProd.Enabled = false;
            txtProd.Enabled = false;
            txtTelProd.Enabled = false;
            string sql = "SELECT * FROM tbl_Produtora";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtProdutora.Load(reader);
                    totalRegistro = dtProdutora.Rows.Count;
                    registroAtual = 0;
                    navegar(); 
                }
            } catch(Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
         
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            txtProd.Enabled = true;
            txtEmailProd.Enabled = true;
            txtTelProd.Enabled = true;
            txtCodProd.Text = "";
            txtProd.Text = "";
            txtTelProd.Text = "";
            txtEmailProd.Text = "";
            btnExcluir.Enabled = false;
            btnPrimeiro.Enabled = false;
            btnAnterior.Enabled = false;
            btnProximo.Enabled = false;
            btnUltimo.Enabled = false;
            btnAlterar.Enabled = false;
            novo = true;
            txtProd.Focus();

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (novo)
            {
                string sql = "INSERT INTO dbo.tbl_Produtora(nomeProd, telProd, emailProd )"
                    + "VALUES ('"+txtProd.Text+"', '" +txtTelProd.Text+"', '"+txtEmailProd.Text+ "')";
                //MessageBox.Show(sql);
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Produtora cadastrada com sucesso.");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                string sql = "UPDATE tbl_Produtora SET nomeProd='" + txtProd.Text +
                    "', emailProd='" + txtEmailProd.Text + "', telProd='" + txtTelProd.Text +
                    "' WHERE codProd=" + txtCodProd.Text;
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open(); 
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Produtora alterada com sucesso!");

                    }
                
                }catch(Exception ex)
                {
                    MessageBox.Show("erro: " + ex.ToString()); 

                }
                finally
                {
                    con.Close();
                }
            }

            btnSalvar.Enabled = false;
            txtCodProd.Enabled = false;
            txtEmailProd.Enabled = false;
            txtProd.Enabled = false;
            txtTelProd.Enabled = false;
            btnNovo.Enabled = true;
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = true;
            btnPrimeiro.Enabled = true;
            btnAnterior.Enabled = true;
            btnProximo.Enabled = true;
            btnUltimo.Enabled = true;
            carregar();
        }

        private void carregar()
        {
            dtProdutora = new DataTable();
            string sql = "SELECT * FROM tbl_Produtora";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtProdutora.Load(reader);
                    totalRegistro = dtProdutora.Rows.Count;
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM tbl_Produtora WHERE codProd=" + txtCodProd.Text;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if(i > 0)
                {
                    MessageBox.Show("Produtora apagada com sucesso!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
            carregar();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            novo = false;
            btnNovo.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = true;
            txtProd.Enabled = true;
            txtEmailProd.Enabled = true;
            txtTelProd.Enabled = true;
            btnPrimeiro.Enabled = false;
            btnAnterior.Enabled = false;
            btnProximo.Enabled = false;
            btnUltimo.Enabled = false;

        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if(registroAtual<totalRegistro - 1)
            {
                registroAtual++;
                navegar();
            }


        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if(registroAtual > 0)
            {
                registroAtual--;
                navegar();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if(registroAtual< totalRegistro - 1)
            {
                registroAtual = totalRegistro - 1;
                navegar();
            }
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (registroAtual > 0 )
            {
                registroAtual = 0;
                navegar(); 
            }
        }
    }
}
