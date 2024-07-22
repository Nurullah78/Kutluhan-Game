using UnityEngine;

namespace Enemy
{
    public class PatrolPath : MonoBehaviour
    {

//----------------- Nesneler -----------------

        const float waypointRadius = 0.4f;


//----------------- Metotlar -----------------

        public Vector3 GetWaypointPosition(int i)
        {
            return transform.GetChild(i).position;
        }

        public int GetNextIndex(int i)
        {
            if (i + 1 == transform.childCount)
                return 0;

            return i + 1;
        }

        void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypointPosition(i), waypointRadius);
                Gizmos.DrawLine(GetWaypointPosition(i), GetWaypointPosition(j));
            }
        }
    }
}
