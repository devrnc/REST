using System.Collections.Generic;
using TestePratico.Model;

namespace TestePratico.Business
{
    public interface IPaymentsBusiness
    {
        Payments Create(Payments payment);
        Payments FindById(long id);
        List<Payments> FindAll();
        List<Payments> FindByName(string item);
        Payments Update(Payments payment);
        void Delete(long id);
    }
}
