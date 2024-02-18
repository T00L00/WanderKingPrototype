using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WK
{
    public class Chambering : MonoBehaviour
    {
        [SerializeField] private FormationLeader formationLeader;

        private FormationFollower activeFollower;
        private Transform activeFollowerParent;

        public void ChamberFollower()
        {
            if (activeFollower != null)
            {
                activeFollower.EnableNavmeshAgent(true);
                activeFollower.ContainerParent = activeFollowerParent;
                formationLeader.ReturnFollower(activeFollower);
            }            

            activeFollower = formationLeader.GetNextFollower();
            activeFollowerParent = activeFollower.ContainerParent;

            activeFollower.EnableNavmeshAgent(false);
            activeFollower.ContainerParent = transform.parent;
            StartCoroutine(MoveToPosition(new Vector3(0, 0, 3), 0.5f));
        }

        IEnumerator MoveToPosition(Vector3 target, float duration)
        {
            Vector3 startPosition = activeFollower.UnitParent.localPosition; // Store the starting position
            float startTime = Time.time; // Remember the start time
            float fraction = 0;


            while (fraction < 1)
            {
                // Calculate the fraction of the total duration that has passed
                fraction = (Time.time - startTime) / duration;

                // Update the GameObject's position
                activeFollower.UnitParent.localPosition = Vector3.Lerp(startPosition, target, fraction);

                // Yield until the next frame
                yield return null;
            }
        }
    }
}