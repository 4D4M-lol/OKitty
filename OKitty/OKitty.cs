// Imports

using static OKitty.OkInput;
using static OKitty.OkInstance;
using static OKitty.OkMath;
using static OKitty.OkScript;
using static OKitty.OkStyling;
using SDL3;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace OKitty;

// Main File

// Configs and Infos

public static class OConfigs
{
    // Enums

    [Flags]
    public enum ODebugMode
    {
        None = 0,
        Cli = 1 << 0,
        Gui = 1 << 1,
        All = Cli | Gui
    }

    [Flags]
    public enum OLogLevel
    {
        None = 0,
        Log = 1 << 0,
        Inform = 1 << 1,
        Warn = 1 << 2,
        Error = 1 << 3,
        All = Log | Inform | Warn | Error
    }

    // Configs

    public static ODebugMode DebugMode { get; set; } = ODebugMode.All;
    public static OLogLevel LogLevel { get; set; } = OLogLevel.All;
    public static bool Debug { get; set; } = true;
    public static bool Log { get; set; } = true;
}

public static class OInfos
{
    // Enums
    
    public enum OPlatform
    {
        Unknown = -1,
        AtariMiNt,
        FreeBsd,
        Haiku,
        Linux,
        MacOs,
        NetBsd,
        OpenBsd,
        Os2,
        QnxNeutrino,
        Solaris,
        Windows,
        WinGdk
    }

    public enum OPlatformTheme
    {
        Unknown = -1,
        Dark,
        Light
    }
    
    // Properties
    
    public static readonly string Author = "4D4M-lol";
    public static readonly string Version = "1.0.0";

    public static OPlatform Platform
    {
        get
        {
            Dictionary<string, OPlatform> platforms = new()
            {
                { "Atari MiNT", OPlatform.AtariMiNt },
                { "FreeBSD", OPlatform.FreeBsd },
                { "Haiku", OPlatform.Haiku },
                { "Linux", OPlatform.Linux },
                { "macOS", OPlatform.MacOs },
                { "NetBSD", OPlatform.NetBsd },
                { "OpenBSD", OPlatform.OpenBsd },
                { "OS/2", OPlatform.Os2 },
                { "QNX Neutrino", OPlatform.QnxNeutrino },
                { "Solaris", OPlatform.Solaris },
                { "Windows", OPlatform.Windows },
                { "WinGdk", OPlatform.WinGdk }
            };
            bool found = platforms.TryGetValue(SDL.GetPlatform(), out OPlatform platform);

            return found ? platform : OPlatform.Unknown;
        }
    }

    public static OPlatformTheme PlatformTheme
    {
        get
        {
            SDL.SystemTheme theme = SDL.SystemTheme.Unknown;

            if (SDL.IsMainThread())
                theme = SDL.GetSystemTheme();
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    theme = SDL.GetSystemTheme();
                }, IntPtr.Zero, false);
            
            return (OPlatformTheme)(theme - 1);
        }
    }
}

// Interfaces

public interface IORenderer
{
    // Enums
    
    public enum ORendererType
    {
        Software,
        OpenGl
    }
    
    // Properties
    
    public string Name { get; }
    public ORendererType Type { get; }
    public OWindow? Window { get; set; }
    
    // Methods

    public void ApplyConfig();
}

// Records

public record OOpenGlRendererOptions
{
    // Properties

    public OOpenGlRenderer.OOpenGlProfile? Profile { get; init; } = null;
    public OOpenGlRenderer.OOpenGlContextResetNotification? ContextResetNotification { get; init; } = null;
    public OOpenGlRenderer.OOpenGlContextFlag? ContextFlag { get; init; } = null;
    public OOpenGlRenderer.OOpenGlContextReleaseBehaviour ContextReleaseBehavior { get; init; } = OOpenGlRenderer.OOpenGlContextReleaseBehaviour.Flush;
    public int MajorVersion { get; init; } = 4;
    public int MinorVersion { get; init; } = 6;
    public int RedSize { get; init; } = 8;
    public int GreenSize { get; init; } = 8;
    public int BlueSize { get; init; } = 8;
    public int AlphaSize { get; init; } = 8;
    public int DepthSize { get; init; } = 24;
    public int StencilSize { get; init; } = 8;
    public int AccumRedSize { get; init; } = 0;
    public int AccumGreenSize { get; init; } = 0;
    public int AccumBlueSize { get; init; } = 0;
    public int AccumAlphaSize { get; init; } = 0;
    public int MultisampleBuffers { get; init; } = 1;
    public int MultisampleSamples { get; init; } = 4;
    public int BufferSize { get; init; } = 32;
    public int SwapInterval { get; init; } = 1;
    public bool AcceleratedVisual { get; init; } = true;
    public bool DoubleBuffer { get; init; } = true;
    public bool SrgbCapable { get; init; } = true;
    public bool Stereo { get; init; } = false;
}

