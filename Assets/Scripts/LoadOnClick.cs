using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {
    public AudioClip onClickSound;

    private void playSound() {
        if (onClickSound != null) {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = onClickSound;
            source.Play();
        }
    }

    public void LoadScene(string levelName) {
        playSound();
        SceneManager.LoadScene(levelName);
    }
}