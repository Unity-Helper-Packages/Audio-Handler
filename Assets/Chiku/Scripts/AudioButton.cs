using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum AudioButtonType { 
    Music = 0,
    SFX = 1
}
[RequireComponent(typeof(Button))]
public class AudioButton : MonoBehaviour
{
    public AudioButtonType AudioButtonType;
    public Color[] colors;

    Image i;
    TextMeshProUGUI t;
    private void Start()
    {
        this.gameObject.name = AudioButtonType + "Button";
        i = GetComponent<Image>();
        t= transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        buttonStatus();
        GetComponent<Button>().onClick.AddListener(() =>
        {
            switch (AudioButtonType)
            {
                case AudioButtonType.Music:
                    AudioManager.inst.musicButtonClicked();
                    buttonStatus();
                    break;
                case AudioButtonType.SFX:
                    AudioManager.inst.sfxButtonClicked();
                    buttonStatus();
                    break;
                default:
                    Debug.Log("No Button Type Found");
                    break;
            }
        });
    }
    void buttonStatus() {
        switch (AudioButtonType)
        {
            case AudioButtonType.Music:                
                t.text = AudioManager.inst.isMusicOn() ? "Music On" : "Music Off";
                i.color = colors[AudioManager.inst.isMusicOn() ? 1 : 0];
                break;
            case AudioButtonType.SFX:                
                t.text = AudioManager.inst.isSfxOn() ? "Sfx On" : "Sfx Off";
                i.color = colors[AudioManager.inst.isSfxOn() ? 1 : 0];
                break;
            default:
                Debug.Log("No Button Type Found");
                break;
        }
    }
}//Class END.
