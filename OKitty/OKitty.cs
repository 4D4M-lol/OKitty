// Imports

using static OKitty.OkInput;
using static OKitty.OkInstance;
using static OKitty.OkMath;
using static OKitty.OkScript;
using static OKitty.OkStyling;
using SDL3;
using System.Data;
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
    public static readonly string Author = "4D4M-lol";
    public static readonly string Version = "1.0.0";
}

// Records

public record OWindowOptions
{
    // Properties
    
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

    private IntPtr _window;
    private IntPtr _renderer;
    private SDL.EventFilter _filter;
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

    public IntPtr WindowHandle => _window;
    public IntPtr RendererHandle => _renderer;
    public OKeyboard Keyboard => _keyboard;
    public OMouse Mouse => _mouse;
    public OStorage Storage => _storage;
    public OScenes Scenes => _scenes;
    public string Icon => "󰍹";
    public string InstanceName => "OWindow";
    public bool Initialized { get; private set; } = false;
    public bool Running { get; private set; } = false;
    public bool Visible { get; private set; } = false;
    public bool RenderWhileHidden { get; set; } = false;
    public int Delay { get; set; } = 16;
    public OWindowCloseOperation CloseOperation { get; set; } = OWindowCloseOperation.Close;
    public OColor BackgroundColor { get; set; } = OColor.White;

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            
            if (!Initialized)
                return;
            
            if (SDL.IsMainThread())
                SDL.SetWindowTitle(_window, value);
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.SetWindowTitle(_window, value);
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
                SDL.SetWindowSize(_window, value.X, value.Y);
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.SetWindowSize(_window, value.X, value.Y);
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
                SDL.SetWindowPosition(_window, value.X, value.Y);
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.SetWindowPosition(_window, value.X, value.Y);
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
                        SDL.SetWindowFullscreen(_window, false);
                        SDL.RestoreWindow(_window);

                        break;
                    case OWindowState.Minimized:
                        SDL.SetWindowFullscreen(_window, false);
                        SDL.MinimizeWindow(_window);

                        break;
                    case OWindowState.Maximized:
                        SDL.SetWindowFullscreen(_window, false);
                        SDL.MaximizeWindow(_window);

                        break;
                    case OWindowState.Fullscreen:
                        SDL.SetWindowFullscreen(_window, true);

