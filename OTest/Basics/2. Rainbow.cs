// 2. Rainbow
// This example demonstrates how to animate the window's background color
// using a smooth rainbow effect. The color changes every frame based on time.

using OKitty;
using static OKitty.OkScript;
using static OKitty.OkStyling;

namespace OTest.Basics;

public class Rainbow
{
    private static OWindow window;

    public static void Start()
    {
        // Create window options (using defaults except for the title).
        
        OWindowOptions options = new OWindowOptions()
        {
            Name = "Rainbow"  // Title of the window
        };

        // Create the window.
        
        window = new OWindow(options);

        // Subscribe to the update event.
        // This runs every frame, allowing smooth animation.
        
        window.OnUpdate += Update;

        // Initialize SDL and create the actual OS window.
        
        window.Initialize();

        // Begin the main application loop.
        
        window.Run();

        // Clean up window resources when the loop ends.
        
        window.Dispose();
    }

    // Update is called every frame.
    // "_" is the delta time in milliseconds (unused here).
    
    private static void Update(double _)
    {
        // Convert ticks to seconds for smooth animation.
        
        double now = Ticks / 1000.0;

        // Generate three sine waves offset by 120° each.
        // This produces a continuous rainbow cycle.
        
        double red = 0.5 + 0.5 * Math.Sin(now);
        double green = 0.5 + 0.5 * Math.Sin(now + Math.PI * 2 / 3);
        double blue = 0.5 + 0.5 * Math.Sin(now + Math.PI * 4 / 3);

        // Create a color using the normalized RGB constructor.
        
        OColor color = new OColor(1.0, red, green, blue);

        // Apply the color to the window's background.
        
        window.BackgroundColor = color;
    }
}