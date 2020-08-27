using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc247.Models;
using SalesWebMvc247.Services;

namespace SalesWebMvc247.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
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


    }
}
