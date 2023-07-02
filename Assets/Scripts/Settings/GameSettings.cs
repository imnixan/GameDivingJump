using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
    public static bool SoundOn;
    public static bool VibrationOn;

    private CameraOperator co;

    private void Awake()
    {
        SoundOn = PlayerPrefs.GetInt("SoundSettings", 1) == 1;
        VibrationOn = PlayerPrefs.GetInt("VibrationSettings", 1) == 1;
    }

    public void Restart()
    {
        co.MoveCameraToStart();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    private void OnEnable()
    {
        co = Camera.main.GetComponent<CameraOperator>();
        co.OperatorOnStart += RestartLevel;
    }

    private void OnDisable()
    {
        co.OperatorOnStart -= RestartLevel;
    }
}
