using UnityEngine;

namespace FlappyBird3D
{
    public class Pipe : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;

        private void Update()
        {
            //Move the object in the negative direction
            transform.Translate(new Vector3(-speed * Time.deltaTime,0f,0f), Space.World);
        }
    }
}
