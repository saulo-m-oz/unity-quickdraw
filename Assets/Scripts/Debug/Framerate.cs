using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Framerate : MonoBehaviour
{
    [SerializeField]
    private GameObject _textMeshProObj;

    private TextMeshProUGUI _textMesh;
    
    void Start()
    {
        if (_textMeshProObj is null)
        {
            Debug.Log("Missing TextMeshPro");
            return;
        }

        _textMesh = _textMeshProObj.GetComponent<TextMeshProUGUI>();
    }
    
    void Update()
    {
        if (_textMesh is null) return;
        _textMesh.text = $"FPS: {(int)(1f / Time.unscaledDeltaTime)}";
    }

    public void ToggleFPS()
    {
        _textMeshProObj.SetActive(!_textMeshProObj.activeSelf);
    }
}
