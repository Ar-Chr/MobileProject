using System;
using System.Collections;
using UnityEngine;

public static class HelperMethods
{
    public static void DelayAction(this MonoBehaviour bhvr, Action action, float delay)
    {
        bhvr.StartCoroutine(DelayActionCoroutine(action, delay));
    }

    static IEnumerator DelayActionCoroutine(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
}