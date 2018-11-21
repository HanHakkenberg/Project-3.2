using UnityEngine;
using UnityEngine.UI;

public class ImageEvent : MonoBehaviour {
	public Image target;

	[Header("Fill Amount")]
	public FloatReference currentFillValue;
	public FloatReference baseFillFloat;

    void Start() {
        if(target == null) {
            target = GetComponent<Image>();
        }
    }

    public void UpdateFillAmount() {
		target.fillAmount = currentFillValue.Value / baseFillFloat.Value;
	}

	[Header("Sprite Switch")]
	public SpriteReference currentSprite;

	public void UpdateSprite() {
		target.sprite = currentSprite.Value;
	}
}