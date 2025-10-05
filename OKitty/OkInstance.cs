// Imports

using System.Collections.ObjectModel;
using static OKitty.OkMath;
using static OKitty.OkScript;

namespace OKitty;

// OkInstance

public static class OkInstance
{
    // Interfaces

    public interface IOPrototype : IDisposable
    {
        // Properties

        public string Icon { get; }
        public string InstanceName { get; }
        public string Name { get; set; }

        // Methods

        public IOPrototype? Clone(bool cloneChildren, bool cloneDescendants);
        public ORenderInfo? Render();

        public string ToString()
        {
            return $"<{InstanceName} Name=\"{Name}\">";
        }
    }

    public interface IOInstance : IOPrototype
    {
        // Properties

        public IOInstance? Parent { get; set; }
        public HashSet<string> Tags { get; }

        // Methods and Functions

        public ReadOnlyCollection<IOInstance> GetChildren();

        public ReadOnlyCollection<IOInstance> GetDescendants()
        {
            List<IOInstance> descendants = new List<IOInstance>();
            ReadOnlyCollection<IOInstance> children = GetChildren();

            descendants.AddRange(children);

            foreach (IOInstance descendant in descendants)
                descendants.AddRange(descendant.GetDescendants());

            return new ReadOnlyCollection<IOInstance>(descendants);
        }

