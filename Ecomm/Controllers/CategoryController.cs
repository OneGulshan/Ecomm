using Azure.Storage.Blobs;
using Ecomm.Data;
using Ecomm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Ecomm.Controllers
{
    [Route("api/[controller]")]//this API Controller provide us default routing
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly Context _db;

        public CategoryController(Context db)
        {
            _db = db;
        }
        //api/category/
        [HttpGet]
        public async Task<IActionResult> Get() // here we used Task of T with async keyword because here ret something and Task of T is always used with Async keyword where something is returned // here all typeof datatypes is changed by IActionResult for Using Status Codes here like Category/void/IEnumerable<Category> etc
        {
            //return NotFound(); // NotFound for 404 NotFound msg
            //return BadRequest(); // BadRequest msg
            return Ok(await _db.Categories.ToListAsync()); // return Ok(await _db.Categories); <-  here showed us that await/Getawaiter type ka koi extension method nahin hai so we used a await typeof method here for resolving this error msg ToListAsync() <- this is found in EF core, In API we req to make queries Async typeof so, we use Async prog here, make most of sql querys to Async typeof, as posible // Ok is found in IActionResult for showing 200 success msg
        }
        //api/category/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetT(int id)
        {
            return Ok(await _db.Categories.FirstOrDefaultAsync(x => x.Id == id));
        }

        //api/category/Test/5
        //[HttpGet("[Test]/{id}")] // [Test]/{id} <- This is attribute routing here that is wrong write is on down side
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Test(int id)//Parameters Conflicted here like Get and Test for saving from this use Attribute Routing Simple
        {
            return Ok(await _db.Categories.FirstOrDefaultAsync(x => x.Id == id));
        }

        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Category category)
        //{
        //    await _db.Categories.AddAsync(category);
        //    await _db.SaveChangesAsync();
        //    return StatusCode(StatusCodes.Status201Created); // when we Post data to Web Server we req to use 201 Status Code
        //}

        //DefaultEndpointsProtocol=https;AccountName=shoppingstorages;AccountKey=BjRk+rB39HFtdSwQOfrH3Yr2Q3BfAf092Z4yZGSbO0TtqiWGa+wqkvH3C9luzrEXTtRSwGEOQW/n+AStxnpocg==;EndpointSuffix=core.windows.net
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Category category) // ye Post method hamara IActionResult hi hoga kunki jab ye return karenge file/jab ham post karenge apni file hamari db me, IActionResult Task return Karta hai par Task kuch return nahi karta isliye hamne yahan Task ka use kia
        {//here our img is uploaded on azure acc using Azure.Storage.Blobs. <- commened item not posible to navigate show a error msg can't navigate to under in caret
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=shoppingstorages;AccountKey=BjRk+rB39HFtdSwQOfrH3Yr2Q3BfAf092Z4yZGSbO0TtqiWGa+wqkvH3C9luzrEXTtRSwGEOQW/n+AStxnpocg==;EndpointSuffix=core.windows.net";
            string containerName = "shoppingcartphotos"; // hamare azure acc me mul container hone ke karan hamein yahan apna container specify karna padta hai
            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);// BlobContainerClient class is contain connectionString and containerName just like SqlConnection class of ADO.NET for using creating constr and also use communicate purpose
            BlobClient blobClient = containerClient.GetBlobClient(category.CategoryImage.FileName); // GetBlobClient used for getting Blob Client with its file name, here our BlobClient is CategoryImage that's it simple
            MemoryStream ms = new MemoryStream(); // jo img file ham apne computer se pick karte hain us img ko hamein apne MemoryStream me save karna hota hai by coping our img file into MemoryStream simple
            await category.CategoryImage.CopyToAsync(ms); // yhan hamne apni img file ko MemoryStream me copy kar diya
            ms.Position = 0; // hamein yahan MemoryStream ki position dekhni hogi bydefault same container ke ander 0 position par honi chaiye hamri imgs saari ki saari
            await blobClient.UploadAsync(ms);
            category.CategoryImagePath = blobClient.Uri.AbsoluteUri; // for getting absolte Uri from blobClient and saving also only pick just uploaded uri only every time bec just i uploaded so na //hamara path hamein db me save karna hai jo ki hamesha file upload karne ke baad banta hai
            await _db.Categories.AddAsync(category); // For Image Url Saving in DB Code
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created); // when we Post data to Web Server we req to use 201 Status Code
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Category category)
        {
            var categoryfromDb = await _db.Categories.FindAsync(id);
            if (categoryfromDb == null)
            {
                return NotFound();
            }
            else
            {
                categoryfromDb.Title = category.Title;
                categoryfromDb.DisplayOrder = category.DisplayOrder;
                _db.Categories.Update(categoryfromDb); // In Update case we can,t have a await typeof method
                await _db.SaveChangesAsync();
                return Ok("Category Updated");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoryfromDb = await _db.Categories.FindAsync(id);
            if (categoryfromDb == null)
            {
                return NotFound();
            }
            else
            {
                _db.Categories.Remove(categoryfromDb);
                await _db.SaveChangesAsync();
                return Ok("Category Deleted");
            }
        }
    }
}

//In our List save only temprary data so we restart our project then our List data is Lost/Unvisible
//Status Code msg is Provided by Asp.Net Core
//Api Can Say that server bec from this we consume data?
//most of/more to more <- replace this by search