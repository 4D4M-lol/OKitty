// Imports

using static OKitty.OkMath;
using static OKitty.OkScript;
using static OKitty.OkStyling;
using System.Collections.ObjectModel;

namespace OKitty;

// OkInstance

public static class OkInstance
{
    // Interfaces

    public interface IOPrototype : IDisposable
    {
        // Properties

        public string Icon { get; }
        public string Name { get; set; }

        // Methods

        public ReadOnlyCollection<IOModifier> GetModifiers();

        public TModifier? GetModifier<TModifier>()
            where TModifier : class, IOModifier;

        public void AddModifier<TModifier>(TModifier modifier)
            where TModifier : class, IOModifier;

        public void RemoveModifier<TModifier>()
            where TModifier : class, IOModifier;

        public bool HasModifier<TModifier>()
            where TModifier : class, IOModifier;

        public IOPrototype? Clone(bool cloneChildren, bool cloneDescendants);
        public ORenderInfo? Render();

        // To String

        public string ToString()
        {
            return $"<{GetType().Name} Name=\"{Name}\">";
        }
    }

    public interface IOInstance : IOPrototype
    {
        // Properties

        public IOInstance? Parent { get; set; }
        public HashSet<string> Tags { get; }

        // Methods and Functions

        public ReadOnlyCollection<IOInstance> GetChildren();
        public void AddChild(IOInstance child);
        public void RemoveChild(IOInstance child);

        public ReadOnlyCollection<IOInstance> GetDescendants()
        {
            List<IOInstance> descendants = new List<IOInstance>();
            ReadOnlyCollection<IOInstance> children = GetChildren();

            descendants.AddRange(children);

            foreach (IOInstance descendant in descendants)
                descendants.AddRange(descendant.GetDescendants());

            return new ReadOnlyCollection<IOInstance>(descendants);
        }

        public ReadOnlyCollection<IOInstance> ChildSelector(Func<IOInstance, bool> selector)
        {
            ReadOnlyCollection<IOInstance> children = GetChildren();
            List<IOInstance> selected = children.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> ChildSelector(Func<IOInstance, int, bool> selector)
        {
            ReadOnlyCollection<IOInstance> children = GetChildren();
            List<IOInstance> selected = children.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> Descendantselector(Func<IOInstance, bool> selector)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();
            List<IOInstance> selected = descendants.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> DescendantSelector(Func<IOInstance, int, bool> selector)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();
            List<IOInstance> selected = descendants.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public IOInstance? FindFirstChildNamed(string name)
        {
            ReadOnlyCollection<IOInstance> children = GetChildren();

            foreach (IOInstance child in children)
                if (child.Name == name)
                    return child;

            return null;
        }

        public TInstance? FindFirstChildWhichIsA<TInstance>()
            where TInstance : class, IOInstance
        {
            ReadOnlyCollection<IOInstance> children = GetChildren();

            foreach (IOInstance child in children)
                if (child is TInstance instance)
                    return instance;

            return null;
        }

        public IOInstance? FindFirstDescendantNamed(string name)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();

            foreach (IOInstance descendant in descendants)
                if (descendant.Name == name)
                    return descendant;

            return null;
        }

        public TInstance? FindFirstDescendantWhichIsA<TInstance>()
            where TInstance : class, IOInstance
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();

            foreach (IOInstance descendant in descendants)
                if (descendant is TInstance instance)
                    return instance;

            return null;
        }

        public IOPrototype? FindFirstAncestorNamed(string name)
        {
            for (IOInstance? next = Parent; next != null; next = next.Parent)
            {
                if (next.Name == name)
                    return next;

                if (next is OStorage storage && storage.Window.Name == name)
                    return storage.Window;

                if (next is OScenes scenes && scenes.Window.Name == name)
                    return scenes.Window;
            }

            return null;
        }

        public TPrototype? FindFirstAncestorWhichIsA<TPrototype>()
            where TPrototype : class, IOPrototype
        {
            if (typeof(TPrototype) == typeof(OWindow))
            {
                for (IOInstance? next = Parent; next != null; next = next.Parent)
                {
                    if (next is OStorage storage)
                        return storage.Window as TPrototype;
                    
                    if (next is OScenes scenes)
                        return scenes.Window as TPrototype;
                }

                return null;
            }

            for (IOInstance? next = Parent; next != null; next = next.Parent)
                if (next is TPrototype instance)
                    return instance;

            return null;
        }

