using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public enum EGamePhases
{
    Ready,
    Wait,
    Draw,
    Finished
}

public class PhaseManager : MonoBehaviour
{
    private static PhaseManager _instance;

    public static PhaseManager Instance
    {
        get { return _instance;  }
    }

    public UnityEvent readyUp;

    [field: SerializeField] 
    private SpriteRenderer _exclamationMark;

    [field: SerializeField] 
    [field: Range(1.0f, 3.0f)]
    private float FromReadyToWait { get; set; }
    
    [field: SerializeField] 
    [field: Range(1.0f, 3.0f)]
    private float FromDrawToFinished { get; set; }
    
    [field: SerializeField] 
    [field: Range(1.0f, 3.0f)]
    private float FromFinishedToReady { get; set; }
    
    private EGamePhases CurrentPhase { get; set; }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public void Start()
    {
        UpdatePhaseAndRestartCoroutine(EGamePhases.Ready, FromReadyToWait);
    }

    public EGamePhases GetCurrentPhase() => _instance.CurrentPhase;
    
    private IEnumerator PhaseUpdateCoroutine(float roundTime)
    {
        yield return new WaitForSeconds(roundTime);
        UpdatePhase();
    }

    private IEnumerator DisableExclamationMark()
    {
        yield return new WaitForSeconds(0.25f);
        _exclamationMark.enabled = false;
    }

    private void UpdatePhase()
    {
        switch (CurrentPhase)
        {
            case EGamePhases.Ready:
                UpdatePhaseAndRestartCoroutine(EGamePhases.Wait, GenerateRandomWaitTime());
                break;
            case EGamePhases.Wait:
                UpdatePhaseAndRestartCoroutine(EGamePhases.Draw, FromDrawToFinished);
                _exclamationMark.enabled = true;
                StartCoroutine(DisableExclamationMark());
                break;
            case EGamePhases.Draw:
                UpdatePhaseAndRestartCoroutine(EGamePhases.Finished, FromFinishedToReady);
                break;
            case EGamePhases.Finished:
                UpdatePhaseAndRestartCoroutine(EGamePhases.Ready, FromReadyToWait);
                readyUp.Invoke();
                break;
        }
    }

    private float GenerateRandomWaitTime()
    {
        return Random.Range(0.15f, 3.0f);
    }

    private void UpdatePhaseAndRestartCoroutine(EGamePhases phase, float roundTime)
    {
        CurrentPhase = phase;
        _instance.StartCoroutine(PhaseUpdateCoroutine(roundTime));
    }
}
