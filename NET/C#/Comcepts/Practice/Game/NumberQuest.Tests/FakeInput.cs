using System.Collections.Generic;

public class FakeInput : IInput
{
    private Queue<int> inputs;

    public FakeInput(IEnumerable<int> inputs)
    {
        this.inputs = new Queue<int>(inputs);
    }

    public int GetInt(string message)
    {
        return inputs.Dequeue();
    }
}