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
            //pra que o vendedor não fique orfã deo departmento
            // depois tive que tirar da seleção não existe default
          //  obj.Department = _context.Department.First();
            
            _context.Add(obj);
            // para confimar a operação no banco de dados com SaveChanges()
            _context.SaveChanges();
        }
        //deletar o vendedor
        //primeiro encontra o Id do vendedor com metodo FindById()
        public Seller FindById(int id)
        {
            return _context.Seller.FirstOrDefault(obj => obj.Id == id);

        }
        //agora implementar o metodo remover agora é uma ação de remover

        public void Remove(int? id)
        {
            //aqui eu pego o objeto na mão
            var obj = _context.Seller.Find(id);
            // agora removi o objeto do dbsete
            _context.Seller.Remove(obj);
            //agora tenho que confimar pra Entity Framework remover do banco de dados
            _context.SaveChanges();
        
        }


    }
}