        public bool IsAChildOf<TPrototype>()
            where TPrototype : class, IOPrototype
        {
            return Parent is TPrototype;
        }

        public bool IsADescendantOf<TPrototype>()
            where TPrototype : class, IOPrototype
        {
            if (typeof(TPrototype) == typeof(OWindow))
                return IsADescendantOf<OStorage>() || IsADescendantOf<OScenes>();

            for (IOInstance? next = Parent; next != null; next = next.Parent)
                if (next is TPrototype)
                    return true;

            return false;
        }

        // Events

        public event OInstanceEvents.OnChildAdded OnChildAdded;
        public event OInstanceEvents.OnChildRemoved OnChildRemoved;
    }

    public interface IOModifier
    {
        // Enums

        [Flags]
        public enum OModifierCallTime
        {
            // None

            None = 0,

            // Layout

            Layout = 1 << 0,
            LayoutBeforeChildren = 1 << 1,
            LayoutChildren = 1 << 2,

            // Render

            PreRender = 1 << 3,
            Render = 1 << 4,
            PostRender = 1 << 5,

            // Children

            PreRenderChildren = 1 << 6,
            RenderChildren = 1 << 7,
            PostRenderChildren = 1 << 8,

            // Gameplay & Logic

            Physics = 1 << 9,
            Animation = 1 << 10,
            Behavior = 1 << 11,

            // Extras

            Extra1 = 1 << 12,
            Extra2 = 1 << 13,
            Extra3 = 1 << 14,
            Extra4 = 1 << 15,
            Extra5 = 1 << 16,
            Extra6 = 1 << 17,
            Extra7 = 1 << 18,
            Extra8 = 1 << 19,
            Extra9 = 1 << 20,

            // All

            All = ~0
        }

        public enum OModifierPriority
        {
            Lowest,
            VeryLow,
            Low,
            Normal,
            High,
            VeryHigh,
            Highest
        }

        // Properties

        public string Icon { get; }
        public OModifierCallTime CallTime { get; }
        public OModifierPriority Priority { get; }
        public bool Active { get; set; }
        public IOPrototype? Parent { get; set; }

        // Methods

        public TReturn? Process<TReturn, TParams>(OModifierCallTime callTime, TParams parameters);
        public bool CanProcess<TReturn, TParams>();
    }

    // Records

    public record OLineInfo
    {
        // Properties

        public required OVector2<float> Start { get; init; }
        public required OVector2<float> End { get; init; }
        public OColor Color { get; init; } = OColor.Black;

        // To String

        public override string ToString()
        {
            return $"{Start} - {End}";
        }
    }

    public record OShapeInfo
    {
        // Static Properties

        public static readonly OShapeInfo Empty = new OShapeInfo() { Lines = new List<OLineInfo>() };

        // Properties

        public required List<OLineInfo> Lines { get; init; }
        public OShapeInfo? Mask { get; init; } = null;
        public OColor Color { get; init; } = OColor.Black;
        public int Layer { get; init; } = 0;

        // Methods and Functions

        public ReadOnlyCollection<OVector2<float>> GetPoints()
        {
            List<OVector2<float>> points = new List<OVector2<float>>();

            foreach (OLineInfo line in Lines)
                points.AddRange(line.Start, line.End);

            return new ReadOnlyCollection<OVector2<float>>(points.Distinct().ToList());
        }

        // To String

        public override string ToString()
        {
            return $"[ShapeInfo]";
        }
    }

    public record OEdgeInfo
    {
        // Properties and Fields

        public required OVector3<float> Start { get; init; }
        public required OVector3<float> End { get; init; }
        public OColor Color { get; init; } = OColor.Black;

        // To String

        public override string ToString()
        {
            return $"{Start} - {End}";
        }
    }

    public record OFaceInfo
    {
        // Properties and Fields

        public required List<OEdgeInfo> Edges { get; init; }
        public List<OTextureInfo>? Textures { get; init; } = null;
        public OColor Color { get; init; } = OColor.Black;

        // To String

        public override string ToString()
        {
            return "[FaceInfo]";
        }
    }

