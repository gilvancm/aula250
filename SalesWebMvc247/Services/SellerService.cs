using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc247.Data;
using SalesWebMvc247.Models;

namespace SalesWebMvc247.Services
{
    public class SellerService
    {
        private readonly SalesWebMvc247Context _context;

        public SellerService(SalesWebMvc247Context context)
        {
            _context = context;
        }


        //cria uma operação de acesso a dados pra retornar com todos os registro do banco de dados.

        // no entanto vai excutar uma busca sincrona de pois vamos alterar pra assicrona

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
           
        }

        //criar um metodo pra inserir o (Seller)vendedor no banco de dados
        //mais que faz ação é o controle
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            // para confimar a operação no banco de dados com SaveChanges()
            _context.SaveChanges();
        }

    }
}
