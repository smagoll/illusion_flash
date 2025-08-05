using UnityEngine;

[CreateAssetMenu(menuName = "Config/Character")]
public class CharacterConfig : ScriptableObject
{
    public int hp;
    public int mp;
    public AbilitySO[] abilities;
}