    public record OTextureInfo
    {
        // TODO!: Create texture.
    }

    public record OGeometryInfo
    {
        // Properties and Fields

        public required List<OFaceInfo> Faces { get; init; }

        // To String

        public override string ToString()
        {
            return "[GeometryInfo]";
        }
    }

    // Classes

    public class ORenderInfo
    {
        // Properties and Fields

        public List<OShapeInfo> Shapes { get; private set; }
        public List<OGeometryInfo> Geometries { get; private set; }

        // Methods and Functions

        public ORenderInfo(List<OShapeInfo>? shapes = null, List<OGeometryInfo>? geometries = null)
        {
            Shapes = shapes ?? new List<OShapeInfo>();
            Geometries = geometries ?? new List<OGeometryInfo>();
        }

        // To String

        public override string ToString()
        {
            return $"[RenderInfo]";
        }
    }

    public class OStorage : IOInstance
    {
        // Properties and Fields
        
        private OWindow _window;
        private List<IOInstance> _children;
        private List<IOModifier> _modifiers;

        IOInstance? IOInstance.Parent { get; set; } = null;
        
        public OWindow Window => _window;
        public string Icon => "";
        public string Name { get; set; } = "OStorage";
        public HashSet<string> Tags { get; } = new HashSet<string>();
        
        // Events

        public event OInstanceEvents.OnChildAdded? OnChildAdded;
        public event OInstanceEvents.OnChildRemoved? OnChildRemoved;
        
        // Methods and Functions

        public OStorage(OWindow window, string name = "OStorage")
        {
            _window = window;
            _children = new List<IOInstance>();
            _modifiers = new List<IOModifier>();

            Name = name;
        }

        public ReadOnlyCollection<IOInstance> GetChildren()
        {
            return new ReadOnlyCollection<IOInstance>(_children);
        }

        public void AddChild(IOInstance child)
        {
            if (_children.Contains(child))
                return;

            _children.Add(child);

            if (child.Parent != this)
                child.Parent = this;

            OnChildAdded?.Invoke(child);
        }

        public void RemoveChild(IOInstance child)
        {
            if (!_children.Remove(child))
                return;

            if (child.Parent == this)
                child.Parent = null;

            OnChildRemoved?.Invoke(child);
        }

        public ReadOnlyCollection<IOModifier> GetModifiers()
        {
            return new ReadOnlyCollection<IOModifier>(_modifiers);
        }

        public TModifier? GetModifier<TModifier>()
            where TModifier : class, IOModifier
        {
            return _modifiers.OfType<TModifier>().FirstOrDefault();
        }

        public void AddModifier<TModifier>(TModifier modifier)
            where TModifier : class, IOModifier
        {
            if (HasModifier<TModifier>())
            {
                ODebugger.Warn($"\"{Name}\" already have a(n) {modifier.GetType().Name} modifier.");

                return;
            }

            _modifiers.Add(modifier);

            _modifiers = _modifiers
                .OrderByDescending((IOModifier modifier) => modifier.Priority)
                .ToList();

            if (modifier.Parent != this)
                modifier.Parent = this;
        }

        public void RemoveModifier<TModifier>()
            where TModifier : class, IOModifier
        {
            TModifier? modifier = GetModifier<TModifier>();

            if (modifier is not null)
            {
                if (modifier.Parent == this)
                    modifier.Parent = null;

                _modifiers.Remove(modifier);
            }
        }

        public bool HasModifier<TModifier>()
            where TModifier : class, IOModifier
        {
            return _modifiers.OfType<TModifier>().Any();
        }

        public ReadOnlyCollection<IOInstance> GetDescendants()
        {
            List<IOInstance> descendants = new List<IOInstance>();

            descendants.AddRange(_children);

            foreach (IOInstance descendant in descendants)
                descendants.AddRange(descendant.GetDescendants());

            return new ReadOnlyCollection<IOInstance>(descendants);
        }

