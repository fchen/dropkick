namespace dropkick.tests.infrastructure.containers
{
    using System;
    using Castle.Windsor;
    using dropkick.infrastructure.containers;
    using dropkick.infrastructure.logging;
    using dropkick.infrastructure.logging.custom;
    using fervent.testing.bdd;

    public class ContainerSpecs
    {
        public abstract class concern_for_container : observations_for_a_static_sut
        {
            protected LoggerFactory result;
            protected InversionContainer the_container;

            protected override void in_this_context()
            {
                the_container = an<InversionContainer>();
                Container.initialize_with(the_container);
            }

            protected override void because()
            {
                result = Container.get_an_instance_of<LoggerFactory>();
            }

            protected override void after_each_observation()
            {
                base.after_each_observation();
                Container.initialize_with(null);
            }
        }

        public class when_asking_the_container_to_initialize : concern_for_container
        {
            protected override void in_this_context()
            {
                base.in_this_context();
            }

            [Observation]
            public void the_container_interface_should_not_be_of_type_IWindsorContainer()
            {
                the_container.should_not_be_an_instance_of<IWindsorContainer>();
            }
        }

        public class when_asking_the_container_to_resolve_an_item_and_it_has_the_item_registered : concern_for_container
        {
            protected override void in_this_context()
            {
                base.in_this_context();
                when(the_container).is_asked_for_its(x => x.Resolve<LoggerFactory>()).Return(new Log4NetLogFactory());
            }

            [Observation]
            public void should_be_an_instance_of_Log4NetLogFactory()
            {
                result.should_be_an_instance_of<Log4NetLogFactory>();
            }

            [Observation]
            public void should_be_an_instance_of_LoggerFactory()
            {
                result.should_be_an_instance_of<LoggerFactory>();
            }
        }

        public class when_asking_the_container_to_resolve_an_item_and_it_does_not_have_the_item_registered : concern_for_container
        {
            private Action attempting_to_get_an_unregistered_item;

            protected override void in_this_context()
            {
                base.in_this_context();
                when(the_container).is_told_to(x => x.Resolve<LoggerFactory>()).Throw(
                    new Exception(string.Format("Had an error finding components registered for {0}.", typeof(LoggerFactory))));
            }

            protected override void because()
            {
                attempting_to_get_an_unregistered_item = doing(() => the_container.Resolve<LoggerFactory>());
            }

            [Observation]
            public void should_throw_an_exception()
            {
                attempting_to_get_an_unregistered_item.should_throw_an<Exception>();
            }
        }
    }
}