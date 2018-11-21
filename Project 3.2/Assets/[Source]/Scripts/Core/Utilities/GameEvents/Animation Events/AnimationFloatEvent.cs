using UnityEngine;

public class AnimationFloatEvent : MonoBehaviour {
    [SerializeField] Animator myAnimator;
    [SerializeField] string Parameter;
    [SerializeField] FloatReference newReferense;

    public void ChangeBool(float newState) {
            myAnimator.SetFloat(Parameter, newState);
    }

    public void ChangeBool() {
        myAnimator.SetFloat(Parameter, newReferense.Value);
    }
}
