using UnityEngine;

namespace StudioName.Runtime.Utility {

    public static class PhysxUtil {

        public static bool IsCollidingWithSide(Collision2D incomingCollider, DirectionType dirToDetect) {
            Vector2 pointOfContactNormal = incomingCollider.contacts[0].normal; //Grab the normal of the contact point we touched

            //Check if we hit any of our four directions
            switch (dirToDetect) {
                case DirectionType.Right:
                    if (pointOfContactNormal == new Vector2(-1, 0))
                        return true;
                    break;

                case DirectionType.Left:
                    if (pointOfContactNormal == new Vector2(1, 0))
                        return true;
                    break;

                case DirectionType.Up:
                    if (pointOfContactNormal == new Vector2(0, -1))
                        return true;
                    break;

                case DirectionType.Down:
                    if (pointOfContactNormal == new Vector2(0, 1))
                        return true;
                    break;
            }

            //If we didn't hit one of our four directions return false
            return false;

        }

        public static DirectionType WhichSideIsHit(Collision2D incomingCollider) {
            Vector2 pointOfContactNormal = incomingCollider.contacts[0].normal; //Grab the normal of the contact point we touched

            //Check if we hit any of our four directions
            if(pointOfContactNormal == new Vector2(-1, 0)) {
                return DirectionType.Right;
            } else if (pointOfContactNormal == new Vector2(1, 0)) {
                return DirectionType.Left;
            } else if (pointOfContactNormal == new Vector2(0, -1)) {
                return DirectionType.Up;
            } else if (pointOfContactNormal == new Vector2(0, 1)) {
                return DirectionType.Down;
            } else {
                Debug.LogWarning("Which Side Is Hit did not detect an up, down, left, or right exactly....returning down");
                return DirectionType.Down;
            }

        }
    }
}
