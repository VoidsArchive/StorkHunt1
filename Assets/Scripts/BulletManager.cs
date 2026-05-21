using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletManager : MonoBehaviour
{
    public Sounds Sounds;
    public TMP_Text bulletText;
    public int bulletCount = 8;
    public int maxBullets = 8;
    public bool isReloading = false;
    public float reloadTime = 2f;

    public void Update()
    {
        UpdateBullets();
        Keyboard keyboard = Keyboard.current;
        if (keyboard.rKey.wasPressedThisFrame && Game.isGameActive)
        {
            Debug.Log("pressing R");
            ReloadBullets();

        }
    }

    private void UpdateBullets()
    {
        bulletText.text = "Ammo: " + GetBulletCount() + "/" + GetMaxBullets() + " " + ReloadStatusAsText();
        bulletText.color = isReloading ? Color.red : Color.white;


    }

    public bool Shoot()
    {
        if (IsOkayToShoot())
        {
            bulletCount--;
            Debug.Log("Shot fired! Bullets left: " + bulletCount);
            Sounds.PlayGunSound();
            return true;
        }

        return false;
    }

    private bool CheckBulletCount()
    {
        if (bulletCount > 0)
        {
            return true;
        }
        return false;
    }

    private bool IsOkayToShoot()
    {
        if (!Game.isGameActive)
        {
            bulletCount = maxBullets;
            return false;
        }
        if (!isReloading && CheckBulletCount())
        {
            return true;
        }
        else if (isReloading)
        {
            Debug.Log("Can't shoot while reloading!");
            return false;
        }
        else if (isReloading && !CheckBulletCount())
        {
            ReloadBullets();
        }
        else if (!CheckBulletCount())
        {
            ReloadBullets();
        }
        return false;
    }
    
    IEnumerator CountdownUntilReloaded()
    {
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
        bulletCount = maxBullets;
        Debug.Log("Finished reloading");
    }
    
    public void ReloadBullets()
    {
        Debug.Log("Currently reloading");
        Sounds.PlayReloadingSound();
        isReloading = true;
        StartCoroutine(CountdownUntilReloaded());
    }
    
    public int GetBulletCount()
    {
        return bulletCount;
    }
    
    public int GetMaxBullets(){
        return maxBullets;
    }

    public bool GetIsReloading()
    {
        return isReloading;
    }

    public string ReloadStatusAsText()
    {
        if (isReloading)
        {
            return "(Reloading)";
        }
        return "";
    }
}
