using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    static class Backend
    {
        public static List<Node> virsotnes = new List<Node>();//do not need rly
        public static bool aistarts;
        public static int j = 0;
        public static void createTree2(string param, Node lastNode)
        {
            param = SortString(param);



            if (param.Length > 1)
            {
                List<string> paramPairs = new List<string>();
                for (int i = 1; i <= param.Length - 1; i++) paramPairs.Add($"{param[i - 1]}{param[i]}");
                for (int i = 0; i < paramPairs.Count; i++)
                {

                    int firstnumber = Convert.ToInt16(paramPairs[i].Substring(0, 1), 10);
                    int secondnumber = Convert.ToInt16(paramPairs[i].Substring(1, 1), 10);
                    int difference = firstnumber - secondnumber;
                    var newParam = param.Select(x => new string(x, 1)).ToList();
                    if (difference >= secondnumber)
                    {
                        newParam.Remove(firstnumber.ToString());
                        var newString = string.Join(String.Empty, newParam);
                        if (lastNode.tick == 0)
                        {
                            Node temp = new Node(newString, lastNode, lastNode.ScoreMin, lastNode.ScoreMax + difference, 1, paramPairs);
                            if (lastNode.child[0] == null) { lastNode.child[0] = temp; }
                            else if (lastNode.child[1] == null) { lastNode.child[1] = temp; }
                            else if (lastNode.child[2] == null) { lastNode.child[2] = temp; }

                            createTree2(newString, temp);
                        }
                        else if (lastNode.tick == 1)
                        {
                            Node temp = new Node(newString, lastNode, lastNode.ScoreMin + difference, lastNode.ScoreMax, 0, paramPairs);
                            if (lastNode.child[0] == null) { lastNode.child[0] = temp; }
                            else if (lastNode.child[1] == null) { lastNode.child[1] = temp; }
                            else if (lastNode.child[2] == null) { lastNode.child[2] = temp; }

                            createTree2(newString, temp);
                        }

                    }
                    else if (difference < firstnumber && difference != 0)
                    {
                        newParam.Remove(firstnumber.ToString());
                        newParam.Add(difference.ToString());
                        var newString = string.Join(String.Empty, newParam);
                        if (lastNode.tick == 0)
                        {
                            Node temp = new Node(newString, lastNode, lastNode.ScoreMin, lastNode.ScoreMax + difference, 1, paramPairs);
                            if (lastNode.child[0] == null) { lastNode.child[0] = temp; }
                            else if (lastNode.child[1] == null) { lastNode.child[1] = temp; }
                            else if (lastNode.child[2] == null) { lastNode.child[2] = temp; }

                            createTree2(newString, temp);

                        }
                        else if (lastNode.tick == 1)
                        {
                            Node temp = new Node(newString, lastNode, lastNode.ScoreMin + difference, lastNode.ScoreMax, 0, paramPairs);
                            if (lastNode.child[0] == null) { lastNode.child[0] = temp; }
                            else if (lastNode.child[1] == null) { lastNode.child[1] = temp; }
                            else if (lastNode.child[2] == null) { lastNode.child[2] = temp; }

                            createTree2(newString, temp);
                        }

                    }
                    else if (difference == 0)
                    {
                        newParam.Remove(firstnumber.ToString());
                        newParam.Remove(secondnumber.ToString());
                        var newString = string.Join(String.Empty, newParam);
                        if (lastNode.tick == 0)
                        {
                            Node temp = new Node(newString, lastNode, lastNode.ScoreMin, lastNode.ScoreMax + 3, 1, paramPairs);
                            if (lastNode.child[0] == null) { lastNode.child[0] = temp; }
                            else if (lastNode.child[1] == null) { lastNode.child[1] = temp; }
                            else if (lastNode.child[2] == null) { lastNode.child[2] = temp; }

                            createTree2(newString, temp);

                        }
                        else if (lastNode.tick == 1)
                        {
                            Node temp = new Node(newString, lastNode, lastNode.ScoreMin + 3, lastNode.ScoreMax, 0, paramPairs);
                            if (lastNode.child[0] == null) { lastNode.child[0] = temp; }
                            else if (lastNode.child[1] == null) { lastNode.child[1] = temp; }
                            else if (lastNode.child[2] == null) { lastNode.child[2] = temp; }

                            createTree2(newString, temp);

                        }
                    }

                }
            }
            else if (param.Length == 1)
            {
                int firstnumber = Convert.ToInt16(param, 10);
                if (lastNode.tick == 0)
                {
                    Node temp = new Node(String.Empty, lastNode, lastNode.ScoreMin, lastNode.ScoreMax + firstnumber, 1, null);
                    if (lastNode.child[0] == null) { lastNode.child[0] = temp; }
                    else if (lastNode.child[1] == null) { lastNode.child[1] = temp; }
                    else if (lastNode.child[2] == null) { lastNode.child[2] = temp; }
                    createTree2(String.Empty, temp);

                }
                else if (lastNode.tick == 1)
                {
                    Node temp = new Node(String.Empty, lastNode, lastNode.ScoreMin + firstnumber, lastNode.ScoreMax, 0, null);
                    if (lastNode.child[0] == null) { lastNode.child[0] = temp; }
                    else if (lastNode.child[1] == null) { lastNode.child[1] = temp; }
                    else if (lastNode.child[2] == null) { lastNode.child[2] = temp; }
                    createTree2(String.Empty, temp);

                }
            }
            else if (param.Length == 0)
            {
                if (lastNode.ScoreMin > lastNode.ScoreMax) lastNode.minimax = 1;
                if (lastNode.ScoreMin < lastNode.ScoreMax) lastNode.minimax = -1;
                if (lastNode.ScoreMin == lastNode.ScoreMax) lastNode.minimax = 0;
            }
        }
        public static string SortString(string param)
        {
            string[] test = param.ToCharArray().Select(c => c.ToString()).ToArray();
            Array.Sort(test);
            Array.Reverse(test);
            return String.Join(string.Empty, test);
        }

        public static int[] MiniMaxSmol(Node? next)
        {
            int[] arr = new int[3];
            if (next.tick == 0)
            {
                arr[0] = 99;
                arr[1] = 99;
                arr[2] = 99;
            }
            else
            {
                arr[0] = -99;
                arr[1] = -99;
                arr[2] = -99;
            }

            for (int i = 0; i < 3; i++)
            {

                if (next.child[i] != null)
                {
                    if (next.tick == 0) arr[i] = MiniMaxSmol(next.child[i]).Max();
                    else if (next.tick == 1) arr[i] = MiniMaxSmol(next.child[i]).Min();
                }
                else arr[0] = next.minimax;
                if (next.tick == 0) next.minimax = arr.Min();
                else if (next.tick == 1) next.minimax = arr.Max();
            }
            return arr;
        }

        
    }
}
