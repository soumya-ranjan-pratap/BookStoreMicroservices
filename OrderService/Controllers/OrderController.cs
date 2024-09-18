using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Data;
using OrderService.Models;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public OrderController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            return Ok(dbContext.Orders.ToList());
        }
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Order>> GetOrder(int id)
        {
            var order = dbContext.Orders.FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public ActionResult<Order> PostBook(AddOrder nOrder)
        {
            var newOrder = new Order()
            {
                ProductId = nOrder.ProductId,
                Quantity = nOrder.Quantity,
                OrderDate = DateTime.Now
            };
            dbContext.Orders.Add(newOrder);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(AddOrder), new {  id = nOrder.ProductId }, nOrder);
        }
        [HttpPut("{ProductId}")]
        public ActionResult UpdateOrders(int id, UpdateOrder nOrder)
        {
            if(id != nOrder.ProductId)
            {
                return BadRequest();
            }
            var uOrder = dbContext.Orders.FirstOrDefault(o=> o.ProductId == nOrder.ProductId);
            if(uOrder != null)
            {
                uOrder.ProductId = nOrder.ProductId;
                uOrder.Quantity = nOrder.Quantity;
                dbContext.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("{ProductId}")]
        public ActionResult Delete(int productId)
        {
            var delOrder = dbContext.Orders.FirstOrDefault(o=> o.ProductId == productId);
            if(delOrder != null)
            {
                dbContext.Orders.Remove(delOrder);
                dbContext.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
