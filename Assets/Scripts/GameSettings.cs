using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettings : MonoBehaviour
{
    public static Material playerRedColor, greenColor, blueColor;
    public bool SoundOn;
    public bool VibrationOn;

    private CameraOperator co;
    

    private void Awake() {
        SoundOn = PlayerPrefs.GetInt("SoundSettings", 1) == 1;
        VibrationOn = PlayerPrefs.GetInt("VibrationSettings", 1) == 1;
    }

    private void Start()
    {
        SetupMaterials();
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToMenu();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.DeleteKey("Record");
            RestartLevel();
        }
    }
    private void SetupMaterials()
    {
        playerRedColor = Resources.Load<Material>("PlayerRedMat");
        greenColor = Resources.Load<Material>("GreenMat");
        blueColor = Resources.Load<Material>("BlueMat");
    }

    public void Restart()
    {
        co.MoveCameraToStart();
        
    }

    public void RestartLevel()
    {
        Debug.Log("restart level");
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
