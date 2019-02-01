using System;
using System.Collections.Generic;
using System.Text;

namespace QrF.Core.ComFr.ViewPort.ElementUI
{
    public class Node
    {
        public string id { get; set; }
        public string label { get; set; }
        public bool disabled { get; set; }
        public List<Node> children { get; set; }
        public bool isLeaf { get; set; }
    }
}
