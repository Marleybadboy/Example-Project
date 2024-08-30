using System;

namespace HCC.StaticEvents
{
    public class StaticEvents
    {
        #region Events
        public static event Action<string> OnItemCollected;
        public static void ExecuteItemCollected(string message) { OnItemCollected?.Invoke(message);}

        public static event Action<string> OnShowMassege;
        public static void ExecuteShowMessage(string message) { OnShowMassege?.Invoke(message); }
        #endregion
    }
}
