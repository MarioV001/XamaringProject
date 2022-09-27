using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XamaringProject.Models;

namespace XamaringProject.Services
{
    public class DataStore : IDataStore<Item>
    {
        readonly string HostURL = @"http://192.168.0.5:5477/api/Customers/";

        public async Task<bool> AddItemAsync(Item item)
        {
            //items.Add(item);
            string jSonConvert = JsonConvert.SerializeObject(item);
            StringContent BodyContent = new StringContent(jSonConvert, Encoding.UTF8, "application/json");
            
            //The client 
            HttpClient ClientSend = new HttpClient();
            ClientSend.BaseAddress = new Uri(HostURL + "AddCustomer");//Host/API URL
            HttpResponseMessage Response = await ClientSend.PostAsync("", BodyContent);
            if (Response.IsSuccessStatusCode) return await Task.FromResult(true);//if success Return TRUE
            return await Task.FromResult(false);

        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            string jSonConvert = JsonConvert.SerializeObject(item);
            StringContent BodyContent = new StringContent(jSonConvert, Encoding.UTF8, "application/json");

            //The client 
            HttpClient ClientSend = new HttpClient();
            ClientSend.BaseAddress = new Uri(HostURL + "UpdateCustomer");//Host/API URL
            HttpResponseMessage Response = await ClientSend.PutAsync("", BodyContent);
            if (Response.IsSuccessStatusCode) return await Task.FromResult(true);//if success Return TRUE
            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            HttpClient ClientSend = new HttpClient();
            ClientSend.BaseAddress = new Uri(HostURL + "DeleteCustomer/"+id);//Host/API URL
            HttpResponseMessage Response = await ClientSend.DeleteAsync("");
            if (Response.IsSuccessStatusCode) return await Task.FromResult(true);//if success Return TRUE
            return await Task.FromResult(false);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            var sItem = new Item();
            HttpClient ClientSend = new HttpClient();
            ClientSend.BaseAddress = new Uri(HostURL + "GetCustomer/" +id);//Host/API URL
            HttpResponseMessage Response = await ClientSend.GetAsync("");
            if (Response.IsSuccessStatusCode)
            {
                string content = Response.Content.ReadAsStringAsync().Result;
                sItem = JsonConvert.DeserializeObject<Item>(content);
            }
            else return null;
            return await Task.FromResult(sItem);

        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            var sItem = new List<Item>();
            HttpClient ClientSend = new HttpClient();
            ClientSend.BaseAddress = new Uri(HostURL + "GetCustomers");//Host/API URL
            HttpResponseMessage Response = await ClientSend.GetAsync("");
            if (Response.IsSuccessStatusCode)
            {
                string content = Response.Content.ReadAsStringAsync().Result;
                sItem = JsonConvert.DeserializeObject<List<Item>>(content);
            }
            else return null;
            return await Task.FromResult(sItem);
        }
    }
}