using UnityEngine;

namespace StudioName.Runtime.ExtensionAndHelpers
{
    /// <summary>
    /// Extends the <see cref="UnityEngine.ParticleSystem"/> class with additional functionality
    /// </summary>
    public static class ParticleSystemExtensions
    {
        /// <summary>
        /// Saves this point in time for the particle system in order to restore to this point in time later
        /// </summary>
        /// <param name="particleSystem">The particle system we are operating on</param>
        /// <returns>The snapshot to restore to later</returns>
        public static ParticleSystemSnapshot SaveSnapshot(this ParticleSystem particleSystem)
        {
            var playbackState = particleSystem.GetPlaybackState();
            var particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
            particleSystem.GetParticles(particles);
            var trails = particleSystem.GetTrails();
            return new ParticleSystemSnapshot
            {
                playbackState = playbackState,
                particles = particles,
                trails = trails
            };
        }
        
        /// <summary>
        /// Restore the particle system to this point in time
        /// </summary>
        /// <param name="particleSystem">The particle system we are operating on</param>
        /// <param name="snapshot">The snapshot in time to restore the particle system to</param>
        public static void RestoreSnapshot(this ParticleSystem particleSystem, ParticleSystemSnapshot snapshot)
        {
            particleSystem.SetPlaybackState(snapshot.playbackState);
            particleSystem.SetParticles(snapshot.particles, snapshot.particles.Length);
            particleSystem.SetTrails(snapshot.trails);
        }
        
        /// <summary>
        /// A snapshot of a point in time for a given particle system
        /// </summary>
        public struct ParticleSystemSnapshot
        {
            /// <summary>
            /// The playback state of the particle system snapshot
            /// </summary>
            public ParticleSystem.PlaybackState playbackState;
            /// <summary>
            /// The particles of the particle system snapshot
            /// </summary>
            public ParticleSystem.Particle[] particles;
            /// <summary>
            /// The trails of the particle system snapshot
            /// </summary>
            public ParticleSystem.Trails trails;
        }
    }
}