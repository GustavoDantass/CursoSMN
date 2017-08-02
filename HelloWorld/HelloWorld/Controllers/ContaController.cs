using HelloWorld.infra;
using HelloWorld.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorld.Controllers
{
    public class ContaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BuscarGrid()
        {
            try
            {
                var resposta = Requisicao.Get("http://localhost:5000/api/Conta");

                if (!resposta.IsSuccessStatusCode)
                    return View("Error", "Erro ao buscar api");

                var conta = JsonConvert.DeserializeObject<IEnumerable<ContaViewModel>>(resposta.Content.ReadAsStringAsync().Result);

                return PartialView("_Grid", conta);
            }
            catch (Exception ex)
            {


                return View("Error:", ex.Message);
            }
        }
        public ActionResult BuscarDados(int? IdConta)
        {
            try
            {
                var conta = new ContaViewModel();

                if (IdConta.HasValue)
                {
                    var resposta = Requisicao.Get("http://localhost:5000/api/Conta?id=" + IdConta);

                    if (!resposta.IsSuccessStatusCode)
                        return View("Error", "Erro ao buscar dados");

                    conta = JsonConvert.DeserializeObject<ContaViewModel>(resposta.Content.ReadAsStringAsync().Result);
                }

                return View("_Dados", conta);
            }
            catch (Exception ex)
            {
                return View("Error:", ex.Message);
            }

        }

        public ActionResult Post(ContaViewModel conta)
        {
            try
            {
                var resposta = Requisicao.Post("http://localhost:5000/api/Conta", conta);

                if(!resposta.IsSuccessStatusCode)
                {
                    Response.StatusCode = 400;
                    Response.TrySkipIisCustomErrors = true;
                    return Content("Erro ao adicionar conta");
                }

                Response.StatusCode = 200;
                return Content("");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
                
            }
        }

        public ActionResult Put(ContaViewModel conta)
        {
            try
            {
                var resposta = Requisicao.Put("http://localhost:5000/api/Conta", conta);

                if(!resposta.IsSuccessStatusCode)
                {
                    Response.StatusCode = 400;
                    Response.TrySkipIisCustomErrors = true;
                    return Content("Falha ao editar Conta");
                }

                Response.StatusCode = 200;
                return Content("");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
                
            }
        }

        
    }
}