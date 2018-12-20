using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol {
	public abstract class GenericVariable<T> : ScriptableObject {
		public T Value;
	}
}