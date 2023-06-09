using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy
{
    public class EnemyWalk : EnemyBase
    {
        [Header("Waypoints")]
        public GameObject[] waypoints;
        public float minDistance = 1f;
        private int _index = 0;

        public override void Update()
        {
            base.Update();
            var distance = Vector3.Distance(transform.position, waypoints[_index].transform.position);

            if (distance < minDistance)
            {
                _index++;
                if (_index >= waypoints.Length)
                {
                    _index = 0;
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, waypoints[_index].transform.position, speed * Time.deltaTime);
            transform.LookAt(waypoints[_index].transform.position);
        }
    }
}