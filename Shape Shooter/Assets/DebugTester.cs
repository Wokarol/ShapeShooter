using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTester : MonoBehaviour
{

    [SerializeField] float value;
    [SerializeField] DebugBlock block;

    private void Update() {
        block.Clear();
        block.Add($"Value",  $"{value}");
        block.Add("Name", $"{name}");
    }
}
