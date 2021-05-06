using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingPanel : MonoBehaviour
{
   
    public void BackToHome()
    {
        StartCoroutine("Inactivate");
        GameManager.instance.InHouseScene();
        
    }

    IEnumerator Inactivate()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
        
    }
}
