using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public  class Expense:BaseEntity
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime PaymentDate { get; set; }
    }


}
