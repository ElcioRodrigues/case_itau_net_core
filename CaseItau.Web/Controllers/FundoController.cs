using CaseItau.Web.Models;
using CaseItau.Web.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;

namespace CaseItau.Web.Controllers
{
    public class FundoController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ServiceRepository _service;
        public FundoController(IConfiguration config) {
            _config = config;
            _service = new ServiceRepository(_config);
        }

        public ActionResult Index()
        {
            return View(_service.GetFundos());
        }


        public ActionResult Create()
        {
            LoadDropDown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FundoVM fundo)
        {
            try
            {
                fundo.Cnpj = Regex.Replace(fundo.Cnpj, @"[^\d]", "");
                var result = _service.CreateFundo(fundo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                LoadDropDown();
                ViewBag.Error = ex.GetBaseException()?.Message ?? ex.Message; 
                return View(fundo);
            }
        }

        public ActionResult Details(string code)
        {
            LoadDropDown();
            return View(_service.GetFundo(code));
        }

        public ActionResult Edit(string code)
        {
            LoadDropDown();
            return View(_service.GetFundo(code));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FundoVM fundo)
        {
            try
            {
                LoadDropDown();
                fundo.Cnpj = Regex.Replace(fundo.Cnpj, @"[^\d]", "");
                var result = _service.EditFundo(fundo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                LoadDropDown();
                ViewBag.Error = ex.GetBaseException()?.Message ?? ex.Message;
                return View(fundo);
            }
        }

        public ActionResult EditPatrimonio(string code)
        {
            return View(_service.GetFundo(code));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPatrimonio(string code, decimal patrimonio)
        {
            try
            {
                var result = _service.EditPatrimonioFundo(code, patrimonio);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                LoadDropDown();
                ViewBag.Error = ex.GetBaseException()?.Message ?? ex.Message;
                return View();
            }
        }


        public ActionResult Delete(string code)
        {
            try
            {
                var result = _service.DeleteFundo(code);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.GetBaseException()?.Message ?? ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        private void LoadDropDown()
        {
            var listTipo = _service.GetTiposFundoInvestimento();
            ViewBag.ListTipo = listTipo.Select(x => new SelectListItem() { Text = x.Nome, Value = x.Codigo.ToString()}).ToList();
        }
    }
}
