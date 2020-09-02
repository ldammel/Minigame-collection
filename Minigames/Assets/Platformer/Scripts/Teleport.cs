using System;
using UnityEngine;

namespace Platformer.Scripts
{
    public class Teleport : MonoBehaviour
    {
        [SerializeField] private Transform startLocation;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.transform.position = startLocation.position;
            }
        }
    }
}
