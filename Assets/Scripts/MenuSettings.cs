using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuSettings : MonoBehaviour
{
    private const string SoundPref = "SoundSettings";
    private const string VibroPref = "VibrationSettings";

    [SerializeField]
    TextMeshPro tablo;

    [SerializeField]
    Button soundButton,
        vibroButton;

    [SerializeField]
    private Sprite[] soundIcons,
        vibroIcons;
    private bool _soundOn,
        _vibroOn;
    public bool SoundOn
    {
        get { return _soundOn; }
        set
        {
            Debug.Log($"Sound was {_soundOn} and now is {value}");
            _soundOn = value;
            UpdatePrefsAndIcon(SoundPref, _soundOn ? 1 : 0, soundButton.image, soundIcons);
        }
    }

    public bool VibroOn
    {
        get { return _vibroOn; }
        set
        {
            Debug.Log($"Vibro was {_vibroOn} and now is {value}");
            _vibroOn = value;
            UpdatePrefsAndIcon(VibroPref, _vibroOn ? 1 : 0, vibroButton.image, vibroIcons);
        }
    }

    private void UpdatePrefsAndIcon(string prefs, int status, Image image, Sprite[] iconsSet)
    {
        PlayerPrefs.SetInt(prefs, status);
        PlayerPrefs.Save();
        image.sprite = iconsSet[status];
    }

    void Start()
    {
        tablo.text = Application.productName;
        SoundOn = PlayerPrefs.GetInt(SoundPref, 1) == 1;
        VibroOn = PlayerPrefs.GetInt(VibroPref, 1) == 1;
        soundButton.onClick.AddListener(
            delegate
            {
                SoundOn = !SoundOn;
            }
        );
        vibroButton.onClick.AddListener(
            delegate
            {
                VibroOn = !VibroOn;
            }
        );
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
