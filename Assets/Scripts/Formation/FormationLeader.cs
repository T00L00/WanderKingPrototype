using System.Collections;
using System.Collections.Generic;
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
        private Queue<FormationFollower> followersQueue;

        public int FollowerCount => followerCount;

        private void Start()
        {
            Init();
        }

        private void Update()
        {
            if (followersQueue is null || followersQueue.Count == 0) return;

            List<Vector3> positions = formationGenerator.GeneratePositions(followersQueue.Count);

            int i = 0;
            foreach (FormationFollower f in followersQueue)
            {
                f.Follow(positions[i]);
                i++;
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

        /// <summary>
        /// Temporary logic to spawn followers.
        /// </summary>
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

            followersQueue = new Queue<FormationFollower>(formationFollowers);
        }

        public FormationFollower GetNextFollower()
        {
            if (followersQueue is null || followersQueue.Count == 0)
            {
                Debug.Log("No more followers in the queue.");
                return null;
            }
            return followersQueue.Dequeue();
        }

        public void ReturnFollower(FormationFollower follower)
        {
            followersQueue.Enqueue(follower);
        }
    }
}