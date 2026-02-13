using DGII.Application.DTOs;
using DGII.Application.Services;
using DGII.Domain.Enums;
using DGII.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DGII.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxpayerController : ControllerBase
    {
        private readonly TaxpayerService _service;
        private readonly ITaxpayerRepository _repository;
        private readonly ILogger<TaxpayerController> _logger;

        public TaxpayerController(
            TaxpayerService service,
    ITaxpayerRepository repository,
    ILogger<TaxpayerController> logger)
        {
            _service = service;
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> getAll()
        {
            try
            {
                _logger.LogInformation("GET /api/taxpayer - Obteniendo todos los contribuyentes");
                var taxpayers = await _repository.GetAllAsync();

                var result = taxpayers.Select(t => new
                {
                    rncCedula = t.RncCedula,
                    nombre = t.Name,
                    tipo = t.Type == TaxpayerType.PersonaFisica ? "PERSONA FISICA" : "PERSONA JURIDICA",
                    estatus = t.Status == TaxpayerStatus.Active ? "activo" : "inactivo"
                });
                _logger.LogInformation("Se obtuvieron {Count} contribuyentes", taxpayers.Count());

                return Ok(result);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error al obtener todos los contribuyentes");
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }




        [HttpGet("{document}")]
        [ProducesResponseType(typeof(TaxpayerReportDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReport(string document)
        {
            var result = await _service.GetTaxpayerReportAsync(document);
            if (result == null)
            {
                _logger.LogWarning("Contribuyente {Document} no encontrado", document);
                return NotFound(new { message = $"El contribuyente con RNC/Cédula {document} no existe." });
            }
            _logger.LogInformation("Reporte generado. Total ITBIS: ${Total:F2}", result.TotalItbis);
            return Ok(result);
        }

    }
}
