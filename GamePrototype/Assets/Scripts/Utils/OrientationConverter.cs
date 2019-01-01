using GameEngine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class OrientationConverter
    {
        public static Quaternion ToQuaternion(Orientation orientation)
        {
            var quaternion = new Quaternion();
            switch(orientation)
            {
                case Orientation.NORTH_EAST:
                    quaternion.eulerAngles = new Vector3(0, 45, 0);
                    break;
                case Orientation.EAST:
                    quaternion.eulerAngles = new Vector3(0, 90, 0);
                    break;
                case Orientation.SOUTH_EAST:
                    quaternion.eulerAngles = new Vector3(0, 135, 0);
                    break;
                case Orientation.SOUTH:
                    quaternion.eulerAngles = new Vector3(0, 180, 0);
                    break;
                case Orientation.SOUTH_WEST:
                    quaternion.eulerAngles = new Vector3(0, 225, 0);
                    break;
                case Orientation.WEST:
                    quaternion.eulerAngles = new Vector3(0, 270, 0);
                    break;
                case Orientation.NORTH_WEST:
                    quaternion.eulerAngles = new Vector3(0, 315, 0);
                    break;
                case Orientation.NORTH:
                case Orientation.CENTER:
                default:
                    quaternion.eulerAngles = new Vector3(0, 0, 0);
                    break;
            }

            return quaternion;
        }
    }
}
