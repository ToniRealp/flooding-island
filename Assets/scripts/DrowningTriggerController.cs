using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowningTriggerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ExecuteAfterDelay(3, () =>
            {
                EventManager.OnGameOver.Invoke(
                    new EventManager.OnGameOverInfo(other.gameObject.name)
                );
            }));
        }
    }
    
    IEnumerator ExecuteAfterDelay(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }
}
