﻿using AIS.FrontEnd_07062021.Service.Interface;
using AIS.Model;
using AIS.Model.Academic;
using AIS.Model.MasterData;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;


namespace AIS.FrontEnd_07062021.Service.Implementation
{
    public class MasterChapterService : IMasterChapterService
    {
        private readonly HttpClient httpClient;

        public MasterChapterService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<MasterChapterListModel>> GetMasterChapterList(int SchoolId, int MasterChapterId, int Status, string OperationType)
        {
            //MasterChapterList[] MasterChapterList;
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            var r = await httpClient.GetJsonAsync<Response>($"/api/MasterChapter/GetMasterChapterList?SchoolId={SchoolId}&MasterChapterId={MasterChapterId}&Status={Status}&OperationType={OperationType}");
            JsonElement el;
            var b = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(r.data.GetRawText()).TryGetProperty("data", out el);

            if (r.isError == false)
            {
                return System.Text.Json.JsonSerializer.Deserialize<MasterChapterListModel[]>(el.GetRawText());
            }
            else
            {
                return null;
            }

        }

        public async Task<APIReturnModel> AddUpdateMasterChapter(MasterChapterModel MasterChapterModel)
        {

            APIReturnModel response = await httpClient.PostJsonAsync<APIReturnModel>("/api/MasterChapter/AddUpdateMasterChapter", MasterChapterModel);
            return response;

        }
    }
}
