using System;

public class TestEvent : Event<TestEvent>
{
    public int Test { get; set; }

    public override string ToString() => "'" + nameof(this.Test) + "' : " + this.Test.ToString();
}