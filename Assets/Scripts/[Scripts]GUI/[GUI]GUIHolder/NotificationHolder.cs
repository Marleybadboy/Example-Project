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
    }
    private void OnDestroy()
    {
        StaticEvents.OnItemCollected -= Manager.ShowNotification;
    }
    #endregion

    #region Methods
    [Button("Show",ButtonSizes.Large)]
    private void Show() 
    {
        StaticEvents.ExecuteItemCollected("Kurwa jesttttt!!!");
    }
    #endregion 
}
