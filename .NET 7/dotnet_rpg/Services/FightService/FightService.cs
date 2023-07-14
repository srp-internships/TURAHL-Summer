using dotnet_rpg.Dtos.Fight;

namespace dotnet_rpg.Services.FightService;

public class FightService : IFightService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public FightService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
    {
        var response = new ServiceResponse<AttackResultDto>();
        try
        {
            var attacker = await _context.Characters
                .Include(c => c.Weapon)
                .FirstOrDefaultAsync(c => c.Id == request.AtackerId);
            var opponent = await _context.Characters
                .FirstOrDefaultAsync(c => c.Id == request.OponnentId);

            if (attacker is null || opponent is null || attacker.Weapon is null)
                throw new Exception("Something fishy is going on here...");

            var damage = DoWeaponAttack(attacker, opponent);

            if (opponent.HitPoints <= 0)
                response.Message = $"Opponent {opponent.Name} has been defeated";

            await _context.SaveChangesAsync();

            response.Data = new AttackResultDto
            {
                Attacker = attacker.Name,
                Opponent = opponent.Name,
                AttackerHP = attacker.HitPoints,
                OpponentHP = opponent.HitPoints,
                Damage = damage
            };
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }

    private static int DoWeaponAttack(Character attacker, Character opponent)
    {
        if (attacker.Weapon is null)
        {
            throw new Exception("Attacker has no weapon");
        }
        
        var damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
        damage -= new Random().Next(opponent.Defense);

        if (damage > 0)
            opponent.HitPoints -= damage;
        return damage;
    }

    public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttckDto request)
    {
        var response = new ServiceResponse<AttackResultDto>();
        try
        {
            var attacker = await _context.Characters
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == request.AtackerId);
            var opponent = await _context.Characters
                .FirstOrDefaultAsync(c => c.Id == request.OponnentId);

            if (attacker is null || opponent is null || attacker.Skills is null)
                throw new Exception("Something fishy is going on here...");
            
            var skill = attacker.Skills.FirstOrDefault(s => s.Id == request.SkillId);
            if (skill is null)
            {
                response.Success = false;
                response.Message = $"{attacker.Name} can not perform the skill";
                return response;
            }

            var damage = DoSkillAttack(skill, attacker, opponent);

            if (opponent.HitPoints <= 0)
                response.Message = $"Opponent {opponent.Name} has been defeated";

            await _context.SaveChangesAsync();

            response.Data = new AttackResultDto
            {
                Attacker = attacker.Name,
                Opponent = opponent.Name,
                AttackerHP = attacker.HitPoints,
                OpponentHP = opponent.HitPoints,
                Damage = damage
            };
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }

    private static int DoSkillAttack(Skill skill, Character attacker, Character opponent)
    {
        int damage = skill.Damage + (new Random().Next(attacker.Intelligence));
        damage -= new Random().Next(opponent.Defense);

        if (damage > 0)
            opponent.HitPoints -= damage;
        return damage;
    }

    public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request)
    {
        var response = new ServiceResponse<FightResultDto>()
        {
            Data = new FightResultDto()
        };

        
            var characters = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .Where(c => request.CharacterIds.Contains(c.Id))
                .ToListAsync();

            bool defeated = false;

            while (!defeated)
            {
                foreach (var attacker in characters)
                {
                    var opponents = characters.Where(c => c.Id != attacker.Id).ToList();
                    var opponent = opponents[new Random().Next(opponents.Count)];

                    var damage = 0;
                    var attackUsed = string.Empty;

                    bool useWeapon = new Random().Next(2) == 0;
                    if (useWeapon && attacker.Weapon is not null)
                    {
                        attackUsed = attacker.Weapon.Name;
                        damage = DoWeaponAttack(attacker, opponent);
                    }
                    else if (!useWeapon && attacker.Skills!.Count > 0)
                    {
                        var skill = attacker.Skills[new Random().Next(attacker.Skills.Count)];
                        attackUsed = skill.Name;
                        damage = DoSkillAttack(skill, attacker, opponent);
                    }
                    else
                    {
                        response.Data.Log
                            .Add($"{attacker.Name} wasnt able to attack");
                        continue;
                    }
                    
                    response.Data.Log
                        .Add($"{attacker.Name} attacks {opponent.Name} with {attackUsed} with {(damage > 0 ? damage : 0)} damage");

                    if (opponent.HitPoints <= 0)
                    {
                        defeated = true;
                        attacker.Victories++;
                        opponent.Defeats++;
                        response.Data.Log.Add($"{opponent.Name} is defeated");
                        response.Data.Log.Add($"{attacker.Name} wins with {attacker.HitPoints} HP left");
                        break;
                    }
                }
            }

            characters.ForEach(c =>
                {
                    c.Fights++;
                    c.HitPoints = 1000;
                });
            await _context.SaveChangesAsync();

        return response;
    }

    public async Task<ServiceResponse<List<HighScoreDto>>> GetHighScore()
    {
        var characters = await _context.Characters
            .Where(c => c.Fights > 0)
            .OrderByDescending(c => c.Victories)
            .ThenBy(c => c.Defeats)
            .ToListAsync();

        var response = new ServiceResponse<List<HighScoreDto>>
        {
            Data = characters.Select(c => _mapper.Map<HighScoreDto>(c)).ToList()
        };

        return response;
    }
}