public record OWindowOptions
{
    // Properties

    public IORenderer Renderer { get; init; } = new OSoftwareRenderer();
    public string Name { get; init; } = $"OKitty v{OInfos.Version} by {OInfos.Author}";
    public OVector2<int> Size { get; init; } = new OVector2<int>(800, 600);
    public OVector2<int> Position { get; init; } = new OVector2<int>(-1, -1);
    public OWindow.OWindowCloseOperation CloseOperation { get; init; } = OWindow.OWindowCloseOperation.Close;
    public OWindow.OWindowState State { get; init; } = OWindow.OWindowState.Normal;
    public OWindow.OWindowBorder Border { get; init; } = OWindow.OWindowBorder.Resizable;
    public OColor BackgroundColor { get; init; } = OColor.White;
    public int Delay { get; init; } = 16;
    public float Opacity { get; init; } = 1;
    public bool RenderWhileHidden { get; init; } = false;
    public bool Topmost { get; init; } = false;
    public bool Focusable { get; init; } = true;
    
    // To String

    public override string ToString()
    {
        string boolean = " " + (RenderWhileHidden ? "RenderWhileHidden " : "")
                             + (Topmost ? "Topmost " : "")
                             + (Focusable ? "Focusable" : "");

        boolean = boolean == " " ? "" : boolean;
        
        return $"\"{Name}\" {Size.X}x{Size.Y} {Position} {CloseOperation} {Border} {BackgroundColor} {Delay}ms {Opacity * 100}%{boolean.TrimEnd()}";
    }
}

// Classes

public class OSoftwareRenderer : IORenderer
{
    // Properties and Fields

    private OWindow? _window;
    
    public string Name => "OKitty";
    public IORenderer.ORendererType Type => IORenderer.ORendererType.Software;

    public OWindow? Window
    {
        get => _window;
        set
        {
            if (_window is OWindow)
            {
                ODebugger.Warn("Renderer is already parented to a window.");
                
                return;
            }

            if (value?.Renderer != this)
            {
                ODebugger.Warn("The provided window has a different renderer.");
                
                return;
            }

            _window = value;
        }
    }
    
    // Methods and Functions

    public OSoftwareRenderer()
    {
        
    }

    public void ApplyConfig()
    {
        
    }

    // To String

    public override string ToString()
    {
        return $"[SoftwareRenderer]";
    }
}

public class OOpenGlRenderer : IORenderer
{
    // Enums
    
    [Flags]
    public enum OOpenGlContextFlag
    {
        Debug = 1 << 0,
        ForwardCompatible = 1 << 1,
        RobustAccess = 1 << 2,
        Isolation = 1 << 3
    }

    public enum OOpenGlProfile
    {
        Core = 1 << 0,
        Compatibility = 1 << 1,
        Es = 1 << 2
    }

    public enum OOpenGlContextResetNotification
    {
        NoNotification,
        LoseContext
    }

    public enum OOpenGlContextReleaseBehaviour
    {
        None,
        Flush
    }
    
    // Properties and Fields

    private OWindow? _window;
    private OOpenGlProfile? _profile;
    private OOpenGlContextResetNotification? _contextResetNotification;
    private OOpenGlContextFlag? _contextFlag;
    private OOpenGlContextReleaseBehaviour _contextReleaseBehaviour;
    private int _majorVersion;
    private int _minorVersion;
    private int _redSize;
    private int _greenSize;
    private int _blueSize;
    private int _alphaSize;
    private int _depthSize;
    private int _stencilSize;
    private int _accumRedSize;
    private int _accumGreenSize;
    private int _accumBlueSize;
    private int _accumAlphaSize;
    private int _multisampleBuffers;
    private int _multisampleSamples;
    private int _bufferSize;
    private int _swapInterval;
    public bool _acceleratedVisual;
    public bool _doubleBuffer;
    public bool _srgbCapable;
    public bool _stereo;
    
    public string Name => "OpenGL";
    public IORenderer.ORendererType Type => IORenderer.ORendererType.OpenGl;

    public OWindow? Window
    {
        get => _window;
        set
        {
            if (_window is OWindow)
            {
                ODebugger.Warn("Renderer is already parented to a window.");
                
                return;
            }

            if (value?.Renderer != this)
            {
                ODebugger.Warn("The provided window has a different renderer.");
                
                return;
            }

            _window = value;
        }
    }

