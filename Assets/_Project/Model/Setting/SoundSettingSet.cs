namespace OtoGame.Model {

    public sealed class SoundSettingSet {

        public SoundSetting Bgm { get; } = new();
        public SoundSetting Se { get; } = new();

    }
}
