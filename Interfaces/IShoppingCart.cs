using Shed.CoreKit.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Models;

namespace Interfaces
{
    public interface IShoppingCart
    {
        Cart Get();

        [HttpPut, Route("addorder/{productId}/{qty}")]
        Cart AddOrder(Guid productId, int qty);

        Cart DeletOrder(Guid orderId);

        [Route("getevents/{timestamp}")]
        IEnumerable<CartEvent> GetCartEvents(long timestamp);
    }
}
