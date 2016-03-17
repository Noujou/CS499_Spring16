using UnityEngine;
using System.Collections.Generic;
using Leap;
using System;

namespace CS499 {

    public enum FingerID { Thumb, Pointer, Middle, Ring, Pinky, Emulated};
    public class HandWrapper : MonoBehaviour {
        public static HandWrapper _inst;
        public static HandWrapper inst {
            get { 
                if (_inst == null) {
                    _inst = FindObjectOfType<HandWrapper>();
                }
                return _inst;
            }
        }
        public HandController hController;
        private Ray[] _fingerRays = null;
        Dictionary<FingerID, Finger.FingerType> _map;
        Dictionary<FingerID, Finger.FingerType> map {
            get {
                if (_map == null) {
                    _map = new Dictionary<FingerID, Finger.FingerType>();
                    _map.Add(FingerID.Thumb, Finger.FingerType.TYPE_THUMB);
                    _map.Add(FingerID.Pointer, Finger.FingerType.TYPE_INDEX);
                    _map.Add(FingerID.Middle, Finger.FingerType.TYPE_MIDDLE);
                    _map.Add(FingerID.Ring, Finger.FingerType.TYPE_RING);
                    _map.Add(FingerID.Pinky, Finger.FingerType.TYPE_PINKY);
                }
                return _map;
            }
        }
        private int _handCount;
        public int handCount
        {
            get { return _handCount; }
        }
        public Ray[] fingerRays {
            get {
                if (_fingerRays == null) {
                    _fingerRays = new Ray[Enum.GetNames(typeof(FingerID)).Length];
                    for (int i = 0; i < _fingerRays.Length; i++)
                        _fingerRays[i] = new Ray();
                }
                return _fingerRays;
            }
        }
        public Ray GetFingerRay(FingerID f) {
            return fingerRays[(int)f];
        }
        public float emulatedDist = 10;
        // Update is called once per frame
        void Update () {
            Frame f = hController.GetFrame();
            _handCount = f.Hands.Count;
            if (f.Hands.Count >= 1)
            {
                for (int i = 0; i < fingerRays.Length; i++) {
                    if ((FingerID)i != FingerID.Emulated)
                    {
                        fingerRays[i].origin = f.Hands[0].Fingers.FingerType(map[(FingerID)i])[0].Bone(Bone.BoneType.TYPE_DISTAL).NextJoint.ToUnity() / 1000;
                        fingerRays[i].direction = -f.Hands[0].Fingers.FingerType(map[(FingerID)i])[0].Bone(Bone.BoneType.TYPE_DISTAL).Direction.ToUnity();
                    }
                }
            }

            fingerRays[(int)FingerID.Emulated].origin = transform.position;

            Vector3 sv = Input.mousePosition;
            sv.x /= Camera.main.pixelWidth;
            sv.y /= Camera.main.pixelHeight;
            sv.z = emulatedDist;
            Vector3 wv = Camera.main.ViewportToWorldPoint(sv);

            fingerRays[(int)FingerID.Emulated].direction = (wv - transform.position).normalized;
        }
    }
}