    public OOpenGlProfile? Profile
    {
        get => _profile;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _profile = value;
        }
    }
    
    public OOpenGlContextResetNotification? ContextResetNotification
    {
        get => _contextResetNotification;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _contextResetNotification = value;
        }
    }

    public OOpenGlContextFlag? ContextFlag
    {
        get => _contextFlag;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _contextFlag = value;
        }
    }
    
    public OOpenGlContextReleaseBehaviour ContextReleaseBehaviour
    {
        get => _contextReleaseBehaviour;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _contextReleaseBehaviour = value;
        }
    }

    public int MajorVersion
    {
        get => _majorVersion;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _majorVersion = value;
        }
    }

    public int MinorVersion
    {
        get => _minorVersion;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _minorVersion = value;
        }
    }

    public int RedSize
    {
        get => _redSize;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _redSize = value;
        }
    }

    public int GreenSize
    {
        get => _greenSize;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _greenSize = value;
        }
    }

    public int BlueSize
    {
        get => _blueSize;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _blueSize = value;
        }
    }

    public int AlphaSize
    {
        get => _alphaSize;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _alphaSize = value;
        }
    }

    public int DepthSize
    {
        get => _depthSize;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _depthSize = value;
        }
    }

    public int StencilSize
    {
        get => _stencilSize;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _stencilSize = value;
        }
    }

    public int AccumRedSize
    {
        get => _accumRedSize;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _accumRedSize = value;
        }
    }

    public int AccumBlueSize
    {
        get => _accumBlueSize;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _accumBlueSize = value;
        }
    }

    public int AccumGreenSize
    {
        get => _accumGreenSize;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _accumGreenSize = value;
        }
    }

    public int AccumAlphaSize
    {
        get => _accumAlphaSize;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _accumAlphaSize = value;
        }
    }
    
    public int MultisampleBuffers
    {
        get => _multisampleBuffers;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _multisampleBuffers = value;
        }
    }
    
    public int MultisampleSamples
    {
        get => _multisampleSamples;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _multisampleSamples = value;
        }
    }
    
    public int BufferSize
    {
        get => _bufferSize;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _bufferSize = value;
        }
    }
    
    public int SwapInterval
    {
        get => _swapInterval;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _swapInterval = value;
        }
    }

    public bool AcceleratedVisual
    {
        get => _acceleratedVisual;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _acceleratedVisual = value;
        }
    }

    public bool DoubleBuffer
    {
        get => _doubleBuffer;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _doubleBuffer = value;
        }
    }

    public bool SrgbCapable
    {
        get => _srgbCapable;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _srgbCapable = value;
        }
    }

    public bool Stereo
    {
        get => _stereo;
        set
        {
            if (_window?.Initialized ?? false)
            {
                ODebugger.Warn("OpenGL properties can only be set before the window was initialized.");
                
                return;
            }

            _stereo = value;
        }
    }
    
    // Methods and Functions

    public OOpenGlRenderer(OOpenGlRendererOptions options)
    {
        _profile = options.Profile;
        _contextResetNotification = options.ContextResetNotification;
        _contextFlag = options.ContextFlag;
        _contextReleaseBehaviour = options.ContextReleaseBehavior;
        _majorVersion = options.MajorVersion;
        _minorVersion = options.MinorVersion;
        _redSize = options.RedSize;
        _greenSize = options.GreenSize;
        _blueSize = options.BlueSize;
        _alphaSize = options.AlphaSize;
        _depthSize = options.DepthSize;
        _stencilSize = options.StencilSize;
        _accumRedSize = options.AccumRedSize;
        _accumGreenSize = options.AccumGreenSize;
        _accumBlueSize = options.AccumBlueSize;
        _accumAlphaSize = options.AccumAlphaSize;
        _multisampleBuffers = options.MultisampleBuffers;
        _multisampleSamples = options.MultisampleSamples;
        _bufferSize = options.BufferSize;
        _swapInterval = options.SwapInterval;
        _acceleratedVisual = options.AcceleratedVisual;
        _doubleBuffer = options.DoubleBuffer;
        _srgbCapable = options.SrgbCapable;
        _stereo = options.Stereo;
    }

    public void ApplyConfig()
    {
        if (_window is null)
        {
            ODebugger.Warn("Renderer must be parented to a window before applying config.");
            
            return;
        }

        if (_window.Initialized)
        {
            ODebugger.Warn("Renderer config can not be applied after the window was initialized.");
            
            return;
        }

        if (SDL.IsMainThread())
            Configure();
        else
            SDL.RunOnMainThread((IntPtr _) =>
            {
                Configure();
            }, IntPtr.Zero, false);
    }

    private void Configure()
    {
        if (_profile is not null)
            SDL.GLSetAttribute(SDL.GLAttr.ContextProfileMask, (int)_profile);
        
        if (_contextFlag is not null)
            SDL.GLSetAttribute(SDL.GLAttr.ContextFlags, (int)_contextFlag);
        
        if (_contextResetNotification is not null)
            SDL.GLSetAttribute(SDL.GLAttr.ContextResetNotification, (int)_contextResetNotification);

        SDL.GLSetAttribute(SDL.GLAttr.ContextReleaseBehavior, (int)_contextReleaseBehaviour);
        SDL.GLSetAttribute(SDL.GLAttr.ContextMajorVersion, _majorVersion);
        SDL.GLSetAttribute(SDL.GLAttr.ContextMinorVersion, _minorVersion);
        SDL.GLSetAttribute(SDL.GLAttr.RedSize, _redSize);
        SDL.GLSetAttribute(SDL.GLAttr.GreenSize, _greenSize);
        SDL.GLSetAttribute(SDL.GLAttr.BlueSize, _blueSize);
        SDL.GLSetAttribute(SDL.GLAttr.AlphaSize, _alphaSize);
        SDL.GLSetAttribute(SDL.GLAttr.DepthSize, _depthSize);
        SDL.GLSetAttribute(SDL.GLAttr.StencilSize, _stencilSize);
        SDL.GLSetAttribute(SDL.GLAttr.AccumRedSize, _accumRedSize);
        SDL.GLSetAttribute(SDL.GLAttr.AccumGreenSize, _accumGreenSize);
        SDL.GLSetAttribute(SDL.GLAttr.AccumBlueSize, _accumBlueSize);
        SDL.GLSetAttribute(SDL.GLAttr.AccumAlphaSize, _accumAlphaSize);
        SDL.GLSetAttribute(SDL.GLAttr.MultisampleBuffers, _multisampleBuffers);
        SDL.GLSetAttribute(SDL.GLAttr.MultisampleSamples, _multisampleSamples);
        SDL.GLSetAttribute(SDL.GLAttr.BufferSize, _bufferSize);
        SDL.GLSetSwapInterval(_swapInterval);
        SDL.GLSetAttribute(SDL.GLAttr.AcceleratedVisual, _acceleratedVisual ? 1 : 0);
        SDL.GLSetAttribute(SDL.GLAttr.DoubleBuffer, _doubleBuffer ? 1 : 0);
        SDL.GLSetAttribute(SDL.GLAttr.FrameBufferSRGBCapable, _srgbCapable ? 1 : 0);
        SDL.GLSetAttribute(SDL.GLAttr.Stereo, _stereo ? 1 : 0);
    }
    
    // To String

    public override string ToString()
    {
        return $"[OpenGLRenderer]";
    }
}

