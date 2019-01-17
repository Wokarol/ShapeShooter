using UnityEngine;

namespace Wokarol.AI
{
    public interface ICanModifyShootingInput
    {
        Vector2 AimDirection { get; set; }
        bool Shoot { get; set; }
    }
}