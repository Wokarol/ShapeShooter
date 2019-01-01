using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DebugTester : MonoBehaviour
{
    [SerializeField] DebugBlock firstBlock;
    [SerializeField] DebugBlock secondBlock;
    private void Start() {
        EditorUtility.SetDirty(this);

        firstBlock.OverrideName = "AI";
        secondBlock.OverrideName = ":P";
        StartCoroutine(TestCoroutine());
        StartCoroutine(QuickCoroutine());
    }

    private IEnumerator TestCoroutine() {
        firstBlock.Define("State");
        while (true) {
            firstBlock.Change("State", "Thinking");
            yield return new WaitForSeconds(0.4f);
            firstBlock.Change("State", "Walking");
            firstBlock.Define("Target");
            firstBlock.Change("Target", "Shop");
            yield return new WaitForSeconds(0.1f);
            firstBlock.Change("Target", "Armory");
            yield return new WaitForSeconds(0.1f);
            firstBlock.Change("Target", "House");
            yield return new WaitForSeconds(0.1f);
            firstBlock.Undefine("Target"); 
        }
    }

    private IEnumerator QuickCoroutine() {
        secondBlock.Define("Value");
        while (true) {
            secondBlock.Change("Value", UnityEngine.Random.value.ToString("F2"));
            yield return null;
        }
    }
}
