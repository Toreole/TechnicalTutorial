using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorial
{
    public static class CustomEvent
    {
        public static Action OnPlayerDied;
        public static Action<int> OnPlayerGotScore;
        public static Action<int> OnPlayerLoseScore;
        public static Action OnDoSomethingElse;
    }

    public static class CustomEvent<TAction>
    {
        private static event Action<TAction> DoStuff;

        public static void Invoke(TAction param) => DoStuff?.Invoke(param);

        public static void AddListener(Action<TAction> listener)
        {
            DoStuff += listener;
        }
        public static void RemoveListener(Action<TAction> listener)
        {
            DoStuff -= listener;
        }
    }
    public class OnPlayerDied
    {

    }
    public class OnPlayerGotScore
    {
        public int score;
    }
    public class OnPlayerLoseScore
    {
        public int score;
    }
}
