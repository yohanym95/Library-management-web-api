using Library.Management.Application.DTOs;
using Library.Management.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Management.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService service)
    {
        _bookService = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _bookService.GetAllAsync();

        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _bookService.GetByIdAsync(id);

        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateBookDto bookDto)
    {
        var result = await _bookService.AddAsync(bookDto);

        return Ok(new {Success = false, result = "Successfully created the book!"});
    }
    
    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, UpdateBookDto bookDto)
    {
        await _bookService.UpdateAsync(id,bookDto);

        return Ok(new {Success = false, result = "Successfully updated the book!"});
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _bookService.DeleteAsync(id);

        return Ok(new {Success = false, result = "Successfully deleted the book!"});
    }
}