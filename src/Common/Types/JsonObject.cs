using System;
using System.Linq;

namespace CodeEfficiencyBenchmark.Common.Types
{
    public class JsonObject
    {
        public string Name { get; set; }

        public int Value { get; set; }

        public JsonObject[] Children { get; set; }

        public JsonObject(string name, int value, JsonObject[] children)
        {
            Name = name;
            Value = value;
            Children = children;
        }

        public static JsonObject Generate(int depth, int count)
        {
            JsonObject[] children;

            if (depth == 0)
            {
                children = Array.Empty<JsonObject>();
            }
            else
            {
                children = Enumerable.Range(1, count).Select(x => Generate(depth - 1, count)).ToArray();
            }

            return new JsonObject($"Name_{depth}_{count}", depth * count, children);
        }
    }
}
