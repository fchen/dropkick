namespace dropkick.tests.Extensions.Files
{
    using System.IO;
    using dropkick.Dsl.Files;
    using NUnit.Framework;

    [TestFixture]
    public class CopyTest
    {
        string _path = ".\\test";
        string _source = ".\\test\\source";
        string _dest = ".\\test\\dest";

        [SetUp]
        public void SetUp()
        {
            if(Directory.Exists(_path))
                Directory.Delete(_path, true);

            Directory.CreateDirectory(_path);
            Directory.CreateDirectory(_source);
            File.WriteAllLines(Path.Combine(_source, "test.txt"), new string[]{"the test"});
            Directory.CreateDirectory(_dest);
        }

        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void Copy()
        {
            var t = new CopyTask(_source, _dest);
            t.Execute();

            var s = File.ReadAllText(Path.Combine(_dest, "test.txt"));
            Assert.AreEqual("the test\r\n", s);
        }

    }
}