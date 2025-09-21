namespace BudgetAppProject.UI;

using Microsoft.AspNetCore.Mvc;
using BudgetAppProject.Application.Usecase.DeleteCategory;
using BudgetAppProject.Application.Usecase.GetCategories;
using BudgetAppProject.Application.Usecase.RegisterCategory;
using BudgetAppProject.Application.Usecase.RenameCategory;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IRegisterCategoryUsecase _registerCategoryUsecase;
    private readonly IRenameCategoryUsecase _renameCategoryUsecase;
    private readonly IDeleteCategoryUsecase _deleteCategoryUsecase;
    private readonly IGetCategoriesUsecase _getCategoriesUsecase;

    public CategoriesController (
        IRegisterCategoryUsecase registerCategoryUsecase,
        IRenameCategoryUsecase renameCategoryUsecase,
        IDeleteCategoryUsecase deleteCategoryUsecase,
        IGetCategoriesUsecase getCategoriesUsecase
    )
    {
        _registerCategoryUsecase = registerCategoryUsecase;
        _renameCategoryUsecase = renameCategoryUsecase;
        _deleteCategoryUsecase = deleteCategoryUsecase;
        _getCategoriesUsecase = getCategoriesUsecase;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterCategoryRequest request)
    {
        await _registerCategoryUsecase.HandleAsync(request);

        return Created();
    }

    [HttpPut("rename")]
    public async Task<IActionResult> Rename([FromBody] RenameCategoryRequest request)
    {
        await _renameCategoryUsecase.HandleAsync(request);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        var request = new DeleteCategoryRequest() { Id = id };
        await _deleteCategoryUsecase.HandleAsync(request);

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var request = new GetCategoriesRequest() {};
        var categories = await _getCategoriesUsecase.HandleAsync(request);

        return Ok(categories);
    }
}