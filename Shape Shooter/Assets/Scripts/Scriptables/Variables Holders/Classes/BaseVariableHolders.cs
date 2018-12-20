using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol
{
	[System.Serializable]
	public class FloatVariableReference : GenericVariableReference<float, FloatVariable>
	{
	}

	[System.Serializable]
	public class StringVariableReference : GenericVariableReference<string, StringVariable>
	{
	}

	[System.Serializable]
	public class BoolVariableReference : GenericVariableReference<bool, BoolVariable>
	{
	}

	[System.Serializable]
	public class IntVariableReference : GenericVariableReference<int, IntVariable>
	{
	}
}