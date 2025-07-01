using System;
using System.Runtime.Serialization;

namespace waterb.Networking
{
	[Serializable]
	public class InvalidRequestTypeException : Exception
	{
		public InvalidRequestTypeException() { }
		public InvalidRequestTypeException(string message) : base(message) { }
		public InvalidRequestTypeException(string message, Exception inner) : base(message, inner) { }
		protected InvalidRequestTypeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}