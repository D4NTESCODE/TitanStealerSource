using System.Xml.Linq;

namespace Titan.Socket.Analyzer.Records
{
    internal record AutoFillRecord
    {
        public string InputName { get; set; }
        public string Value { get; set; }

        public AutoFillRecord(string input, string val)
        {
            InputName = input;
            Value = val;
        }

        public override string ToString()
        {
            return $"-- Input Name(class): {InputName} *#* Value: {Value}";
        }
    }
}
