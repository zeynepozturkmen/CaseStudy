using Microsoft.AspNetCore.Mvc;
using User.Application.DTO;
using User.Core.Interfaces;

namespace User.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IRepository<Core.Entities.User> _repository;

    public UsersController(IRepository<Core.Entities.User> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _repository.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserDto userDto)
    {
        var user = new Core.Entities.User()
        {
            Name = userDto.Name
        };

        await _repository.AddAsync(user);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Core.Entities.User user)
    {
        var existingUser = await _repository.GetByIdAsync(id);
        if (existingUser == null) return NotFound();

        existingUser.Name = user.Name;
        _repository.Update(existingUser);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null) return NotFound();

        _repository.Delete(user);
        return NoContent();
    }
}