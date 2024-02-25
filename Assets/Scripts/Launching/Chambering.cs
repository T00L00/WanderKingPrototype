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
                activeFollower.FormationParent = activeFollowerParent;
                formationLeader.ReturnFollower(activeFollower);
            }

            activeFollower = formationLeader.GetNextFollower();
            activeFollowerParent = activeFollower.FormationParent;

            activeFollower.EnableNavmeshAgent(false);
            activeFollower.FormationParent = transform.parent;
            StartCoroutine(MoveToPosition(new Vector3(0, 0, 3), 0.5f));
        }

        IEnumerator MoveToPosition(Vector3 target, float duration)
        {
            Vector3 startPosition = activeFollower.UnitParent.localPosition;
            float startTime = Time.time;
            float fraction = 0;

            while (fraction < 1)
            {
                fraction = (Time.time - startTime) / duration;
                activeFollower.UnitParent.localPosition = Vector3.Lerp(startPosition, target, fraction);
                yield return null;
            }
        }
    }
}