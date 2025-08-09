using System.Collections;
using UnityEngine;

public class Haptics : MonoBehaviour
{
    [SerializeField]
    private float duration = 0.1f;
    [SerializeField]
    private float frequency = 150f;
    [SerializeField]
    private float amplitude = 0.5f;
    public void TriggerHaptic(bool isLeftHand)
    {
        OVRInput.Controller controller = isLeftHand ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch;
        StartCoroutine(PlayHapticFeedback(controller));
    }

    private IEnumerator PlayHapticFeedback(OVRInput.Controller controller)
    {
        OVRInput.SetControllerVibration(frequency, amplitude, controller);
        yield return new WaitForSeconds(duration);
        OVRInput.SetControllerVibration(0, 0, controller);
    }
}