        public ReadOnlyCollection<IOInstance> ChildSelector(Func<IOInstance, bool> selector)
        {
            List<IOInstance> selected = _children.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> ChildSelector(Func<IOInstance, int, bool> selector)
        {
            List<IOInstance> selected = _children.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> Descendantselector(Func<IOInstance, bool> selector)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();
            List<IOInstance> selected = descendants.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> DescendantSelector(Func<IOInstance, int, bool> selector)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();
            List<IOInstance> selected = descendants.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public IOInstance? FindFirstChildNamed(string name)
        {
            foreach (IOInstance child in _children)
                if (child.Name == name)
                    return child;

            return null;
        }

        public TInstance? FindFirstChildWhichIsA<TInstance>()
            where TInstance : class, IOInstance
        {
            foreach (IOInstance child in _children)
                if (child is TInstance instance)
                    return instance;

            return null;
        }

        public IOInstance? FindFirstDescendantNamed(string name)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();

            foreach (IOInstance descendant in descendants)
                if (descendant.Name == name)
                    return descendant;

            return null;
        }

        public TInstance? FindFirstDescendantWhichIsA<TInstance>()
            where TInstance : class, IOInstance
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();

            foreach (IOInstance descendant in descendants)
                if (descendant is TInstance instance)
                    return instance;

            return null;
        }

        public IOPrototype? FindFirstAncestorNamed(string name)
        {
            return _window.Name == name ? _window : null;
        }

        public TPrototype? FindFirstAncestorWhichIsA<TPrototype>()
            where TPrototype : class, IOPrototype
        {
            return typeof(OWindow) == typeof(TPrototype) ? _window as TPrototype : null;
        }

        public bool IsAChildOf<TPrototype>()
            where TPrototype : class, IOPrototype
        {
            return typeof(TPrototype) == typeof(OWindow);
        }

        public bool IsADescendantOf<TPrototype>()
            where TPrototype : class, IOPrototype
        {
            return typeof(TPrototype) == typeof(OWindow);
        }
        
        public IOPrototype? Clone(bool cloneChildren, bool cloneDescendants)
        {
            OStorage clone = new OStorage(_window, Name);

            if (cloneChildren || cloneDescendants)
            {
                foreach (IOInstance child in _children)
                {
                    IOInstance? childClone = (IOInstance?)child.Clone(cloneDescendants, cloneDescendants);
                    
                    if (childClone != null)
                        clone.AddChild(childClone);
                }
            }

            return clone;
        }

        public void Dispose()
        {
            foreach (IOInstance child in _children)
                child.Dispose();
            
            _children.Clear();
        }

        public ORenderInfo? Render()
        {
            foreach (IOModifier modifier in _modifiers)
            {
                if (!modifier.Active)
                    continue;

                if (
                    !modifier.CallTime.HasFlag(IOModifier.OModifierCallTime.Physics)
                    && !modifier.CallTime.HasFlag(IOModifier.OModifierCallTime.Animation)
                    && !modifier.CallTime.HasFlag(IOModifier.OModifierCallTime.Behavior)
                )
                    continue;

                modifier.Process<object, object>(modifier.CallTime, this);
            }

            return null;
        }

        // To String

        public override string ToString()
        {
            return $"<OStorage Window=\"{_window.Name}\" Name=\"{Name}\">";
        }
    }

    public class OScenes : IOInstance
    {
        // Properties and Fields
        
        private OWindow _window;
        private OScene _main;
        private List<OScene> _scenes;
        private List<IOModifier> _modifiers;
        private int _active;
    
        IOInstance? IOInstance.Parent { get; set; }
    
        public OWindow Window => _window;
        public OScene Main => _main;
        public string Icon => "󰉏";
        public string Name { get; set; } = "OScenes";
        public HashSet<string> Tags { get; } = new HashSet<string>();

        public int Active
        {
            get => _active;
            set
            {
                if (value < 0 || value > _scenes.Count)
                {
                    ODebugger.Warn($"\"Active\" can only be between 0 to {_scenes.Count - 1}.");

                    return;
                }

                _active = value;
            }
        }
    
        // Events
    
        public event OInstanceEvents.OnChildAdded? OnChildAdded;
        public event OInstanceEvents.OnChildRemoved? OnChildRemoved;
    
        // Methods and Functions
        
        public OScenes(OWindow window, OScene main, string name = "OScenes")
        {
            _window = window;
            _main = main;
            _scenes = new List<OScene>();
            _modifiers = new List<IOModifier>();
            _active = 0;

            Name = name;
            
            _scenes.Add(_main);
            
            _main.Parent = this;
        }

