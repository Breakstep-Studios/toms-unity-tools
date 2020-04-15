using UnityEngine;

namespace StudioName.Runtime {

    //TODO We need to comment the crap out of this. This is not ready for production
    public class ObjectTracker : MonoBehaviour {
        public GameObject itemToTrack;
        public TrackBy trackBy;
        [Header("Track Position")]
        public bool trackPositionX;
        public bool trackPositionY;
        public bool trackPositionZ;
        [Header("Track Rotation")]
        public bool trackRotationX;
        public bool trackRotationY;
        public bool trackRotationZ;
        [Header("Track Scale")]
        public bool trackScaleX;
        public bool trackScaleY;
        public bool trackScaleZ;
        public enum TrackBy { Position, Rotation, Scale, All }

        // Update is called once per frame
        void Update() {
            Track();
        }

        private void Track() {
            switch ( trackBy ) {
                case TrackBy.All:
                case TrackBy.Position:
                    //need to remove zero and replace with x y z later on did this just for speed level
                    transform.position = new Vector3(
                        (trackPositionX) ? itemToTrack.transform.position.x : transform.position.x,
                        (trackPositionY) ? itemToTrack.transform.position.y : transform.position.y,
                        (trackPositionZ) ? itemToTrack.transform.position.z : transform.position.z );

                    if ( trackBy == TrackBy.All ) {
                        goto case TrackBy.Rotation;
                    }

                    break;
                case TrackBy.Rotation:
                    transform.eulerAngles = new Vector3(
                        (trackRotationX) ? itemToTrack.transform.eulerAngles.x : transform.eulerAngles.x,
                        (trackRotationY) ? itemToTrack.transform.eulerAngles.y : transform.eulerAngles.y,
                        (trackRotationZ) ? itemToTrack.transform.eulerAngles.z : transform.eulerAngles.z);

                    if ( trackBy == TrackBy.All ) {
                        goto case TrackBy.Scale;
                    }

                    break;
                case TrackBy.Scale:
                    transform.localScale = new Vector3(
                        (trackScaleX) ? itemToTrack.transform.localScale.x : transform.localScale.x,
                        (trackScaleY) ? itemToTrack.transform.localScale.y : transform.localScale.y,
                        (trackScaleZ) ? itemToTrack.transform.localScale.z : transform.localScale.z );
                    break;
            }
        }
    }

}
