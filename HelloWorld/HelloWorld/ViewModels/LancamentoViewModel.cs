using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HelloWorld.ViewModels
{
    public class LancamentoViewModel
    {
        public int IdLancamento { get; set; }
        public DateTime? DataEvento { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string Descricao { get; set; }
        public decimal? Valor { get; set; }
        public int? IdCategoria { get; set; }
        public int? IdAcao { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdConta { get; set; }
        public byte? FG_Pago { get; set; }


   

        //Vinculos
        public CategoriaViewModel Categoria { get; set; }
        public AcaoViewModel Acao { get; set; }
        public ContaViewModel Conta { get; set; }
        public UsuarioViewModel Usuario { get; set; }

        public List<AcaoViewModel> ListaAcao { get; set; }
        public List<CategoriaViewModel> ListaCategoria{ get; set; }
        public List<ContaViewModel> ListaConta { get; set; }
        public List<UsuarioViewModel> ListaUsuario { get; set; }


        public LancamentoViewModel()
        {
            ListaAcao = new List<AcaoViewModel>();
            ListaCategoria = new List<CategoriaViewModel>();
            ListaConta = new List<ContaViewModel>();
            ListaUsuario = new List<UsuarioViewModel>();
        }

        //================================================================================================================





    }
}