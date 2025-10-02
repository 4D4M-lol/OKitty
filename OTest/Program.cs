using OKitty;

namespace OTest;

class Program
{
    static void Main(string[] args)
    {
        OWindowOptions options = new OWindowOptions()
        {
            
        };
        OWindow window = new OWindow(options);
        
        window.BackgroundColor = OkStyling.OColor.FromArgb(125, 255, 255, 255);
        
        window.Initialize();
        window.Run();
        window.Dispose();
    }
}