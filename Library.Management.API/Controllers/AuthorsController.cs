using Library.Management.Application.DTOs;
using Library.Management.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Management.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService service)
    {
        _authorService = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _authorService.GetAllAsync();

        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _authorService.GetByIdAsync(id);

        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(AuthorDto authorDto)
    {
        var result = await _authorService.AddAsync(authorDto);

        return Ok(new {Success = false, result = "Successfully created the author!"});
    }
    
    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, AuthorDto authorDto)
    {
        await _authorService.UpdateAsync(id,authorDto);

        return Ok(new {Success = false, result = "Successfully updated the author!"});
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _authorService.DeleteAsync(id);

        return Ok(new {Success = false, result = "Successfully deleted the author!"});
    }
}