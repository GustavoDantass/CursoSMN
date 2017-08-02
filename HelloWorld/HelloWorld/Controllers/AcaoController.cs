using HelloWorld.infra;
using HelloWorld.ViewModels;
using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HelloWorld.Controllers
{
    public class AcaoController : Controller
    {
        //=============================================================================================================


        //private BancoDeDados _connect;

        //public AcaoController()
        //{
        //    _connect = new BancoDeDados();
        //}
      
        public void LoadAcao(AcaoViewModel testmodel)
        {
            //_connect.ExecutarProcedure("GKSSP_SelAcoes");

            //var acoes = new List<AcaoViewModel>();
            //using (var leitor = _connect.ExecuteReader())
            //{
            //    while (leitor.Read())
            //    {
            //        acoes.Add(new AcaoViewModel
            //        {
            //            IdAcao = leitor.GetInt32(leitor.GetOrdinal("IdAcao")),
            //            Nome = leitor.GetString(leitor.GetOrdinal("Nome"))
            //        });
            //    }
            //}

            var resposta = Requisicao.Get("http://localhost:5000/api/Acao");

            var acao = JsonConvert.DeserializeObject<IEnumerable<AcaoViewModel>>(resposta.Content.ReadAsStringAsync().Result);

            testmodel.ListaAcao = acao.ToList();
            

        }


        public ActionResult Test()
        {
            AcaoViewModel testmodel = new AcaoViewModel();

            LoadAcao(testmodel);

            return View(testmodel);
        }

        //[HttpPost]
        //public ActionResult Test(AcaoViewModel teste)
        //{
        //    LoadAcao(teste);


        //    return View(teste);
        //}


        //=============================================================================================================

        public ActionResult Index()
        {
            return View();  
        }

        public ActionResult BuscarGrid()
        {
            try
            {
                var resposta = Requisicao.Get("http://localhost:5000/api/Acao");
                if(!resposta.IsSuccessStatusCode)
                    return View("Error", "Erro em buscar api");

                var acao = JsonConvert.DeserializeObject<IEnumerable<AcaoViewModel>>(resposta.Content.ReadAsStringAsync().Result);

                return PartialView("_Grid", acao);
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }
        }

        public ActionResult BuscarDados(int? IdAcao)
        {
            try
            {
                var acao = new AcaoViewModel();

                if (IdAcao.HasValue)
                {
                    var resposta = Requisicao.Get("http://localhost:5000/api/Acao?id=" + IdAcao);

                    if (!resposta.IsSuccessStatusCode)
                    {
                        Response.StatusCode = 400;
                        Response.TrySkipIisCustomErrors = true;
                        return Content("Erro ao buscar dados");
                    }

                    acao = JsonConvert.DeserializeObject<AcaoViewModel>(resposta.Content.ReadAsStringAsync().Result);
                }

                return View("_Dados", acao);
            }
            catch (Exception ex)
            {
                return View("Error:", ex.Message);
            }
        }

        public ActionResult Post(CategoriaViewModel acao)
        {
            try
            {
                var resposta = Requisicao.Post("http://localhost:5000/api/Acao", acao);

                if (!resposta.IsSuccessStatusCode)
                {
                    Response.StatusCode = 400;
                    Response.TrySkipIisCustomErrors = true;
                    return Content("Erro ao buscar dados");
                }

                Response.StatusCode = 200;
                Response.TrySkipIisCustomErrors = true;

                return Content("");
            }
            catch (Exception ex)
            {
                return View("Erro", ex.Message);
            }
        }

        public ActionResult Put(AcaoViewModel acao)
        {
            try
            {
                var resposta = Requisicao.Put("http://localhost:5000/api/Acao", acao);

                if (!resposta.IsSuccessStatusCode)
                {
                    Response.StatusCode = 400;
                    Response.TrySkipIisCustomErrors = true;
                    return Content("Falha ao excluir Acao");
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
        public ActionResult Delete(int IdAcao)
        {
            try
            {
                var resposta = Requisicao.Delete("http://localhost:5000/api/Acao?idAcao=" + IdAcao);


                if (!resposta.IsSuccessStatusCode)
                {
                    Response.StatusCode = 400;
                    Response.TrySkipIisCustomErrors = true;
                    return Content("Erro em deletar Ação");
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