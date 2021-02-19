using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API;
using DakarRally.Contracts;

namespace DakarRally.Api.Controllers
{
    /// <summary>
    /// Represents the base DakarRally API controller.
    /// </summary>
    [Route("api")]
    public class DakarRallyController : ControllerBase
    {
        /// <summary>
        /// Creates an <see cref="OkObjectResult"/> that produces a <see cref="StatusCodes.Status200OK"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The created <see cref="OkObjectResult"/> for the response.</returns>
        protected new IActionResult Ok(object value)
        { 
           return base.Ok(value); 
        }

        /// <summary>
        /// Creates an <see cref="BadRequestObjectResult"/> that produces a <see cref="StatusCodes.Status400BadRequest"/>.
        /// response based on the specified <see cref="Result"/>.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>The created <see cref="BadRequestObjectResult"/> for the response.</returns>
        protected IActionResult BadRequest(List<string> errorsList)
        {
            return BadRequest(new DakarRallyApplicationError(errorsList));
        }

        /// <summary>
        /// Creates an <see cref="NotFoundResult"/> that produces a <see cref="StatusCodes.Status404NotFound"/>.
        /// </summary>
        /// <returns>The created <see cref="NotFoundResult"/> for the response.</returns>
        protected new IActionResult NotFound()
        {
            return NotFound("The requested resource was not found."); 
        }

        protected IActionResult HandleResult(Result result)
        {
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorsList);
        }

        protected IActionResult HandleObjectResult<TValue>(Result<TValue> result)       
        {
            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }
    }
}
