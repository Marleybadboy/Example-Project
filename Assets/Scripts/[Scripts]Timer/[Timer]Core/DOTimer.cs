using DG.Tweening;

public class DOTimer
{
    #region Fields
    private float _time;

    public delegate void TimerCallback();
    #endregion

    #region Properties
    #endregion

    #region Functions
    #endregion

    #region Methods
    public DOTimer(float time, TimerCallback callback) 
    { 
        _time = time;

        ActiveTimer(callback);   
    }

    private void ActiveTimer(TimerCallback callback) 
    {
        DOVirtual.Float(0, 1, _time, null).OnComplete(() => callback());
    }

    #endregion 
}
