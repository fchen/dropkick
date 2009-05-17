namespace dropkick.tests.infrastructure.mapping
{
    using bdddoc.core;
    using developwithpassion.bdd.contexts;
    using developwithpassion.bdd.mbunit;
    using developwithpassion.bdd.mbunit.standard;
    using developwithpassion.bdd.mbunit.standard.observations;
    using dropkick.Dsl.Files;
    using dropkick.infrastructure.containers;
    using dropkick.infrastructure.mapping;
    using Engine;
    using Rhino.Mocks;

    public class MapBuilderSpecs
    {
        public abstract class concern_for_map_builder : observations_for_a_sut_without_a_contract<MapBuilder<string>>
        {
        }

        [Concern(typeof(MapBuilder<string>))]
        public class when_map_builder_is_called_to_convert_an_item_from_something_to_something_else : concern_for_map_builder
        {
            protected static object result;
            static string command;
            protected static InversionContainer the_container;
            protected static Mapper<string, Task> mock_mapper;

            context c = () =>
                {
                    the_container = an<InversionContainer>();
                    Container.initialize_with(the_container);

                    mock_mapper = an<Mapper<string, Task>>();

                    command = "dudue";
                    provide_a_basic_sut_constructor_argument(command);

                    the_container.Stub(x => x.Resolve<Mapper<string, Task>>()).Return(mock_mapper);
                    mock_mapper.Stub(x => x.map_from(command)).Return(new CopyTask("d", "c"));
                };

            because b = () => { result = sut.to<Task>(); };

            [Observation]
            public void should_map_successfully()
            {
                result.should_be_an_instance_of<Task>();
            }

            [Observation]
            public void should_have_called_the_container_to_resolve_an_IMapper_of_the_correct_type()
            {
                the_container.AssertWasCalled(x => x.Resolve<Mapper<string, Task>>());
            }

            [Observation]
            public void should_have_called_the_map_from_method_on_the_correct_mapper()
            {
                mock_mapper.AssertWasCalled(x => x.map_from(command));
            }
        }
    }
}