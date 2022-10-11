using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    private void OnMouseEnter()
    {
        gameObject.transform.localScale = new Vector3(8.5f, 8.5f, 1);
    }

    private void OnMouseExit()
    {
        gameObject.transform.localScale = new Vector3(8f, 8f, 1);
    }

    private void OnMouseUp()
    {
        Debug.Log("Click");
    }
}
