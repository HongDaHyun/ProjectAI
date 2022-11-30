using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    GameManager gameManager;
    public CinemachineVirtualCamera cinemachine;
    CinemachineBasicMultiChannelPerlin cinema;
    float startIntensity;
    float shakeTimeTotal;
    float shakeTime;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        cinema = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        transform.position = gameManager.king.transform.position;

        UpdateShake();
    }

    public void ShakeCam(float intensity, float time)
    {
        startIntensity = intensity;
        cinema.m_AmplitudeGain = startIntensity;
        cinema.m_FrequencyGain = 1f;
        shakeTimeTotal = time;
        shakeTime = shakeTimeTotal;
    }

    private void UpdateShake()
    {
        if(shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            cinema.m_AmplitudeGain = Mathf.Lerp(startIntensity, 0f, shakeTime / shakeTimeTotal);

            if (shakeTime <= 0f)
            {
                cinema.m_AmplitudeGain = 0f;
                cinema.m_FrequencyGain = 0f;
            }
        }
    }
}
