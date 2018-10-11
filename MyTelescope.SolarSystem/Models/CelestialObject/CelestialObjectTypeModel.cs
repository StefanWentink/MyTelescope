namespace MyTelescope.SolarSystem.Models.CelestialObject
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics;
    using Utilities.Interfaces;

    [DebuggerDisplay("{" + nameof(Code) + "}")]
    public class CelestialObjectTypeModel : IKeyModel
    {
        [Obsolete("For serialisation")]
        public CelestialObjectTypeModel()
        {
            InitCollections();
        }

        public CelestialObjectTypeModel(string code)
        {
            Id = Guid.NewGuid();
            Code = code;
            InitCollections();
        }

        private void InitCollections()
        {
            CelestialObjects = new HashSet<CelestialObjectModel>();
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

        public virtual ICollection<CelestialObjectModel> CelestialObjects { get; set; }
    }
}