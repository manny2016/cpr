namespace Org.Joey.Common
{
    public interface ITransientFaultDetecter<T>
    {
        bool Detect(T condition,bool ifHasDetailErrorMessageThrowIt);
    }
}