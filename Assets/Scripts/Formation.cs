using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WK
{
    public class Formation : MonoBehaviour
    {
        [SerializeField] private Transform followTarget;
        [SerializeField] private float followDistance = 3f;

        [Range(0, 1)]
        [SerializeField] private float noise = 0;
        [SerializeField] private float spread = 1;
        [SerializeField] private float groupWidth = 5;
        [SerializeField] private float groupLength = 5;
        [SerializeField] private float nthOffset = 0;
        [SerializeField] private bool hollow = false;
        [SerializeField] private bool debug = true;
        [SerializeField] private FollowerAI[] followers;

        private List<Vector3> positions;

        private void Update()
        {
            positions = GeneratePositions();
            UpdateFollowerPositions();
        }

        public void UpdateFollowerPositions()
        {
            for (int i = 0; i < followers.Length; i++)
            {
                followers[i]?.Follow(positions[i]);
            }
        }

        private List<Vector3> GeneratePositions()
        {
            Vector3 middleOffset = new Vector3(groupWidth * 0.5f, 0, groupLength * 0.5f);

            List<Vector3> positions = new();

            for (int x = 0; x < groupWidth; x++)
            {
                for (int z = 0; z < groupLength; z++)
                {
                    if (hollow && 
                        x != 0 && 
                        x != groupWidth - 1 && 
                        z != 0 && 
                        z != groupLength - 1) 
                        continue;

                    Vector3 pos = new Vector3(x + (z % 2 == 0 ? 0 : nthOffset), 0, z - followDistance);
                    pos -= middleOffset;
                    pos += GetNoise(pos);
                    pos *= spread;

                    pos = followTarget.TransformPoint(pos);
                    positions.Add(pos);
                }
            }

            return positions;
        }

        private Vector3 GetNoise(Vector3 pos)
        {
            float perlinNoise = Mathf.PerlinNoise(pos.x * noise, pos.z * noise);
            return new Vector3(perlinNoise, 0, perlinNoise);
        }

        private void OnDrawGizmos()
        {
            if (!debug) return;

            List<Vector3> positions = GeneratePositions();
            foreach (Vector3 pos in positions)
            {
                Gizmos.DrawWireSphere(pos, 0.5f);
            }
        }
    }
}