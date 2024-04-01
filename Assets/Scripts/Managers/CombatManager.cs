using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private Character[] Characters { get; set; }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        Characters = FindObjectsOfType<Character>();
        foreach (var character in Characters)
        {
            character.onAttack.AddListener(OnCharacterAttack);
        }
    }

    private void OnCharacterAttack(Character attacker)
    {
        foreach (Character character in Characters)
        {
            if (attacker == character) continue;
            character.UpdateState(ECharacterStates.Defeated);
        }
    }
}
