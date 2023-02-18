using Ecomm.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.Controllers
{
    [Route("api/[controller]")]//this API Controller provide us default routing
    [ApiController]
    public class CategoryListController : ControllerBase
    {//This List Data is a mock data of example
        public static List<Category> listofCategories = new List<Category> // Ye List sabhi other methods/Projects me show ho jae iske liye hamein is List ko static banana padega
        {
            new Category{ Id = 1, Title = "Samsung", DisplayOrder = 1 },//here we treat mobile phones companies as a categories // here List index position is Started from 0, so we work here on list item so we make sure we works by/remember initial 0 position of List here of Thanks
             new Category{ Id = 2, Title = "Nokia", DisplayOrder = 2 },
              new Category{ Id = 3, Title = "LG", DisplayOrder = 3 },
               new Category{ Id = 4, Title = "Xiomi", DisplayOrder = 4 },
                new Category{ Id = 5, Title = "Apple", DisplayOrder = 5 }
        };

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return listofCategories;
        }

        [HttpGet("{id}")]
        public Category GetT(int id)
        {
            return listofCategories[id]; // here id matched from list item and returned that's it
        }

        [HttpPost]
        public void Post([FromBody] Category category) // Ye body of the content hai isliye hamne yahan [FromBody] define kia hai <- which means jo data hamare pass aata hai vo Postman ki Body Content se aata hai <- simple means agar hamara data postman se aata hai to ham yahan likhenge from body
        {
            listofCategories.Add(category); // Agar hamein List me Data save karna hai to hamare pass ek method hota hai -> Add
        }

        [HttpPut("{id}")] // so Put ke case me url se jo bhi id starting by 0 bhejenge to vo List ke 0 positioned item se match ho kar update ho jaega, body ki id se url id ka koi lena dena nahi hoga in List case jo id body me hogi vahi update ho jaegi
        public void Put(int id, [FromBody] Category category)
        {
            listofCategories[id] = category;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            listofCategories.RemoveAt(id); // RemoveAt func for 1 id rec del karne ke liye
        }
    }
}

//In our List save only temrary data so we restart our project then our List data is Lost/Unvisible