using UnityEngine;

namespace com.ColorSelect
{
    [CreateAssetMenu(fileName = "Config", menuName = "ColorSelect/Config", order = 1)]
    public class Config : ScriptableObject
    {
        [field: SerializeField] public LogLevel LogLevel { get; private set; }
    }
}