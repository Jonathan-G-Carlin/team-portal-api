using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TeamPortal.Api.Dtos;
using TeamPortal.Api.Services;

namespace TeamPortal.Api.Controller;

[ApiController]
[Route("api/[Controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        var result = await _userService.CreateUserAsync(userDto);
        return CreatedAtAction(nameof(CreateUser), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var result = await _userService.GetUserByIdAsync(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers() 
    {
        var result = await _userService.GetUsersAsync();
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserAsync(int id, [FromBody]UpdateUserDto updateDto) 
    {
        var result = await _userService.UpdateUserAsync(id, updateDto);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> UpdatePasswordAsync(int id, UpdatePasswordDto updateDto)
    {
        var result = await _userService.UpdatePasswordAsync(id, updateDto);

        if (!result)
            return BadRequest("User not found or Password is incorrent");

        return NoContent();
    }
}