public class OWindow : IOPrototype
{
    // Enums
    
    public enum OWindowCloseOperation
    {
        None,
        Hide,
        Close,
        Confirm
    }

    public enum OWindowState
    {
        Normal,
        Minimized,
        Maximized,
        Fullscreen
    }

    public enum OWindowBorder
    {
        None,
        Fixed,
        Resizable
    }
    
    // Properties and Fields

    private IntPtr _sdlWindow;
    private IntPtr _sdlRenderer;
    private SDL.EventFilter _filter;
    private IORenderer _renderer;
    private OKeyboard _keyboard;
    private OMouse _mouse;
    private OStorage _storage;
    private OScenes _scenes;
    private Stopwatch _stopwatch;
    private string _name;
    private OVector2<int> _size;
    private OVector2<int> _position;
    private OWindowState _state;
    private OWindowBorder _border;
    private float _opacity;
    private bool _topmost;
    private bool _focusable;

    public IntPtr WindowHandle => _sdlWindow;
    public IntPtr RendererHandle => _sdlRenderer;
    public IORenderer Renderer => _renderer;
    public OKeyboard Keyboard => _keyboard;
    public OMouse Mouse => _mouse;
    public OStorage Storage => _storage;
    public OScenes Scenes => _scenes;
    public string Icon => "󰍹";
    public string InstanceName => "OWindow";
    public bool Initialized { get; private set; }
    public bool Running { get; private set; }
    public bool Visible { get; private set; }
    public bool RenderWhileHidden { get; set; }
    public int Delay { get; set; }
    public OWindowCloseOperation CloseOperation { get; set; }
    public OColor BackgroundColor { get; set; }

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            
            if (!Initialized)
                return;
            
