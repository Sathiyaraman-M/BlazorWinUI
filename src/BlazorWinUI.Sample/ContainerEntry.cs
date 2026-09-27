namespace BlazorWinUI_Sample;

public sealed class ContainerEntry(string name, string image, string status, string ports, string created, bool running)
{
    public string Id { get; } = name;
    public string Name { get; } = name;
    public string Image { get; } = image;
    public string Status { get; set; } = status;
    public string Ports { get; } = ports;
    public string Created { get; } = created;
    public bool Running { get; set; } = running;
}
