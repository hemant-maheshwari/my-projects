using PocketCloset.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace PocketCloset.Controller
{
    public class BaseController<T> 
    {
        protected RestAPICRUDService<T> RestAPICRUDService;

        public BaseController()
        {
            RestAPICRUDService = new RestAPICRUDService<T>();
        }

        public async Task<bool> createModel(T modelObject)
        {
            return await RestAPICRUDService.createModelAsync(modelObject);
        }

        public async Task<bool> deleteModel(int searchId)
        {
            return await RestAPICRUDService.deleteModelAsync(searchId);
        }

        public async Task<List<T>> getAllModel(int searchId)
        {
            return await RestAPICRUDService.getAllModelAsync(searchId);
        }


        public async Task<T> getModel(int searchId)
        {
            return await RestAPICRUDService.getModelAsync(searchId);
        }

        public async Task<bool> updateModel(T modelObject)
        {
            return await RestAPICRUDService.updateModelAsync(modelObject);
        }
        

    }
}
