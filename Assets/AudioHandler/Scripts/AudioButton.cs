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
        spriteImage.sprite = AudioManager.inst.getButtonStatus(AudioButtonType).Item3;        
    }
}//Class END.
