using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Controller
{
    public class CameraController: MonoBehaviour
    {
        public float speedForward;
        public float speedBackward;
        public float speedLateral;

        public void Update()
        {
            var direction = Vector3.zero;

            if(Input.GetKey(KeyCode.Z))
            {
                direction.z += speedForward;
            }
            if(Input.GetKey(KeyCode.S))
            {
                direction.z -= speedBackward;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                direction.x -= speedLateral;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction.x += speedLateral;
            }

            this.transform.Translate(direction);
        }
    }
}
