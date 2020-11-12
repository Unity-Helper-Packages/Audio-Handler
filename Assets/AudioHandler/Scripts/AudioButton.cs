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
    [Header("Select Audio Button Type")]
    public AudioButtonType AudioButtonType;

    [Space]
    public Sprite[] toggleSprites;
    
    [Space]
    [SerializeField] private TextMeshProUGUI lable;
    [SerializeField] private Image colorImage;
    [SerializeField] private Image spriteImage;

    private void Start()
    {
        this.gameObject.name = AudioButtonType + "Button";                
        refreshButtonStatus();
        GetComponent<Button>().onClick.AddListener(() =>
        {
            switch (AudioButtonType)
            {
                case AudioButtonType.Music:
                    AudioManager.inst.musicButtonClicked();                    
                    break;
                case AudioButtonType.SFX:
                    AudioManager.inst.sfxButtonClicked();
                    break;
                default:
                    Debug.Log("No Button Type Found");
                    break;
            }
            refreshButtonStatus();
        });
    }
    void refreshButtonStatus() {
        lable.text = AudioManager.inst.getButtonStatus(AudioButtonType).Item1;
        colorImage.color = AudioManager.inst.getButtonStatus(AudioButtonType).Item2;
        switch (AudioButtonType)
        {
            case AudioButtonType.Music:
                spriteImage.sprite = AudioManager.inst.isMusicOn() ? toggleSprites[1] : toggleSprites[0];
                break;
            case AudioButtonType.SFX:
                spriteImage.sprite = AudioManager.inst.isSfxOn() ? toggleSprites[1] : toggleSprites[0];
                break;
         }
    }
}//Class END.
