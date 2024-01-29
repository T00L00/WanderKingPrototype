using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WK
{
    public class FormationGenerator : MonoBehaviour
    {
        [SerializeField] private float followDistance = 3f;
        [Range(0, 1)]
        [SerializeField] private float noise = 0;
        [SerializeField] private float spread = 1;
        [SerializeField] private int groupWidth = 5;
        [SerializeField] private float nthOffset = 0;
        [SerializeField] private bool hollow = false;

        private int groupLength;

        public List<Vector3> GeneratePositions(int followerCount)
        {
            groupLength = Mathf.CeilToInt((float)followerCount / groupWidth);

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

                    pos = transform.parent.TransformPoint(pos);
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
    }
}