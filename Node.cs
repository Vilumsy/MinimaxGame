using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Node
    {
        public string data;
        public Node? parent;
        public Node[] child = new Node[3];
        public List<string> pairs = new List<string>();
        public int ScoreMax = 0;
        public int ScoreMin = 0;
        public int tick = 0;
        public int minimax;


        public Node(string data, Node? parent, int ScoreMin, int ScoreMax, int tick, List<string>? pairs)
        {
            this.parent = parent;
            this.data = data;
            this.ScoreMax = ScoreMax;
            this.ScoreMin = ScoreMin;
            this.tick = tick;
            this.pairs = pairs;
        }

    }
}
