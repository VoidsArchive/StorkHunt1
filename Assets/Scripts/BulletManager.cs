using System.Collections;
using TMPro;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    /*
     * ammo variable
     * check if the player is reloading
     * total mag capacity
     * cancel shooting when player is out of bullets
     * reload when the player is out of bullets
     * reload time
     * only be able to shoot when ammo > 0 and not reloading
     * add a reload button that can be pressed to reload the gun
     */

    public TMP_Text bulletText;
    public int bulletCount = 8;
    public int maxBullets = 8;
    public bool isReloading = false;
    public float reloadTime = 2f;

    public void Update()
    {
        UpdateBullets();
    }

    private void UpdateBullets()
    {
        bulletText.text = "Ammo: " + GetBulletCount() + "/" + GetMaxBullets() + " " + ReloadStatusAsText();
    }

    public bool Shoot()
    {
        if (IsOkayToShoot())
        {
            bulletCount--;
            Debug.Log("Shot fired! Bullets left: " + bulletCount);
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
