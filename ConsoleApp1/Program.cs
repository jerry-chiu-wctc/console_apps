// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;



public class TreeNode
{
    public int Value;
    public List<TreeNode> Children = new();

    public TreeNode(int value)
    {
        Value = value;
    }
}

public class MinMaxResult
{
    public int Min;
    public int Max;

    public MinMaxResult(int min, int max)
    {
        Min = min;
        Max = max;
    }
}

public static class TreeMinMax
{
    public static MinMaxResult FindMinMax(TreeNode root)
    {
        if (root == null)
            throw new ArgumentException("Tree cannot be null");

        return Dfs(root);
    }

    // depth-first search
    private static MinMaxResult Dfs(TreeNode node)
    {
        int min = node.Value;
        int max = node.Value;

        foreach (var child in node.Children)
        {
            var childResult = Dfs(child);
            min = Math.Min(min, childResult.Min);
            max = Math.Max(max, childResult.Max);
        }

        return new MinMaxResult(min, max);
    }
    
    // Example usage
    /*public static void Main(string[] args)
    {
        TreeNode root = new TreeNode(10);
        TreeNode child1 = new TreeNode(5);
        TreeNode child2 = new TreeNode(20);
        TreeNode child3 = new TreeNode(3);

        root.Children.Add(child1);
        root.Children.Add(child2);
        child1.Children.Add(child3);

        MinMaxResult result = TreeMinMax.FindMinMax(root);
        Console.WriteLine($"Min: {result.Min}, Max: {result.Max}");
    }*/
    
    public static void Main(string[] args)
    {
        var rng = new Random(42);

        TreeNode BuildTree(int depth)
        {
            var node = new TreeNode(rng.Next(1, 1001)); // 1..1000

            if (depth >= 5)
                return node;

            int childCount = rng.Next(1, 4); // 1 to 3 children
            for (int i = 0; i < childCount; i++)
            {
                node.Children.Add(BuildTree(depth + 1));
            }

            return node;
        }
        
        void PrintTree(TreeNode node, int depth)
        {
            Console.WriteLine($"{new string(' ', depth * 2)}- Value: {node.Value} (Children: {node.Children.Count})");
            foreach (var child in node.Children)
            {
                PrintTree(child, depth + 1);
            }
        }

        TreeNode root = BuildTree(1);
        
        Console.WriteLine("Generated tree:");
        PrintTree(root, 0);

        MinMaxResult result = TreeMinMax.FindMinMax(root);
        Console.WriteLine($"Min: {result.Min}, Max: {result.Max}");
    }
}
