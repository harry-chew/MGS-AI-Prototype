using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;

    private void Start() {
        offset = transform.position - player.position;
    }
    
    private void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}
