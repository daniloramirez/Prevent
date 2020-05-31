﻿using Newtonsoft.Json;
using Prevent.Common.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Prevent.Common.Services
{
    public class ApiService : IApiService
    {
        public async Task<Response> GetPreventAsync(string preventType, string urlBase, string servicePrefix, string controller)
        {
            try
            {
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };

                string url = $"{servicePrefix}{controller}/{preventType}";
                HttpResponseMessage response = await client.GetAsync(url);
                string result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                PreventTypeResponse model = JsonConvert.DeserializeObject<PreventTypeResponse>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = model
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}