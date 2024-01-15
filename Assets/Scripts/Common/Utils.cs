using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace TooLoo
{
    public static class Utils
    {
        /// <summary>
        /// Randomly select an item from the given list based on the provided probabilities for each.
        /// </summary>
        /// <param name="choices"></param>
        /// <param name="weights"></param>
        /// <returns></returns>
        public static string WeightedSampling(IEnumerable<string> choices, IEnumerable<float> weights)
        {
            var cumulativeWeights = new List<float>();
            float sum = 0;
            foreach (var cur in weights)
            {
                sum += cur;
                cumulativeWeights.Add(sum);
            }

            float randomWeight = Random.Range(0, sum);
            int i = 0;
            foreach (var cur in choices)
            {
                if (randomWeight < cumulativeWeights[i])
                {
                    return cur;
                }
                i++;
            }
            return null;
        }

        /// <summary>
        /// Randomly selects item based on their given weights. Weights must add up to 1.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="weightedItems"></param>
        /// <returns></returns>
        public static T WeightedSampling<T>(Dictionary<T, float> weightedItems)
        {
            var cumulativeWeights = new List<float>();
            float sum = 0;
            foreach (KeyValuePair<T, float> item in weightedItems)
            {
                sum += item.Value;
                cumulativeWeights.Add(sum);
            }

            float randomWeight = Random.Range(0, sum);
            int i = 0;
            foreach (KeyValuePair<T, float> item in weightedItems)
            {
                if (randomWeight < cumulativeWeights[i])
                {
                    return item.Key;
                }
                i++;
            }
            return default;
        }

        /// <summary>
        /// Return a random position on a flat plane navmesh.
        /// </summary>
        /// <param name="sourcePos"></param>
        /// <param name="maxDistance"></param>
        /// <returns></returns>
        public static Vector3 GetRandomNavMeshPosition(Vector3 sourcePos, float maxDistance)
        {
            Vector3 randomDirection = Random.insideUnitSphere * maxDistance + sourcePos;
            Vector3 finalPosition = sourcePos;
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 4f, NavMesh.AllAreas))
            {
                finalPosition = hit.position;
            }
            return finalPosition;
        }

        /// <summary>
        /// Return a random navmesh position on an uneven terrain.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="maxDistance"></param>
        /// <param name="terrainLayerMask"></param>
        /// <param name="areaMask"></param>
        /// <returns></returns>
        public static Vector3 GetRandomNavMeshPosition(Vector3 source, float maxDistance, LayerMask terrainLayerMask, int areaMask = NavMesh.AllAreas)
        {
            Vector3 randomPosition = source;
            Vector2 randomCirclePoint = Random.insideUnitCircle * maxDistance;
            Vector3 randomDirection = new Vector3(randomCirclePoint.x, 0, randomCirclePoint.y);
            Vector3 targetPosition = source + randomDirection;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(targetPosition, out hit, maxDistance, areaMask))
            {
                // Perform a raycast from above the hit position to check for overlapping terrains
                RaycastHit raycastHit;
                Vector3 raycastOrigin = hit.position + Vector3.up * 100;
                if (Physics.Raycast(raycastOrigin, Vector3.down, out raycastHit, 200f, terrainLayerMask))
                {
                    // Check if the hit point is on the same terrain as the random NavMesh position
                    if (Mathf.Abs(raycastHit.point.y - hit.position.y) <= 0.5f)
                    {
                        randomPosition = hit.position;
                        return randomPosition;
                    }
                }
            }

            return randomPosition;
        }

        /// <summary>
        /// Return an array of random positions within a ring around the source position.
        /// </summary>
        /// <param name="sourcePos"></param>
        /// <param name="minRadius"></param>
        /// <param name="maxRadius"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static Vector3[] GetRandomCirclePosition(Vector3 sourcePos, float minRadius, float maxRadius, int count)
        {
            Vector3[] positions = new Vector3[count];

            for (int i = 0; i < count; i++)
            {
                positions[i] = GetRandomCirclePosition(sourcePos, minRadius, maxRadius);
            }
            return positions;
        }

        /// <summary>
        /// Return a random position within a ring around the source position.
        /// </summary>
        /// <param name="sourcePos"></param>
        /// <param name="minRadius"></param>
        /// <param name="maxRadius"></param>
        /// <returns></returns>
        public static Vector3 GetRandomCirclePosition(Vector3 sourcePos, float minRadius, float maxRadius)
        {
            float angle = Random.Range(0f, 2 * Mathf.PI);
            float distance = minRadius + Random.Range(0f, maxRadius - minRadius); 

            Vector3 position = new Vector3(
                sourcePos.x + distance * Mathf.Cos(angle),
                sourcePos.y,
                sourcePos.z + distance * Mathf.Sin(angle)
            );

            return position;
        }

        /// <summary>
        /// Return a random position on a circle of given radius around the source position.
        /// </summary>
        /// <param name="sourcePos"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static Vector3 GetRandomCirclePosition(Vector3 sourcePos, float radius)
        {
            Vector3 position = new();

            position = new Vector3(
                sourcePos.x + RandomNegOrPos() * (radius * Mathf.Cos(Random.Range(0, 2) * Mathf.PI)),
                0,
                sourcePos.z + RandomNegOrPos() * (radius * Mathf.Sin(Random.Range(0, 2) * Mathf.PI)));

            return position;
        }

        /// <summary>
        /// Return an array of evenly spaced positions on a circle around the source position.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="radius"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static Vector3[] GenerateEvenCirclePositions(Vector3 source, float radius, int count)
        {
            Vector3[] positions = new Vector3[count];
            float angleIncrement = 360f / count;

            for (int i = 0; i < count; i++)
            {
                float angle = i * angleIncrement * Mathf.Deg2Rad;
                Vector3 position = new Vector3(source.x + radius * Mathf.Cos(angle), source.y, source.z + radius * Mathf.Sin(angle));
                positions[i] = position;
            }

            return positions;
        }

        /// <summary>
        /// Randomly generate a -1 or 1
        /// </summary>
        /// <returns></returns>
        public static int RandomNegOrPos()
        {
            return (Random.Range(0, 2) * 2 - 1);
        }

        /// <summary>
        /// Return the an object of type T that is nearest to the source position.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="things"></param>
        /// <returns></returns>
        public static T GetNearest<T>(Vector3 source, List<T> things) where T : MonoBehaviour
        {
            if (things.Count == 0) return null;

            float distance = Mathf.Infinity;
            T nearest = null;
            foreach (T t in things)
            {
                float distFromSource = Vector3.Distance(source, t.transform.position);
                if (distFromSource < distance)
                {
                    nearest = t;
                    distance = distFromSource;
                }
            }

            return nearest;
        }

        /// <summary>
        /// Checks to see if target transform is within view of the the source transform.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="minAngle"></param>
        /// <param name="maxAngle"></param>
        /// <returns></returns>
        public static bool IsInFOV(Transform source, Transform target, float minAngle, float maxAngle)
        {
            Vector3 targetDirection = target.position - source.position;
            float angle = Vector3.Angle(targetDirection, source.forward);
            if (angle > minAngle && angle < maxAngle)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sort a list of objects of type T according to their distance (nearest to farthest) from the source position.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static List<T> SortByDistance<T>(List<T> items, Vector3 position)
            where T : MonoBehaviour
        {
            return items.OrderBy(i => Vector3.SqrMagnitude(i.transform.position - position)).ToList();
        }

        /// <summary>
        /// Randomly shuffle of list of objects of type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(List<T> list)
        {
            System.Random rng = new();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Rotates the given transform towards the given target position.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static Quaternion FaceTowards(Vector3 target, Transform unit)
        {
            Vector3 facing;
            facing = target - unit.position;
            facing.y = 0f;
            facing.Normalize();

            //Apply Rotation
            Quaternion targ_rot = Quaternion.LookRotation(facing, Vector3.up);
            Quaternion nrot = Quaternion.RotateTowards(unit.rotation, targ_rot, 360f);
            return nrot;
        }
    }
}