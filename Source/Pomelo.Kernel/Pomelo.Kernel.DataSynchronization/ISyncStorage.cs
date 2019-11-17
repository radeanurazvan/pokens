using System;
using System.Threading.Tasks;

namespace Pomelo.Kernel.DataSynchronization
{
    public interface ISyncStorage
    {
        Task Create<T>(T entity) where T : SynchronizationModel, new();
        Task Update<T>(string id, Action<T> update) where T : SynchronizationModel, new();
        Task Delete<T>(string id) where T : SynchronizationModel, new();
    }
}