using UnityEngine;

public class AnimationBoolEvent : MonoBehaviour {
    [SerializeField] Animator myAnimator;
    [SerializeField] string Parameter;
    [SerializeField] BoolReference newReferense;

    public void ChangeBool() {
        myAnimator.SetBool(Parameter, newReferense.Value);
    }

    public void ChangeBool(bool newState) {
        myAnimator.SetBool(Parameter, newState);
    }
}
