namespace MyTelescope.SolarSystem.Models.CelestialObject
{
    using SWE.Model.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics;

    [DebuggerDisplay("{" + nameof(Code) + "}")]
    public class CelestialObjectType : IKey
    {
        [Obsolete("For serialisation")]
        public CelestialObjectType()
        {
            InitCollections();
        }

        public CelestialObjectType(string code)
        {
            Id = Guid.NewGuid();
            Code = code;
            InitCollections();
        }

        private void InitCollections()
        {
            CelestialObjects = new HashSet<CelestialObject>();
        }

        /// <summary>
        /// Random guid given on creation
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        public string Code { get; set; }

        public virtual ICollection<CelestialObject> CelestialObjects { get; set; }
    }
}