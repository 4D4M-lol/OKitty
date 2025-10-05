using System.Collections.ObjectModel;
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
            Name = "OKitty"
        };
        OWindow window = new OWindow(options);
        
        window.Initialize();
        window.Run();
        window.Dispose();
    }
}