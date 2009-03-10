namespace dropkick.tests.infrastructure.logging
{
    using dropkick.infrastructure.containers;
    using dropkick.infrastructure.containers.custom;
    using dropkick.infrastructure.logging;
    using fervent.testing.bdd;

    public class LogSpecs
    {
        public abstract class concern_for_logging : observations_for_a_static_sut
        {
            protected Logger result;

            protected InversionContainer the_container;
            protected LoggerFactory mock_log_factory;
            protected Logger mock_logger;

            protected override void in_this_context()
            {
                the_container = an<InversionContainer>();
                Container.initialize_with(the_container);
                base.in_this_context();
            }

            protected override void after_each_observation()
            {
                base.after_each_observation();
                Container.initialize_with(null);
            }
        }

        public class when_asking_for_a_logger_and_one_has_been_registered : concern_for_logging
        {
            protected override void in_this_context()
            {
                base.in_this_context();
                mock_log_factory = an<LoggerFactory>();
                mock_logger = an<Logger>();
                when(the_container).is_told_to(x => x.Resolve<LoggerFactory>())
                    .Return(mock_log_factory);
                when_the(mock_log_factory).is_told_to(x => x.create_logger_bound_to(typeof(WindsorContainer)))
                    .IgnoreArguments()
                    .Return(mock_logger);
                // when_the(mock_logger).
            }

            protected override void because()
            {
                result = Log.bound_to(typeof(WindsorContainer));
            }


            [Observation]
            public void should_have_called_the_container_to_resolve_a_registered_logger()
            {
                the_container.was_told_to(x_ => x_.Resolve<LoggerFactory>());
            }

            [Observation]
            public void should_not_be_null()
            {
                result.should_not_be_null();
            }

            [Observation]
            public void should_return_a_custom_logger()
            {
                //mock_log_factory.should_be_equal_to(result);
                //result.should_be_an_instance_of<ILogFactory>();
                // result.should_be_equal_to(mock_log_factory);
            }
        }
    }
}