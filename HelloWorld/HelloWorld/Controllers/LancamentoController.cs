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


namespace HelloWorld.Controllers
{
    public class LancamentoController : Controller
    {

        //=============================================================================================================

        private BancoDeDados _connect;

        public LancamentoController()
        {
            _connect = new BancoDeDados();
        }

        public void LoadAcao(LancamentoViewModel testmodel)
        {
            _connect.ExecutarProcedure("GKSSP_SelAcoes");

            var acoes = new List<AcaoViewModel>();
            using (var leitor = _connect.ExecuteReader())
            {
                while (leitor.Read())
                {
                    acoes.Add(new AcaoViewModel
                    {
                        IdAcao = leitor.GetInt32(leitor.GetOrdinal("IdAcao")),
                        Nome = leitor.GetString(leitor.GetOrdinal("Nome"))
                    });
                }
            }

            testmodel.ListaAcao = acoes;


        }


        public ActionResult Test()
        {
            LancamentoViewModel testmodel = new LancamentoViewModel();

            LoadAcao(testmodel);

            return View(testmodel);
        }

        [HttpPost]
        public ActionResult Test(LancamentoViewModel teste)
        {
            LoadAcao(teste);


            return View(teste);
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

                var prop = new LancamentoViewModel();

               

                return PartialView("_Grid", Lancamento);

            
            }
            catch (Exception ex)
            {


                return View("Error:", ex.Message);
            }
        }
        public ActionResult BuscarDados(int? IdLancamento)
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

                

                LoadAcao(lancamento);

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