using SalesWebMvc247.Data;
using SalesWebMvc247.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace SalesWebMvc247.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvc247Context _context;
       

        public DepartmentService(SalesWebMvc247Context context)
        {
            _context = context;
        }

        //criamos abaixo um metodos pra retornar todos os departamentos da lista
        //Tacks é um objeto que emcapsula o processamento assicrona  -- async assinatura do metodo
        public async Task<List<Department>> FindAllAsync()  // pertnece ----  using System.Threading.Tasks;   
        //o sufixo Async não é obrigatorio mais é recomendado é recomandação da plantaforma c# 
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync(); //oListAsync pertence a using Microsoft.EntityFrameworkCore;
        }
    }
}
