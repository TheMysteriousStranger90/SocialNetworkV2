using System.Runtime.Serialization;

namespace BLL.Exceptions;

[Serializable]
public class SocialNetworkException : Exception
{
    private static readonly string DefaultMessage = "Exception was thrown.";

    public SocialNetworkException() : base(DefaultMessage)
    {
    }

    public SocialNetworkException(string message) : base(message)
    {
    }

    public SocialNetworkException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected SocialNetworkException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}