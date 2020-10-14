using System.Collections.Generic;
using TestePratico.Model;

namespace TestePratico.Business
{
    public interface IPaymentsRepository
    {
        Payments Create(Payments item);
        Payments FindById(long id);
        List<Payments> FindAll();
        List<Payments> FindByName(string item);
        Payments Update(Payments item);
        void Delete(long id);
        bool Exists(long? id);
    }
}
