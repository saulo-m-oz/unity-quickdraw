using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;



public class GameLoop : MonoBehaviour
{
    [field: SerializeField] 
    private float InitialRoundTime { get; set; } = 3.0f;
    
    [field: SerializeField]
    private SpriteRenderer ExclamationMark { get; set; }
    
    private EGamePhases _currentPhase;

    public void Start()
    {
        StartCoroutine(PhaseUpdateCoroutine(InitialRoundTime));
    }

    public void HandleCharacterAttack()
    {
        if (_currentPhase == EGamePhases.Wait)
        {
        }
    }

    private IEnumerator PhaseUpdateCoroutine(float roundTime)
    {
        yield return new WaitForSeconds(roundTime);
        UpdatePhase();
    }

    private IEnumerator DisableExclamationMark()
    {
        yield return new WaitForSeconds(0.35f);
        ExclamationMark.enabled = false;
    }

    private void UpdatePhase()
    {
        switch (_currentPhase)
        {
            case EGamePhases.Ready:
                UpdatePhaseAndRestartCoroutine(EGamePhases.Wait, 1.5f);
                break;
            case EGamePhases.Wait:
                UpdatePhaseAndRestartCoroutine(EGamePhases.Draw, 3.0f);
                ExclamationMark.enabled = true;
                StartCoroutine(DisableExclamationMark());
                break;
            case EGamePhases.Draw:
                UpdatePhaseAndRestartCoroutine(EGamePhases.Finished, 4.0f);
                break;
            default:
                UpdatePhaseAndRestartCoroutine(EGamePhases.Ready, 2.0f);
                break;
        }
    }

    private void UpdatePhaseAndRestartCoroutine(EGamePhases phase, float roundTime)
    {
        _currentPhase = phase;
        StartCoroutine(PhaseUpdateCoroutine(roundTime));
    }
}
