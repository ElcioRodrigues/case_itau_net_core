using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaseItau.API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CaseItau.Service;
using static System.Reflection.Metadata.BlobBuilder;
using AutoMapper;
using CaseItau.Domain;
using CaseItau.Domain.Common;

namespace CaseItau.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundoController : ControllerBase
    {
        private readonly IFundoService _fundoService;
        private readonly ITipoFundoService _tipoFundoService;
        private readonly IMapper _mapper;

        public FundoController(
            IFundoService fundoService,
            IMapper mapper,
            ITipoFundoService tipoFundoService)
        {
            _fundoService = fundoService;
            _mapper = mapper;
            _tipoFundoService = tipoFundoService;
        }

        // GET: api/Fundo/GetAll
        [HttpGet("GetAll", Name = "GetAll")]
        public async Task<IEnumerable<FundoVM>> GetAllAsync()
        {
            var fundos = await _fundoService.GetAllAsync();
            var responseVW = _mapper.Map<IEnumerable<FundoVM>>(fundos);
            return responseVW;
        }

        // GET: api/Fundo/ITAUTESTE01
        [HttpGet("{codigo}", Name = "Get")]
        public async Task<IActionResult> GetAsync(string codigo)
        {
            var fundo = await _fundoService.FindAsync(x => x.Codigo == codigo);
            if (fundo == null)
            {
                return NotFound();
            }
            var responseVW = _mapper.Map<FundoVM>(fundo);
            return Ok(responseVW);
        }

        // GET: api/Fundo/GetAllTiposFundo
        [HttpGet("GetAllTiposFundo", Name = "GetAllTiposFundo")]
        public async Task<IEnumerable<TipoFundoVM>> GetAllTiposFundoAsync()
        {
            var tipos = await _tipoFundoService.GetAsync(x => x.Codigo);
            var responseVW = _mapper.Map<IEnumerable<TipoFundoVM>>(tipos);
            return responseVW;
        }

        // POST: api/Fundo
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FundoVM value)
        {
            var fundo = _mapper.Map<Fundo>(value);
            var result = await _fundoService.InsertAsync(fundo);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // PUT: api/Fundo/ITAUTESTE01
        [HttpPut("{codigo}")]
        public async Task<IActionResult> Put(string codigo, [FromBody] FundoVM value)
        {
            value.Codigo = codigo;
            var fundo = _mapper.Map<Fundo>(value);
            var result = await _fundoService.UpdateAsync(fundo);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // DELETE: api/Fundo/ITAUTESTE01
        [HttpDelete("{codigo}")]
        public async Task<IActionResult> Delete(string codigo)
        {
            var result = await _fundoService.DeleteAsync(codigo);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // PUT: api/Fundo/ITAUTESTE01/patrimonio
        [HttpPut("{codigo}/patrimonio")]
        public async Task<IActionResult> MovimentarPatrimonio(string codigo, [FromBody] decimal value)
        {
            var result = await _fundoService.MoveAssetsAsync(codigo, value);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
