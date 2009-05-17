namespace dropkick.infrastructure.mapping
{
    using containers;
    using logging;

    public class MapBuilder<From>
    {
        private readonly From from_object;

        public MapBuilder(From from_object)
        {
            this.from_object = from_object;
        }

        public To to<To>()
        {
            Log.bound_to(this).Debug("{0} is calling the container for an Mapper<{1},{2}>",ApplicationParameters.name,typeof(From).Name,typeof(To).Name);
            return Container.get_an_instance_of<Mapper<From,To>>().map_from(from_object);
        }
    }
}