
namespace nitou.Particle {

    /// <summary>
    /// ParticleSystem�̍Đ����̃C�x���g
    /// </summary>
    public interface IParticleEvent {

        /// <summary>
        /// �Đ��J�n���̃C�x���g
        /// </summary>
        public void OnParticlePlayed();

        /// <summary>
        /// �Đ��I�����̃C�x���g
        /// </summary>
        public void OnParticlemStopped();
    }
}