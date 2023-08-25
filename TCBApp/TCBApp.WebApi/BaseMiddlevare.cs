namespace TCBApp.WebApi;

public  class BaseMiddlevare
{
    public ValueTask ExecuteAsync()
    {
        throw new InvalidOperationException();
    }
}