namespace AroundTheWorld.Globe
{
    public interface IGlobeInput
    {
        bool IsEnabled { get; }
        void GetInput(out float latitude, out float longitude);
    }
}