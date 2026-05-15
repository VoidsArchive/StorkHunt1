using UnityEngine;

public class Sounds : MonoBehaviour
{
   public AudioClip FallingSound;
   public AudioClip GunSound;
   public AudioClip ReloadingSound;
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
}
