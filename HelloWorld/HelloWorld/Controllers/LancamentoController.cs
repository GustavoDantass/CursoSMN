using HelloWorld.infra;
using HelloWorld.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace HelloWorld.Controllers
{
    public class LancamentoController : Controller
    {

        //=============================================================================================================


        public void LoadAcao(LancamentoViewModel lancamento)
        {
            var resposta = Requisicao.Get("http://localhost:5000/api/Acao");
            var Acao = JsonConvert.DeserializeObject<IEnumerable<AcaoViewModel>>(resposta.Content.ReadAsStringAsync().Result);
            
            lancamento.ListaAcao = Acao.ToList();


        }

        public void LoadCategoria(LancamentoViewModel testmodel)
        {
            var resposta = Requisicao.Get("http://localhost:5000/api/Categoria");
            var Categoria = JsonConvert.DeserializeObject<IEnumerable<CategoriaViewModel>>(resposta.Content.ReadAsStringAsync().Result);

            testmodel.ListaCategoria= Categoria.ToList();
        }

        public void LoadConta(LancamentoViewModel testmodel)
        {
            var resposta = Requisicao.Get("http://localhost:5000/api/Conta");
            var Conta = JsonConvert.DeserializeObject<IEnumerable<ContaViewModel>>(resposta.Content.ReadAsStringAsync().Result);

            testmodel.ListaConta = Conta.ToList();
        }

        public void LoadUsuario(LancamentoViewModel testmodel)
        {
            var resposta = Requisicao.Get("http://localhost:5000/api/Usuario");
            var Usuario = JsonConvert.DeserializeObject<IEnumerable<UsuarioViewModel>>(resposta.Content.ReadAsStringAsync().Result);

            testmodel.ListaUsuario = Usuario.ToList();
        }


        //================================================================================================

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BuscarGrid()
        {
            try
            {
                var resposta = Requisicao.Get("http://localhost:5000/api/Lancamento");

                if (!resposta.IsSuccessStatusCode)
                    return View("Error", "Erro ao buscar api");

                var Lancamento = JsonConvert.DeserializeObject<IEnumerable<LancamentoViewModel>>(resposta.Content.ReadAsStringAsync().Result);
                

               

                return PartialView("_Grid", Lancamento);

            
            }
            catch (Exception ex)
            {


                return View("Error:", ex.Message);
            }
        }
        public ActionResult BuscarDados(int? IdLancamento, DropDownList dll)
        {
            try
            {


                var lancamento = new LancamentoViewModel();

                if (IdLancamento.HasValue)
                {
                    var resposta = Requisicao.Get("http://localhost:5000/api/Lancamento?id=" + IdLancamento);

                    if (!resposta.IsSuccessStatusCode)
                        return View("Error", "Erro ao buscar dados");

                    lancamento = JsonConvert.DeserializeObject<LancamentoViewModel>(resposta.Content.ReadAsStringAsync().Result);
                }

              
                LoadCategoria(lancamento);
                LoadAcao(lancamento);
                LoadConta(lancamento);
                LoadUsuario(lancamento);

                return View("_Dados", lancamento);
            }
            catch (Exception ex)
            {
                return View("Error:", ex.Message);
            }

        }

        public ActionResult Post(LancamentoViewModel lancamento)
        {
            try
            {
                    var resposta = Requisicao.Post("http://localhost:5000/api/Lancamento", lancamento);

                if (!resposta.IsSuccessStatusCode)
                {
                    Response.StatusCode = 400;
                    Response.TrySkipIisCustomErrors = true;
                    return Content("Erro ao adicionar Lancamento");
                }

                Response.StatusCode = 200;
                return Content("");
            }
            catch(Exception ex)
            {
                Response.StatusCode = 500;
                Response.TrySkipIisCustomErrors = true;
                return Content(ex.Message);
            }

        }

        public ActionResult Put(LancamentoViewModel lancamento)
        {
            try
            {
                var resposta = Requisicao.Put("http://localhost:5000/api/Lancamento", lancamento);

                if(!resposta.IsSuccessStatusCode)
                {
                    Response.StatusCode = 400;
                    Response.TrySkipIisCustomErrors = true;
                    return Content("Erro ao editar lancamento");
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

        public ActionResult Delete(int IdLancamento)
        {
            try
            {
                var resposta = Requisicao.Delete("http://localhost:5000/api/Lancamento?IdLancamento=" + IdLancamento);

                if(!resposta.IsSuccessStatusCode)
                {
                    Response.StatusCode = 400;
                    Response.TrySkipIisCustomErrors = true;
                    return Content("Erro ao deletar lancamento");
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