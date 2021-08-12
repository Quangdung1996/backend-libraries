using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PansyDev.Common.Application.Extensions
{
    public static class TaskEnumerableExtensions
    {
        public static async Task<T[]> ToArrayAsync<T>(this IEnumerable<Task<T>> tasks)
        {
            var taskList = tasks.ToArray();

            await Task.WhenAll(taskList);
            return taskList.Select(x => x.Result).ToArray();
        }
    }
}