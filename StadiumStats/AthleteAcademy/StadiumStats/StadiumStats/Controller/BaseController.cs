using StadiumStats.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StadiumStats.Controller
{
    public class BaseController<T>
    {
        private RestAPICRUDService<T> RestAPICRUDService;
        public BaseController()
        {
            RestAPICRUDService = new RestAPICRUDService<T>();
        }

        public async Task<bool> create(T modelObject)
        {
            return await RestAPICRUDService.createModelAsync(modelObject);
        }

        public async Task<List<T>> getAll()
        {
            return await RestAPICRUDService.getAllModelAsync();
        }

        public async Task<T> get(int searchId)
        {
            return await RestAPICRUDService.getModelAsync(searchId);
        }

        public async Task<bool> delete(int searchId)
        {
            return await RestAPICRUDService.deleteModelAsync(searchId);
        }

        public async Task<bool> deleteAll()
        {
            return await RestAPICRUDService.deleteAllModelAsync();
        }

        public async Task<bool> update(T modelObject)
        {
            return await RestAPICRUDService.updateModelAsync(modelObject);
        }

        
    }
}
