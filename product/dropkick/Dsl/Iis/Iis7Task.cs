namespace dropkick.Dsl.Iis
{
    using Engine;

    public class Iis7Task
    {
        private void CheckVersionOfWindowsAndIis(VerificationResult result)
        {
            var shouldBe6 = System.Environment.OSVersion.Version.Major;
            if (shouldBe6 != 6)
                result.AddError("This machine does not have IIS7 on it");
        }
    }
}