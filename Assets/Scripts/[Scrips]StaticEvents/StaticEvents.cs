using System;

namespace HCC.StaticEvents
{
    public class StaticEvents
    {
        #region Events
        public static event Action<string> OnItemCollected;
        public static void ExecuteItemCollected(string message) { OnItemCollected?.Invoke(message);}
        #endregion
    }
}
