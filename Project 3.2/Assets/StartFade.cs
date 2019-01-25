using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartFade : MonoBehaviour {
    [SerializeField] Image target;
    [SerializeField] float speed;
    [SerializeField] bool fadeIn;
    [SerializeField] AudioSource audio;

    void Update() {
        if(fadeIn) {
            target.color = new Vector4(0, 0, 0, Mathf.Lerp(target.color.a, 1, speed * Time.deltaTime));

            if(target.color.a > 0.95f) {
                target.color = new Vector4(0, 0, 0, 1);
                Application.Quit();
            }
        }
        else {
            target.color = new Vector4(0, 0, 0, Mathf.Lerp(target.color.a, 1, speed * Time.deltaTime));

            if(target.color.a > 0.95f) {
                target.color = new Vector4(0, 0, 0, 1);
                SceneManager.LoadScene("Main");
            }
        }

        audio.volume = Mathf.Lerp(audio.volume, 0, speed * Time.deltaTime);

    }

    public void SetFade(bool fadIn) {
        fadeIn = fadIn;
    }
}