        public ReadOnlyCollection<IOInstance> ChildrenSelector(Func<IOInstance, bool> selector)
        {
            ReadOnlyCollection<IOInstance> children = GetChildren();
            List<IOInstance> selected = children.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> ChildrenSelector(Func<IOInstance, int, bool> selector)
        {
            ReadOnlyCollection<IOInstance> children = GetChildren();
            List<IOInstance> selected = children.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> DescendantsSelector(Func<IOInstance, bool> selector)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();
            List<IOInstance> selected = descendants.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> DescendantsSelector(Func<IOInstance, int, bool> selector)
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

        public IOInstance? FindFirstChildWhichIsA(string instance)
        {
            ReadOnlyCollection<IOInstance> children = GetChildren();

            foreach (IOInstance child in children)
                if (child.InstanceName == instance)
                    return child;

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

        public IOInstance? FindFirstDescendantWhichIsA(string instance)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();

            foreach (IOInstance descendant in descendants)
                if (descendant.InstanceName == instance)
                    return descendant;

            return null;
        }

        public bool IsAChildOf(string instance)
        {
            return Parent is not null && Parent.InstanceName == instance;
        }

        public bool IsADescendantOf(string instance)
        {
            for (IOInstance? next = Parent; next != null; next = next.Parent)
                if (next.InstanceName == instance)
                    return true;

            return false;
        }

        public void AddChild(IOInstance child);
        public void RemoveChild(IOInstance child);

        // Events

        public event OInstanceEvents.OnChildAdded OnChildAdded;
        public event OInstanceEvents.OnChildRemoved OnChildRemoved;
    }

    // Structs

    public struct OEdgeInfo
    {
        // Properties

        public required OVector2<float> Start { get; init; }
        public required OVector2<float> End { get; init; }

        // To String

        public override string ToString()
        {
            return $"{Start} - {End}";
        }
    }

    public struct OFaceInfo
    {
        // Properties

        public required List<OEdgeInfo> Edges { get; init; }

        // To String

        public override string ToString()
        {
            return $"[{{string.Join(\", \", Edges)}}]";
        }
    }

    // Classes

    public class ORenderInfo
    {
        // Properties and Fields

        public List<OFaceInfo> Faces { get; private set; }
        public OFaceInfo? Mask { get; set; }

        // Methods and Functions

        public ORenderInfo(List<OFaceInfo>? faces = null, OFaceInfo? mask = null)
        {
            Faces = faces ?? new List<OFaceInfo>();
            Mask = mask;
        }

        // To String

        public override string ToString()
        {
            string faces = string.Join(", ", Faces);
            string mask = Mask is not null ? $"{Mask}" : "[null]";

            return $"M{mask} F{faces}";
        }
    }

    public class OStorage : IOInstance
    {
        // Properties and Fields
        
        private OWindow _window;
        private List<IOInstance> _children;

        IOInstance? IOInstance.Parent { get; set; } = null;
        
        public OWindow Window => _window;
        public string Icon => "";
        public string InstanceName => "OStorage";
        public string Name { get; set; } = "OStorage";
        public HashSet<string> Tags { get; } = new HashSet<string>();
        
        // Events
        
        public event OInstanceEvents.OnChildAdded? OnChildAdded;
        public event OInstanceEvents.OnChildRemoved? OnChildRemoved;
        
        // Methods and Functions

        public OStorage(OWindow window, string name = "OStorage")
        {
            _window = window;
            Name = name;
        }

        public ReadOnlyCollection<IOInstance> GetChildren()
        {
            return new ReadOnlyCollection<IOInstance>(_children);
        }

        public ReadOnlyCollection<IOInstance> GetDescendants()
        {
            List<IOInstance> descendants = new List<IOInstance>();

            descendants.AddRange(_children);

            foreach (IOInstance descendant in descendants)
                descendants.AddRange(descendant.GetDescendants());

            return new ReadOnlyCollection<IOInstance>(descendants);
        }

        public ReadOnlyCollection<IOInstance> ChildrenSelector(Func<IOInstance, bool> selector)
        {
            List<IOInstance> selected = _children.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> ChildrenSelector(Func<IOInstance, int, bool> selector)
        {
            List<IOInstance> selected = _children.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> DescendantsSelector(Func<IOInstance, bool> selector)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();
            List<IOInstance> selected = descendants.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> DescendantsSelector(Func<IOInstance, int, bool> selector)
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

        public IOInstance? FindFirstChildWhichIsA(string instance)
        {
            foreach (IOInstance child in _children)
                if (child.InstanceName == instance)
                    return child;

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

        public IOInstance? FindFirstDescendantWhichIsA(string instance)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();

            foreach (IOInstance descendant in descendants)
                if (descendant.InstanceName == instance)
                    return descendant;

            return null;
        }
        
        public bool IsAChildOf(string instance)
        {
            return instance == "OWindow";
        }

        public bool IsADescendantOf(string instance)
        {
            return instance == "OWindow";
        }
        
        public IOPrototype? Clone(bool cloneChildren, bool cloneDescendants)
        {
            return null;
        }

        public void Dispose()
        {
            foreach (IOInstance child in _children)
                child.Dispose();
            
            _children.Clear();
        }

        public ORenderInfo? Render()
        {
            return null;
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
        private int _active;
    
        IOInstance? IOInstance.Parent { get; set; } = null;
    
        public OWindow Window => _window;
        public OScene Main => _main;
        public string Icon => "󰉏";
        public string InstanceName => "OScenes";
        public string Name { get; set; } = "OScenes";
        public HashSet<string> Tags { get; } = new HashSet<string>();

        public int Active
        {
            get => _active;
            set
            {
                if (value < 0 || value > _scenes.Count)
                {
                    ODebugger.Throw(new IndexOutOfRangeException($"\"Active\" can only be between 0 to {_scenes.Count}."));

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

        public ReadOnlyCollection<IOInstance> GetDescendants()
        {
            List<IOInstance> descendants = new List<IOInstance>();

            descendants.AddRange(_scenes);

            foreach (IOInstance descendant in descendants)
                descendants.AddRange(descendant.GetDescendants());

            return new ReadOnlyCollection<IOInstance>(descendants);
        }

        public ReadOnlyCollection<IOInstance> ChildrenSelector(Func<IOInstance, bool> selector)
        {
            List<IOInstance> selected = _scenes.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> ChildrenSelector(Func<IOInstance, int, bool> selector)
        {
            List<IOInstance> selected = _scenes.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> DescendantsSelector(Func<IOInstance, bool> selector)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();
            List<IOInstance> selected = descendants.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> DescendantsSelector(Func<IOInstance, int, bool> selector)
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

        public IOInstance? FindFirstChildWhichIsA(string instance)
        {
            if (instance == "OScene")
                return _main;

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

        public IOInstance? FindFirstDescendantWhichIsA(string instance)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();

            foreach (IOInstance descendant in descendants)
                if (descendant.InstanceName == instance)
                    return descendant;

            return null;
        }

        public bool IsAChildOf(string instance)
        {
            return instance == "OWindow";
        }

        public bool IsADescendantOf(string instance)
        {
            return instance == "OWindow";
        }
    
        public IOPrototype? Clone(bool cloneChildren, bool cloneDescendants)
        {
            return null;
        }
    
        public void Dispose()
        {
            foreach (IOInstance scene in _scenes)
                scene.Dispose();
    
            _scenes.Clear();
        }
    
        public ORenderInfo? Render()
        {
            ORenderInfo? info = _active == 0 ? _main.Render() : _scenes[_active - 1].Render();
    
            return info;
        }
    
        public void AddChild(IOInstance child)
        {
            if (!(child is OScene scene))
            {
                ODebugger.Throw(new ArgumentException("Only an OScene can be parented to an OScenes."));
                
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
            if (!(child is OScene scene))
            {
                ODebugger.Throw(new ArgumentException("Only an OScene can be parented to an OScenes."));
                
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
    
        public string Icon => "󰈟";
        public string InstanceName => "OScene";
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

                if (Main)
                {
                    if (!(_parent is null))
                    {
                        ODebugger.Throw(new ArgumentException("The parent of an OScene can not be changed."));

                        return;
                    }

                    if (!(value is OScenes scenes))
                    {
                        ODebugger.Throw(new ArgumentException("Parent must be an OScenes if this was a main scene."));

                        return;
                    }

                    if (scenes.Main != this)
                    {
                        ODebugger.Throw(new ArgumentException("The main scene of the provided OScenes is a different scene."));

                        return;
                    }

                    _parent = value;

                    return;
                }

                if (_parent is IOInstance)
                    _parent.RemoveChild(this);

                _parent = value;

                if (value is IOInstance)
                    _parent.AddChild(this);
            }
        }
    
        // Events
        
        public event OInstanceEvents.OnChildAdded? OnChildAdded;
        public event OInstanceEvents.OnChildRemoved? OnChildRemoved;
    
        // Methods and Functions
    
        public OScene(IOInstance? parent = null, string name = "OScene", bool main = false)
        {
            Name = name;
            Main = main;
    
            if (main && !(parent is null))
            {
                ODebugger.Throw(new ArgumentException("Parent must be null if this scene is a main scene."));
    
                return;
            }
    
            _parent = parent;
            _children = new List<IOInstance>();
            
            if (parent is IOInstance)
                parent.AddChild(this);
        }

        public ReadOnlyCollection<IOInstance> GetChildren()
        {
            return new ReadOnlyCollection<IOInstance>(_children);
        }

        public ReadOnlyCollection<IOInstance> GetDescendants()
        {
            List<IOInstance> descendants = new List<IOInstance>();

            descendants.AddRange(_children);

            foreach (IOInstance descendant in descendants)
                descendants.AddRange(descendant.GetDescendants());

            return new ReadOnlyCollection<IOInstance>(descendants);
        }

        public ReadOnlyCollection<IOInstance> ChildrenSelector(Func<IOInstance, bool> selector)
        {
            List<IOInstance> selected = _children.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> ChildrenSelector(Func<IOInstance, int, bool> selector)
        {
            List<IOInstance> selected = _children.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> DescendantsSelector(Func<IOInstance, bool> selector)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();
            List<IOInstance> selected = descendants.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> DescendantsSelector(Func<IOInstance, int, bool> selector)
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

        public IOInstance? FindFirstChildWhichIsA(string instance)
        {
            foreach (IOInstance child in _children)
                if (child.InstanceName == instance)
                    return child;

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

        public IOInstance? FindFirstDescendantWhichIsA(string instance)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();

            foreach (IOInstance descendant in descendants)
                if (descendant.InstanceName == instance)
                    return descendant;

            return null;
        }

        public bool IsAChildOf(string instance)
        {
            return _parent is not null && _parent.InstanceName == instance;
        }

        public bool IsADescendantOf(string instance)
        {
            for (IOInstance? next = _parent; next != null; next = next.Parent)
                if (next.InstanceName == instance)
                    return true;

            return false;
        }

        public IOPrototype? Clone(bool cloneChildren, bool cloneDescendants)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            foreach (IOInstance child in _children)
                child.Dispose();

            _children.Clear();
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

        public ORenderInfo? Render()
        {
            // TODO!

            return null;
        }

        public override string ToString()
        {
            return $"<OScene Name=\"{Name}\" Main={Main}>";
        }
    }
}