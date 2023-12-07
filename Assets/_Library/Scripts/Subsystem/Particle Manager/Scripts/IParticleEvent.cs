
namespace nitou.Particle {

    /// <summary>
    /// ParticleSystemの再生時のイベント
    /// </summary>
    public interface IParticleEvent {

        /// <summary>
        /// 再生開始時のイベント
        /// </summary>
        public void OnParticlePlayed();

        /// <summary>
        /// 再生終了時のイベント
        /// </summary>
        public void OnParticlemStopped();
    }
}