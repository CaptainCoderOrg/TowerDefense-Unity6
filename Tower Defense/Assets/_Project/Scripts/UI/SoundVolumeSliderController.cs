public sealed class SoundVolumeSliderController : AbstractVolumeSliderController
{
    protected override VolumeControl VolumeControl => MusicManagerController.Instance.SoundVolume;
}
