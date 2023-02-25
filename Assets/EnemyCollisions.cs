using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            this.gameObject.GetComponentInParent<Enemy>().ChangeEnemyState(EnemyState.Chase);
        }
    }
}
