using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjAula4
{
    public partial class CadastroCidade : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDDL();
                LoadGrid();
            }
            
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            int IdEstado = int.Parse(dlEstado.SelectedValue.ToString());

            bdaula4Entities context = new bdaula4Entities();

            Cidade cidade = new Cidade()
            {
                Descricao = txtDescricao.Text,
                Estado = context.Estado.First<Estado>(v => v.Id == IdEstado)
            };

            context.Cidade.Add(cidade);
            context.SaveChanges();
            LoadGrid();
            SendMessage("Registro Inserido!");
        }

        private void LoadGrid()
        {

            bdaula4Entities context = new bdaula4Entities();

            var dados = (from c in context.Cidade
                         from e in context.Estado.Where(x => x.Id == c.Id)
                         select new
                         {
                             IdCidade = c.Id,
                             NomeCidade = c.Descricao,
                             Estado = e.Descricao
                         }).ToList();
                         
            gvCidade.DataSource = dados;
            gvCidade.DataBind();
        }

        private void LoadDDL()
        {
            dlEstado.DataSource = new bdaula4Entities().Estado.ToList<Estado>();
            dlEstado.DataTextField = "Descricao";
            dlEstado.DataValueField = "Id";
            dlEstado.DataBind();
        }

        private void SendMessage(string txt)
        {
            lblMsg.Text = txt;
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}