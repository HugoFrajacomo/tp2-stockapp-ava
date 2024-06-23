using System.Threading.Tasks;

namespace StockApp.Application.Interfaces
{
    public interface IJustInTimeInventoryService
    {
        Task OptimizeInventoryAsync();
    }
}
