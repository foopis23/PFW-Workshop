using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedDeathFloorController : MonoBehaviour
{
    public Transform playerRespawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = playerRespawnPoint.position;
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
    }
}
