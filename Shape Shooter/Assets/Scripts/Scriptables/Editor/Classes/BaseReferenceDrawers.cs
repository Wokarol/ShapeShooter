using UnityEditor;
namespace Wokarol {
	[CustomPropertyDrawer(typeof(FloatVariableReference))]
	[CustomPropertyDrawer(typeof(StringVariableReference))]
	[CustomPropertyDrawer(typeof(IntVariableReference))]
	[CustomPropertyDrawer(typeof(BoolVariableReference))]
	public class ReferenceDrawer : GenericReferenceDrawer { }
}