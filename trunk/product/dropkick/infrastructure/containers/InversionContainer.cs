namespace dropkick.infrastructure.containers
{
    public interface InversionContainer
    {
        TypeToReturn Resolve<TypeToReturn>();
    }
}