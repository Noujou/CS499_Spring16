using UnityEngine;
using System.Collections;

public class SUPERSECRETSCRIPT : MonoBehaviour {
    //Congratulations you have found the hidden message.
    void Update () {
        if (Cardboard.SDK.Triggered) {
#if UNITY_EDITOR
            FindObjectOfType<TextMesh>().text = "this " + " is" + " where\n"+" the"+" number"+" would"+" show.";
#else
            FindObjectOfType<TextMesh>().text = Application.bundleIdentifier+"\n"+Application.bundleIdentifier.GetHashCode().ToString("x8");
#endif
        }
    }
}