        public ReadOnlyCollection<OScene> GetScenes()
        {
            List<OScene> scenes = new List<OScene>();
            
            scenes.AddRange(_scenes);
    
            return new ReadOnlyCollection<OScene>(scenes);
        }
    
        public ReadOnlyCollection<IOInstance> GetChildren()
        {
            List<IOInstance> children = new List<IOInstance>();
            
            children.AddRange(_scenes);
    
            return new ReadOnlyCollection<IOInstance>(children);
        }

        public void AddChild(IOInstance child)
        {
            if (child is not OScene scene)
            {
                ODebugger.Warn("Only an OScene can be parented to an OScenes.");

                return;
            }

            if (_scenes.Contains(scene))
                return;

            _scenes.Add(scene);

            if (scene.Parent != this)
                scene.Parent = this;

            OnChildAdded?.Invoke(scene);
        }

        public void RemoveChild(IOInstance child)
        {
            if (child is not OScene scene)
            {
                ODebugger.Warn("Only an OScene can be parented to an OScenes.");

                return;
            }

            if (scene == _main)
                return;

            if (!_scenes.Contains(scene))
                return;

            if (_active != 0)
                if (_scenes[_active - 1] == child)
                    _active = 0;

            _scenes.Remove(scene);

            if (scene.Parent == this)
                scene.Parent = null;

            OnChildRemoved?.Invoke(scene);
        }

        public ReadOnlyCollection<IOModifier> GetModifiers()
        {
            return new ReadOnlyCollection<IOModifier>(_modifiers);
        }

        public TModifier? GetModifier<TModifier>()
            where TModifier : class, IOModifier
        {
            return _modifiers.OfType<TModifier>().FirstOrDefault();
        }

        public void AddModifier<TModifier>(TModifier modifier)
            where TModifier : class, IOModifier
        {
            if (HasModifier<TModifier>())
            {
                ODebugger.Warn($"\"{Name}\" already have a(n) {modifier.GetType().Name} modifier.");

                return;
            }

            _modifiers.Add(modifier);

            _modifiers = _modifiers
                .OrderByDescending((IOModifier modifier) => modifier.Priority)
                .ToList();

            if (modifier.Parent != this)
                modifier.Parent = this;
        }

        public void RemoveModifier<TModifier>()
            where TModifier : class, IOModifier
        {
            TModifier? modifier = GetModifier<TModifier>();

            if (modifier is not null)
            {
                if (modifier.Parent == this)
                    modifier.Parent = null;

                _modifiers.Remove(modifier);
            }
        }

        public bool HasModifier<TModifier>()
            where TModifier : class, IOModifier
        {
            return _modifiers.OfType<TModifier>().Any();
        }

        public ReadOnlyCollection<IOInstance> GetDescendants()
        {
            List<IOInstance> descendants = new List<IOInstance>();

            descendants.AddRange(_scenes);

            foreach (IOInstance descendant in descendants)
                descendants.AddRange(descendant.GetDescendants());

            return new ReadOnlyCollection<IOInstance>(descendants);
        }

