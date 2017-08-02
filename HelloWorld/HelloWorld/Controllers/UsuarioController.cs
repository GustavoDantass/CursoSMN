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
    public class UsuarioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BuscarGrid()
        {
            try
            {
                var resposta = Requisicao.Get("http://localhost:5000/api/Usuario");
                if (!resposta.IsSuccessStatusCode)
                    return View("Error", "Erro ao buscar api");

                var usuario = JsonConvert.DeserializeObject<IEnumerable<UsuarioViewModel>>(resposta.Content.ReadAsStringAsync().Result);

                return PartialView("_Grid", usuario);
            }
            catch (Exception ex)
            {


                return View("Error:", ex.Message);
            }
        }
        public ActionResult BuscarDados(int? IdUsuario)
        {
            try
            {
                var usuario = new UsuarioViewModel();

                if (IdUsuario.HasValue)
                {
                    var resposta = Requisicao.Get("http://localhost:5000/api/Usuario?id=" + IdUsuario);

                    if (!resposta.IsSuccessStatusCode)
                        return View("Error", "Erro ao buscar dados");

                    usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(resposta.Content.ReadAsStringAsync().Result);
                }

                return View("_Dados", usuario);
            }
            catch (Exception ex)
            {
                return View("Error:", ex.Message);
            }

        }


        public ActionResult Post(UsuarioViewModel usuario)
        {
            try
            {
                var resposta = Requisicao.Post("http://localhost:5000/api/Usuario", usuario);

                if (!resposta.IsSuccessStatusCode)
                {
                    Response.StatusCode = 400;
                    Response.TrySkipIisCustomErrors = true;
                    return Content("Erro ao adicionar usuario");
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

        public ActionResult Put(UsuarioViewModel usuario)
        {
            try
            {
                var resposta = Requisicao.Put("http://localhost:5000/api/Usuario", usuario);

                if (!resposta.IsSuccessStatusCode)
                {
                    Response.StatusCode = 400;
                    Response.TrySkipIisCustomErrors = true;
                    return Content("Erro ao editar usuario");
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

        public ActionResult Delete(int IdUsuario)
        {
            try
            {
                var resposta = Requisicao.Delete("http://localhost:5000/api/Lancamento?IdUsuario=" + IdUsuario);

                if (!resposta.IsSuccessStatusCode)
                {
                    Response.StatusCode = 400;
                    Response.TrySkipIisCustomErrors = true;
                    return Content("Erro ao deletar usuario");
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