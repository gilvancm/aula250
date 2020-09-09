using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc247.Models;
using SalesWebMvc247.Models.ViewModels;
using SalesWebMvc247.Services;
using SalesWebMvc247.Services.Exceptions;

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

        public async Task<IActionResult>  Index()  // temos que respeitar a chamada do controle Index não muda 
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            //intanciou abaixo
            var viewModel = new SellerFormViewModel { Departments = departments };  //departamentos populados
            return View(viewModel);  // retorno vai receber este objeto pronto
        }

        // criar a ação de inserir no banco de dados

        //vamos colocar 2 ações abaixo e vamos colocar um aniteicho
        [HttpPost]
        [ValidateAntiForgeryToken]  // par minha apalicação não sofra ataque CSRF , quando alguem aprovewita de sua atenticacao e manda dados maliciosos
        public async Task<IActionResult> Create(Seller seller)
        {
            //verifico se o model não foi validado   -- vai acontecer enquanto o usaruio não terminar de preencher o formulario 1 se
            //não deu certo vou ter de alterar pra pegar o viewModel
            /*
             if (!ModelState.IsValid)
             {
                 return View(seller);
             }
             */
            //nova instanciação
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }


            await _sellerService.InsertAsync(seller);
            //para redirecionar 
            //return RedirectToAction("Index");
            return RedirectToAction(nameof(Index));

        }

        // ? do tipo valor opcional
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
               // return NotFound();
                // passomos um ojeto anonimo ----- new { message = "Id not provided == Id não foi fornecido( nulo)" });
                return RedirectToAction(nameof(Error), new { message = "Id not provided == Id não foi fornecido( nulo)" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                // return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not found == Id não Existe" });
            }

            return View(obj);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {

            try {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));

            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
                        
        }

        //criar a detalhes detalhes
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                // return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not provided == Id não foi fornecido( nulo)" });
            }
            //acionar o FindById é com control+click
            var obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                // return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not found == Id não Existe" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                // return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not provided == Id não foi fornecido( nulo)" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                // return NotFound();
                return RedirectToAction(nameof(Error), new { message = "Id not found == Id não Existe" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();

            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

            return View(viewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id , Seller seller)
        {
            //verifico se o model não foi validado  -- vai acontecer enquanto o usaruio não terminar de preencher o formulario 1 se
            /*
            if (!ModelState.IsValid)
            {
                return View(seller);
            }
            */
            //nova instanciação
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
               // return BadRequest();
                return RedirectToAction(nameof(Error), new { message = "Id mismatch == Id não Corresponde" });
            }


            try
            {
               await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));

            }
            /*
            // estes 2 caso abaixo como são exeções vai ser diferente o tratamento --- crio um apelido e
            catch (NotFoundException e)
            {
                 // return NotFound();
                   return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            catch (DbConcurrencyException e)
            {
                  // return BadRequest();
                 return RedirectToAction(nameof(Error), new { message = e.Message });
            }
            */
            // podemos facilitar sua vida na expessão de erro faço aplicação na hieraquia
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }


        }
        //acção da pagina de erro(carregar)
        // a ação de erro não pode ser assicrona porque ela não acesso a dados
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }


        
    }
}
