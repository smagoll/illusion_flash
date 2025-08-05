using System.Collections.Generic;

public class AbilityController
{
    private readonly Dictionary<string, IAbility> _abilities = new();
    private readonly Character _character;

    public AbilityController(Character character, IEnumerable<AbilitySO> abilities)
    {
        _character = character;

        foreach (var abilitySo in abilities)
        {
            var ability = abilitySo.Create();
            ability.Initialize(_character);
            _abilities.TryAdd(ability.Id, ability);
        }
    }
    
    public bool TryExecute(string id)
    {
        if (_abilities.TryGetValue(id, out var ability))
        {
            if (ability.CanExecute())
            {
                ability.Execute();
                return true;
            }
        }

        return false;
    }

    public void Cleanup()
    {
        foreach (var ability in _abilities.Values)
        {
            ability.Cleanup(_character);
        }

        _abilities.Clear();
    }
    
    public bool AddAbility(IAbility ability)
    {
        if (ability == null || !_abilities.TryAdd(ability.Id, ability))
            return false;

        ability.Initialize(_character);
        return true;
    }
    
    public bool RemoveAbility(string id)
    {
        if (_abilities.TryGetValue(id, out var ability))
        {
            ability.Cleanup(_character);
            _abilities.Remove(id);
            return true;
        }

        return false;
    }
}