                        break;
                }
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    switch (value)
                    {
                        case OWindowState.Normal:
                            SDL.SetWindowFullscreen(_window, false);
                            SDL.RestoreWindow(_window);
                        
                            break;
                        case OWindowState.Minimized:
                            SDL.SetWindowFullscreen(_window, false);
                            SDL.MinimizeWindow(_window);
                        
                            break;
                        case OWindowState.Maximized:
                            SDL.SetWindowFullscreen(_window, false);
                            SDL.MaximizeWindow(_window);
                        
                            break;
                        case OWindowState.Fullscreen:
                            SDL.SetWindowFullscreen(_window, true);
                        
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
                        SDL.SetWindowBordered(_window, false);
                        
                        break;
                    case OWindowBorder.Fixed:
                        SDL.SetWindowBordered(_window, true);
                        SDL.SetWindowResizable(_window, false);
                        
                        break;
                    case OWindowBorder.Resizable:
                        SDL.SetWindowBordered(_window, true);
                        SDL.SetWindowResizable(_window, true);
                        
                        break;
                }
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    switch (value)
                    {
                        case OWindowBorder.None:
                            SDL.SetWindowBordered(_window, false);
                        
                            break;
                        case OWindowBorder.Fixed:
                            SDL.SetWindowBordered(_window, true);
                            SDL.SetWindowResizable(_window, false);
                        
                            break;
                        case OWindowBorder.Resizable:
                            SDL.SetWindowBordered(_window, true);
                            SDL.SetWindowResizable(_window, true);
                        
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
                SDL.SetWindowOpacity(_window, _opacity);
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.SetWindowOpacity(_window, _opacity);
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
                SDL.SetWindowAlwaysOnTop(_window, value);
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.SetWindowAlwaysOnTop(_window, value);
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
                SDL.SetWindowFocusable(_window, value);
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.SetWindowFocusable(_window, value);
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
        CloseOperation = options.CloseOperation;
        _state = options.State;
        _border = options.Border;
        BackgroundColor = options.BackgroundColor;
        Delay = options.Delay;
        _opacity = options.Opacity;
        RenderWhileHidden = options.RenderWhileHidden;
        _topmost = options.Topmost;
        _focusable = options.Focusable;

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
        if (_renderer != IntPtr.Zero)
        {
            SDL.DestroyRenderer(_renderer);
            ODebugger.Inform("SDL renderer destroyed.\n");
        }
        
        if (_window != IntPtr.Zero)
        {
            SDL.DestroyWindow(_window);
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
            SDL.ShowSimpleMessageBox(SDL.MessageBoxFlags.ButtonsLeftToRight, title, message, _window);
        else
            SDL.RunOnMainThread((IntPtr _) =>
            {
                SDL.ShowSimpleMessageBox(SDL.MessageBoxFlags.ButtonsLeftToRight, title, message, _window);
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
            SDL.ShowSimpleMessageBox(SDL.MessageBoxFlags.Information, title, message, _window);
        else
            SDL.RunOnMainThread((IntPtr _) =>
            {
                SDL.ShowSimpleMessageBox(SDL.MessageBoxFlags.Information, title, message, _window);
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
            SDL.ShowSimpleMessageBox(SDL.MessageBoxFlags.Warning, title, message, _window);
        else
            SDL.RunOnMainThread((IntPtr _) =>
            {
                SDL.ShowSimpleMessageBox(SDL.MessageBoxFlags.Warning, title, message, _window);
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
            SDL.ShowSimpleMessageBox(SDL.MessageBoxFlags.Error, title, message, _window);
        else
            SDL.RunOnMainThread((IntPtr _) =>
            {
                SDL.ShowSimpleMessageBox(SDL.MessageBoxFlags.Error, title, message, _window);
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
            SDL.HideWindow(_window);
        else
            SDL.RunOnMainThread((IntPtr _) =>
            {
                SDL.HideWindow(_window);
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
            SDL.ShowWindow(_window);
        else
            SDL.RunOnMainThread((IntPtr _) =>
            {
                SDL.ShowWindow(_window);
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
        
        SDL.SetRenderDrawColor(_renderer, red, green, blue, alpha);
        SDL.SetRenderDrawBlendMode(_renderer, SDL.BlendMode.None);
        SDL.RenderClear(_renderer);
        SDL.RenderPresent(_renderer);
        _stopwatch.Stop();
        OnUpdate?.Invoke(_stopwatch.Elapsed.TotalMilliseconds);
        _stopwatch.Reset();
    
        return null;
    }

    private bool Filter(IntPtr _, ref SDL.Event ev)
    {
        if (ev.Window.WindowID != SDL.GetWindowID(_window) || !Initialized)
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
                SDL.GetWindowSize(_window, out int width, out int height);

                if (width == _size.X && height == _size.Y)
                    break;
                
                _size = new OVector2<int>(width, height);

                OnResize?.Invoke(_size);

                break;
            case (uint)SDL.EventType.WindowMoved:
                Render();
                SDL.GetWindowPosition(_window, out int x, out int y);

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
        
        SDL.CreateWindowAndRenderer(
            _name, _size.X, _size.Y, SDL.WindowFlags.Resizable | SDL.WindowFlags.Transparent,
            out _window, out _renderer
        );
        
        if (_window == IntPtr.Zero || _renderer == IntPtr.Zero)
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
            SDL.SetWindowPosition(_window, (int)SDL.WindowPosCentered(), (int)SDL.WindowPosCentered());
        else
            SDL.SetWindowPosition(_window, _position.X, _position.Y);
        
        switch (_state)
        {
            case OWindowState.Normal:
                SDL.SetWindowFullscreen(_window, false);
                SDL.RestoreWindow(_window);

                break;
            case OWindowState.Minimized:
                SDL.SetWindowFullscreen(_window, false);
                SDL.MinimizeWindow(_window);

                break;
            case OWindowState.Maximized:
                SDL.SetWindowFullscreen(_window, false);
                SDL.MaximizeWindow(_window);

                break;
            case OWindowState.Fullscreen:
                SDL.SetWindowFullscreen(_window, true);

                break;
        }
        
        switch (_border)
        {
            case OWindowBorder.None:
                SDL.SetWindowBordered(_window, false);
                    
                break;
            case OWindowBorder.Fixed:
                SDL.SetWindowBordered(_window, true);
                SDL.SetWindowResizable(_window, false);
                    
                break;
            case OWindowBorder.Resizable:
                SDL.SetWindowBordered(_window, true);
                SDL.SetWindowResizable(_window, true);
                    
                break;
        }

        SDL.SetWindowOpacity(_window, _opacity);
        SDL.SetWindowAlwaysOnTop(_window, _topmost);
        SDL.SetWindowFocusable(_window, _focusable);

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