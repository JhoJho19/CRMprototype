using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientManagementAPI.Data;
using ClientManagementAPI.Hubs;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClientsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetClients()
    {
        var clients = await _context.Clients.ToListAsync();
        return Ok(clients);
    }

    [HttpPost]
    public async Task<IActionResult> AddClient([FromBody] Client client)
    {
        if (client == null)
        {
            return BadRequest("Client data is null.");
        }

        client.AddedAt = DateTime.Now;

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        await NotificationHub.NotifyClients();

        return Ok(client);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient(int id, [FromBody] Client client)
    {
        var existingClient = await _context.Clients.FindAsync(id);

        if (existingClient == null)
        {
            return NotFound("Client not found.");
        }

        existingClient.Name = client.Name;
        existingClient.Email = client.Email;
        existingClient.Message = client.Message;

        await _context.SaveChangesAsync();

        await NotificationHub.NotifyClients();

        return Ok(existingClient);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var client = await _context.Clients.FindAsync(id);

        if (client == null)
        {
            return NotFound("Client not found.");
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        await NotificationHub.NotifyClients();

        return Ok("Client deleted.");
    }


}
