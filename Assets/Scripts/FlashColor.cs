using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    [Header("Setup")]
    public Color color = Color.red;
    public float duration = .1f;

    private Color defaultColor;
    private Tween _currentTween;

    private void Start()
    {
        defaultColor = meshRenderer.material.GetColor("_EmissionColor");
    }

    [NaughtyAttributes.Button]
    public void Flash()
    {
        var meshMaterial = meshRenderer.material;

        if (!_currentTween.IsActive())
        {
            _currentTween = meshMaterial.DOColor(color, "_EmissionColor", duration).SetLoops(6, LoopType.Yoyo);
        }
    }


}
