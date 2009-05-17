namespace dropkick.infrastructure.mapping
{
    public interface Mapper<From, To>
    {
        To map_from(From from);
    }
}