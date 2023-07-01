using UnityEngine;

public class ScreenSettings : MonoBehaviour
{
    private void Awake() {
        Screen.orientation = ScreenOrientation.Portrait;
        Application.targetFrameRate = 60;
    }
}
