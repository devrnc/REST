using System.Collections.Generic;
using TestePratico.Model;


namespace TestePratico.Business.Implementattions
{
    public class PaymentsBusinessImpl : IPaymentsBusiness
    {
        private IPaymentsRepository repository;

        public PaymentsBusinessImpl(IPaymentsRepository repository)
        {
            this.repository = repository;
        }

        public Payments Create(Payments payment)
        {
            return this.repository.Create(payment);
        }

        public Payments FindById(long id)
        {
            return this.repository.FindById(id);
        }

        public List<Payments> FindAll()
        {
            return this.repository.FindAll();
        }

        public List<Payments> FindByName(string name)
        {
            return this.repository.FindByName(name);
        }

        public Payments Update(Payments payment)
        {
            return this.repository.Update(payment);
        }

        public void Delete(long id)
        {
            this.repository.Delete(id);
        }

        public bool Exists(long id)
        {
            return this.repository.Exists(id);
        }
    }
}