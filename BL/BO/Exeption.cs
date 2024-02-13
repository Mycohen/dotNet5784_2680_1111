namespace BO;
using System;

[Serializable]
public class BlDoesNotExistExeption : Exception
{
   public BlDoesNotExistExeption(string? message) : base(message) { }
}
[Serializable]
public class BlInvalidInputException : Exception
{
    public BlInvalidInputException(string? message) : base(message) { }
}
public class BlNullException : Exception
{
    public BlNullException(string? message) : base(message) { }
}

[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
}

[Serializable]
public class BlDeletionImpossible : Exception
{
    public BlDeletionImpossible() :

    base("This entity can not be deleted - please deactivate instead")
    { }
}
[Serializable]
public class BlUpdateImpossible : Exception
{
    public BlUpdateImpossible() :

    base("This entity can not be updated - please deactivate instead")
    { }
}

