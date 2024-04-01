using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ECharacterStates
{
    Ready,
    TooEarly,
    Drawing,
    Defeated
}

[System.Serializable]
public class AttackEvent : UnityEvent<Character>
{
}

public class Character : MonoBehaviour, ICombat
{
    public AttackEvent onAttack;
    
    [field:SerializeField]
    private Sprite[] Sprites { get; set; }

    [field: SerializeField] 
    private SpriteRenderer SpriteRenderer { get; set; }
    
    [field: SerializeField]
    private SpriteRenderer Cross { get; set; }
    
    [field: SerializeField]
    private AudioSource AudioSource { get; set; }
    
    [field: SerializeField]
    private AudioClip SFX { get; set; }
    
    private ECharacterStates State { get; set; }
    
    public void Start()
    {
        PhaseManager.Instance.readyUp.AddListener(ReadyUp);
    }

    public void UpdateState(ECharacterStates state)
    {
        State = state;
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        switch (State)
        {
            case ECharacterStates.Ready:
                SpriteRenderer.sprite = Sprites[0];
                break;
            case ECharacterStates.Drawing:
                SpriteRenderer.sprite = Sprites[1];
                break;
            case ECharacterStates.Defeated:
                SpriteRenderer.sprite = Sprites[2];
                Cross.enabled = false;
                break;
            default:
                SpriteRenderer.sprite = Sprites[0];
                break;
        };
    }

    public void Attack()
    {
        if (State != ECharacterStates.Ready) return;

        switch (PhaseManager.Instance.GetCurrentPhase())
        {
            case EGamePhases.Draw:
                
                UpdateState(ECharacterStates.Drawing);
                onAttack.Invoke(this);
                AudioSource.PlayOneShot(SFX);
                break;
            case EGamePhases.Wait:
                Cross.enabled = true;
                State = ECharacterStates.TooEarly;
                break;
            default:
                return;
        }
    }

    private void ReadyUp()
    {
        UpdateState(ECharacterStates.Ready);
        Cross.enabled = false;
    }
}
