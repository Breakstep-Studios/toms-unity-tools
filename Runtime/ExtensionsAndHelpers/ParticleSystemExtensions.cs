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
                trails = trails,
                isEmitting = particleSystem.isEmitting,
                isPlaying = particleSystem.isPlaying,
                isPaused = particleSystem.isPaused,
                isStopped = particleSystem.isStopped,
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
            //TODO the below may be buggy, double check returning to correct play state is working as it's supposed to later
            //if our particle was stopped when we saved our snapshot that means no particles were alive so clear them all
            //and don't continue below
            if(snapshot.isStopped)
            {
                particleSystem.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
                return;
            }
            //if we are paused we are not playing or emitting
            if (snapshot.isPaused)
            {
                particleSystem.Pause(false);
                return;
            }
            //if we were playing ensure we start things back up again
            if (snapshot.isPlaying)
            {
                particleSystem.Play(false);
            }
            //we may have been playing but called Stop(ParticleSystemStopBehavior.StopEmitting) when we saved the snapshot
            //bottom line make sure to stop emitting if that's what we were doing when snapshot was saved
            if (!snapshot.isEmitting)
            {
                particleSystem.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            }
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
            /// <summary>
            /// Is the particle system emitting particles. See <see cref="ParticleSystem.isEmitting"/>
            /// </summary>
            public bool isEmitting;
            /// <summary>
            /// Is the particle system playing. See <see cref="ParticleSystem.isPlaying"/>
            /// </summary>
            public bool isPlaying;
            /// <summary>
            /// Is the particle system paused. See <see cref="ParticleSystem.isPaused"/>
            /// </summary>
            public bool isPaused;
            /// <summary>
            /// Is the particle system stopped. See <see cref="ParticleSystem.isStopped"/>
            /// </summary>
            public bool isStopped;
        }
    }
}