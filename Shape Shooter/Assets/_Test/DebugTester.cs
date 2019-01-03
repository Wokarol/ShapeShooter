using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTester : MonoBehaviour
{
    public DebugBlock FirstBlock { get; private set; } = new DebugBlock("Test");
    private void Start() {
        StartCoroutine(TestCoroutine());
    }

    private IEnumerator TestCoroutine() {
        FirstBlock.Define("State");
        while (true) {
            FirstBlock.Change("State", "Thinking");
            yield return new WaitForSeconds(0.4f);
            FirstBlock.Change("State", "Walking");
            FirstBlock.Define("Target");
            FirstBlock.Change("Target", "Shop");
            yield return new WaitForSeconds(0.1f);
            FirstBlock.Change("Target", "Armory");
            yield return new WaitForSeconds(0.1f);
            FirstBlock.Change("Target", "House");
            yield return new WaitForSeconds(0.1f);
            FirstBlock.Undefine("Target"); 
        }
    }
}