        public ReadOnlyCollection<IOInstance> ChildSelector(Func<IOInstance, bool> selector)
        {
            List<IOInstance> selected = _scenes.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> ChildSelector(Func<IOInstance, int, bool> selector)
        {
            List<IOInstance> selected = _scenes.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> Descendantselector(Func<IOInstance, bool> selector)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();
            List<IOInstance> selected = descendants.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> DescendantSelector(Func<IOInstance, int, bool> selector)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();
            List<IOInstance> selected = descendants.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public IOInstance? FindFirstChildNamed(string name)
        {
            foreach (OScene scene in _scenes)
                if (scene.Name == name)
                    return scene;

            return null;
        }

        public TInstance? FindFirstChildWhichIsA<TInstance>()
            where TInstance : class, IOInstance
        {
            return typeof(TInstance) == typeof(OScene) ? _main as TInstance : null;
        }

        public IOInstance? FindFirstDescendantNamed(string name)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();

            foreach (IOInstance descendant in descendants)
                if (descendant.Name == name)
                    return descendant;

            return null;
        }

        public TInstance? FindFirstDescendantWhichIsA<TInstance>()
            where TInstance : class, IOInstance
        {
            if (typeof(TInstance) == typeof(OScene))
                return _main as TInstance;
            
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();

            foreach (IOInstance descendant in descendants)
                if (descendant is TInstance instance)
                    return instance;

            return null;
        }

        public IOPrototype? FindFirstAncestorNamed(string name)
        {
            return _window.Name == name ? _window : null;
        }

        public TPrototype? FindFirstAncestorWhichIsA<TPrototype>()
            where TPrototype : class, IOPrototype
        {
            return typeof(OWindow) == typeof(TPrototype) ? _window as TPrototype : null;
        }

        public bool IsAChildOf<TPrototype>()
            where TPrototype : class, IOPrototype
        {
            return typeof(TPrototype) == typeof(OWindow);
        }

        public bool IsADescendantOf<TPrototype>()
            where TPrototype : class, IOPrototype
        {
            return typeof(TPrototype) == typeof(OWindow);
        }
    
        public IOPrototype? Clone(bool cloneChildren, bool cloneDescendants)
        {
            OScene mainClone = (OScene)(_main.Clone(cloneDescendants, cloneDescendants) ?? new OScene(null, "Main", true));
            OScenes clone = new OScenes(_window, mainClone, Name);

            if (cloneChildren || cloneDescendants)
            {
                foreach (OScene scene in _scenes)
                {
                    if (scene.Main)
                        continue;
                    
                    OScene? sceneClone = (OScene?)scene.Clone(cloneDescendants, cloneDescendants);
                    
                    if (sceneClone != null)
                        clone.AddChild(sceneClone);
                }
            }

            clone.Active = _active;
            
            return clone;
        }
    
        public void Dispose()
        {
            foreach (IOInstance scene in _scenes)
                scene.Dispose();
    
            _scenes.Clear();
        }
    
        public ORenderInfo? Render()
        {
            ORenderInfo? info = null;

            foreach (IOModifier modifier in _modifiers)
                if (modifier.Active && modifier.CallTime.HasFlag(IOModifier.OModifierCallTime.PreRender))
                    modifier.Process<object, ORenderInfo?>(IOModifier.OModifierCallTime.PreRender, info);

            info = _scenes[_active].Render();

            foreach (IOModifier modifier in _modifiers)
                if (modifier.Active && modifier.CallTime.HasFlag(IOModifier.OModifierCallTime.PostRender))
                    info = modifier.Process<ORenderInfo?, ORenderInfo?>(IOModifier.OModifierCallTime.PostRender, info);

            return info;
        }

        // To String
    
        public override string ToString()
        {
            return $"<OScenes Window=\"{_window.Name}\" Name=\"{Name}\" Main=\"{Main.Name}\">";
        }
    }
    
    public class OScene : IOInstance
    {
        // Properties and Fields
    
        private IOInstance? _parent;
        private List<IOInstance> _children;
        private List<IOModifier> _modifiers;
    
        public string Icon => "󰈟";
        public string Name { get; set; } = "OScene";
        public HashSet<string> Tags { get; } = new HashSet<string>();
        public bool Main { get; private set; } = false;

        public IOInstance? Parent
        {
            get => _parent;
            set
            {
                if (_parent == value)
                    return;

                if (!Main)
                {
                    _parent?.RemoveChild(this);

                    _parent = value;

                    _parent?.AddChild(this);

                    return;
                }

                if (_parent is not null)
                {
                    ODebugger.Warn("The parent of a main scene can not be changed.");

                    return;
                }

                if (value is not OScenes scenes)
                {
                    ODebugger.Warn("Parent must be an OScenes if this was a main scene.");

                    return;
                }

                if (scenes.Main != this)
                {
                    ODebugger.Warn("The main scene of the provided OScenes is a different scene.");

                    return;
                }

                _parent = value;
            }
        }
    
        // Events
        
        public event OInstanceEvents.OnChildAdded? OnChildAdded;
        public event OInstanceEvents.OnChildRemoved? OnChildRemoved;
    
        // Methods and Functions
    
        public OScene(IOInstance? parent = null, string name = "OScene", bool main = false)
        {
            if (main && parent is not null)
            {
                ODebugger.Warn("Parent must be null if this scene is a main scene.");
    
                return;
            }
    
            _parent = parent;
            _children = new List<IOInstance>();
            _modifiers = new List<IOModifier>();

            Name = name;
            Main = main;

            _parent?.AddChild(this);
        }

