using OKitty;
using static OKitty.OkInput;
using static OKitty.OkInstance;
using static OKitty.OkMath;
using static OKitty.OkScript;
using static OKitty.OkSecurity;
using static OKitty.OkStyling;

namespace OTest;

class Program
{
    static void Main(string[] args)
    {
        OWindowOptions options = new OWindowOptions()
        {
            Name = ""
        };
        OWindow window = new OWindow(options);

        window.Keyboard.OnKeyDown += (OKeyboard.OKeyboardKey key, OKeyboard.OModifierKey modifier) =>
        {
            
            if (window.Keyboard.IsModifier(key))
                return;

            if (key == OKeyboard.OKeyboardKey.Backspace)
            {
                if (window.Name != "")
                    window.Name = window.Name.Remove(window.Name.Length - 1);
                
                return;
            }

            if (key == OKeyboard.OKeyboardKey.Space)
            {
                window.Name += " ";
                
                return;
            }
            
            string value = window.Keyboard.GetValue(key, modifier);

            window.Name += value;
        };

        Console.WriteLine($"{window.Icon} OWindow");
        window.Initialize();
        window.Run();
        window.Dispose();
    }
}