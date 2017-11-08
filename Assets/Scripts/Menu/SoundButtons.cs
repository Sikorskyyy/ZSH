using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButtons : MonoBehaviour 
{
    [SerializeField]
    private AudioClip click;

    [SerializeField]
    private Sprite offPic;

    [SerializeField]
    private Sprite onPic;

    [SerializeField]
    private SpriteRenderer soundBtn;

    private void OnMouseDown()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);

    }
        
    private void OnMouseUp()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
    }


    private void OnMouseUpAsButton()
    {
        AudioManager.Instance.PlaySound();

        SwitchMute();
    }


    public void SwitchMute()
    {
        AudioManager.soundMute = !AudioManager.soundMute;
       // AudioManager.Instance.PlaySoundOneShot (click);
        setSoundPic (AudioManager.soundMute);
    }


    public void setSoundPic(bool isOff)
    {
        if (isOff)
        {
            soundBtn.sprite = offPic;

        } else
        {
            soundBtn.sprite = onPic;    
        }
    }
	
}
