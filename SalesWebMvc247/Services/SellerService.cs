using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc247.Data;
using SalesWebMvc247.Models;
using SalesWebMvc247.Services.Exceptions;


//acima são os imports

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

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
           
        }

        //criar um metodo pra inserir o (Seller)vendedor no banco de dados
        //mais que faz ação é o controle
        public async Task InsertAsync(Seller obj)  // troca void por Task
        {
            //pra que o vendedor não fique orfã deo departmento
            // depois tive que tirar da seleção não existe default
          //  obj.Department = _context.Department.First();
            
            _context.Add(obj); // a operação add é só feito em memoria
            // para confimar a operação no banco de dados com SaveChanges()
            await _context.SaveChangesAsync(); // a saveChanges como é no servidor banco ai coloca
        }
        //deletar o vendedor
        //primeiro encontra o Id do vendedor com metodo FindById()
        public async Task<Seller> FindByIdAsync(int id)

        {
            //Include(obj => obj.Department) (namespace: Microsoft.EntityFrameworkCore) pra fazer join das tabelas
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);

        }
        //agora implementar o metodo remover agora é uma ação de remover

        public async Task RemoveAsync(int id)
        {

            try {

                //aqui eu pego o objeto na mão
                var obj = await _context.Seller.FindAsync(id);
                // agora removi o objeto do dbsete 
                _context.Seller.Remove(obj);
                //agora tenho que confimar pra Entity Framework remover do banco de dados
                await _context.SaveChangesAsync();

            }
            catch(DbUpdateException e) //DbUpdateException   ----  interceptar essa essessão que é do EntityFrameworkCore
            {
                ///aqui vamos lançar no nivel de serviço aesseção
              //  throw new IntegrityException(e.Message);
                throw new IntegrityException("Não podemos excluir o registro de Saller, existe Sales");
            }
        


        }

        public async Task UpdateAsync(Seller obj)
        {
            //any (verifica se no banco existe alguma coisa com a condição que vc vai passar aqui abaixo
            //predicado x que leva =>oque é banco(vendedor x da tabela) e objeto que recebemos (obj.Id) 

            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if(!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();

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
