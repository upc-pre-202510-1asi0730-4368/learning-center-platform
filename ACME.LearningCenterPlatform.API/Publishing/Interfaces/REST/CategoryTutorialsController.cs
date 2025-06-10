using System.Net.Mime;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Queries;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Services;
using ACME.LearningCenterPlatform.API.Publishing.Interfaces.REST.Resources;
using ACME.LearningCenterPlatform.API.Publishing.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ACME.LearningCenterPlatform.API.Publishing.Interfaces.REST;

[ApiController]
[Route("api/v1/categories/{categoryId:int}/tutorials")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Categories")]
public class CategoryTutorialsController(
    ITutorialQueryService tutorialQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation( 
        Summary = "Get Tutorials by Category Id",
        Description = "Returns a list of tutorials associated with a specific category.",
        OperationId = "GetTutorialsByCategoryId")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of tutorials", typeof(IEnumerable<TutorialResource>))]
    public async Task<IActionResult> GetTutorialsByCategoryId(int categoryId)
    {
        var getAllTutorialsByCategoryIdQuery = new GetAllTutorialsByCategoryIdQuery(categoryId);
        var tutorials = await tutorialQueryService.Handle(getAllTutorialsByCategoryIdQuery);
        var tutorialResources = tutorials
            .Select(TutorialResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(tutorialResources);
    }
}