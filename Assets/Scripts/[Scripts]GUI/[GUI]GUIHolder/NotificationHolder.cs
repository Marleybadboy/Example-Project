using HCC.GUI;
using HCC.StaticEvents;
using HCC.Structs.GUI;
using Sirenix.OdinInspector;

public class NotificationHolder : GUIHolder<NotificationManager,Notification> 
{
    #region Fields
    #endregion

    #region Properties
    #endregion

    #region Functions
    public override void Start()
    {
        Manager.SetData(DataParam);

        StaticEvents.OnItemCollected += Manager.ShowNotification;
        StaticEvents.OnShowMassege += Manager.ShowNotification;
    }
    private void OnDestroy()
    {
        StaticEvents.OnItemCollected -= Manager.ShowNotification;
        StaticEvents.OnShowMassege -= Manager.ShowNotification;
    }
    #endregion

    #region Methods
    #endregion 
}
