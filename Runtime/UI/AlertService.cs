using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace StudioName.Runtime.UI {
    /// <inheritdoc />
    /// <summary>
    /// Allows us to not have to call AlertService{AlertService} every time
    /// </summary>
    [Singleton("",true)]
    public class AlertService : AlertService<AlertService> { }
    
    /// <summary>
    /// A component that allows us to quickly display alerts to users with a image and message
    /// </summary>
    //TODO Honestly this should probably just be a class not s singleton
    public class AlertService<T> : Singleton<T> where T : MonoBehaviour  {
        //TODO Add enum to AlertManager for good, bad, and informational Alerts.
        //TODO Add an queue system to the AlertManager so multiple events can be set at once
        [Tooltip("The image component that holds our changeable alert image")]
        public Image alertImageComponent;
        [Tooltip("The text element that holds our changeable alert message")]
        public Text alertText;
        [Tooltip("The panel that holds all UI elements for our alert. (alertText & alertImageRenderer)")]
        public RectTransform alertPanel;
        /// <summary>
        /// The position the alert starts from, used to animate properly
        /// </summary>
        public Position alertStartingPosition;

        /// <summary>
        /// Called when our Alert presentation animation has completed, and our Alert is fully on screen.
        /// </summary>
        public static event Action OnAlertPresented;
        /// <summary>
        /// Called when our Alert dismissal animation has completed, and our Alert is fully off screen.
        /// </summary>
        public static event Action OnAlertDismissed;
        
        /// <summary>
        /// The default image to be used when displaying an alert.
        /// </summary>
        private Sprite DefaultImage { get; set; }
        /// <summary>
        /// The default time to display our alert.
        /// </summary>
        private float DefaultDisplayTime { get; set; } 
        /// <summary>
        /// The time it will take for the alert to enter / exit.
        /// </summary>
        private float DefaultTransitionTime { get; set; }
        /// <inheritdoc cref="IsVisible"/>
        private bool isVisible;
        /// <inheritdoc cref="IsAnimating"/>
        private bool isAnimating;

        public AlertService() {
            DefaultDisplayTime = 1.5f;
            DefaultTransitionTime = 0.5f;
        }

        /// <summary>
        /// Initialize the AlertService with predefined parameters
        /// </summary>
        /// <param name="defaultDisplayTime">Default time the alert will display</param>
        /// <param name="defaultTransitionTime">Default time it will take to transition the alert</param>
        public void Initialize(float defaultDisplayTime, float defaultTransitionTime) {
            DefaultDisplayTime = defaultDisplayTime;
            DefaultTransitionTime = defaultTransitionTime;
        }

        /// <summary>
        /// Display the alert to the user with the given message.
        /// </summary>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="alertImage">The image to be used for the alert. If empty will use default.</param>
        public virtual void Alert(string message, Sprite alertImage = null) {
            alertImageComponent.sprite = alertImage ? alertImage : DefaultImage;
            alertText.text = message;

            if (DefaultDisplayTime < 0) {
                isAnimating = true;
                alertPanel.DOPivotY(StartPositionToAnimationPosition(true), DefaultTransitionTime)
                    .OnComplete(() => {
                        isAnimating = false;
                        isVisible = true;
                        OnAlertPresented?.Invoke();
                    });
                return;
            }
            
            //Play up and down animation for alert
            isAnimating = true;
            Sequence alertSequence = DOTween.Sequence();
            alertSequence.Append(alertPanel.DOPivotY(StartPositionToAnimationPosition(true), DefaultTransitionTime))
                .AppendCallback(() => {
                    isAnimating = false;
                    isVisible = true;
                    OnAlertPresented?.Invoke();    
                })
                .AppendInterval(DefaultDisplayTime)
                .AppendCallback(() => {
                    isAnimating = true;
                })
                .Append(alertPanel.DOPivotY(StartPositionToAnimationPosition(false), DefaultTransitionTime))
                .OnComplete(() => {
                    isAnimating = false;
                    isVisible = false;
                    OnAlertDismissed?.Invoke();
                });
            alertSequence.Play();
        }

        /// <summary>
        /// Dismiss an alert that will not dismiss eventually by it's own
        /// </summary>
        public virtual void Dismiss() {
            if (Dismissable) {
                isAnimating = true;
                alertPanel.DOPivotY(StartPositionToAnimationPosition(false), DefaultTransitionTime)
                .OnComplete(() => {
                    isAnimating = false;
                    isVisible = false;
                    OnAlertDismissed?.Invoke();
                });
            }
        }

        protected override void SingletonAwake(){
            DefaultImage = alertImageComponent.sprite;
        }

        /// <summary>
        /// Converts <see cref="alertStartingPosition"/> to the appropriate end animation position
        /// <param name="toDisplayPosition">Should the return value be the position the alert is displayed or hidden?</param>
        /// </summary>
        private int StartPositionToAnimationPosition(bool toDisplayPosition) {
            switch (alertStartingPosition) {
                case Position.Top:
                    return (toDisplayPosition)? 1 : 0;
                case Position.Bottom:
                    return (toDisplayPosition)? 0 : 1;
                case Position.Right:
                    return (toDisplayPosition)? 1 : 0;
                case Position.Left:
                    return (toDisplayPosition)? 0 : 1;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        /// <summary>
        /// Can be dismissed because duration is not infinite
        /// </summary>
        private bool Dismissable {
            get {
                return DefaultDisplayTime < 0;
            }
        }

        /// <summary>
        /// Is the alert visible?
        /// </summary>
        public bool IsVisible {
            get { return isVisible; }
        }

        /// <summary>
        /// Is the alert animating in or out of a transition?
        /// </summary>
        public bool IsAnimating {
            get { return isAnimating; }
        }
    }

}
