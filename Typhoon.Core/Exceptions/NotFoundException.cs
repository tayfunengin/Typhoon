namespace Typhoon.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(int id) : base($"Entity with Id {id} was not found!") { }
    }
}
