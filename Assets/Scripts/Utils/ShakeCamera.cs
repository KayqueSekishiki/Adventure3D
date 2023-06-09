using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Cinemachine;

public class ShakeCamera : Singleton<ShakeCamera>
{
    public CinemachineVirtualCamera virtualCamera;

    [Header("Shake Values")]
    public float amplitude = 3f;
    public float frequency = 3f;
    public float shakeTime = 0.3f;
    private float _shakeTime;

    private CinemachineBasicMultiChannelPerlin _cinemachineBMCP;

    [NaughtyAttributes.Button]
    public void Shake()
    {
        _shakeTime = shakeTime;
        Shake(amplitude, frequency, _shakeTime);
    }

    private void Start()
    {
        _cinemachineBMCP = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake(float amplitude, float frequency, float time)
    {
        _cinemachineBMCP.m_AmplitudeGain = amplitude;
        _cinemachineBMCP.m_FrequencyGain = frequency;
        shakeTime = time;
    }

    private void Update()
    {
        if (_shakeTime > 0)
        {
            _shakeTime -= Time.deltaTime;
        }
        else
        {
            _cinemachineBMCP.m_AmplitudeGain = 0f;
            _cinemachineBMCP.m_FrequencyGain = 0f;
        }
    }
}
