using uniitTesting.Model;

namespace uniitTesting.Services
{
    public interface ITaskServices
    {
        public Task<List<TaskItems>> GetTaskItems();
        public Task<bool> Create(TaskItems task);
        public Task<bool> Update(TaskItems task);
        public Task<bool> Delete(int id);
    }
}
