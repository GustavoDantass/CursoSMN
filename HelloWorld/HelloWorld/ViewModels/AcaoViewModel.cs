using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorld.ViewModels
{
	public class AcaoViewModel
	{
        public int IdAcao { get; set; }
        public string Nome { get; set; }

        public List<AcaoViewModel> ListaAcao { get; set; }

        public AcaoViewModel()
        {
            ListaAcao = new List<AcaoViewModel>();
        }

    }
}