// 1. Window
// Basic example showing how to create a window and use its main lifecycle events.

using OKitty;
using static OKitty.OkScript;

namespace OTest.Basics;

public static class Window
{
    public static void Start()
    {
        // Configure the window (title, size, etc.)
        
        OWindowOptions options = new OWindowOptions()
        {
            Name = "Window",
            BackgroundColor = OkStyling.OColor.Black,
            PresentAfterCallback = true
        };

        // Create the window using the given options
        
        OWindow window = new OWindow(options);

        // Window Events
        // These events let you run code at specific points
        // during the window’s lifecycle.

        // Called once, before the main loop begins
        
        window.OnStart += () =>
        {
            ODebugger.Log("Window has started.\n");
        };

        // Called every frame. deltaTime = time between frames (ms)
        
        window.OnUpdate += (double deltaTime) =>
        {
            ODebugger.Log($"Delta time: {deltaTime} ms\n");
            
            window.Renderer.RenderPoint(new OkMath.OVector2<float>(400, 300), OkStyling.OColor.White);
            window.Renderer.RenderLine(new OkMath.OVector2<float>(100,  100), new OkMath.OVector2<float>(100, 300), OkStyling.OColor.White);
            window.Renderer.RenderLine(new OkMath.OVector2<float>(100,  300), new OkMath.OVector2<float>(250, 300), OkStyling.OColor.White);
            window.Renderer.RenderLine(new OkMath.OVector2<float>(100,  100), new OkMath.OVector2<float>(250, 300), OkStyling.OColor.White);
            window.Renderer.Present();
        };

        // Called when the window is about to stop
        
        window.OnEnd += (bool closedByUser) =>
        {
            ODebugger.Log(closedByUser
                ? "Window ended (closed by user).\n"
                : "Window ended (stopped programmatically).\n");
        };

        // Called after Dispose(), cleans up resources
        
        window.OnDispose += () =>
        {
            ODebugger.Log("Window disposed.\n");
        };

        // Prepare internal systems and SDL
        
        window.Initialize();

        // Enter the main loop (keeps the window running)
        
        window.Run();

        // Clean up resources after the main loop stops
        
        window.Dispose();
    }
}