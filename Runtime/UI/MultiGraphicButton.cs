using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Game_Assets.Scripts.Runtime.UI
{
    /// <summary>
    /// A button that will change color for all graphic children when disabled or enabled
    /// TODO support all other modes, not just color tint
    /// </summary>
    public class MultiGraphicButton : Button
    {
        private Graphic[] m_graphics;

        protected Graphic[] Graphics
        {
            get
            {
                return m_graphics ?? (m_graphics = targetGraphic.transform.GetComponentsInChildren<Graphic>());
            }
        }

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            Color color;
            switch (state)
            {
                case SelectionState.Normal:
                    color = colors.normalColor;
                    break;
                case SelectionState.Highlighted:
                    color = colors.highlightedColor;
                    break;
                case SelectionState.Pressed:
                    color = colors.pressedColor;
                    break;
                case SelectionState.Disabled:
                    color = colors.disabledColor;
                    break;
                case SelectionState.Selected:
                    color = colors.selectedColor;
                    break;
                default:
                    color = Color.black;
                    break;
            }

            if (!gameObject.activeInHierarchy)
            {
                return;
            }

            //TODO for now only supports color tint
            switch (transition)
            {
                case Transition.ColorTint:
                    ColorTween(color * colors.colorMultiplier, instant);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Tweens a color from one color to another for all our <see cref="Graphics"/> defined
        /// </summary>
        /// <param name="targetColor">The target color to tween to.</param>
        /// <param name="instant">Should we tween instantly</param>
        private void ColorTween(Color targetColor, bool instant)
        {
            if (targetGraphic == null)
            {
                return;
            }

            foreach (var g in Graphics)
            {
                g.CrossFadeColor(targetColor, (!instant) ? colors.fadeDuration : 0f, true, true);
            }
        }
    }
}