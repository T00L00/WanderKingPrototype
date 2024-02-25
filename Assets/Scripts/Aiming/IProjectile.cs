using UnityEngine;

namespace WK.Aiming 
{
    public interface IProjectile
    {
        public void Launch(ProjectileTrajectory trajectoryPath);
    }
}