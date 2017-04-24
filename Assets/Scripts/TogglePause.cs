using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePause : MonoBehaviour {

    public void tooglePause() {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        GameObject pauseHUD;
        Canvas hudrend;
        if ((pauseHUD = GameObject.Find("PauseMenu")) != null && (hudrend = pauseHUD.GetComponent<Canvas>()) != null) {
            hudrend.enabled = !hudrend.enabled;
        }
    }
}

