using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorld.ViewModels
{
	public class UsuarioViewModel
	{
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public byte FG_Ativo { get; set; }
    }
}