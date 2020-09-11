using Microsoft.EntityFrameworkCore;
using SalesWebMvc247.Data;
using SalesWebMvc247.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc247.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvc247Context _context;

        public SalesRecordService(SalesWebMvc247Context context)
        {
            _context = context;
        }


        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            /// é um dbsete ---- _context.SalesRecord select obj
            // com este resullado criamos um objeto result Iqueryable  === questionável ai vou pode utilizar e criar mais consltas 
            //vamos poder acresentar mais detalhes
            var result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
            .Include(x => x.Seller)  //EntityFrameworkCore
            .Include(x => x.Seller.Department) //EntityFrameworkCore
            .OrderByDescending(x => x.Date)  // linq
            .ToListAsync();



        }


    }
}
