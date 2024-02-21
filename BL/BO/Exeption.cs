namespace BO;

using DO;
using System;

[Serializable]
public class BlDoesNotExistExeption : Exception
{
   public BlDoesNotExistExeption(string? message) : base(message) { }
   
   //exeption with inner exception
    public BlDoesNotExistExeption(string? massage, DalDoesNotExistExeption innerExeption)
        : base(massage, innerExeption) 
    {
    }
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
    public BlAlreadyExistsException(string? massage, DalAlreadyExistsException innerExeption)
      : base(massage, innerExeption)
    {
    }
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

    public BlUpdateImpossible(string massege) : base(massege) { }
}

[Serializable]
public class BlEngineerHasTaskExeption : Exception
{
    public BlEngineerHasTaskExeption(string? message) : base(message) { }
}


// generic error from DAL
[Serializable]
public class BlDalError : Exception
{ 
    public BlDalError(string? errorMassag, Exception innerMassage) : base(errorMassag, innerMassage)
    { }
}