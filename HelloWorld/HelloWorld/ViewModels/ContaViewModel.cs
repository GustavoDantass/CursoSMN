using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorld.ViewModels
{
	public class ContaViewModel
	{
        public int IdConta { get; set; }
        public string Nome { get; set; }
        public DateTime? DataCadastro { get; set; }
        public int? IdUsuario { get; set; }

        //Vinculo
        public UsuarioViewModel Usuario { get; set; }
    }
}