        public ReadOnlyCollection<IOInstance> GetChildren()
        {
            return new ReadOnlyCollection<IOInstance>(_children);
        }

        public void AddChild(IOInstance child)
        {
            if (_children.Contains(child))
                return;

            _children.Add(child);

            if (child.Parent != this)
                child.Parent = this;

            OnChildAdded?.Invoke(child);
        }

        public void RemoveChild(IOInstance child)
        {
            if (!_children.Remove(child))
                return;

            if (child.Parent == this)
                child.Parent = null;

            OnChildRemoved?.Invoke(child);
        }

        public ReadOnlyCollection<IOModifier> GetModifiers()
        {
            return new ReadOnlyCollection<IOModifier>(_modifiers);
        }

        public TModifier? GetModifier<TModifier>()
            where TModifier : class, IOModifier
        {
            return _modifiers.OfType<TModifier>().FirstOrDefault();
        }

        public void AddModifier<TModifier>(TModifier modifier)
            where TModifier : class, IOModifier
        {
            if (HasModifier<TModifier>())
            {
                ODebugger.Warn($"\"{Name}\" already have a(n) {modifier.GetType().Name} modifier.");

                return;
            }

            _modifiers.Add(modifier);

            _modifiers = _modifiers
                .OrderByDescending((IOModifier modifier) => modifier.Priority)
                .ToList();

            if (modifier.Parent != this)
                modifier.Parent = this;
        }

        public void RemoveModifier<TModifier>()
            where TModifier : class, IOModifier
        {
            TModifier? modifier = GetModifier<TModifier>();

            if (modifier is not null)
            {
                if (modifier.Parent == this)
                    modifier.Parent = null;

                _modifiers.Remove(modifier);
            }
        }

        public bool HasModifier<TModifier>()
            where TModifier : class, IOModifier
        {
            return _modifiers.OfType<TModifier>().Any();
        }

        public ReadOnlyCollection<IOInstance> GetDescendants()
        {
            List<IOInstance> descendants = new List<IOInstance>();

            descendants.AddRange(_children);

            foreach (IOInstance descendant in descendants)
                descendants.AddRange(descendant.GetDescendants());

            return new ReadOnlyCollection<IOInstance>(descendants);
        }

        public ReadOnlyCollection<IOInstance> ChildSelector(Func<IOInstance, bool> selector)
        {
            List<IOInstance> selected = _children.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> ChildSelector(Func<IOInstance, int, bool> selector)
        {
            List<IOInstance> selected = _children.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> Descendantselector(Func<IOInstance, bool> selector)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();
            List<IOInstance> selected = descendants.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> DescendantSelector(Func<IOInstance, int, bool> selector)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();
            List<IOInstance> selected = descendants.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public IOInstance? FindFirstChildNamed(string name)
        {
            foreach (IOInstance child in _children)
                if (child.Name == name)
                    return child;

            return null;
        }

        public TInstance? FindFirstChildWhichIsA<TInstance>()
            where TInstance : class, IOInstance
        {
            foreach (IOInstance child in _children)
                if (child is TInstance instance)
                    return instance;

            return null;
        }

        public IOInstance? FindFirstDescendantNamed(string name)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();

            foreach (IOInstance descendant in descendants)
                if (descendant.Name == name)
                    return descendant;

            return null;
        }

        public TInstance? FindFirstDescendantWhichIsA<TInstance>()
            where TInstance : class, IOInstance
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();

            foreach (IOInstance descendant in descendants)
                if (descendant is TInstance instance)
                    return instance;

            return null;
        }

        public IOPrototype? FindFirstAncestorNamed(string name)
        {
            for (IOInstance? next = Parent; next != null; next = next.Parent)
            {
                if (next.Name == name)
                    return next;

                if (next is OStorage storage && storage.Window.Name == name)
                    return storage.Window;

                if (next is OScenes scenes && scenes.Window.Name == name)
                    return scenes.Window;
            }

            return null;
        }

