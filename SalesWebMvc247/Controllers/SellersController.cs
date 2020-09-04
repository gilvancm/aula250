using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc247.Models;
using SalesWebMvc247.Models.ViewModels;
using SalesWebMvc247.Services;

namespace SalesWebMvc247.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            //intanciou abaixo
            var viewModel = new SellerFormViewModel { Departments = departments };  //departamentos populados
            return View(viewModel);  // retorno vai receber este objeto pronto
        }

        // criar a ação de inserir no banco de dados

        //vamos colocar 2 ações abaixo e vamos colocar um aniteicho
        [HttpPost]
        [ValidateAntiForgeryToken]  // par minha apalicação não sofra ataque CSRF , quando alguem aprovewita de sua atenticacao e manda dados maliciosos
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            //para redirecionar 
            //return RedirectToAction("Index");
            return RedirectToAction(nameof(Index));

        }

        // ? do tipo valor opcional
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        //criar a detalhes detalhes
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //acionar o FindById é com control+click
            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }



    }
}
