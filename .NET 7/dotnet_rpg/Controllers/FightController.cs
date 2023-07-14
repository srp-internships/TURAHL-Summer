using dotnet_rpg.Dtos.Fight;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers;

[ApiController]
[Route("[controller]")]
public class FightController : ControllerBase
{
    private readonly IFightService _fightService;

    public FightController(IFightService fightService)
    {
        _fightService = fightService;
    }

    [HttpPost("Weapon")]
    public async Task<ActionResult<ServiceResponse<AttackResultDto>>> WeaponAttack(
        WeaponAttackDto request)
    {
        return Ok(await _fightService.WeaponAttack(request));
    }

    [HttpPost("Skill")]
    public async Task<ActionResult<ServiceResponse<AttackResultDto>>> SkillAttck(
        SkillAttckDto request)
    {
        return Ok(await _fightService.SkillAttack(request));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<FightResultDto>>> Fight(
        FightRequestDto request)
    {
        return Ok(await _fightService.Fight(request));
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<HighScoreDto>>> GetHighScore()
    {
        return Ok(await _fightService.GetHighScore());
    }
}