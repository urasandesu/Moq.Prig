# Moq.Prig
[Moq](https://github.com/Moq/moq4) supplemental library for [Prig](https://github.com/urasandesu/Prig).



## INSTALLATION
Install Chocolatey in accordance with [the top page](https://chocolatey.org/). Then, run command prompt as Administrator, execute the following command: 
```dos
CMD C:\> cinst moq.prig -y
```

Finally, execute `Install-Package` in the Package Manager Console for your test project: 
```powershell
PM> Install-Package moq.prig
```



## USAGE
You can setup fluently with MockStorage through itself: 
```cs
[TestFixture]
public class Class1
{
    [Test]
    public void MockStorage_should_provide_fluent_setup_through_itself()
    {
        using (new IndirectionsContext())
        {
            // Arrange
            var ms = new MockStorage(MockBehavior.Strict);
            PProcessMixin.AutoBodyBy(ms);
            ms.Customize(c => c.
                    Do(PProcess.StartProcessStartInfo).Expect(_ => _(It.Is<ProcessStartInfo>(x =>
                        x.Arguments == "\"arg ument1\" \"argume nt2\""
                    ))).Returns(new PProxyProcess())
               );

            // Act
            var proc = Process.Start(new ProcessStartInfo(Guid.NewGuid().ToString(), "\"arg ument1\" \"argume nt2\""));

            // Assert
            Assert.IsNotNull(proc);
            ms.Verify();
        }
    }
}

public static class PProcessMixin
{
    public static MockStorage AutoBodyBy(MockStorage ms)
    {
        ms.Customize(c => c.Do(PProcess.GetCurrentProcess).Setup(_ => _()).Returns(new PProxyProcess())).
           Customize(c => c.Do(PProcess.StartProcessStartInfo).Setup(_ => _(It.IsAny<ProcessStartInfo>())).Returns(new PProxyProcess()));
        // save other mock setups...
        return ms;
    }
}
```

Also, you can do same things to use MockProxy: 
```cs
[TestFixture]
public class Class1
{
    [Test]
    public void MockStorage_should_provide_fluent_setup_through_MockProxy()
    {
        using (new IndirectionsContext())
        {
            // Arrange
            var ms = new MockStorage(MockBehavior.Strict);
            PProcess.StartStringString().BodyBy(ms).Expect(_ => _("file name", "arguments")).Returns(Process.GetCurrentProcess());

            // Act
            var proc = Process.Start("file name", "arguments");

            // Assert
            Assert.AreEqual(Process.GetCurrentProcess().Id, proc.Id);
            ms.Verify();
        }
    }
}
```
