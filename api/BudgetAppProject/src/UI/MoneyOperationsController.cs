namespace BudgetAppProject.UI;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BudgetAppProject.Application.Usecase.DeleteMoneyOperation;
using BudgetAppProject.Application.Usecase.EditMoneyOperation;
using BudgetAppProject.Application.Usecase.GetIncomeAndExpenses;
using BudgetAppProject.Application.Usecase.GetMoneyOperations;
using BudgetAppProject.Application.Usecase.RegisterMoneyOperation;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MoneyOperationsController : ControllerBase
{
    private readonly IRegisterMoneyOperationUsecase _registerMoneyOperationUsecase;
    private readonly IEditMoneyOperationUsecase _editMoneyOperationUsecase;
    private readonly IDeleteMoneyOperationUsecase _deleteMoneyOperationUsecase;
    private readonly IGetMoneyOperationsUsecase _getMoneyOperationsUsecase;
    private readonly IGetIncomeAndExpensesUsecase _getIncomeAndExpensesUsecase;

    public MoneyOperationsController (
        IRegisterMoneyOperationUsecase registerMoneyOperationUsecase,
        IEditMoneyOperationUsecase editMoneyOperationUsecase,
        IDeleteMoneyOperationUsecase deleteMoneyOperationUsecase,
        IGetMoneyOperationsUsecase getMoneyOperationsUsecase,
        IGetIncomeAndExpensesUsecase getIncomeAndExpensesUsecase
    )
    {
        _registerMoneyOperationUsecase = registerMoneyOperationUsecase;
        _editMoneyOperationUsecase = editMoneyOperationUsecase;
        _deleteMoneyOperationUsecase = deleteMoneyOperationUsecase;
        _getMoneyOperationsUsecase = getMoneyOperationsUsecase;
        _getIncomeAndExpensesUsecase = getIncomeAndExpensesUsecase;
    }

    [HttpPost("new")]
    public async Task<IActionResult> Register([FromBody] RegisterMoneyOperationRequest request)
    {
        await _registerMoneyOperationUsecase.HandleAsync(request);

        return Created();
    }

    [HttpPut("edit")]
    public async Task<IActionResult> Edit([FromBody] EditMoneyOperationRequest request)
    {
        await _editMoneyOperationUsecase.HandleAsync(request);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        var request = new DeleteMoneyOperationRequest() { Id = id };
        await _deleteMoneyOperationUsecase.HandleAsync(request);

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetMoneyOperationsRequest request)
    {
        var response = await _getMoneyOperationsUsecase.HandleAsync(request);
        return Ok(response);
    }

    [HttpGet("income-and-expenses")]
    public async Task<IActionResult> GetIncomeAndExpenses()
    {
        var request = new GetIncomeAndExpensesRequest() {};
        var response = await _getIncomeAndExpensesUsecase.HandleAsync(request);

        return Ok(response);
    }
}
