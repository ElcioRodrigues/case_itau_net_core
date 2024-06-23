using CaseItau.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;

namespace CaseItau.Web.Repository
{
    public class ServiceRepository
    {
        public HttpClient Client { get; set; }

        private readonly IConfiguration _config;

        public ServiceRepository(IConfiguration config)
        {
            Client = new HttpClient();
            _config = config;
            Client.BaseAddress = new Uri(_config.GetValue<string>("ServiceUrl") ?? string.Empty);
        }

        public List<FundoVM> GetFundos()
        {
            List<FundoVM> fundos = [];
            var response = Client.GetAsync("fundo/GetAll").Result;
            if (response.IsSuccessStatusCode)
            {
                fundos = response.Content.ReadFromJsonAsync<List<FundoVM>>().Result ?? [];
            }
            return fundos;
        }

        public FundoVM? GetFundo(string code)
        {
            var response = Client.GetAsync($"fundo/{code}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<FundoVM>().Result;
            }
            return null;
        }

        public List<TipoFundoVM> GetTiposFundoInvestimento()
        {
            List<TipoFundoVM> tiposFundoInvestimento = [];
            var response = Client.GetAsync("fundo/GetAllTiposFundo").Result;
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                tiposFundoInvestimento = response.Content.ReadFromJsonAsync<List<TipoFundoVM>>().Result ?? [];
            }
            return tiposFundoInvestimento;
        }

        public Result CreateFundo(FundoVM fundo)
        {
            Result? result = new();
            var response = Client.PostAsJsonAsync("fundo", fundo).Result;
            return HandleReturnAPI(out result, response);
        }

        private Result HandleReturnAPI(out Result? result, HttpResponseMessage response)
        {
            result = JsonConvert.DeserializeObject<Result>(response.Content.ReadAsStringAsync().Result);
            if (result == null && !response.IsSuccessStatusCode)
                throw new ApplicationException("Erro ao realizar a chamada.");
            if (result != null && !result.Success)
            {
                throw new ApplicationException(string.Join(" ", result.Messages));
            }
            return result ?? new Result();
        }

        public Result EditFundo(FundoVM fundo)
        {
            Result? result = new();
            var response = Client.PutAsJsonAsync($"fundo/{fundo.Codigo}", fundo).Result;
            return HandleReturnAPI(out result, response);
        }

        public Result EditPatrimonioFundo(string code, decimal value)
        {
            Result? result = new();
            var response = Client.PutAsJsonAsync($"fundo/{code}/patrimonio", value).Result;
            return HandleReturnAPI(out result, response);
        }

        public Result DeleteFundo(string code)
        {
            Result? result = new();
            var response = Client.DeleteAsync($"fundo/{code}").Result;
            return HandleReturnAPI(out result, response);
        }
    }
}
