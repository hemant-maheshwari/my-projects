using Newtonsoft.Json;
using PocketCloset.Models;
using PocketCloset.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PocketCloset.Service
{
    public class RestAPICRUDService<T>: WebAPIConfiguration
    {
        private static string ACTION_CREATE = "create";
        private static string ACTION_DELETE = "delete";
        private static string ACTION_GET = "get";
        private static string ACTION_GET_ALL = "getAll";
        private static string ACTION_UPDATE = "update";
        private static string FORWARD_SLASH = "/";

        public async Task<bool> createModelAsync(T modelObject)
        {
            return await saveModelAsync(modelObject, ACTION_CREATE);
        }

        public async Task<bool> deleteModelAsync(int searchId)                  //deletes given object from database
        {
            string url = WEB_API_BASE_URL + getWebAPIControllerName() + FORWARD_SLASH + ACTION_DELETE + FORWARD_SLASH + searchId;
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

        public async Task<List<T>> getAllModelAsync(int searchId)             //gets a list of all given object from web api
        {
            string url = WEB_API_BASE_URL + getWebAPIControllerName() + FORWARD_SLASH + ACTION_GET_ALL + FORWARD_SLASH + searchId;
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

        public async Task<T> getModelAsync(int searchId)                        //gets given object from web api
        {
            string url = WEB_API_BASE_URL + getWebAPIControllerName() + FORWARD_SLASH + ACTION_GET + FORWARD_SLASH + searchId;
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

        public async Task<bool> updateModelAsync(T modelObject)                    //sends givenobject to eb api to be updated
        {
            return await saveModelAsync(modelObject, ACTION_UPDATE);
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

        private List<T> getListModelFromResponse(Response response)     //gets list of given object from database
        {
            string userString = response.data;
            T[] returnedModel = JsonConvert.DeserializeObject<T[]>(userString);
            List<T> modelList = returnedModel.ToList<T>();
            return modelList;
        }

        private string getWebAPIControllerName()                                //gets web api controller name
        {
            string[] fullNameArray = typeof(T).ToString().ToLower().Split('.');
            string tmpControllerName = fullNameArray[fullNameArray.Length - 1];
            if (tmpControllerName.Equals("secquestion"))
            {
                fullNameArray[fullNameArray.Length - 1] = "secQuestion";            //CONVERT TO CONSTANTS
            }
            else if (tmpControllerName.Equals("profilepicture"))
            {
                fullNameArray[fullNameArray.Length - 1] = "profilePicture";
            }
            else if (tmpControllerName.Equals("postrecord"))
            {
                fullNameArray[fullNameArray.Length - 1] = "postRecord";
            }
            string controllerName = fullNameArray[fullNameArray.Length - 1];
            return controllerName;
        }

        private async Task<bool> saveModelAsync(T modelObject, string methodName)         //saves given object to database
        {
            string url = WEB_API_BASE_URL + getWebAPIControllerName() + FORWARD_SLASH + methodName;
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


    }
}
