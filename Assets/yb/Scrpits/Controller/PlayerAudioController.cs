using yb;

public class PlayerAudioController : AudioController
{
    private PlayerController _player;
    public void SetSfx(Define.PlayerAudioType type) {
        if(_player.IphotonView) {
            if (_sources[(int)type].isPlaying) {
                _sources[(int)type].Stop();
            }
            _sources[(int)type].Play();
        }
    }

    protected override void Start() {
        base.Start();
        _player = GetComponent<PlayerController>();
    }
}
