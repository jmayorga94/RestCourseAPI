using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Api.Constants;
using Movies.Api.Mapping;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Application.Services;
using Movies.Contracts.Requests;

namespace Movies.Api.Controllers;

[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;
 
    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [Authorize(AuthConstants.TrustedMemberPolicyName)]
    [HttpPost(ApiEndpoints.Movies.Create)]
   public async Task<IActionResult> CreateMovie([FromBody] CreateMovieRequest request)
   {
      var movie = request.MapToMovie();

      var result = await _movieService.CreateAsync(movie);

     return CreatedAtAction(nameof(Get), new { id = movie.Id },result);
   }

   [HttpGet(ApiEndpoints.Movies.Get)]
   public async Task<IActionResult> Get([FromRoute] Guid id)
   {
    var movie = await _movieService.GetByIdAsync(id);

    if (movie is null)
    {
        return NotFound();
    }
    
    var response = movie.MapToResponse();

    return Ok(response);
   }

   [AllowAnonymous]
   [HttpGet(ApiEndpoints.Movies.GetAll)]
   public async Task<IActionResult> GetAll()
   {
     var movies = await _movieService.GetAllAsync();
     var moviesResponse = movies.MapToResponse();

     return Ok(moviesResponse);
     
   }
   
   [Authorize(AuthConstants.TrustedMemberClaimName)]
   [HttpPut(ApiEndpoints.Movies.Update)]
   public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateMovieRequest request)
   {
      var movie = request.MapToMovie(id);
      var updated = await _movieService.UpdateAsync(movie);
      
      if (updated is null)
      {
        return NotFound();
      }

      var response = updated.MapToResponse();

      return Ok(response);
   }

    [Authorize(AuthConstants.AdminUserPolicyName)]
    [HttpDelete(ApiEndpoints.Movies.Delete)]
   public async Task<IActionResult> Delete([FromRoute] Guid id)
   {
     var deleted = await _movieService.DeletebyIdAsync(id);

     if (!deleted)
     {
       return NotFound();
     }

     return Ok();
   }
}
