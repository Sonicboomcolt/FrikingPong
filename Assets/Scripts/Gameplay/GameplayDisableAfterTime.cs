using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayDisableAfterTime : MonoBehaviour
{
    [SerializeField] private float timeTillDespawn = .25f;
    private float coutdownTillDespawn;

    private void OnEnable()
    {
        coutdownTillDespawn = timeTillDespawn;
    }

    private void Update()
    {
        if(coutdownTillDespawn >= 0)
        {
            coutdownTillDespawn -= Time.deltaTime;
            if(coutdownTillDespawn <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
