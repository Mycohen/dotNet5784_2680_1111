namespace DO;
using System;


[Serializable]
public class DalDoesNotExistExeption : Exception
{
    public DalDoesNotExistExeption(string? message) : base(message) {}
}

[Serializable]
public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException(string? message) : base(message) { }
}

[Serializable]
public class DalDeletionImpossible : Exception
{
    public DalDeletionImpossible() :

    base("This entity can not be deleted - please deactivate instead")
    { }

}
public class DalXMLFileLoadCreateException : Exception
{
    public DalXMLFileLoadCreateException(string? message) : base(message) { }
}




