namespace dropkick.tests.infrastructure.containers.custom
{
    using System;
    using bdddoc.core;
    using Castle.MicroKernel;
    using Castle.Windsor;
    using dropkick.infrastructure.containers;
    using dropkick.infrastructure.logging;
    using dropkick.infrastructure.logging.custom;
    using fervent.testing.bdd;
    using WindsorContainer=dropkick.infrastructure.containers.custom.WindsorContainer;

    public class WindsorContainerSpecs
    {
        public abstract class concern_for_windsor_container : observations_for_a_sut_with_a_contract<InversionContainer,
                                                                  WindsorContainer>
        {
            protected IWindsorContainer the_container;

            protected override void in_this_context()
            {
                base.in_this_context();
                provide_a_basic_sut_constructor_argument(the_container);
            }

            protected override void after_each_observation()
            {
                base.after_each_observation();
                Container.initialize_with(null);
            }
        }

        public abstract class concerns_using_a_fake_container : concern_for_windsor_container
        {
            protected override void in_this_context()
            {
                the_container = an<IWindsorContainer>();
                base.in_this_context();
            }
        }

        public abstract class concerns_using_a_real_container : concern_for_windsor_container
        {
        }

        [Concern(typeof(Castle.Windsor.WindsorContainer))]
        public class when_the_container_is_initialized : concerns_using_a_fake_container
        {
            protected override void in_this_context()
            {
                the_container = an<IWindsorContainer>();
                base.in_this_context();
            }

            protected override void because()
            {
            }

            [Observation]
            public void should_be_an_instance_of_IWindsorContainer()
            {
                the_container.should_be_an_instance_of<IWindsorContainer>();
            }
        }

        [Concern(typeof(Castle.Windsor.WindsorContainer))]
        public class when_asking_the_container_for_an_item_and_it_has_that_that_item_registered : concerns_using_a_fake_container
        {
            private LoggerFactory result;

            protected override void in_this_context()
            {
                base.in_this_context();

                when(the_container).is_told_to(x => x.Resolve<LoggerFactory>()).Return(new Log4NetLogFactory());
            }

            protected override void because()
            {
                result = sut.Resolve<LoggerFactory>();
            }

            [Observation]
            public void should_return_the_item_successfully()
            {
                result.should_be_an_instance_of<LoggerFactory>();
            }
        }

        [Concern(typeof(Castle.Windsor.WindsorContainer))]
        public class when_asking_the_container_using_castle_windsor_for_an_item_and_it_has_that_that_item_registered :
            concerns_using_a_real_container
        {
            private LoggerFactory result;

            protected override void in_this_context()
            {
                the_container = new Castle.Windsor.WindsorContainer();
                the_container.AddComponent("h", typeof(LoggerFactory), typeof(Log4NetLogFactory));
                base.in_this_context();
            }

            protected override void because()
            {
                result = sut.Resolve<LoggerFactory>();
            }

            [Observation]
            public void should_return_the_item_successfully()
            {
                result.should_be_an_instance_of<Log4NetLogFactory>();
            }
        }

        [Concern(typeof(Castle.Windsor.WindsorContainer))]
        public class when_asking_the_container_to_resolve_an_item_and_it_does_not_have_the_item_registered :
            concerns_using_a_fake_container
        {
            private Action attempting_to_get_an_unregistered_item;

            protected override void in_this_context()
            {
                base.in_this_context();
                when(the_container).is_told_to(x => x.Resolve<LoggerFactory>()).Throw(
                    new ComponentNotFoundException(typeof(LoggerFactory)));
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

        [Concern(typeof(Castle.Windsor.WindsorContainer))]
        public class when_asking_the_container_using_castle_windsor_to_resolve_an_item_and_it_does_not_have_the_item_registered :
            concerns_using_a_real_container
        {
            private Action attempting_to_get_an_unregistered_item;

            protected override void in_this_context()
            {
                the_container = new Castle.Windsor.WindsorContainer();
                base.in_this_context();
            }

            protected override void because()
            {
                attempting_to_get_an_unregistered_item = doing(() => the_container.Resolve<LoggerFactory>());
            }

            [Observation]
            public void should_throw_an_exception()
            {
                attempting_to_get_an_unregistered_item.should_throw_an<ComponentNotFoundException>();
            }
        }
    }
}