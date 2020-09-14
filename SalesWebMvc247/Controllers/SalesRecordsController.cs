using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc247.Services;


namespace SalesWebMvc247.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            //aqui na condicional tenho que passar o valos seleciona e passar pra view  -- massete pra os valores ficar na caixinhas
            //testando
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1); //passei primeiro de do ano que estamos
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            // vou passar este dados minDat e maxDate lá  pra minha view com o dicionario ViewDate
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");


            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);
            
            return View(result);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            //aqui na condicional tenho que passar o valos seleciona e passar pra view  -- massete pra os valores ficar na caixinhas
            //testando
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1); //passei primeiro de do ano que estamos
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
          // vou passar este dados minDat e maxDate lá  pra minha view com o dicionario ViewDate
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
                      

            var result = await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate);



            return View(result);
        }


    }
}
