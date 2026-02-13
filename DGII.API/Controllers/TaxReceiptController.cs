using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using DGII.Application.Services;

namespace DGII.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxReceiptController : ControllerBase
    {
        private readonly TaxReceiptService _service;
        private readonly ILogger<TaxReceiptController> _logger;

        public TaxReceiptController
            (
            TaxReceiptService service,
            ILogger<TaxReceiptController> logger
            )
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _logger.LogInformation("GET /api/taxreceipt - Obteniendo todos los comprobantes");

                var receipts = await _service.GetAllAsync();

                _logger.LogInformation("Se obtuvieron {Count} comprobantes", receipts.Count());
                return Ok(receipts);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error al obtener comprobantes");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpGet("taxpayer/{rncCedula}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByTaxpayer(string rncCedula)
        {
            try
            {
                _logger.LogInformation("GET /api/taxreceipt/taxpayer/{RncCedula}", rncCedula);

                var receipts = await _service.GetByTaxpayerAsync(rncCedula);

                return Ok(receipts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener comprobantes del contribuyente {RncCedula}", rncCedula);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateTaxReceiptRequest request)
        {
            var result = await _service.CreateAsync(request.RncCedula, request.NCF, request.Amount);

            return CreatedAtAction(
                nameof(GetByTaxpayer),
                new { rncCedula = result.RncCedula },
                result);
        }

        // DTO de entrada para el POST
        public record CreateTaxReceiptRequest(string RncCedula, string NCF, decimal Amount);
    }
}
