public sealed class MusicVolumeSliderController : AbstractVolumeSliderController
{
    protected override VolumeControl VolumeControl => MusicManagerController.Instance.MusicVolume;
}
