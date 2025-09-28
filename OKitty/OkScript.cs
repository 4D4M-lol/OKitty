// Imports

using System.Collections.ObjectModel;
using static OKitty.OkInput;
using static OKitty.OkInstance;
using static OKitty.OkMath;

namespace OKitty;

// OkScript

public static class OkScript
{
    // Classes
    
    public static class OInstanceEvents
    {
        // Events
        
        public delegate void OnChildAdded(IOInstance child);
        public delegate void OnChildRemoved(IOInstance child);
    }
    
    public static class OWindowEvents
    {
        // Events
        
        public delegate void OnInitialization();
        public delegate void OnStart();
        public delegate void OnUpdate(double deltaTime);
        public delegate void OnEnd(bool closed);
        public delegate void OnResize(OVector2<int> size);
        public delegate void OnMove(OVector2<int> position);
    }

    public static class OKeyboardEvents
    {
        // Events
        
        public delegate void OnInitialization();
        public delegate void OnKeyDown(OKeyboard.OKeyboardKey key, OKeyboard.OModifierKey modifier);
        public delegate void OnKeyUp(OKeyboard.OKeyboardKey key, OKeyboard.OModifierKey modifier);
    }

    public static class OMouseEvents
    {
        // Events

        public delegate void OnInitialization();
        public delegate void OnButtonDown(OMouse.OMouseButton button, OVector2<float> position, byte clicks);
        public delegate void OnButtonUp(OMouse.OMouseButton button, OVector2<float> position, byte clicks);
        public delegate void OnScroll(OVector2<float> scrolled, OVector2<float> position, bool flipped);
        public delegate void OnMove(OVector2<float> position);
    }

    public static class ODebugger
    {
        // Functions

        public static void Log(object value)
        {
            if (!OConfigs.Log)
                return;

            DateTime now = DateTime.Now;
            
            Console.ResetColor();
            Console.Write($"[i] @ {now.Hour:00}:{now.Minute:00}:{now.Second:00} => {value}");
        }

        public static void Inform(object value)
        {
            if (!OConfigs.Log)
                return;

            DateTime now = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Blue;
            
            Console.Write($"[i] @ {now.Hour:00}:{now.Minute:00}:{now.Second:00} => {value}");
        }

        public static void Warn(object value)
        {
            if (!OConfigs.Log)
                return;

            DateTime now = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Yellow;
            
            Console.Write($"[i] @ {now.Hour:00}:{now.Minute:00}:{now.Second:00} => {value}");
        }

        public static void Error(object value)
        {
            if (!OConfigs.Log)
                return;

            DateTime now = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Red;
            
            Console.Write($"[i] @ {now.Hour:00}:{now.Minute:00}:{now.Second:00} => {value}");
        }

        public static void Throw(Exception exception)
        {
            if (OConfigs.Debug)
                Error(exception.Message);
            else
                throw exception;
        }
    }
}