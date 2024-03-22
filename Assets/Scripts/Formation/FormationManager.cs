using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WK 
{
    public class FormationManager : MonoBehaviour
    {
        [SerializeField] private FormationLeader captainLeader;
        [SerializeField] private FormationLeader soldierLeader;
        
        private FormationFollower chamberedCaptain;
        private Transform chamberedCaptainParent;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public FormationFollower GetCaptainForChambering()
        {
            // Return previous follower to its original leader
            if (chamberedCaptain != null)
            {
                chamberedCaptain.EnableNavmeshAgent(true);
                chamberedCaptain.SetFormationParent(chamberedCaptainParent);
                captainLeader.ReturnFollower(chamberedCaptain);
            }

            chamberedCaptain = captainLeader.GetNextFollower();

            // No more followers to launch
            if (chamberedCaptain is null) return null;

            chamberedCaptainParent = chamberedCaptain.FormationParent;
            chamberedCaptain.EnableNavmeshAgent(false);
            chamberedCaptain.SetFormationParent(transform.parent);

            return chamberedCaptain;
        }

        public void ChargeChamberedCaptain()
        {
            if (chamberedCaptain is null) return;

            // Move one soldier to chambered captain position
            FormationFollower soldier = soldierLeader.GetNextFollower();
            if (soldier is null) return;

            // Set movement ai to follow chambered captain
        }
    }
}