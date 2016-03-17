using UnityEngine;
using System.Collections;
namespace CS499 {
    public class SimpleTTL : MonoBehaviour {
        public float lifeTime;
	    void Update () {
            lifeTime -= Time.deltaTime;
            if (lifeTime < 0) {
                Destroy(this.gameObject);
            }
	    }
    }
}
