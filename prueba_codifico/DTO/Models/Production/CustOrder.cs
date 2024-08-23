using System;
using System.Collections.Generic;

namespace prueba_codifico.DTO.Models.Production;

public partial class CustOrder
{
    public int? Custid { get; set; }

    public DateTime? Ordermonth { get; set; }

    public int? Qty { get; set; }
}
