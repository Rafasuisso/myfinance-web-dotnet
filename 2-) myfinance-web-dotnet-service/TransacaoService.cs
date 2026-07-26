using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet_domain.Entities;
using myfinance_web_dotnet_infra;
using myfinance_web_dotnet_service.Interfaces;

namespace myfinance_web_dotnet_service
{
    public class TransacaoService : ITransacaoService
    {
        private MyFinanceDbContext _dbContext;

        public TransacaoService(MyFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Cadastrar(Transacao entidade)
        {
           var dbSet = _dbContext.Transacao;

           if(entidade.Id == null)
           {
                dbSet.Add(entidade);
           }
           else
           {
                dbSet.Attach(entidade);
                _dbContext.Entry(entidade).State = EntityState.Modified;
           }

            _dbContext.SaveChanges();
        }

        public void Excluir(int Id)
        {
            var transacao = new Transacao { Id = Id };
            _dbContext.Attach(transacao);
            _dbContext.Remove(transacao);
            _dbContext.SaveChanges();
        }

        public List<Transacao> ListarRegistros()
        {
            var dbSet = _dbContext.Transacao.Include(x => x.PlanoConta);
            return dbSet.ToList();
        }

        public Transacao RetornarRegistro(int Id)
        {
            return _dbContext.Transacao.Where(x => x.Id == Id).First();
        
        }
    }
}