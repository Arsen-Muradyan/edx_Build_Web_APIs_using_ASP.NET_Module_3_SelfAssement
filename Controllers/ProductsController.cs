using Microsoft.AspNetCore.Mvc;
using WebServer.Models;
using System.Linq;
namespace WebServer.Controllers
{
  [Route("api/[controller]")]
  public class ProductsController : Controller 
  {
    [HttpGet]
    public ActionResult Get() 
    {
      var products = FakeData.Products.Values.ToArray();
      return Ok(products);
    }
    [HttpGet("{id}")]
    public ActionResult GetProductById([FromRoute] int id) 
    {
      if (FakeData.Products.ContainsKey(id)) 
      {
        return Ok(FakeData.Products[id]);
      } 
      else 
      {
        return NotFound();
      }
    }
    [HttpGet("price/{low}/{high}")]
    public ActionResult SelectWithPrice([FromRoute] double low, [FromRoute] double high) 
    {
      var products = FakeData.Products.Values.Where(p => p.Price <= high && p.Price >= low).ToArray();
      if (products.Length > 0) 
      {
        return Ok(products);
      } 
      else 
      {
        return NotFound();
      }
    }
    [HttpDelete("{id}")]
    public ActionResult DeleteProductById([FromRoute] int id) 
    {
      if (FakeData.Products.ContainsKey(id)) 
      {
        FakeData.Products.Remove(id);
        return Ok();
      } 
      else 
      {
        return NotFound();
      }
    }
    [HttpPost]
    public ActionResult CreateProduct([FromBody] Product product) 
    {
      product.ID = FakeData.Products.Keys.Max() +1;
      FakeData.Products.Add(product.ID, product);
      return Created($"api/products/{product.ID}", product);
    }
    [HttpPut("{id}")]
    public ActionResult UpdateProduct([FromRoute] int id, [FromBody] Product product) 
    {
      if (FakeData.Products.ContainsKey(id)) 
      {
        var target = FakeData.Products[id];
        target.ID = product.ID;
        target.Name = product.Name;
        target.Price = product.Price;
        return Ok(); 
      } 
      else 
      {
        return NotFound();
      } 
    }
    [HttpPut("raise/{price}")]
    public ActionResult RaisePrices([FromRoute] double price)
    {
      foreach (var product in FakeData.Products.Values)
      {
        product.Price += price;
      }
      return Ok();
    }

  }
  
}