using System.Collections.Generic;
using TestePratico.Model;
using TestePratico.Model.Context;
using System;
using System.Linq;
using TestePratico.Business;


namespace TestePratico.Repository.Implementattion
{
    public class PaymentsRepositoryImpl : IPaymentsRepository
    {
        private readonly MySQLContext context;

        public PaymentsRepositoryImpl(MySQLContext context)
        {
            this.context = context;
        }

        public Payments Create(Payments payment)
        {
            if (Exists(payment.Id))
                return null;

            try
            {
                BussinessRules(payment);
                
                this.context.Add(payment);
                this.context.SaveChanges();
                return payment;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Delete(long id)
        {
            Payments payment = this.context .Payments.SingleOrDefault(i => i.Id.Equals(id));

            if (payment == null)
                return;

            try
            {
                this.context.Payments.Remove(payment);
                this.context.SaveChanges();
            }
            catch (Exception)
            {
                return;
            }
        }

        // Método responsável por retornar uma registro
        public Payments FindById(long id)
        {
            return this.context.Payments.SingleOrDefault(p => p.Id.Equals(id));
        }

        // Método responsável por retornar todos os registros
        public List<Payments> FindAll()
        {
            return this.context.Payments.ToList();
        }

        public List<Payments> FindByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                return this.context.Payments.Where(p => p.Name.Contains(name)).ToList();

            return this.context.Payments.ToList();
        }

        public bool Exists(long? id)
        {
            return this.context.Payments.Any(b => b.Id.Equals(id));
        }

        public Payments Update(Payments payment)
        {
            if (!Exists(payment.Id))
                return null;

            Payments result = this.context.Payments.SingleOrDefault(b => b.Id == payment.Id);
            if (result != null)
            {
                try
                {
                   BussinessRules(payment);

                    this.context.Entry(result).CurrentValues.SetValues(payment);
                    this.context.SaveChanges();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return result;
        }

        // Bussines Rules
        private void BussinessRules(Payments payment)
        {
            int days = payment.Payday.Date.Subtract(payment.DueDate.Date).Days;
            decimal value;

            if (days > 0 && days <= 3)
            {
                value = payment.Value + (payment.Value * 2 / 100) + (payment.Value * 1 / 1000) * days;
            }
            else if (days > 3 && days <= 5)
            {
                value = payment.Value + (payment.Value * 3 / 100) + (payment.Value * 2 / 1000) * days;
            }
            else if (days > 5)
            {
                value = payment.Value + (payment.Value * 5 / 100) + (payment.Value * 3 / 1000) * days;
            }
            else
            {
                value = payment.Value;
            }
            payment.CorrectedValue = value;
            payment.Days = days;
        }

    }
}
