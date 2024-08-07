using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIManager;

public class Highlighter : MonoBehaviour
{
    [SerializeField] GameEvent0ParamSO onESCPressed;
    [SerializeField] string hightlightedObjectTag;

    public List<Transform> hightlightedObjects = new List<Transform>();

    private void OnEnable()
    {
        onESCPressed.onEventRaised += ClearSelected;

    }
    private void OnDisable()
    {
        onESCPressed.onEventRaised -= ClearSelected;
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        Debug.Log("esc pressed");
    //        foreach (var item in hightlightedObjects)
    //        {
    //            item.gameObject.SetActive(false);
    //            hightlightedObjects.Remove(item);
    //            Debug.Log("cleared");
    //        }
    //    }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(hightlightedObjectTag))
        {
            foreach (Transform child in other.transform)
            {
                if (child.name == "Highlight" && !child.gameObject.activeSelf)
                {
                    hightlightedObjects.Add(child);
                    child.gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(hightlightedObjectTag))
        {
            foreach (Transform child in other.transform)
            {
                if (child.name == "Highlight" && child.gameObject.activeSelf)
                {
                    hightlightedObjects.Remove(child);
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    public void ClearSelected()
    {
        foreach (var item in hightlightedObjects)
        {
            item.gameObject.SetActive(false);
            Debug.Log("cleared");
        }
        hightlightedObjects.Clear();
    }
}
