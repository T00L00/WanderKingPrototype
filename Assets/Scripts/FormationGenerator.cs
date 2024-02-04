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
            int unitsInLastRow = followerCount % groupWidth;
            if (unitsInLastRow == 0 && followerCount > 0) unitsInLastRow = groupWidth;  // If the last row is full or there's only one row that's full.

            Vector3 middleOffset = new Vector3((groupWidth - 1) * spread * 0.5f, 0, (groupLength - 1) * spread * 0.5f);

            List<Vector3> positions = new List<Vector3>();

            for (int z = 0; z < groupLength; z++)
            {
                int unitsInThisRow = (z == groupLength - 1) ? unitsInLastRow : groupWidth;
                float spacing = (unitsInThisRow > 1) ? (groupWidth - 1) * spread / (unitsInThisRow - 1) : 0;

                for (int x = 0; x < unitsInThisRow; x++)
                {
                    if (hollow && z != 0 && z != groupLength - 1 && x != 0 && x != unitsInThisRow - 1)
                        continue;

                    float offsetX = (unitsInThisRow > 1) ? x * spacing : (groupWidth - 1) * spread * 0.5f;
                    Vector3 pos = new Vector3(offsetX + (z % 2 == 0 ? 0 : nthOffset), 0, z * spread - followDistance);
                    pos -= middleOffset;
                    pos += GetNoise(pos);

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