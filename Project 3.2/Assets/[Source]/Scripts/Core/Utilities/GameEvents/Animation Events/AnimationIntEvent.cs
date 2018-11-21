using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationIntEvent : MonoBehaviour {
    [SerializeField] Animator myAnimator;
    [SerializeField] string Parameter;
    [SerializeField] IntReference newReferense;

    public void ChangeBool(int newState) {
        myAnimator.SetInteger(Parameter, newState);
    }

    public void ChangeBool() {
        myAnimator.SetInteger(Parameter, newReferense.Value);
    }
}
