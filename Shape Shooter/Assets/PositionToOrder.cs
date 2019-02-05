using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class PositionToOrder : MonoBehaviour
{
    SortingGroup group;

    private void Update() {
        if (group == null) group = GetComponent<SortingGroup>();
        group.sortingOrder = -Mathf.FloorToInt(transform.position.y * 10);
    }
}
