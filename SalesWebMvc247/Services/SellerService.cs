using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc247.Data;
using SalesWebMvc247.Models;
using SalesWebMvc247.Services.Exceptions;

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
            //Include(obj => obj.Department) (namespace: Microsoft.EntityFrameworkCore) pra fazer join das tabelas
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);

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

        public void Update(Seller obj)
        {
            //any (verifica se no banco existe alguma coisa com a condição que vc vai passar aqui abaixo
            //predicado x que leva =>oque é banco(vendedor x da tabela) e objeto que recebemos (obj.Id) 
            if (!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);
                _context.SaveChanges();

            }
            //execção de concorrencia ao acessar o banco de dados EntityFrameworkCore nivel de banco
            catch (DbUpdateConcurrencyException e)
            {
                //lançando em nivel de camada de serviço  ---- servico e controle se comunica
                throw new DbConcurrencyException(e.Message); 
            }
            

        }


    }
}
