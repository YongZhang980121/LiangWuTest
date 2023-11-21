using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Content : MonoBehaviour
{
    public TMP_Text contentDescription;
    public List<Transform> upArrows;
    public Transform downArrow;
    public bool positive;

    public void Reset()
    {
        foreach (var upArrow in upArrows)
        {
            upArrow.gameObject.SetActive(false);
        }
        downArrow.gameObject.SetActive(false);
    }
}
