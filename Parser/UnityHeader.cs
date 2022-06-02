namespace ModelSaber.Main.Parser
{
    public struct UnityHeader
    {
        public string UnitySystem;
        public int UnitySystemVersion;
        public string MinVersion;
        public string BuildVersion;

        public UnityHeader(string unitySystem, int unitySystemVersion, string minVersion, string buildVersion)
        {
            UnitySystem = unitySystem;
            UnitySystemVersion = unitySystemVersion;
            MinVersion = minVersion;
            BuildVersion = buildVersion;
        }
    }
}
