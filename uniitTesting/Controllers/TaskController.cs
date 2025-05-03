using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using uniitTesting.Model;
using uniitTesting.Services;

namespace uniitTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskServices service;
        public TaskController(ITaskServices task)
        {
            service = task;
        }

        [HttpGet]
        
        public async Task <IActionResult> GetAllAsync()
        {
            var ListOfItens =await service.GetTaskItems();
            if(ListOfItens.Any())
            {
                return Ok(ListOfItens);

            }
            else
            {
                return BadRequest("No Items Found ");
            }

        }

        [HttpPost]

        public async Task <IActionResult> CreateData(TaskItems task)
        {
            try
            {
                var created =await service.Create(task);
                if (created)
                {
                    return Ok("TaskCreated successfuly");
                }
                else
                {
                    return BadRequest("cant Create the task");
                }
            }catch (Exception ex)
            {
                throw new ArgumentException("ERROR :",ex.Message);
            }
        }

        [HttpPut]

        public async Task <IActionResult> UpdateData(TaskItems task)
        {
            try
            {
                var updatedData = await service.Update(task);
                if (updatedData)
                {
                    return Ok("Product Updated Sucessfully");
                }
                else
                {
                    return BadRequest("Update failed");
                }
            }catch (Exception ex)
            {
                throw new ArgumentException("ERROR :",ex.Message);
            }
        }
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteData(int id)
        {
            try
            {
                var deletedId = await service.Delete(id);
                if (deletedId)
                {
                    return Ok("Product Delted Sucessfully");
                }
                else
                {
                    return BadRequest("Can't delete the product");
                }


            }catch (Exception ex)
            {
                throw new ArgumentException("ERROR:", ex.Message);
            }
        }
    }
}
