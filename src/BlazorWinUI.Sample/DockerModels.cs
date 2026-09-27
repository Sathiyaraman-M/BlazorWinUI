namespace BlazorWinUI_Sample;

public sealed record ImageEntry(string Name, string Id, string Tag, string Size, string Containers);
public sealed record VolumeEntry(string Name, string UsedBy, string Size);
public sealed record NoticeMessage(string Title, string Message);
