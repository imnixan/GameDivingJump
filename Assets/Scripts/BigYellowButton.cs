using UnityEngine.UI;
using TMPro;

public class BigYellowButton : JumperSubscribers
{
    private TextMeshProUGUI buttonText;
    private Button button;

    public void Hide()
    {
        button.interactable = false;
        buttonText.text = "";
    }

    private void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
    }

    protected override void OnJumperIdle()
    {
        button.interactable = true;
        buttonText.text = "Jump!";
    }

    protected override void OnJumperJump()
    {
        button.interactable = false;
        buttonText.text = "";
    }

    protected override void OnJumperLay()
    {
        button.interactable = true;
        buttonText.text = "Next!";
    }
}
