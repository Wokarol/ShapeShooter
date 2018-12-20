using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol {
	[System.Serializable]
	public abstract class GenericVariableReference<T, VarT>
		where VarT : GenericVariable<T> {

		public T Value {
			get {
				return (UseConstant) ? ConstantValue : ScriptableVariable.Value;
			}
			set {
				if (UseConstant) {
					ConstantValue = value;
				} else {
					ScriptableVariable.Value = value;
				}
			}
		}

		public bool UseConstant;

		public VarT ScriptableVariable;
		public T ConstantValue;

	}
}