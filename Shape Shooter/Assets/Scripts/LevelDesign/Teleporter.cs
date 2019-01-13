using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Transform[] AffectedTransforms;

    public void Teleport(Vector3 target) {
        foreach (var trans in AffectedTransforms) {
            Vector3 offset = trans.position - transform.position;
            trans.position = target + offset;
        }
    }
}
