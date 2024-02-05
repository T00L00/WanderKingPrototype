using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace WK
{
    [RequireComponent(typeof(FormationGenerator))]
    public class FormationLeader : MonoBehaviour
    {
        [SerializeField] private GameObject followerPrefab;
        [SerializeField] private FormationGenerator formationGenerator;
        [SerializeField] private int followerCount;
        [SerializeField] private bool debug = true;

        private List<FormationFollower> formationFollowers;

        public int FollowerCount => followerCount;

        private void Start()
        {
            Init();
        }

        private void Update()
        {
            if (followerCount == 0) return;

            List<Vector3> positions = formationGenerator.GeneratePositions(followerCount);
            for (int i = 0; i < followerCount; i++)
            {
                formationFollowers[i].Follow(positions[i]);
            }
        }

        private void OnDrawGizmos()
        {
            if (!debug) return;

            List<Vector3> positions = formationGenerator.GeneratePositions(followerCount);
            foreach (Vector3 pos in positions)
            {
                Gizmos.DrawWireSphere(pos, 0.5f);
            }
        }

        public void Init()
        {
            if (followerCount == 0) return;

            formationFollowers = new();
            GameObject followerParent = Instantiate<GameObject>(new GameObject());
            followerParent.transform.position = Vector3.zero;

            for (int i = 0; i < followerCount; i++)
            {
                GameObject follower = Instantiate(followerPrefab, followerParent.transform);
                formationFollowers.Add(follower.GetComponentInChildren<FormationFollower>());
            }
        }
    }
}