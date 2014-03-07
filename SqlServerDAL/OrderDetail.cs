using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.DataAnnotations;

namespace WebApi2.SqlServerDAL
{
    public class OrderDetail
    {
        [AutoIncrement]
        public int Id { get; set; }

        [References(typeof(Order))]
        public int OrderId { get; set; }

        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}
