using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using Random = UnityEngine.Random;

public class Lighting : MonoBehaviour
{
    private Light2D light;
    public AnimationCurve[] brightAnim;
    public float lightIntensity;
    private bool loop;
    private float duration;
    public float startDelay;
    private bool startLight;

    private void Start()
    {
        light = GetComponent<Light2D>();
    }

    void Update()
    {
        duration += Time.deltaTime;
        if (duration >= startDelay)
        {
            startLight = true;
        }

        if (startLight)
        {
            if (!loop)
            {
                loop = true;
                StartCoroutine(Bright());
            }
        }
    }

    private IEnumerator Bright()
    {
        int num = Random.Range(0, brightAnim.Length);
        for (float j = 0; j <= 1f; j += 0.01f)
        {
            light.intensity = Mathf.Lerp(0f, lightIntensity, brightAnim[0].Evaluate(j));
            yield return null;
        }
        loop = false;
    }
}