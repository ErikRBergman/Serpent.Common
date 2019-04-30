namespace Serpent.Common.Xml
{
    using System;

    public class Result<T>
    {
        public Result(string inputFilename, ResultStatus status, T resultItem)
        {
            this.InputFilename = inputFilename;
            this.Status = status;
            this.ResultItem = resultItem;
        }

        public Result(string inputFilename, ResultStatus status, Exception exception)
        {
            this.InputFilename = inputFilename;
            this.Status = status;
            this.Exception = exception;
        }

        public Result(string inputFilename, ResultStatus status, T resultItem, Exception exception)
        {
            this.InputFilename = inputFilename;
            this.Status = status;
            this.ResultItem = resultItem;
            this.Exception = exception;
        }

        public Exception Exception { get; }

        public string InputFilename { get; }

        public T ResultItem { get; }

        public ResultStatus Status { get; }

        public Result<T> WithFilename(string filename)
        {
            return new Result<T>(filename, this.Status, this.ResultItem, this.Exception);
        }
    }
}