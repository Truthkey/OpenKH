namespace khkh_xldMii.V
{
    using System;

    public class ProtInvalidTypeException : ApplicationException
    {
        public ProtInvalidTypeException() : base("Has to be typ1 or typ2")
        {
        }
    }
}

