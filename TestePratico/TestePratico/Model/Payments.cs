using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace TestePratico.Model
{
    [Table("payments")]
    public class Payments
    {
        [Column("id")]
        public long? Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("DueDate")]
        public DateTime DueDate { get; set; }

        [Column("Payday")]
        public DateTime Payday { get; set; }

        [Column("Value")]
        public decimal Value { get; set; }

        [Column("CorrectedValue")]
        public decimal CorrectedValue { get; set; }

        [Column("Days")]
        public int Days { get; set; }

    }
}