        public TPrototype? FindFirstAncestorWhichIsA<TPrototype>()
            where TPrototype : class, IOPrototype
        {
            if (typeof(TPrototype) == typeof(OWindow))
            {
                for (IOInstance? next = Parent; next != null; next = next.Parent)
                {
                    if (next is OStorage storage)
                        return storage.Window as TPrototype;

                    if (next is OScenes scenes)
                        return scenes.Window as TPrototype;
                }

                return null;
            }

            for (IOInstance? next = Parent; next != null; next = next.Parent)
                if (next is TPrototype instance)
                    return instance;

            return null;
        }

        public bool IsAChildOf<TPrototype>()
            where TPrototype : class, IOPrototype
        {
            return Parent is TPrototype;
        }

        public bool IsADescendantOf<TPrototype>()
            where TPrototype : class, IOPrototype
        {
            if (typeof(TPrototype) == typeof(OWindow))
                return IsADescendantOf<OStorage>() || IsADescendantOf<OScenes>();

            for (IOInstance? next = Parent; next != null; next = next.Parent)
                if (next is TPrototype)
                    return true;

            return false;
        }

        public IOPrototype? Clone(bool cloneChildren, bool cloneDescendants)
        {
            OScene clone = new OScene(null, Name, Main);

            if (cloneChildren || cloneDescendants)
                foreach (IOInstance child in _children)
                {
                    IOInstance? childClone = (IOInstance?)child.Clone(cloneDescendants, cloneDescendants);
                    
                    if (childClone != null)
                        clone.AddChild(childClone);
                }

            return clone;
        }

        public void Dispose()
        {
            foreach (IOInstance child in _children)
                child.Dispose();

            _children.Clear();
        }

        public ORenderInfo? Render()
        {
            if (_parent is not OScenes scenes)
                return null;

            if (scenes.Active != scenes.GetScenes().IndexOf(this))
                return null;

            ORenderInfo result = new ORenderInfo();
            List<OGeometryInfo> sceneGeometries = new();
            List<(OGeometryInfo geometry, int layer)> uiGeometries = new();
            List<OShapeInfo> sceneShapes = new();
            List<(OShapeInfo shape, int layer)> uiShapes = new();

            foreach (IOModifier modifier in _modifiers)
                if (modifier.Active && modifier.CallTime.HasFlag(IOModifier.OModifierCallTime.PreRenderChildren))
                    modifier.Process<object, object>(IOModifier.OModifierCallTime.PreRenderChildren, this);

            foreach (IOInstance child in _children)
            {
                ORenderInfo? info = child.Render();

                if (info is null)
                    continue;

                bool isInterface = child is OkInterface.IOGui || child is OkInterface.IOInterface;

                if (!isInterface)
                {
                    sceneGeometries.AddRange(info.Geometries);
                    sceneShapes.AddRange(info.Shapes);
                }
                else
                {
                    int layer = child switch
                    {
                        OkInterface.IOGui gui => gui.Layer,
                        OkInterface.IOInterface ui => ui.Layer,
                        _ => 0
                    };

                    foreach (OGeometryInfo geometry in info.Geometries)
                        uiGeometries.Add((geometry, layer));

                    foreach (OShapeInfo shape in info.Shapes)
                        uiShapes.Add((shape, layer));
                }

                foreach (IOModifier modifier in _modifiers)
                    if (modifier.Active && modifier.CallTime.HasFlag(IOModifier.OModifierCallTime.RenderChildren))
                        modifier.Process<object, ORenderInfo?>(IOModifier.OModifierCallTime.RenderChildren, info);
            }

            uiGeometries = uiGeometries.OrderBy(e => e.layer).ToList();
            uiShapes = uiShapes.OrderBy(e => e.layer).ToList();

            result.Geometries.AddRange(sceneGeometries);
            result.Geometries.AddRange(uiGeometries.Select(e => e.geometry));
            result.Shapes.AddRange(sceneShapes);
            result.Shapes.AddRange(uiShapes.Select(e => e.shape));

            foreach (IOModifier modifier in _modifiers)
                if (modifier.Active && modifier.CallTime.HasFlag(IOModifier.OModifierCallTime.PostRenderChildren))
                    result = modifier.Process<ORenderInfo?, ORenderInfo?>(IOModifier.OModifierCallTime.PostRenderChildren, result);
            
            return result;
        }

        // To String

        public override string ToString()
        {
            return $"<OScene Name=\"{Name}\" Main={Main}>";
        }
    }
}