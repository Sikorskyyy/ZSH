using UnityEngine;

public class RandomSpritePicker : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private Sprite[] subSprites;
    [SerializeField]
    private AudioClip [] Clips;

    int lastIndex;
    public Sprite Pick()
    {
        if (null == sprites || 0 == sprites.Length)
        {
            Debug.LogError("Try to pick random sprite, but array is null or empty");
            return null;
        }

        lastIndex = Random.Range(0, sprites.Length);
        return sprites[lastIndex];
    }

    public Sprite GetSubPicture()
    {
       return subSprites[lastIndex];
    }

    public AudioClip GetClip()
    {
        return Clips[lastIndex];
    }
}
