using System.Net.Mime;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Queries;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Services;
using ACME.LearningCenterPlatform.API.Publishing.Interfaces.REST.Resources;
using ACME.LearningCenterPlatform.API.Publishing.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ACME.LearningCenterPlatform.API.Publishing.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Tutorial Endpoints")]
public class TutorialsController(
    ITutorialCommandService tutorialCommandService,
    ITutorialQueryService tutorialQueryService
    ) : ControllerBase
{
    [HttpGet("{tutorialId:int}")]
    [SwaggerOperation( 
        Summary = "Get Tutorial by Id",
        Description = "Returns a tutorial by its unique identifier.",
        OperationId = "GetTutorialById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Tutorial found", typeof(TutorialResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Tutorial not found")]
    public async Task<IActionResult> GetTutorialById([FromRoute] int tutorialId)
    {
        var tutorial = await tutorialQueryService.Handle(new GetTutorialByIdQuery(tutorialId));
        if (tutorial is null) return NotFound();
        var resource = TutorialResourceFromEntityAssembler.ToResourceFromEntity(tutorial);
        return Ok(resource);
    }
    
    [HttpGet]
    [SwaggerOperation( 
        Summary = "Get All Tutorials",
        Description = "Returns a list of all available tutorials.",
        OperationId = "GetAllTutorials")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of tutorials", typeof(IEnumerable<TutorialResource>))]
    public async Task<IActionResult> GetAllTutorials()
    {
        var tutorials = await tutorialQueryService.Handle(new GetAllTutorialsQuery());
        var tutorialResources = tutorials
            .Select(TutorialResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(tutorialResources);
    }
    
    [HttpPost]
    [SwaggerOperation( 
        Summary = "Create a New Tutorial",
        Description = "Creates a new tutorial and returns the created tutorial resource.",
        OperationId = "CreateTutorial")]
    [SwaggerResponse(StatusCodes.Status201Created, "Tutorial created successfully", typeof(TutorialResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Tutorial could not be created")]
    public async Task<IActionResult> CreateTutorial([FromBody] CreateTutorialResource resource)
    {
        var createTutorialCommand = CreateTutorialCommandFromResourceAssembler.ToCommandFromResource(resource);
        var tutorial = await tutorialCommandService.Handle(createTutorialCommand);
        if (tutorial is null) return BadRequest("Tutorial could not be created.");
        var createdResource = TutorialResourceFromEntityAssembler.ToResourceFromEntity(tutorial);
        return CreatedAtAction(nameof(GetTutorialById), new { tutorialId = createdResource.Id }, createdResource);
    }

    [HttpPost("{tutorialId:int}/videos")]
    public async Task<IActionResult> AddVideoToTutorial([FromBody] AddVideoAssetToTutorialResource resource,
        [FromRoute] int tutorialId)
    {
        var addVideoAssetToTutorialCommand =
            AddVideoAssetToTutorialCommandFromResourceAssembler.ToCommandFromResource(resource, tutorialId);
        var tutorial = await tutorialCommandService.Handle(addVideoAssetToTutorialCommand);
        if (tutorial is null) return BadRequest("Video could not be added to the tutorial.");
        var tutorialResource = TutorialResourceFromEntityAssembler.ToResourceFromEntity(tutorial);
        return CreatedAtAction(nameof(GetTutorialById), new { tutorialId = tutorialResource.Id }, tutorialResource);
    }
}