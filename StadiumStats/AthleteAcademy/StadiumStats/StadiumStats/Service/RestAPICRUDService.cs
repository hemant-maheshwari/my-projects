using Newtonsoft.Json;
using StadiumStats.DataStructure;
using StadiumStats.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StadiumStats.Service
{
    public class RestAPICRUDService<T>: WebAPIConfiguration
    {
        private static string ACTION_CREATE = "create";
        private static string ACTION_DELETE = "delete";
        private static string ACTION_DELETE_ALL = "deleteAll";
        private static string ACTION_GET = "get";
        private static string ACTION_GET_ALL = "getAll";
        private static string ACTION_UPDATE = "update";
        private static string FORWARD_SLASH = "/";

        private static string CONTROLLER_NAME;

        public RestAPICRUDService() {
            CONTROLLER_NAME = getWebAPIControllerName();
        }

        public async Task<bool> createModelAsync(T modelObject)
        {
            return await saveModelAsync(modelObject, ACTION_CREATE);
        }

        public async Task<bool> deleteModelAsync(int searchId)
        {
            string url = getURLWithId(searchId, ACTION_DELETE);
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    Response responseObject = await getHTTPResponse(response);
                    return responseObject.status;
                }
                else
                {
                    Debug.WriteLine("Error Occured!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> deleteAllModelAsync()
        {
            string url = getURL(ACTION_DELETE_ALL);
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    Response responseObject = await getHTTPResponse(response);
                    return true;
                }
                else
                {
                    Debug.WriteLine("Error Occured!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<T>> getAllModelAsync()
        {
            string url = getURL(ACTION_GET_ALL);
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    Response responseObject = await getHTTPResponse(response);
                    return getListModelFromResponse(responseObject);
                }
                else
                {
                    Debug.WriteLine("Error Occured!");
                    return default(List<T>);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return default(List<T>);
            }
        }

        public async Task<T> getModelAsync(int searchId)
        {
            string url = getURLWithId(searchId, ACTION_GET);
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    Response responseObject = await getHTTPResponse(response);
                    return getModelFromResponse(responseObject);
                }
                else
                {
                    Debug.WriteLine("Error Occured!");
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return default(T);
            }
        }

        public async Task<bool> updateModelAsync(T modelObject)
        {
            return await saveModelAsync(modelObject, ACTION_UPDATE);
        }

        private async Task<bool> saveModelAsync(T modelObject, string methodName)
        {
            string url = getURL(methodName);
            try
            {
                var json = JsonConvert.SerializeObject(modelObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    Response responseObject = await getHTTPResponse(response);
                    return responseObject.status;
                }
                else
                {
                    Debug.WriteLine("Error Occured!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        private async Task<Response> getHTTPResponse(HttpResponseMessage response)
        {
            string result = await response.Content.ReadAsStringAsync();
            Response responseObject = JsonConvert.DeserializeObject<Response>(result);
            return responseObject;
        }

        private T getModelFromResponse(Response response)
        {
            string userString = response.data;
            T returnedModel = JsonConvert.DeserializeObject<T>(userString);
            return returnedModel;
        }

        private List<T> getListModelFromResponse(Response response)
        {
            string listString = response.data;
            T[] returnedModel = JsonConvert.DeserializeObject<T[]>(listString);
            List<T> modelList = returnedModel.ToList<T>();
            return modelList;
        }

        private string getWebAPIControllerName()
        {
            string[] fullNameArray = typeof(T).ToString().ToLower().Split('.');
            string controllerName = fullNameArray[fullNameArray.Length - 1];
            return controllerName;
        }

        private String getURLWithId(int searchId, string action) {
            return WEB_API_BASE_URL + CONTROLLER_NAME + FORWARD_SLASH + action + FORWARD_SLASH + searchId;
        }

        private String getURL(string methodName) {
            return WEB_API_BASE_URL + CONTROLLER_NAME + FORWARD_SLASH + methodName;
        }
    }
}
