namespace VoxSmart.FinancialEntityExtractor.Exceptions;

public sealed class ReadInformationSourceException : Exception
{
    /// <summary>
    /// Constructs a new instance of the <see cref="ReadInformationSourceException"/> class
    /// </summary>
    public ReadInformationSourceException()
    {
    }

    /// <summary>
    /// Constructs a new instance of the <see cref="ReadInformationSourceException"/> class
    /// </summary>
    /// <param name="message">Message for the exception</param>
    public ReadInformationSourceException(string message) : base(message)
    {
    }

    /// <summary>
    /// Constructs a new instance of the <see cref="ReadInformationSourceException"/> class
    /// </summary>
    /// <param name="message">Message for the exception</param>
    /// <param name="innerException">Optional inner exception</param>
    public ReadInformationSourceException(string message, Exception? innerException) : base(message, innerException)
    {
    }
}