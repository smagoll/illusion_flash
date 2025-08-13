using System;
using UnityEngine;

public class UIGameplay : MonoBehaviour
{
    public static UIGameplay Instance;

    [SerializeField] private HUD hud;
    
    private Character _character;

    public HUD HUD => hud;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void Init(Character character)
    {
        _character = character;
        
        hud.Init(character.Model);
    }
}