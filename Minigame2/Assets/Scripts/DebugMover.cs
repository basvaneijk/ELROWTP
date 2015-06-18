using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class DebugMover : MonoBehaviour
    {
        private NavMeshAgent agent;
        private readonly List<Vector2> xyPosList = new List<Vector2>();

        private void Start()
        {
            xyPosList.AddRange(new[]
            {
                new Vector2(160, 80),
                new Vector2(50, 123),
                new Vector2(8, 44),
            });
            agent = GetComponent<NavMeshAgent>();
            NextGoal();
        }

        private void NextGoal()
        {
            Vector2 v = xyPosList.ElementAt(0);
            Vector3 goal = new Vector3(v.x, 11, v.y);
            agent.destination = goal;
        }

        private void Update()
        {
            if (transform.position.x - agent.destination.x < 5)
            {
                if (transform.position.z - agent.destination.z < 5)
                {
                    NextGoal();
                }
            }
        }
    }
}