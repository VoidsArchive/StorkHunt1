using UnityEngine;

public class Sounds : MonoBehaviour
{
   public AudioClip FallingSound;
   public AudioClip GunSound;
   public AudioClip ReloadingSound;
   public AudioClip ClickSound;
   public AudioSource audioSource;

   public void Awake()
   {
      audioSource = GetComponent<AudioSource>();
   }

   public void PlayFallingSound()
   {
      audioSource.PlayOneShot(FallingSound);
   }

   public void PlayGunSound()
   {
      audioSource.PlayOneShot(GunSound);
   }

   public void PlayReloadingSound()
   {
      audioSource.PlayOneShot(ReloadingSound);
   }

   public void PlayClickSound()
   {
      audioSource.PlayOneShot(ClickSound);
   }
}
