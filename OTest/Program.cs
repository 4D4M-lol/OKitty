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
            string value = window.Keyboard.GetValue(key, modifier);

            window.Name += value;
        };
        
        window.Initialize();
        window.Run();
        window.Dispose();
    }
}