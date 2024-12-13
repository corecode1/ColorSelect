using UnityEngine;

namespace com.ColorSelect
{
    [CreateAssetMenu(fileName = "Config", menuName = "ColorSelect/Config", order = 1)]
    public class Config : ScriptableObject
    {
        [field: SerializeField] public LogLevel LogLevel { get; private set; }
        [field: SerializeField] public int SelectablesCount { get; private set; } = 3;
        [field: SerializeField] public float SpacePerSelectable { get; private set; } = 1.2f;
        [field: SerializeField] public ColorDescription[] ColorDescriptions { get; private set; }
        [field: SerializeField] public ColorDescription WrongSelectionFxColor { get; private set; }
        [field: SerializeField] public int WrongSelectionFxDurationMs { get; private set; } = 1000;
        [field: SerializeField] public int AsyncWaitsFrameSkips { get; private set; } = 3;
        [field: SerializeField] public int ExpectedMatchesPoolSize { get; private set; } = 100;
    }
}