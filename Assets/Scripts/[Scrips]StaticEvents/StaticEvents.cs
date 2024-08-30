using System;

namespace HCC.StaticEvents
{
   /* The class `StaticEvents` in C# contains static events `OnItemCollected` and `OnShowMessage` along
   with corresponding methods to execute these events. */
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