            if (SDL.IsMainThread())
                SDL.SetWindowTitle(_sdlWindow, value);
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.SetWindowTitle(_sdlWindow, value);
                }, IntPtr.Zero, false);
        }
    }

    public OVector2<int> Size
    {
        get => _size;
        set
        {
            _size = value;
            
            if (!Initialized)
                return;

            if (SDL.IsMainThread())
                SDL.SetWindowSize(_sdlWindow, value.X, value.Y);
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.SetWindowSize(_sdlWindow, value.X, value.Y);
                }, IntPtr.Zero, false);
        }
    }

    public OVector2<int> Position
    {
        get => _position;
        set
        {
            _position = value;
            
            if (!Initialized)
                return;

            if (SDL.IsMainThread())
                SDL.SetWindowPosition(_sdlWindow, value.X, value.Y);
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.SetWindowPosition(_sdlWindow, value.X, value.Y);
                }, IntPtr.Zero, false);
        }
    }

    public OWindowState State
    {
        get => _state;
        set
        {
            _state = value;
            
            if (!Initialized)
                return;

            if (SDL.IsMainThread())
                switch (value)
                {
                    case OWindowState.Normal:
                        SDL.SetWindowFullscreen(_sdlWindow, false);
                        SDL.RestoreWindow(_sdlWindow);

                        break;
                    case OWindowState.Minimized:
                        SDL.SetWindowFullscreen(_sdlWindow, false);
                        SDL.MinimizeWindow(_sdlWindow);

                        break;
                    case OWindowState.Maximized:
                        SDL.SetWindowFullscreen(_sdlWindow, false);
                        SDL.MaximizeWindow(_sdlWindow);

                        break;
                    case OWindowState.Fullscreen:
                        SDL.SetWindowFullscreen(_sdlWindow, true);

                        break;
                }
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    switch (value)
                    {
                        case OWindowState.Normal:
                            SDL.SetWindowFullscreen(_sdlWindow, false);
                            SDL.RestoreWindow(_sdlWindow);
                        
                            break;
                        case OWindowState.Minimized:
                            SDL.SetWindowFullscreen(_sdlWindow, false);
                            SDL.MinimizeWindow(_sdlWindow);
                        
                            break;
                        case OWindowState.Maximized:
                            SDL.SetWindowFullscreen(_sdlWindow, false);
                            SDL.MaximizeWindow(_sdlWindow);
                        
                            break;
                        case OWindowState.Fullscreen:
                            SDL.SetWindowFullscreen(_sdlWindow, true);
                        
                            break;
                    }
                }, IntPtr.Zero, false);
            
            OnStateChanged?.Invoke(value);
        }
    }

    public OWindowBorder Border
    {
        get => _border;
        set
        {
            _border = value;
            
            if (!Initialized)
                return;
            
            if (SDL.IsMainThread())
                switch (value)
                {
                    case OWindowBorder.None:
                        SDL.SetWindowBordered(_sdlWindow, false);
                        
                        break;
                    case OWindowBorder.Fixed:
                        SDL.SetWindowBordered(_sdlWindow, true);
                        SDL.SetWindowResizable(_sdlWindow, false);
                        
                        break;
                    case OWindowBorder.Resizable:
                        SDL.SetWindowBordered(_sdlWindow, true);
                        SDL.SetWindowResizable(_sdlWindow, true);
                        
                        break;
                }
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    switch (value)
                    {
                        case OWindowBorder.None:
                            SDL.SetWindowBordered(_sdlWindow, false);
                        
                            break;
                        case OWindowBorder.Fixed:
                            SDL.SetWindowBordered(_sdlWindow, true);
                            SDL.SetWindowResizable(_sdlWindow, false);
                        
                            break;
                        case OWindowBorder.Resizable:
                            SDL.SetWindowBordered(_sdlWindow, true);
                            SDL.SetWindowResizable(_sdlWindow, true);
                        
                            break;
                    }
                }, IntPtr.Zero, false);
        }
    }

    public float Opacity
    {
        get => _opacity;
        set
        {
            _opacity = Math.Clamp(_opacity, 0, 1);
            
            if (!Initialized)
                return;

            if (SDL.IsMainThread())
                SDL.SetWindowOpacity(_sdlWindow, _opacity);
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.SetWindowOpacity(_sdlWindow, _opacity);
                }, IntPtr.Zero, false);
        }
    }

    public bool Topmost
    {
        get => _topmost;
        set
        {
            _topmost = value;
            
            if (!Initialized)
                return;

            if (SDL.IsMainThread())
                SDL.SetWindowAlwaysOnTop(_sdlWindow, value);
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.SetWindowAlwaysOnTop(_sdlWindow, value);
                }, IntPtr.Zero, false);
        }
    }

    public bool Focusable
    {
        get => _focusable;
        set
        {
            _focusable = value;
            
            if (!Initialized)
                return;

            if (SDL.IsMainThread())
                SDL.SetWindowFocusable(_sdlWindow, value);
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.SetWindowFocusable(_sdlWindow, value);
                }, IntPtr.Zero, false);
        }
    }
    
    // Events

    public event OWindowEvents.OnInitialization? OnInitialization;
    public event OWindowEvents.OnStart? OnStart; 
    public event OWindowEvents.OnUpdate? OnUpdate; 
    public event OWindowEvents.OnEnd? OnEnd;
    public event OWindowEvents.OnStateChanged? OnStateChanged;
    public event OWindowEvents.OnResize? OnResize;
    public event OWindowEvents.OnMove? OnMove;

    // Methods and Functions
    
    public OWindow(OWindowOptions options)
    {
        _filter = Filter;
        _keyboard = new OKeyboard(this);
        _mouse = new OMouse(this);
        _storage = new OStorage(this, "Storage");
        _stopwatch = new Stopwatch();
        _name = options.Name;
        _size = options.Size;
        _position = options.Position;
        _state = options.State;
        _border = options.Border;
        _opacity = options.Opacity;
        _topmost = options.Topmost;
        _focusable = options.Focusable;
        RenderWhileHidden = options.RenderWhileHidden;
        CloseOperation = options.CloseOperation;
        Delay = options.Delay;
        BackgroundColor = options.BackgroundColor;

        if (options.Renderer.Window is OWindow)
        {
            ODebugger.Warn("The provided renderer is already parented into another window.");

            return;
        }
        
        _renderer = options.Renderer;

        _renderer.Window = this;

        OScene main = new OScene(null, "Main", true);

        _scenes = new OScenes(this, main, "Scenes");
    }

    public IOPrototype? Clone(bool cloneChildren, bool cloneDescendants)
    {
        OWindowOptions options = new OWindowOptions()
        {
            Name = _name,
            Size = _size,
            Position = _position,
            CloseOperation = CloseOperation,
            State = _state,
            Border = Border,
            BackgroundColor = BackgroundColor,
            Delay = Delay,
            Opacity = _opacity,
            RenderWhileHidden = RenderWhileHidden,
            Topmost = _topmost,
            Focusable = _focusable
        };

        OWindow clone = new OWindow(options);

        if (cloneChildren || cloneDescendants)
        {
            foreach (IOInstance child in _storage.GetChildren())
            {
                IOInstance? childClone = (IOInstance?)child.Clone(cloneDescendants, cloneDescendants);
                
                if (childClone != null)
                    clone._storage.AddChild(childClone);
            }

            foreach (OScene scene in _scenes.GetScenes())
            {
                OScene? sceneClone = (OScene?)scene.Clone(cloneDescendants, cloneDescendants);
                
                if (sceneClone != null)
                    clone._scenes.AddChild(sceneClone);
            }
        }

        return clone;
    }

    public void Dispose()
    {
        if (_sdlRenderer != IntPtr.Zero)
        {
            SDL.DestroyRenderer(_sdlRenderer);
            ODebugger.Inform("SDL renderer destroyed.\n");
        }
        
        if (_sdlWindow != IntPtr.Zero)
        {
            SDL.DestroyWindow(_sdlWindow);
            ODebugger.Inform("SDL window destroyed.\n");
        }
        
        SDL.QuitSubSystem(SDL.InitFlags.Video);
        ODebugger.Inform("Successfully quit from SDL subsystem.\n");

        Initialized = false;
        Running = false;
    }

    public void Initialize()
    {
        if (Initialized)
        {
            ODebugger.Warn("Window can not be initialized twice.\n");
            
            return;
        }
        
        if (SDL.IsMainThread())
            Create();
        else
            SDL.RunOnMainThread((IntPtr _) =>
            {
                Create();
            }, IntPtr.Zero, false);

        OnInitialization?.Invoke();
    }

    public void Run()
    {
        if (!Initialized)
        {
            ODebugger.Warn("Window must be initialized before being ran.\n");
            
            return;
        }

        Running = true;
        Visible = true;
        
        OnStart?.Invoke();

        SDL.RunOnMainThread((IntPtr _) =>
        {
            while (Running)
            {
                Render();
                SDL.WaitEventTimeout(out SDL.Event _, Delay);
            }
        }, IntPtr.Zero, true);
    }

    public void Log(string title = "Log", string message = "This is a message!")
    {
        if (!Initialized)
        {
            ODebugger.Warn("Window must be initialized before showing a message box.\n");
            
            return;
        }
        
        if (SDL.IsMainThread())
            SDL.ShowSimpleMessageBox(SDL.MessageBoxFlags.ButtonsLeftToRight, title, message, _sdlWindow);
        else
            SDL.RunOnMainThread((IntPtr _) =>
            {
                SDL.ShowSimpleMessageBox(SDL.MessageBoxFlags.ButtonsLeftToRight, title, message, _sdlWindow);
            }, IntPtr.Zero, true);
    }

    public void Inform(string title = "Information", string message = "This is an information!")
    {
        if (!Initialized)
        {
            ODebugger.Warn("Window must be initialized before showing a message box.\n");
            
            return;
        }
        
        if (SDL.IsMainThread())
            SDL.ShowSimpleMessageBox(SDL.MessageBoxFlags.Information, title, message, _sdlWindow);
        else
            SDL.RunOnMainThread((IntPtr _) =>
            {
                SDL.ShowSimpleMessageBox(SDL.MessageBoxFlags.Information, title, message, _sdlWindow);
            }, IntPtr.Zero, true);
    }

    public void Warn(string title = "Warning", string message = "This is a warning!")
    {
        if (!Initialized)
        {
            ODebugger.Warn("Window must be initialized before showing a message box.\n");
            
            return;
        }
        
        if (SDL.IsMainThread())
            SDL.ShowSimpleMessageBox(SDL.MessageBoxFlags.Warning, title, message, _sdlWindow);
        else
            SDL.RunOnMainThread((IntPtr _) =>
            {
                SDL.ShowSimpleMessageBox(SDL.MessageBoxFlags.Warning, title, message, _sdlWindow);
            }, IntPtr.Zero, true);
    }

    public void Error(string title = "Error", string message = "Something went wrong.")
    {
        if (!Initialized)
        {
            ODebugger.Warn("Window must be initialized before showing a message box.\n");
            
            return;
        }
        
        if (SDL.IsMainThread())
            SDL.ShowSimpleMessageBox(SDL.MessageBoxFlags.Error, title, message, _sdlWindow);
        else
            SDL.RunOnMainThread((IntPtr _) =>
            {
                SDL.ShowSimpleMessageBox(SDL.MessageBoxFlags.Error, title, message, _sdlWindow);
            }, IntPtr.Zero, true);
    }

    public void Hide()
    {
        if (!Initialized)
        {
            ODebugger.Warn("Window must be initialized before being hid.\n");
            
            return;
        }

        Visible = false;

        if (SDL.IsMainThread())
            SDL.HideWindow(_sdlWindow);
        else
            SDL.RunOnMainThread((IntPtr _) =>
            {
                SDL.HideWindow(_sdlWindow);
            }, IntPtr.Zero, true);
        
        ODebugger.Inform($"Window \"{Name}\" hid.");
    }

    public void Show()
    {
        if (!Initialized)
        {
            ODebugger.Warn("Window must be initialized before being shown.\n");
            
            return;
        }

        Visible = true;

        if (SDL.IsMainThread())
            SDL.ShowWindow(_sdlWindow);
        else
            SDL.RunOnMainThread((IntPtr _) =>
            {
                SDL.ShowWindow(_sdlWindow);
            }, IntPtr.Zero, true);
        
        ODebugger.Inform($"Window \"{Name}\" shown.");
    }

    public ORenderInfo? Render()
    {
        if (!Initialized)
        {
            ODebugger.Warn("Window must be initialized before being rendered.\n");
            
            return null;
        }

        if (!Visible && !RenderWhileHidden)
            return null;
    
        _stopwatch.Start();
    
        (byte alpha, byte red, byte green, byte blue) = BackgroundColor.Argb;
        float opacity = alpha / 255f;
        red = (byte)(red * opacity);
        green = (byte)(green * opacity);
        blue = (byte)(blue * opacity);
        
        SDL.SetRenderDrawColor(_sdlRenderer, red, green, blue, alpha);
        SDL.SetRenderDrawBlendMode(_sdlRenderer, SDL.BlendMode.None);
        SDL.RenderClear(_sdlRenderer);
        SDL.SetRenderDrawBlendMode(_sdlRenderer, SDL.BlendMode.Blend);
        
        
        
        SDL.RenderPresent(_sdlRenderer);
        _stopwatch.Stop();
        OnUpdate?.Invoke(_stopwatch.Elapsed.TotalMilliseconds);
        _stopwatch.Reset();
    
        return null;
    }

    private bool Filter(IntPtr _, ref SDL.Event ev)
    {
        if (ev.Window.WindowID != SDL.GetWindowID(_sdlWindow) || !Initialized)
            return false;

        switch (ev.Type)
        {
            case (uint)SDL.EventType.WindowCloseRequested:
            case (uint)SDL.EventType.Quit:
                if (CloseOperation == OWindowCloseOperation.Hide)
                    Hide();
                
                if (CloseOperation == OWindowCloseOperation.Close)
                    Running = false;

                if (CloseOperation == OWindowCloseOperation.Confirm)
                {
                    // TODO: Create confirmation dialog.
                }
        
                OnEnd?.Invoke(!Running);

                break;
            case (uint)SDL.EventType.WindowExposed:
            case (uint)SDL.EventType.WindowResized:
            case (uint)SDL.EventType.WindowPixelSizeChanged:
                Render();
                SDL.GetWindowSize(_sdlWindow, out int width, out int height);

                if (width == _size.X && height == _size.Y)
                    break;
                
                _size = new OVector2<int>(width, height);

                OnResize?.Invoke(_size);

                break;
            case (uint)SDL.EventType.WindowMoved:
                Render();
                SDL.GetWindowPosition(_sdlWindow, out int x, out int y);

                if (x == _position.X && y == _position.Y)
                    break;

                _position = new OVector2<int>(x, y);

                OnMove?.Invoke(_position);

                break;
        }
        
        return false;
    }

    private void Create()
    {
        if (!SDL.Init(SDL.InitFlags.Video))
        {
            ODebugger.Throw(new ExternalException($"Failed to initialize SDL video: {SDL.GetError()}.\n"));
            
            return;
        }

        SDL.WindowFlags flags = SDL.WindowFlags.Resizable | SDL.WindowFlags.Transparent;

        if (_renderer.Type == IORenderer.ORendererType.OpenGl)
            flags |= SDL.WindowFlags.OpenGL;
        
        _renderer.ApplyConfig();
        SDL.CreateWindowAndRenderer(_name, _size.X, _size.Y, flags, out _sdlWindow, out _sdlRenderer);
        
        if (_sdlWindow == IntPtr.Zero || _sdlRenderer == IntPtr.Zero)
        {
            ODebugger.Throw(new ExternalException($"Failed to create SDL window and/or renderer: {SDL.GetError()}.\n"));
            
            return;
        }
        
        if (!SDL.AddEventWatch(_filter, IntPtr.Zero))
        {
            ODebugger.Throw(new ExternalException($"Failed to add SDL event watch: {SDL.GetError()}.\n"));
            
            return;
        }

        if (_position.X == -1 && _position.Y == -1)
            SDL.SetWindowPosition(_sdlWindow, (int)SDL.WindowPosCentered(), (int)SDL.WindowPosCentered());
        else
            SDL.SetWindowPosition(_sdlWindow, _position.X, _position.Y);
        
        switch (_state)
        {
            case OWindowState.Normal:
                SDL.SetWindowFullscreen(_sdlWindow, false);
                SDL.RestoreWindow(_sdlWindow);

                break;
            case OWindowState.Minimized:
                SDL.SetWindowFullscreen(_sdlWindow, false);
                SDL.MinimizeWindow(_sdlWindow);

                break;
            case OWindowState.Maximized:
                SDL.SetWindowFullscreen(_sdlWindow, false);
                SDL.MaximizeWindow(_sdlWindow);

                break;
            case OWindowState.Fullscreen:
                SDL.SetWindowFullscreen(_sdlWindow, true);

                break;
        }
        
        switch (_border)
        {
            case OWindowBorder.None:
                SDL.SetWindowBordered(_sdlWindow, false);
                    
                break;
            case OWindowBorder.Fixed:
                SDL.SetWindowBordered(_sdlWindow, true);
                SDL.SetWindowResizable(_sdlWindow, false);
                    
                break;
            case OWindowBorder.Resizable:
                SDL.SetWindowBordered(_sdlWindow, true);
                SDL.SetWindowResizable(_sdlWindow, true);
                    
                break;
        }

        SDL.SetWindowOpacity(_sdlWindow, _opacity);
        SDL.SetWindowAlwaysOnTop(_sdlWindow, _topmost);
        SDL.SetWindowFocusable(_sdlWindow, _focusable);

        Initialized = true;
        
        _keyboard.Initialize();
        _mouse.Initialize();
        ODebugger.Inform("Window successfully created.\n");
    }
    
    // To String

    public override string ToString()
    {
        return $"<OWindow Name=\"{_name}\" Size={_size.X}x{_size.Y} Position={Position}>";
    }
}