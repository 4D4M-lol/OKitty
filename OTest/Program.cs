using OKitty;
using static OKitty.OkInput;

namespace OTest;

class Program
{
    static void Main(string[] args)
    {
        OWindowOptions options = new OWindowOptions()
        {
            
        };
        OWindow window = new OWindow(options);
        
        window.Initialize();
        window.Run();
        window.Dispose();
    }
}