using UnityEngine;

[CreateAssetMenu(menuName = "Config/Character")]
public class CharacterConfig : ScriptableObject
{
    public int hp;
    public int mp;
    public int stamina;
    public AbilitySO[] abilities;
}