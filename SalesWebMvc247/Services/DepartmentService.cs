using SalesWebMvc247.Data;
using SalesWebMvc247.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


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
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
