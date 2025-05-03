using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using uniitTesting.Data;
using uniitTesting.Model;

namespace uniitTesting.Services
{
    public class TaskService : ITaskServices
    {
        private readonly AppDbContext _appDbContext;
        public TaskService (AppDbContext context)
        {
            _appDbContext = context;
        }

        public async Task<bool> Create(TaskItems task)
        {
            try
            {

            await _appDbContext.AddAsync (task);
           await _appDbContext.SaveChangesAsync();
                return true;
            }  catch (Exception ex)
            {
                Console.WriteLine($"Error : ", ex.Message);
                return false;
            } 
            
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var Delted = await _appDbContext.Tasks.FindAsync(id);
                if(Delted != null)
                {
                    _appDbContext.Tasks.Remove(Delted);
                    await _appDbContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false; 
                }
            }catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<List<TaskItems>> GetTaskItems()
        {
            try
            {
                var data = await _appDbContext.Tasks.ToListAsync();
                return data;
            }catch (Exception ex)
            {
                throw new ArgumentException("dont find anything", ex);
            }
        }

        public async Task<bool> Update(TaskItems task)
        {
            try
            {
                var existingTask = await _appDbContext.Tasks.FindAsync(task.Id);
                 if( existingTask != null)
                {
                    existingTask.Title = task.Title;
                    existingTask.IsCompleted = task.IsCompleted;
                    await _appDbContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                } 
               
            }catch (Exception ex)
            {
                throw new ArgumentException("error occur durign adding", ex.Message);
            }
        }
    }
}
