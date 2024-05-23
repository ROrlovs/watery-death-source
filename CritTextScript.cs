using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CritTextScript : MonoBehaviour
{

    GameObject critTextObj;
    float ogFontSize;
    // Start is called before the first frame update
    void Start()
    {
        critTextObj = gameObject.transform.Find("Canvas").gameObject.transform.Find("CritText").gameObject;
        ogFontSize = critTextObj.GetComponent<TextMeshProUGUI>().fontSize;
        critTextObj.SetActive(false);
    }

    private void Update() {
        if(critTextObj.activeInHierarchy){
            if(!(critTextObj.transform.localScale==new Vector3(0,0,0))){
                critTextObj.transform.localScale-=new Vector3(0.005f,0.005f,0.005f);
            }
            
            
        }
    }

    public void Crit(){
        StartCoroutine(ShowCritText());
    }

    IEnumerator ShowCritText(){
        Debug.Log("show critext");
        critTextObj.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        critTextObj.SetActive(false);
        critTextObj.transform.localScale=new Vector3(1,1,1);
    }
}
