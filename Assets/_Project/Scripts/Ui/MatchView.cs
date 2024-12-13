using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace com.ColorSelect
{
    public class MatchView : UiPanel
    {
        [SerializeField] private TextMeshProUGUI _colorText;
        [SerializeField] private Image _colorImage;

        public void SetColor(ColorDescription colorDescription)
        {
            _colorText.text = string.Format(Msg.SelectColor, colorDescription.Name);
            _colorImage.color = colorDescription.Color;
        }
    }
}