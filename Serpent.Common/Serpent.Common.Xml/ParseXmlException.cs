namespace Serpent.Common.Xml
{
    using System;

    public class ParseXmlException : Exception
    {
        public ParseXmlException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ParseXmlException(string message, string filename, Exception innerException)
            : base(message, innerException)
        {
            this.Filename = filename;
        }

        public string Filename { get; internal set; }
    }
}