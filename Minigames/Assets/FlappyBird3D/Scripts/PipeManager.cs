using System.Collections.Generic;
using UnityEngine;

namespace FlappyBird3D
{
    public class PipeManager : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private Pipe topPipePrefab;
        [SerializeField] private Pipe bottomPipePrefab;
        [Header("SpawnValues")]
        [SerializeField] private float gapSize = 4f; // Space between the pipes
        [SerializeField] private float spawnDelay = 2f; // Time between spawns
        [SerializeField] private float minCenterHeight = -1.5f;
        [SerializeField] private float maxCenterHeight = 4f;
        [SerializeField] private float destroyDistance = -15f; // Distance at which the pipe is destroyed

        private float _spawnTimer = 0;
        public bool canSpawn;
        private List<Pipe> _activePipes = new List<Pipe>();

        private void Update()
        {
            if (!canSpawn) return;
            RemoveOldPipes();
            SpawnNewPipes();
        }

        /// <summary>
        /// Destroy all active pipes and clear the list
        /// </summary>
        public void ResetPipes()
        {
            foreach (var pipe in _activePipes)
            {
                Destroy(pipe.gameObject);
            }
            _activePipes.Clear();
            _spawnTimer = 0f;
        }
        
        /// <summary>
        /// Remove old pipes once the target position is reached
        /// </summary>
        private void RemoveOldPipes()
        {
            for (int i = 0; i < _activePipes.Count; i++)
            {
                if (_activePipes[i].transform.position.x < destroyDistance)
                {
                    Destroy(_activePipes[i].gameObject);
                    _activePipes.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// Spawn new pipes once the spawn time is reached
        /// </summary>
        private void SpawnNewPipes()
        {
            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer > 0f) return;

            // Instantiate the pipes, one regular on the bottom and one reversed on top
            Pipe topPipe = Instantiate(topPipePrefab, transform.position, Quaternion.Euler(0f, 0f, 180f),transform);
            Pipe bottomPipe = Instantiate(bottomPipePrefab, transform.position, Quaternion.identity,transform);

            // Generate a random center height
            float centerHeight = Random.Range(minCenterHeight, maxCenterHeight);
            
            // Translate the pipes to the random center height
            topPipe.transform.Translate(Vector3.up * (centerHeight + (gapSize / 2)), Space.World);
            bottomPipe.transform.Translate(Vector3.up * (centerHeight - (gapSize / 2)), Space.World);

            _activePipes.Add(topPipe);
            _activePipes.Add(bottomPipe);

            _spawnTimer = spawnDelay;
        }

    }
}
