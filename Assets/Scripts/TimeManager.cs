using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Texture2D skyboxNight;
    [SerializeField] private Texture2D skyboxSunrise;
    [SerializeField] private Texture2D skyboxDay;
    [SerializeField] private Texture2D skyboxSunset;

    [SerializeField] private Gradient gradientNightToSunrise;
    [SerializeField] private Gradient gradientSunriseToDay;
    [SerializeField] private Gradient gradientDayToSunset;
    [SerializeField] private Gradient gradientSunsetToNight;

    [SerializeField] private Light globalLight;
    private int Minutes { get { return minutes; } set { minutes = value; OnMinutesChange(value); } }
    private int Hours { get { return hours; } set { hours = value; OnHoursChange(value); } }
    private int Days { get { return days; } set { days = value; OnDaysChange(value); } }
    private int minutes;
    private int hours;
    private int days;

    public float sunSpeed = 100.0f;
    private void Awake()
    {
        
    }
    private void Start()
    {

    }
    private float tempSeconds;
    public void Update()
    {
        tempSeconds+= Time.deltaTime;
        globalLight.transform.Rotate(Vector3.up, (1f / 1440f) * 360f *Time.deltaTime, Space.World);

        if (tempSeconds >=1)
        {
            Minutes += 1;
            tempSeconds = 0;
        }
        Debug.Log(Days);
        Debug.Log(Hours);
        Debug.Log(Minutes);
    }
    
    private void OnMinutesChange(int value)
    {
        if (value >= 60)
        {
            Hours++;
            minutes = 0;
        }
        if(Hours >= 24)
        {
            Hours = 0;
            Days++;
        }
    }
    private void OnHoursChange(int value)
    {
        if(value == 6)
        {
            StartCoroutine(LerpSkybox(skyboxNight, skyboxSunrise, 10f));
            StartCoroutine(LerpLight(gradientNightToSunrise,10f));
        }
        else if(value == 8)
        {
            StartCoroutine(LerpSkybox(skyboxSunrise, skyboxDay, 10f));
            StartCoroutine(LerpLight(gradientSunriseToDay, 10f));

        }
        else if(value == 18)
        {
            StartCoroutine(LerpSkybox(skyboxDay, skyboxSunset, 10f));
            StartCoroutine(LerpLight(gradientDayToSunset, 10f));

        }
        else if (value == 22)
        {
            StartCoroutine(LerpSkybox(skyboxSunset, skyboxNight, 10f));
            StartCoroutine(LerpLight(gradientSunsetToNight, 10f));
        }
    }

    private void OnDaysChange(int value)
    {

    }

    private IEnumerator LerpSkybox(Texture2D a, Texture2D b,float time)
    {
        RenderSettings.skybox.SetTexture("_Texture1", a);
        RenderSettings.skybox.SetTexture("_Texture2", b);
        RenderSettings.skybox.SetFloat("_Blend", 0);
        for (float i = 0; i < time;i+=Time.deltaTime)
        {
            RenderSettings.skybox.SetFloat("_Blend", i / time);
            yield return null;
        }
        RenderSettings.skybox.SetTexture("_Texture1", b);
    }

    private IEnumerator LerpLight(Gradient lightGradient, float time)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            globalLight.color = lightGradient.Evaluate(i / time);
            yield return null;
        }
    }
}
