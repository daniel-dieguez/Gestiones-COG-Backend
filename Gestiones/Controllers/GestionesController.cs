using System.Security.Cryptography;
using Gestiones.context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestiones.Controllers;

[ApiController]
[Route("[controller]")]
public class GestionesController : Controller
{

    private readonly ApplicationDbContext _context;
    private readonly ILogger<GestionesController> _logger;

    public GestionesController(ApplicationDbContext context, ILogger<GestionesController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet("/gestiones/viewAll")]
    public async Task<ActionResult<IEnumerable<models.Gestiones>>> viewAll()
    {
        var response = new Dictionary<String, Object>();
        try
        {
            _logger.LogInformation("Se a realizado una consulta en gestiones view");
            response.Add("mensaje", "consulta de gestiones");
            var gestionesView = await _context.Gestiones.ToListAsync();

            return Ok(gestionesView);

        }
        catch (Exception e)
        {

            _logger.LogInformation(e,$"error en la peticion..{e.Message}");
            return StatusCode(500, "prueba nuevamente ");
        }
    }
    
    [HttpPost("/gestiones/newSolicitud")]
    public async Task<IActionResult> newGestion(models.Gestiones gestiones)
    {
        var response = new Dictionary<string, object>();
        try
        {
            gestiones.solicitud = (int)GenerateRandomInt();
            await _context.Gestiones.AddAsync(gestiones);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation($"Se a creado una nueva solicitud..{gestiones.tipo_solicitud}");
            response.Add("mensaje", $"se a creado una nueva solicitud...{gestiones.tipo_solicitud}");
            return CreatedAtAction(nameof(viewAll), new { id = gestiones.solicitud });
        }
        catch (Exception e)
        {
        
            _logger.LogInformation(e, $"error al crear la nueva gestion...{e.Message}");
            return StatusCode(500);
        }
    }
    
    //generador de id de solicitud post
    private int GenerateRandomInt()
    {
        byte[] buffer = new byte[4]; // 4 bytes para int
        RandomNumberGenerator.Fill(buffer);
        return BitConverter.ToInt32(buffer, 0) & int.MaxValue; // solo positivo
       // return RandomNumberGenerator.GetInt32(100000, 1000000); // Genera un número entre 100000 y 999999

    }
    
    [HttpPut("/gestiones/updateGestion/{solicitudes}")]
    public async Task<ActionResult<Dictionary<string, object>>> updateProducto(long solicitudes,
        [FromBody] models.Gestiones gestiones)
    {
        var response = new Dictionary<string, object>();
        try
        {
            var newGestion = await _context.Gestiones.FindAsync(solicitudes);

            if (newGestion == null)
            {
                return NotFound();
            }

            //elemento de tabla a cambiar
            newGestion.tipo_solicitud = gestiones.tipo_solicitud;

            await _context.SaveChangesAsync();
            response.Add("mensaje", $"gestion actualizada...a...{newGestion.tipo_solicitud}");
            return Ok(response);
        }
        catch (Exception e)
        {

            _logger.LogInformation(e,$"No se a podido realizar la actualizacion de forma correcta...{e.Message}");
            return StatusCode(500);

        }
    }

    [HttpDelete("/gestion/delete/{solicitudes}")]
    public async Task<ActionResult<Dictionary<string, object>>> deleteGestion(long solicitudes)
    {
        var response = new Dictionary<string, object>();

        var gestionDelete = await _context.Gestiones.FindAsync(solicitudes);
        if (gestionDelete == null)
        {
            return NotFound("Gestion no encontrada");
        }

        _context.Gestiones.Remove(gestionDelete);
        try
        {
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Se elimina la gestion {gestionDelete.solicitud}");
            response.Add("mensaje", $"se a eliminado el gestion...{gestionDelete.tipo_solicitud}");
            return Ok(response);

        }
        catch (Exception e)
        {

            _logger.LogInformation(e, $"error la eliminacion ... {e.Message}");
            return StatusCode(500);
        }

    }
    
    //filtro de busqueda por palabras
    [HttpGet("/gestiones/{palabraClave}")]
    public async Task<IActionResult> filtroPalabras(string palabraClave)
    {
        try
        {

            if (string.IsNullOrEmpty(palabraClave))
            {
                return BadRequest("Debe proporcionar una palabra clave para filtrar.");
            }

            // filtro para gestiones que contienen la palabra clave
            var filtroGestiones = await _context.Gestiones
                .Where(g => g.tipo_solicitud.Contains(palabraClave))
                .ToListAsync();

            if (!filtroGestiones.Any())
            {
                return NotFound($"No se encontraron gestiones que contengan la palabra clave...{palabraClave}");
            }

            return Ok(filtroGestiones);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al filtrar las gestiones.");
            return StatusCode(500, $"Ocurrió un error al procesar la solicitud...{e.Message}");
        }
    }




}