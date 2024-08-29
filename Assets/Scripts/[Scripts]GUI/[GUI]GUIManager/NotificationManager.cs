using DG.Tweening;
using HCC.Interfaces;
using HCC.Structs.GUI;
using Unity.Mathematics;

namespace HCC.GUI
{
    public class NotificationManager : GUIManager
    {
        #region Fields

        private Notification _notificationData;
        private Sequence _dotweenSequence;

        #endregion

        #region Properties
        #endregion

        #region Functions
        #endregion

        #region Methods
        public override void CloseUI()
        {
            Restore();
        }

        public override void OpenUI()
        {           
            Restore();

            MoveWayPointsSequence();
        }

        public void ShowNotification(string message) 
        { 
            _notificationData.NotificationText = message;

            OpenUI();
        }

        public override void SetData(IUIDataHolder holder)
        {
            _notificationData = (Notification)holder;
        }

        private void Restore() 
        {
            _notificationData.CanvasAlpha = 0;
            _notificationData.NotificationObjPos = float2.zero;

            if (_dotweenSequence == null) return;

            _dotweenSequence.Kill();
        }

        private Sequence MoveWayPointsSequence() 
        {
            Sequence seq = DOTween.Sequence();

            seq.Prepend(WaypointTween().OnStart( () => FadeCanvas(1f)).OnWaypointChange(OnPointComplete)).OnComplete(CloseUI);

            _dotweenSequence = seq;

            return seq;
        }

        private Tween WaypointTween() 
        {
            Tween tween = _notificationData.NotificationObj.DOLocalPath(_notificationData.CreateWaypoints(), _notificationData.Duration)
                            .OnWaypointChange(OnPointComplete);

            return tween;
        }

        private Tween FadeCanvas(float fadeTo) 
        { 
            float duration = _notificationData.Duration/2;

            return _notificationData.CanvasGroup.DOFade(fadeTo, duration);
        
        }

        private void OnPointComplete(int value) 
        { 
            if(value < 1) return; 

            FadeCanvas(0f);       
        }

       
        #endregion

    }
}
