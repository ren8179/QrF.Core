using QrF.Core.Utils.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QrF.Core.ComFr.ViewPort.ElementUI
{
    public class Tree<T> where T : class
    {
        IEnumerable<T> DataSource;
        Func<T, string> valueProperty;
        Func<T, string> parentProperty;
        Func<T, string> textProperty;
        bool _disabled;
        Dictionary<string, string> _events = new Dictionary<string, string>();
        List<string> _plugins = new List<string>();
        List<Node> nodes;
        string _rootId;
        string _name;
        string _check_callback;

        public Tree()
        {
            this._name = Guid.NewGuid().ToString("N");
        }
        public Tree<T> Name(string name)
        {
            this._name = name;
            return this;
        }
        public Tree<T> Source(IEnumerable<T> source)
        {
            this.DataSource = source;
            return this;
        }
        public Tree<T> On(string events, string fun)
        {
            if (_events.ContainsKey(events))
            {
                _events[events] = fun;
            }
            else
            {
                _events.Add(events, fun);
            }
            return this;
        }
        public Tree<T> Id(Func<T, string> value)
        {
            valueProperty = value;
            return this;
        }
        public Tree<T> Text(Func<T, string> text)
        {
            textProperty = text;
            return this;
        }
        public Tree<T> Parent(Func<T, string> parent)
        {
            parentProperty = parent;
            return this;
        }

        public Tree<T> AddPlugin(string plugin)
        {
            if (!_plugins.Contains(plugin))
            {
                _plugins.Add(plugin);
            }
            return this;
        }
        public Tree<T> RootId(string rootId)
        {
            _rootId = rootId;
            return this;
        }
        public Tree<T> CheckCallBack(string fun)
        {
            _check_callback = fun;
            return this;
        }
        public List<Node> ToNode(Func<T, string> value, Func<T, string> text, Func<T, string> parent, string rootId)
        {
            return ToNode(value, text, parent, rootId, false);
        }
        public List<Node> ToNode(Func<T, string> value, Func<T, string> text, Func<T, string> parent, string rootId, bool disabled)
        {
            valueProperty = value;
            parentProperty = parent;
            textProperty = text;
            _rootId = rootId;
            _disabled = disabled;
            InitDode();
            return nodes;
        }
        private void InitDode()
        {
            if (nodes == null && DataSource != null)
            {
                nodes = new List<Node>();
                DataSource
               .Where(m => parentProperty(m) == _rootId)
               .Each(m =>
               {
                   nodes.Add(InitNode(m));
               });
            }
        }
        private Node InitNode(T data)
        {
            Node node = new Node();
            node.id = valueProperty(data);
            node.label = textProperty(data);
            node.disabled = _disabled;
            node.children = new List<Node>();
            DataSource.Where(m => parentProperty(m) == node.id).Each(m => node.children.Add(InitNode(m)));
            node.isLeaf = node.children==null || node.children.Count == 0;
            return node;
        }
    }
}
